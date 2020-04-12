using System;
using System.Windows;
using System.ComponentModel;
using System.Collections;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using SevenRiversTD.Model;
using SevenRiversTD.Properties;
using SevenRiversTD.Data;

namespace SevenRiversTD.Forms
{
	public partial class Form1 : Form
	{	
		public Form1()
		{
			InitializeComponent();
			HealthBox.Text = Config.INIT_HP.ToString();
			HealthBox.BackColor = Color.FromArgb(72, 224, 50);
			t_hp.BackColor = Color.FromArgb(72, 224, 50);
			GoldBox.Text = Config.INIT_GOLD.ToString();
		}

		private int mx, my, gx, gy; // mouse x/y, game area mouse x/y
		private sbyte ti; // turret index
		private sbyte sx; // turret selection x/y
		private sbyte sy;
		short px, py; // proposed turret x/y
		int eci, tei, eei; // enemy current index, tower examine index, enemy examine index
		int eMax, tMax, next, wi, gold, hp; // next = time until next enemy; wi = wave index
		double lp; // load progress
		sbyte[,] tv;
		sbyte[] map;
		static bool FreezeCP = false;
		bool draw, paused;
		Bitmap tiles, top, info, bmp, cpb;
		Turret[] ts;
		static Enemy[] es;
		LoadDialog ld;
		LevelSelect lsd;
		LevelEditor led;
		static Font pf = new Font("Copperplate Gothic Bold", 12);
		static Font pfs = new Font("Copperplate Gothic Light", 9.5f);
		static Random r = new Random();
		StringFormat sf;
		Pen p, p1, p2, p3;
		Object obj;
		PaintEventArgs pea;
		EventArgs ea;
		MouseEventArgs mea;
		SolidBrush br;
		HatchBrush hb, hb2;
		FileStream fs;
		StreamReader sr;
		Turret t_temp;
		Wave CurrentWave;
		WaveSequence waves;
		WaveSequence Sequence;

		private Label StartGame;
		private Panel ControlPanel;
		private Panel GameArea;
		private Timer GameLoop;
		private Panel InfoPanel;
		private Label HealthBox;
		private Label t_hp;
		private Label t_g;
		private Label GoldBox;
		private Label t_sp;
		private Label SpeedLabel;
		private Label t_next;
		private Label NextWave;
		private Label ThisWave;
		private Label t_cur;
		private Label StartEditor;
		private OpenFileDialog LoadLevel;
		private OpenFileDialog LoadWaves;

		private void SetInitialSettings()
		{
			ld.LoadMeter.Value = 1;
			ld.Details.Text = "Initializing variable";
			ld.Details.Refresh();
			tv = new sbyte[Map.MW + 8, Map.MH + 8];
			ts = new Turret[Config.TMAX];
			es = new Enemy[Config.EMAX];
			for (int i = 0; i < Config.TMAX; i++)
				ts[i] = new Turret();
			for (int i = 0; i < Config.EMAX; i++)
				es[i] = new Enemy();
			sf = new StringFormat();
			sf.Alignment = StringAlignment.Center;
			sf.LineAlignment = StringAlignment.Center;
			eMax = 40;
			tMax = 1;
			tei = -1; eei = -1;
			hp = Config.INIT_HP;
			gold = Config.INIT_GOLD;
			paused = false;
			map = new sbyte[Config.SSMAX];
			obj = new Object();
			pea = new PaintEventArgs(ControlPanel.CreateGraphics(), ControlPanel.ClientRectangle);
			ea = new EventArgs();
			mea = new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0);
			HealthBox.BackColor = Color.FromArgb(0, 240, 0);
			t_hp.BackColor = Color.FromArgb(0, 240, 0);
			HealthBox.Text = Config.INIT_HP.ToString();
			GoldBox.Text = Config.INIT_GOLD.ToString();
			ld.LoadMeter.Value = 3;
			ld.Details.Text = "Creating image template";
			ld.Details.Refresh();
			bmp = new Bitmap(GameArea.Width, GameArea.Height);
			cpb = new Bitmap(ControlPanel.Width, ControlPanel.Height);
			p = new Pen(Color.Black);
			p1 = new Pen(Color.Gray, 1);
			p2 = new Pen(Color.Silver, 3);
			p3 = new Pen(Color.Red, 2);
			br = new SolidBrush(Color.Black);
			hb = new HatchBrush(HatchStyle.LargeConfetti, Color.DimGray, Color.Black);
			hb2 = new HatchBrush(HatchStyle.SmallConfetti, Color.DarkRed, Color.Black);
			t_temp = new Turret();
			ld.LoadMeter.Value = 7;
			ld.Details.Text = "Reading string table";
			ld.Details.Refresh();
			Strings.Initialize();
			Enemies.Initialize();
		}

		private void SetGold(int _g)
		{
			gold = _g;
			GoldBox.Text = gold.ToString();
		}

		private void DrawGrid(Graphics g)
		{
			for (int i = 0; i <= Config.AS; i += Config.GS)
			{
				g.DrawLine(Pens.Black, new Point(i, 0), new Point(i, Config.AS));
				g.DrawLine(Pens.Black, new Point(0, i), new Point(Config.AS, i));
			}
		}

		private void DrawTopLayer(Graphics g, sbyte _tv, int i, int j)
		{
			SolidBrush br = new SolidBrush(GameArea.BackColor);
			Point[] pts = new Point[4];
			LinearGradientBrush lg;
			Color fec; // Fading effect color
			if (_tv <= 6)
				fec = Color.Lime;
			else
				fec = Color.DarkRed;
			switch (_tv) // For all tiles inherited from enemy path
			{
				case 1:
				case 2:
					break;
				case 3:
				case 7:
					pts[0] = new Point(i * Config.GS, (int)((j + 1 - Config.FDL) * Config.GS)); // [0] is the TRANSPARENT end
					pts[1] = new Point(i * Config.GS, j * Config.GS);
					lg = new LinearGradientBrush(pts[0], pts[1], Color.FromArgb(0, fec), Color.FromArgb(200, fec));
					g.FillRectangle(lg, i * Config.GS, j * Config.GS, Config.GS, (int)(Config.GS * Config.GSM));
					break;
				case 4:
				case 8:
					pts[0] = new Point((int)((i + 1 - Config.FDL) * Config.GS), j * Config.GS);
					pts[1] = new Point(i * Config.GS, j * Config.GS);
					lg = new LinearGradientBrush(pts[0], pts[1], Color.FromArgb(0, fec), Color.FromArgb(200, fec));
					g.FillRectangle(lg, i * Config.GS, j * Config.GS, (int)(Config.GS * Config.GSM), Config.GS);
					break;
				case 5:
				case 9:
					pts[0] = new Point(i * Config.GS, (int)((j + Config.FDL) * Config.GS));
					pts[1] = new Point(i * Config.GS, (j + 1) * Config.GS);
					lg = new LinearGradientBrush(pts[0], pts[1], Color.FromArgb(0, fec), Color.FromArgb(200, fec));
					g.FillRectangle(lg, i * Config.GS, (int)((j + Config.FD) * Config.GS), Config.GS, (int)(Config.GS * Config.GSM));
					break;
				case 6:
				case 10:
					pts[0] = new Point((int)((i + Config.FDL) * Config.GS), j * Config.GS);
					pts[1] = new Point(i * Config.GS + Config.GS, j * Config.GS);
					lg = new LinearGradientBrush(pts[0], pts[1], Color.FromArgb(0, fec), Color.FromArgb(200, fec));
					g.FillRectangle(lg, (int)((i + Config.FD) * Config.GS), j * Config.GS, (int)(Config.GS * Config.GSM), Config.GS);
					break;
			}
		}
		private void DrawTiles(Graphics g, sbyte[] m)
		{
			br.Color = GameArea.BackColor;
			Point[] pts = new Point[4];
			LinearGradientBrush lg;
			Color fec; // Fading effect color
			ld.Details.Text = "Unpacking map";
			ld.Details.Refresh();
			tv = Map.ToGrid(m);
			for (int i = 0; i < Map.MW; i++)
			{
				ld.Details.Text = String.Concat("Unpacking tiles: ", (i + 1).ToString(), "/", (Map.MW + 1).ToString());
				ld.Details.Refresh();
				for (int j = 0; j < Map.MH; j++)
				{
					switch (tv[i, j])
					{
						case 0:
						case -8: // Turret spot -8
							g.FillRectangle(br, i * Config.GS, j * Config.GS, Config.GS, Config.GS);
							break;
						case Config.CHARRED: // Charred
							g.FillRectangle(hb, i * Config.GS + 3, j * Config.GS + 3, Config.GS - 6, Config.GS - 6);
							g.FillEllipse(hb2, i * Config.GS + 1, j * Config.GS + 1, Config.GS - 2, Config.GS - 2);
							break;
						case 1:
						case 2:
						case 3:
						case 4:
						case 5:
						case 6: // SU 3 - SL 4 - SD 5 - SR 6
						case 7:
						case 8:
						case 9:
						case 10: // FU 7 - FL 8 - FD 9 - FR 10
							pts[0] = new Point(i * Config.GS, j * Config.GS);
							pts[1] = new Point(i * Config.GS + Config.GS, j * Config.GS + Config.GS);
							lg = new LinearGradientBrush(pts[0], pts[1], Color.LightGray, Color.DarkGray);
							g.FillRectangle(lg, i * Config.GS, j * Config.GS, Config.GS, Config.GS);
							DrawTopLayer(Graphics.FromImage(top), tv[i, j], i, j);
							break;
						case 11: // TODO: River tiles (cases through to about 18 may be needed)
							break;
						default:
							MessageBox.Show(tv[i, j].ToString(), "Undefined tile");
							break;
					}
				}
				lp += 0.5;
				ld.LoadMeter.Value = (int)lp;
			}
		}

		private void DrawTiles(Graphics g)
		{
			br.Color = GameArea.BackColor;
			Point[] pts = new Point[4];
			LinearGradientBrush lg;
			Color fec; // Fading effect color
			for (int i = 0; i < Map.MW; i++)
			{
				ld.Details.Text = String.Concat("Unpacking tiles: ", (i + 1).ToString(), "/", (Map.MW + 1).ToString());
				ld.Details.Refresh();
				for (int j = 0; j < Map.MH; j++)
				{
					switch (tv[i, j])
					{
						case 0:
						case -8: // Turret spot -8
							g.FillRectangle(br, i * Config.GS, j * Config.GS, Config.GS, Config.GS);
							break;
						case Config.CHARRED: // Charred
							g.FillRectangle(hb, i * Config.GS + 3, j * Config.GS + 3, Config.GS - 6, Config.GS - 6);
							g.FillEllipse(hb2, i * Config.GS + 1, j * Config.GS + 1, Config.GS - 2, Config.GS - 2);
							break;
						case 1:
						case 2:
						case 3:
						case 4:
						case 5:
						case 6: // SU 3 - SL 4 - SD 5 - SR 6
						case 7:
						case 8:
						case 9:
						case 10: // FU 7 - FL 8 - FD 9 - FR 10
							pts[0] = new Point(i * Config.GS, j * Config.GS);
							pts[1] = new Point(i * Config.GS + Config.GS, j * Config.GS + Config.GS);
							lg = new LinearGradientBrush(pts[0], pts[1], Color.LightGray, Color.DarkGray);
							g.FillRectangle(lg, i * Config.GS, j * Config.GS, Config.GS, Config.GS);
							DrawTopLayer(Graphics.FromImage(top), tv[i, j], i, j);
							break;
						case 11: // TODO: River tiles (cases through to about 18 may be needed)
							break;
						default:
							MessageBox.Show(tv[i, j].ToString(), "Undefined tile");
							break;
					}
				}
			}
		}

		private void InitializeGraphics(sbyte[] m) // Initializes all graphics from a selected map
		{
			Point loc = this.Location;
			this.Location = new Point(0, 0);
			ld.LoadMeter.Value = 18;
			ld.Details.Text = "Initializing bitmap";
			ld.Details.Refresh();
			Bitmap bit = new Bitmap(GameArea.Width, GameArea.Height);
			tiles = new Bitmap(GameArea.Width, GameArea.Height);
			top = new Bitmap(GameArea.Width, GameArea.Height);
			info = new Bitmap(160, 260);
			Graphics g = Graphics.FromImage(bmp);
			Graphics gt = Graphics.FromImage(tiles);
			Graphics gf = Graphics.FromImage(bit);
			Graphics gi = Graphics.FromImage(info);
			StartGame.Visible = false;
			StartEditor.Visible = false;
			ld.LoadMeter.Value = 21;
			lp = 21;
			DrawTiles(gt, m);
			ld.Details.Text = "Initializing grid";
			ld.Details.Refresh();
			DrawGrid(gt);
			ld.LoadMeter.Value = 28;
			ld.Details.Text = "Initializing interface";
			ld.Details.Refresh();
			DrawInfoBox(gi);
			ld.LoadMeter.Value = 31;
			ld.Details.Text = "Initializing turret";
			ld.Details.Refresh();
			Turrets.InitializeTurretImages();
			ld.LoadMeter.Value = 34;
			ld.Details.Text = "Initializing translucency";
			ld.Details.Refresh();
			g.DrawImage(top, 0, 0); // Add translucent layer
			ld.LoadMeter.Value = 38;
			ld.Details.Text = "Initializing tileset";
			ld.Details.Refresh();
			gf.DrawImage(tiles, 0, 0);
			ld.LoadMeter.Value = 41;
			ld.Details.Text = "Initializing graphic";
			ld.Details.Refresh();
			gf.DrawImage(bmp, 0, 0);
			GameArea.CreateGraphics().DrawImage(bit, 0, 0);
			this.Location = loc;
		}
		private void InitializeGraphics() // Updates tile graphics
		{
			Bitmap bit = new Bitmap(GameArea.Width, GameArea.Height);
			tiles = new Bitmap(GameArea.Width, GameArea.Height);
			top = new Bitmap(GameArea.Width, GameArea.Height);
			Graphics g = Graphics.FromImage(bmp);
			Graphics gt = Graphics.FromImage(tiles);
			Graphics gf = Graphics.FromImage(bit);
			DrawTiles(gt);
			DrawGrid(gt);
			g.DrawImage(top, 0, 0); // Add translucent layer
			gf.DrawImage(tiles, 0, 0);
			gf.DrawImage(bmp, 0, 0);
			GameArea.CreateGraphics().DrawImage(bit, 0, 0);
		}

		private void InitializeEnemy(int _wi, int _delay)
		{
			if (_wi >= Sequence.Count)
				FinalizeGame(Strings.GameOver[Config.VICTORY]);
			else
			{
				CurrentWave = Sequence.GetWave(_wi);
				eci = 0;
				next = _delay;
				ThisWave.Text = Strings.EnemyNames[Sequence.GetWave(wi).Type];
				ThisWave.BackColor = Enemies.ColorCode[Sequence.GetWave(wi).Type];
				t_cur.BackColor = NextWave.BackColor;
				wi++; // Conveniently assigns a human-readable wave number for this wave.
				t_cur.Text = String.Concat("Current (", wi.ToString(), ")");
				if (wi < Sequence.Count)
				{
					NextWave.Text = Strings.EnemyNames[Sequence.GetWave(wi).Type];
					NextWave.BackColor = Enemies.ColorCode[Sequence.GetWave(wi).Type];
					t_next.BackColor = NextWave.BackColor;
				}
				else
				{
					NextWave.Text = Strings.None;
					NextWave.BackColor = Enemies.DefaultColorCode;
					t_next.BackColor = NextWave.BackColor;
				}
				es = CurrentWave.EnemyList;
				eMax = CurrentWave.Count;
			}
		}

		private void FinalizeGame(String s)
		{
			GameLoop.Enabled = false;
			if (MessageBox.Show(s, "7 Rivers Tower Defence", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.OK)
			{
				Graphics g = GameArea.CreateGraphics();
				g.Clear(GameArea.BackColor);
			}
			else
				this.Close();
			StartGame.Visible = true;
			StartEditor.Visible = true;
			return;
		}
		private void FinalizeGame()
		{
			GameLoop.Enabled = false;
			Graphics g = GameArea.CreateGraphics();
			g.Clear(GameArea.BackColor);
			StartGame.Visible = true;
			StartEditor.Visible = true;
			return;
		}
		private void BuildTurret(sbyte _ti, short _x, short _y)
		{
			int i = 0;
			FreezeCP = false;
			if (gold >= Turret.BuildCost(_ti))
				SetGold(gold - Turret.BuildCost(_ti));
			else
				return;
			for (i = 0; i < Config.TMAX; i++)
			{
				if (ts[i].Type == -1)
				{
					i = -i;
					tMax++;
					break;
				}
			}
			if (i > 0)
			{
				GameLoop.Enabled = false;
				if (MessageBox.Show("Turret limit reached!", "7 Towers TD", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.OK)
				{
					GameLoop.Enabled = true;
					return;
				}
			}
			ts[-i] = new Turret(_ti, _x, _y, true);
			if (ts[-i].Type != -1) // If it IS -1, it may as well not even have got created. So there.
				tv[_x, _y] = -8;
		}
		private int CheckRange(int _ti)
		{
			for (int i = 0; i < eMax; i++)
			{
				if (es[i].Type == null)
					continue;
				if (Math.Pow(ts[_ti].X * Config.GS - es[i].X, 2) + Math.Pow(ts[_ti].Y * Config.GS - es[i].Y, 2) <= Math.Pow(ts[_ti].Range, 2))
				{
					return i; // Returns the index of an enemy lying within a circle of radius [Range]
				}
			}
			return -1;
		}
		private int CheckRange(int _ti, int _min)
		{
			for (int i = _min; i < eMax; i++)
			{
				if (es[i].Type == null)
					continue;
				if (Math.Pow(ts[_ti].X * Config.GS - es[i].X, 2) + Math.Pow(ts[_ti].Y * Config.GS - es[i].Y, 2) <= Math.Pow(ts[_ti].Range, 2))
				{
					return i; // Returns the index of an enemy lying within a circle of radius [Range]
				}
			}
			return -1;
		}
		private void ReduceHealth(int _dmg)
		{
			hp -= _dmg;
			if (hp <= 0)
				hp = 0;
			HealthBox.Text = hp.ToString();
			HealthBox.BackColor = Color.FromArgb((28 - hp) * 9, (int)Math.Sqrt(hp) * 50, 50);
			t_hp.BackColor = HealthBox.BackColor;
			if (hp == 0)
				FinalizeGame(Strings.GameOver[Config.DEFEAT]);
		}

		private void ShowInformation(Graphics g, sbyte _sx, sbyte _sy)
		{
			sbyte _ti = (sbyte)((_sy - 1) * 2 + _sx - 1);
			t_temp = new Turret(_ti, 0, 0, false);
			if (t_temp.Type == -1)
			{
				return;
			}
			g.DrawImageUnscaled(info, 10, Config.INFO_Y);
			RectangleF rectft = new RectangleF(10, 10 + Config.INFO_Y, 160, 50);
			RectangleF rectf = new RectangleF(99 + 10, 94 + Config.INFO_Y, 50, 26);
			RectangleF trec = new RectangleF(10 + 10, 94 + Config.INFO_Y, 85, 26);
			g.DrawString("Damage", pf, Brushes.Gray, trec, sf);
			trec.Y += 30;
			g.DrawString("Range", pf, Brushes.Gray, trec, sf);
			trec.Y += 30;
			g.DrawString("Delay", pf, Brushes.Gray, trec, sf);
			g.DrawString(t_temp.Name, pf, Brushes.Silver, rectft, sf);
			rectft.Y += 40;
			g.DrawString(String.Concat("Cost: ", Turret.BuildCost(t_temp.Type).ToString()), pf, Brushes.Silver, rectft, sf);
			g.DrawString(t_temp.Damage.ToString(), pf, Brushes.DarkGray, rectf, sf);
			rectf.Y += 30;
			g.DrawString(t_temp.Range.ToString(), pf, Brushes.DarkGray, rectf, sf);
			rectf.Y += 30;
			g.DrawString(t_temp.ShotDelay.ToString(), pf, Brushes.DarkGray, rectf, sf);
			t_temp.Type = -1;
		}

		private void ShowInformation(Graphics g, Turret tu)
		{
			g.DrawImageUnscaled(info, 10, Config.INFO_Y);
			RectangleF rectft = new RectangleF(10, 10 + Config.INFO_Y, 160, 50);
			RectangleF rectf = new RectangleF(99 + 10, 94 + Config.INFO_Y, 50, 26);
			RectangleF trec = new RectangleF(10 + 10, 94 + Config.INFO_Y, 85, 26);
			RectangleF rectf1 = new RectangleF(15 + 10, 199 + Config.INFO_Y, 70, 30);
			RectangleF rectf2 = new RectangleF(95 + 10, 199 + Config.INFO_Y, 50, 30);
			Rectangle rect1 = new Rectangle((int)rectf1.Left, (int)rectf1.Top, (int)rectf1.Width, (int)rectf1.Height);
			Rectangle rect2 = new Rectangle((int)rectf2.Left, (int)rectf2.Top, (int)rectf2.Width, (int)rectf2.Height);
			g.DrawString("Damage", pf, Brushes.Gray, trec, sf);
			trec.Y += 30;
			g.DrawString("Range", pf, Brushes.Gray, trec, sf);
			trec.Y += 30;
			g.DrawString("Delay", pf, Brushes.Gray, trec, sf);
			g.DrawString(tu.Name, pf, Brushes.Silver, rectft, sf);
			rectft.Y += 40;
			if (tu.Level == Config.MASTER)
				g.DrawString(Strings.Special[tu.Type], pf, Brushes.Lime, rectft, sf);
			else
				g.DrawString(String.Concat("Level ", tu.Level.ToString()), pf, Brushes.Silver, rectft, sf);
			g.DrawString(tu.Damage.ToString(), pf, Brushes.DarkGray, rectf, sf);
			rectf.Y += 30;
			g.DrawString(tu.Range.ToString(), pf, Brushes.DarkGray, rectf, sf);
			rectf.Y += 30;
			g.DrawString(tu.ShotDelay.ToString(), pf, Brushes.DarkGray, rectf, sf);
			p.Color = Color.WhiteSmoke;
			p.Width = 2;
			g.DrawRectangle(p, rect1);
			g.DrawRectangle(p, rect2);
			g.DrawString(String.Concat("Upgrade (", tu.UpgradeCost.ToString(), ")"), pfs, Brushes.DarkGray, rectf1, sf);
			g.DrawString(String.Concat("Sell (", tu.SellValue.ToString(), ")"), pfs, Brushes.DarkGray, rectf2, sf);
		}
		private void ShowInformation(Graphics g, Enemy en)
		{
			g.DrawImageUnscaled(info, 10, Config.INFO_Y);
			RectangleF rectft = new RectangleF(10, 10 + Config.INFO_Y, 160, 50);
			RectangleF rectf = new RectangleF(99 + 10, 94 + Config.INFO_Y, 50, 26);
			RectangleF trec = new RectangleF(10 + 10, 94 + Config.INFO_Y, 85, 26);
			g.DrawString("HP", pf, Brushes.Gray, trec, sf);
			trec.Y += 30;
			g.DrawString("Speed", pf, Brushes.Gray, trec, sf);
			trec.Y += 30;
			g.DrawString("ID", pf, Brushes.Gray, trec, sf);
			g.DrawString(en.Name, pf, Brushes.Silver, rectft, sf);
			if (en.HP >= 1000)
				g.DrawString(en.HP.ToString(), pfs, Brushes.DarkGray, rectf, sf);
			else
				g.DrawString(en.HP.ToString(), pf, Brushes.DarkGray, rectf, sf);
			rectf.Y += 30;
			g.DrawString((10 * Config.GS / en.Speed).ToString("G3"), pf, Brushes.DarkGray, rectf, sf);
			rectf.Y += 30;
			g.DrawString(eei.ToString(), pf, Brushes.DarkGray, rectf, sf);
			p.Color = Color.Black;
			p.Width = 1;
		}
		private void DrawInfoBox(Graphics g)
		{
			HatchBrush hb = new HatchBrush(HatchStyle.HorizontalBrick, Color.FromArgb(25, 25, 25), Color.Black);
			LinearGradientBrush lgb = new LinearGradientBrush(new Point(10, 10), new Point(160, 250), Color.FromArgb(100, Color.Silver), Color.FromArgb(100, Color.Black));
			LinearGradientBrush lg2 = new LinearGradientBrush(new Point(0, 0), new Point(160, 260), Color.Gray, Color.FromArgb(30, 30, 30));
			LinearGradientBrush lgs = new LinearGradientBrush(new Point(16, 16), new Point(24, 24), Color.LightGray, Color.Black);
			Rectangle rect = new Rectangle(10, 10, 140, 240);
			Rectangle rect2 = new Rectangle(0, 0, 160, 260);
			g.FillRectangle(lg2, rect2);
			g.DrawRectangle(Pens.Black, rect2);
			g.DrawRectangle(Pens.Black, rect);
			g.DrawLine(Pens.Black, new Point(0, 0), new Point(10, 10));
			g.DrawLine(Pens.Black, new Point(0, 260), new Point(10, 250));
			g.DrawLine(Pens.Black, new Point(160, 0), new Point(150, 10));
			g.DrawLine(Pens.Black, new Point(160, 260), new Point(150, 250));
			g.FillRectangle(hb, rect);
			g.FillRectangle(lgb, rect);
			g.FillEllipse(lgs, 16, 16, 8, 8);
			lgs = new LinearGradientBrush(new Point(15 + 122, 15), new Point(24 + 122, 24), Color.LightGray, Color.Black);
			g.FillEllipse(lgs, 16 + 122, 16, 8, 8);
			lgs = new LinearGradientBrush(new Point(10 + 122, 10 + 222), new Point(24 + 122, 24 + 222), Color.LightGray, Color.Black);
			g.FillEllipse(lgs, 16 + 122, 16 + 222, 8, 8);
			lgs = new LinearGradientBrush(new Point(14, 14 + 222), new Point(24, 24 + 222), Color.LightGray, Color.Black);
			g.FillEllipse(lgs, 16, 16 + 222, 8, 8);
			g.DrawLine(Pens.DarkGray, 20, 50, 140, 50);
			g.DrawLine(Pens.DarkGray, 20, 51, 139, 51);
			g.DrawLine(Pens.Gray, 20, 52, 138, 52);
			g.DrawLine(Pens.Gray, 20, 53, 137, 53);
			g.DrawLine(Pens.DimGray, 20, 54, 136, 54);
			g.DrawLine(Pens.DimGray, 20, 55, 135, 55);
			for (int i = 90; i <= 180; i += 30)
			{
				g.DrawLine(Pens.DarkGray, 20, i, 139, i);
				g.DrawLine(Pens.Gray, 20, i + 1, 138, i + 1);
				g.DrawLine(Pens.Gray, 20, i + 2, 137, i + 2);
				g.DrawLine(Pens.DimGray, 20, i + 3, 136, i + 3);
				g.DrawLine(Pens.DimGray, 20, i + 4, 135, i + 4);
			}
			g.DrawLine(Pens.DarkGray, 95, 88, 95, 190);
			g.DrawLine(Pens.DarkGray, 96, 88, 96, 189);
			g.DrawLine(Pens.Gray, 97, 88, 97, 188);
			g.DrawLine(Pens.Gray, 98, 88, 98, 187);
			g.DrawLine(Pens.DimGray, 99, 88, 99, 186);
			g.DrawLine(Pens.DimGray, 100, 88, 100, 185);
		}

		private void StartGame_Click(object sender, EventArgs e)
		{
			lsd = new LevelSelect(1, GameArea.Width, GameArea.Height);
			if (lsd.ShowDialog() != DialogResult.OK)
				return;
			ld = new LoadDialog();
			ld.Details.Text = "Initializing graphic";
			ld.Show(); // Non-modal to keep Form1 initializing
			ld.timer1.Tick += InitializeGame;
			ld.timer1.Enabled = true;
		}

		private void InitializeGame(object sender, EventArgs e)
		{
			ld.timer1.Enabled = false;
			ld.Details.Text = "Initializing variable";
			ld.Details.Refresh();
			SetInitialSettings();
			ld.Details.Refresh();
			LoadLevel.InitialDirectory = Environment.CurrentDirectory;
			String file, subfile = null, filedir;
			String[] data;
			bool loadfile = true;
			if (lsd.LevelIndex == 0)
			{
				if (LoadLevel.ShowDialog() == DialogResult.OK)
				{
					try
					{
						fs = new FileStream(LoadLevel.FileName, FileMode.Open, FileAccess.Read);
						sr = new StreamReader(fs, Encoding.ASCII);
						file = sr.ReadToEnd();
						sr.Close();
						data = new String[4];
						data = file.Split(",".ToCharArray(), 4);
						filedir = LoadLevel.FileName.Remove(LoadLevel.FileName.LastIndexOf("\\"), LoadLevel.FileName.Length - LoadLevel.FileName.LastIndexOf("\\"));
						subfile = String.Concat(filedir, "\\", data[0]); // data[0] is map file name
						if (!File.Exists(subfile))
						{
							subfile = String.Concat(filedir, "\\", data[0], data[2]);
							if (!File.Exists(subfile)) // data[2] is a standard extension
								throw new Exception("Map file failed to load!");
						}
						map = Map.FromFile(subfile, false);
						subfile = String.Concat(filedir, "\\", data[1]); // data[1] is wave file name
						if (!File.Exists(subfile))
						{
							subfile = String.Concat(filedir, "\\", data[1], data[2]);
							if (!File.Exists(subfile)) // data[2] is a standard extension
								throw new Exception("Wave file failed to load!");
						}
					}
					catch (Exception ex)
					{
						loadfile = false;
						MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
						try
						{
							sr.Close();
						}
						catch (Exception ex2)
						{
							MessageBox.Show(ex2.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
						map = Maps.map1;
					}
				}
			}
			else
			{
				switch (lsd.LevelIndex)
				{
					case 1:
						map = Maps.map1;
						loadfile = false;
						break;
					case 2:
						map = Maps.map2;
						loadfile = false;
						break;
					case 3:
						map = Maps.map3;
						loadfile = false;
						break;
					case 4:
						map = Maps.map4;
						loadfile = false;
						break;
					case 5:
						map = Maps.map5;
						loadfile = false;
						break;
					case 6:
						map = Maps.map6;
						loadfile = false;
						break;
					default:
						break;
				}
			}
			if (map.Length == 1)
			{
				ld.Close();
				FinalizeGame();
				return;
			}
			InitializeGraphics(map);
			ld.LoadMeter.Value = 50;
			ld.Details.Text = "Initializing wave";
			ld.Details.Refresh();
			tMax = 1;
			waves = new WaveSequence();
			if (loadfile)
				this.Sequence = WaveSequence.get_FileSequence(subfile, map, false);
			else
				this.Sequence = Waves.get_Sequence(lsd.LevelIndex);
			if (Sequence.Count == 0)
			{
				ld.Close();
				FinalizeGame();
				return;
			}
			ld.LoadMeter.Value = 95;
			ld.Details.Text = "Initializing enemie";
			ld.Details.Refresh();
			wi = Config.INIT_WAVE - 1;
			InitializeEnemy(wi, 50);
			GameLoop.Enabled = true;
			ld.LoadMeter.Value = 100;
			ld.Details.Text = "Initializing interface";
			ld.Details.Refresh();
			ControlPanel_Paint(obj, pea);
			ld.Close();
		}

		private void StartGame_MouseEnter(object sender, EventArgs e)
		{
			StartGame.Font = new Font(StartGame.Font, FontStyle.Bold);
		}

		private void StartGame_MouseLeave(object sender, EventArgs e)
		{
			StartGame.Font = new Font(StartGame.Font, FontStyle.Regular);
		}

		private void GameLoop_Tick(object sender, EventArgs e)
		{
			// ---- GameArea logic ----
			if (GameLoop.Interval == Config.FAST)
				draw = !draw;
			else
				draw = true;
			Graphics g = Graphics.FromImage(bmp);
			if (draw)
			{
				g.Clear(GameArea.BackColor);
				p.Color = Color.Black;
				p.Width = 1;
				br.Color = Color.Black;
				g.DrawImage(tiles, 0, 0);
			}
			int cr;
			// Spawn new enemies at appropriate times
			if (eci < CurrentWave.Count)
				next--;
			if (next == 0)
			{
				es[eci].Type = CurrentWave.Type;
				next = es[eci].Trigger;
				eci++;
			}
			// Enemy logic
			for (int i = 0; i < eMax; i++)
			{
				if (es[i].Type == null)
					continue;
				// Apply traits
				switch (es[i].Type)
				{
					case EnemyType.REGEN: // Regenerator
						es[i].HP++; // Limited in set method
						break;
					case EnemyType.REGEN_BOSS:
						es[i].HP += 2;
						break;
					case EnemyType.COMMANDER:
						if (es[i].HP < es[i].MaxHP / 2)
							es[i].HP += 10;
						else
							es[i].HP += 5;
						break;
					case EnemyType.ERRATIC: // Erratic
					case EnemyType.ERRATIC_BOSS:
						if (es[i].Speed > 1000)
						{
							es[i].SpeedPhase--;
							if (r.Next(0, Config.ERR_RATE) == 0)
								es[i].Speed = es[i].InitialSpeed;
						}
						else
						{
							if (r.Next(0, Config.ERR_RATE) == 0)
								es[i].Speed = Int32.MaxValue;
						}
						break;
				}
				if (draw)
					es[i].Draw(g);
				switch (es[i].Direction)
				{
					case 1:
						es[i].Y -= (double)Config.GS / es[i].Speed;
						break;
					case 2:
						es[i].X -= (double)Config.GS / es[i].Speed;
						break;
					case 3:
						es[i].Y += (double)Config.GS / es[i].Speed;
						break;
					case 4:
						es[i].X += (double)Config.GS / es[i].Speed;
						break;
				}
				es[i].SpeedPhase++;
				if (draw) // The logic to limit phase bounds is conveniently in its set method :)
					es[i].Phase += 0.5;
				if (es[i].Speed == es[i].SpeedPhase)
				{
					es[i].SpeedPhase = 0; // So this only happens every [speed] ticks.
					if (es[i].Type == EnemyType.ACCELERATOR)
					{
						if (r.NextDouble() < 0.33 && es[i].Speed > 3)
						{
							es[i].Speed--;
							es[i].InitialSpeed--;
						}
					}
					if (tv[(int)((es[i].X + 1) / Config.GS), (int)((es[i].Y + 1) / Config.GS)] == 2)
					{
						es[i].Direction = es[i].get_Path(es[i].PathPoint);
						es[i].PathPoint++;
					}
					else if (tv[(int)((es[i].X + 1) / Config.GS), (int)((es[i].Y + 1) / Config.GS)] > 6 && tv[(int)((es[i].X + 1) / Config.GS), (int)((es[i].Y + 1) / Config.GS)] <= 10)
					{
						if (es[i].IsBoss)
							ReduceHealth(Config.BOSS_DAMAGE);
						else
							ReduceHealth(1);
						es[i].HP = 0;
						es[i].Type = null;
						if (eei == i)
							eei = -1;
						bool w_end = true;
						for (int j = 0; j < eMax; j++)
						{
							if (es[j].HP > 0)
							{
								w_end = false;
								break;
							}
						}
						if (w_end)
							InitializeEnemy(wi, 1);
					}
				}
			}
			// Turret logic
			for (int i = 0; i < tMax; i++)
			{
				if (ts[i].Type == -1)
					continue;
				if (ts[i].CurrentDelay != 0)
					ts[i].CurrentDelay--;
				else
				{
					cr = CheckRange(i);
					while (cr != -1) // Enemy targeted
					{
						es[cr].HP -= ts[i].Damage; // The logic to limit HP bounds is equally convenient!
						p.Color = ts[i].ShotColor;
						p.Width = ts[i].ShotWidth;
						g.DrawLine(p, ts[i].X * Config.GS + Config.GS / 2, ts[i].Y * Config.GS + Config.GS / 2, (int)es[cr].X + Config.GS / 2, (int)es[cr].Y + Config.GS / 2);
						ts[i].CurrentDelay = ts[i].ShotDelay;
						if (es[cr].Type == EnemyType.DISRUPTOR)
						{
							ts[i].CurrentDelay += 8;
						}
						if (ts[i].Type == 3 && ts[i].Level == Config.MASTER)
						{
							ts[i].ShotCount++;
							if (ts[i].ShotCount % 40 == 10)
								ts[i].ShotDelay = 12;
							else if (ts[i].ShotCount % 40 == 20)
								ts[i].ShotDelay = 28;
						}
						if (es[cr].Type == null)
						{
							SetGold(gold + es[cr].Gold);
							if (eei == cr)
								eei = -1;
							bool w_end = true;
							for (int j = 0; j < eMax; j++)
							{
								if (es[j].HP > 0)
								{
									w_end = false;
									break;
								}
							}
							if (w_end)
							{
								InitializeEnemy(wi, Config.RELAX);
							}
						}
						if (ts[i].Type == 2 && ts[i].Level == Config.MASTER)
							cr = CheckRange(i, cr + 1);
						else
							cr = -1;
					}
				}
				// Draw turret
				if (draw)
				{
					g.DrawImage(ts[i].Image, ts[i].X * Config.GS, ts[i].Y * Config.GS, Config.GS, Config.GS);
					for (int j = 1; j < ts[i].Level; j++)
					{
						// It's probably not obvious, so for the record, this creates one plus sign per level above 1.
						g.DrawLine(Pens.Black, ts[i].X * Config.GS + 28, ts[i].Y * Config.GS + j * 5, ts[i].X * Config.GS + 30, ts[i].Y * Config.GS + j * 5);
						g.DrawLine(Pens.Black, ts[i].X * Config.GS + 29, ts[i].Y * Config.GS + j * 5 - 1, ts[i].X * Config.GS + 29, ts[i].Y * Config.GS + j * 5 + 1);
					}
				}
			}
			// Draw predefined top-layer effects
			if (draw)
				g.DrawImage(top, 0, 0);
			px = (short)(gx / Config.GS);
			py = (short)(gy / Config.GS);
			if (MouseInGameArea())
			{
				// Check turret placement if applicable and draw turret range
				if (FreezeCP && gx != -64 && draw)
				{
					if (tv[px, py] == 0)
						br.Color = Color.FromArgb(70, Color.RoyalBlue);
					else
						br.Color = Color.FromArgb(70, Color.Red);
					g.FillRectangle(br, px * Config.GS, py * Config.GS, Config.GS, Config.GS);
					p.Color = Color.FromArgb(220, Color.Lime);
					p.Width = 2;
					Turret t_temp = new Turret(ti, 0, 0, false);
					g.DrawEllipse(p, px * Config.GS + Config.GS / 2 - t_temp.Range, py * Config.GS + Config.GS / 2 - t_temp.Range, t_temp.Range * 2, t_temp.Range * 2);
				}
				// Draw turret range if mouse is hovering over it
				else if (gx != -64)
				{
					int j = 0;
					if (tv[px, py] == -8)
					{
						for (j = 0; j < Config.TMAX; j++)
						{
							if (ts[j].X == px && ts[j].Y == py)
								break;
						}
						if (draw)
						{
							p.Color = Color.FromArgb(220, Color.Lime);
							p.Width = 2;
							g.DrawEllipse(p, ts[j].X * Config.GS + Config.GS / 2 - ts[j].Range, ts[j].Y * Config.GS + Config.GS / 2 - ts[j].Range, ts[j].Range * 2, ts[j].Range * 2);
						}
					}
				}
			}
			GameArea.CreateGraphics().DrawImage(bmp, 0, 0);
			// ---- ControlPanel logic ----
			if (mx > 34 && mx < 154 && my > 39 && my < 40 + 30 * Config.TMAX)
			{
				sx = (sbyte)((mx + 26) / 60);
				sy = (sbyte)((my + 21) / 60);
			}
			else
				sx = 0;
			if (!FreezeCP && draw)
				ControlPanel_Paint(obj, pea);
		}

		private void Form1_Closing(object sender, CancelEventArgs e)
		{
			GameLoop.Enabled = false;
		}

		private void ControlPanel_Paint(object sender, PaintEventArgs e)
		{
			if (!GameLoop.Enabled || FreezeCP)
				return;
			Graphics g = Graphics.FromImage(cpb);
			g.Clear(ControlPanel.BackColor);
			for (int i = 1; i <= Config.TMAX / 2 + 1; i++)
				g.DrawLine(p2, 34, i * 60 - 21, 154, i * 60 - 21);
			for (int i = 1; i <= 3; i++)
				g.DrawLine(p2, i * 60 - 27, 40, i * 60 - 27, 40 + 30 * Config.TMAX);
			for (int i = 1; i <= Config.TMAX / 2 + 1; i++)
				g.DrawLine(p1, 34, i * 60 - 20, 154, i * 60 - 20);
			for (int i = 1; i <= 3; i++)
				g.DrawLine(p1, i * 60 - 26, 40, i * 60 - 26, 40 + 30 * Config.TMAX);
			g.DrawImage(Turrets.MGTurret, new Rectangle(38, 44, 52, 52));
			g.DrawImage(Turrets.SniperTurret, new Rectangle(38 + 60, 44, 52, 52));
			g.DrawImage(Turrets.LaserTurret, new Rectangle(38, 44 + 60, 52, 52));
			g.DrawImage(Turrets.PlasmaCannon, new Rectangle(38 + 60, 44 + 60, 52, 52));
			if (sx > 0 && sy > 0 && tei == -1 && eei == -1)
			{
				ShowInformation(g, sx, sy);
				g.DrawRectangle(p3, sx * 60 - 25, sy * 60 - 19, 56, 56);
			}
			else if (tei != -1)
			{
				ShowInformation(g, ts[tei]);
			}
			if (eei != -1)
			{
				ShowInformation(g, es[eei]);
			}
			e.Graphics.DrawImage(cpb, 0, 0);
		}

		private void ControlPanel_MouseMove(object sender, MouseEventArgs e)
		{
			mx = e.X;
			my = e.Y;
		}

		private void ControlPanel_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Left)
				return;
			Graphics g = ControlPanel.CreateGraphics();
			Rectangle rect_up = new Rectangle(15 + 10, 199 + Config.INFO_Y, 70, 40);
			Rectangle rect_se = new Rectangle(95 + 10, 199 + Config.INFO_Y, 50, 40);
			GraphicsPath gp = new GraphicsPath();
			gp.AddRectangle(new Rectangle(mx - 1, my - 1, 3, 3));
			Region re_up = new Region(gp);
			Region re_se = new Region(gp);
			re_up.Intersect(rect_up);
			re_se.Intersect(rect_se);
			int u_temp;
			if ((tei != -1 && !re_up.IsEmpty(g)) || e.Delta == -10)
			{
				u_temp = ts[tei].Upgrade(gold);
				if (u_temp >= 0)
					SetGold(u_temp);
				eei = -1;
				ControlPanel_Paint(obj, pea);
			}
			else if ((tei != -1 && !re_se.IsEmpty(g)) || e.Delta == -15)
			{
				SetGold(ts[tei].Sell(gold));
				tv[ts[tei].X, ts[tei].Y] = Config.CHARRED; // Charred
				InitializeGraphics();
				FreezeCP = false;
				ti = -1;
				tei = -1;
				eei = -1;
			}
			else if (tei != -1)
				tei = -1;
			else if (sx != 0 && !FreezeCP)
			{
				FreezeCP = true;
				ti = (sbyte)((sy - 1) * 2 + sx - 1);
			}
			else if (FreezeCP)
			{
				FreezeCP = false;
				ti = -1;
			}
			// And if none of the above is true, this does absolutely NOTHING! (Which is a good thing.)
		}

		private void ControlPanel_MouseUp(object sender, MouseEventArgs e)
		{
			if (sx != 0)
			{
				var _ti = (sbyte)((sy - 1) * 2 + sx - 1);
				if (_ti < Config.TTMAX)
				{
					FreezeCP = true;
					ti = _ti;
				}
			}
		}
		private void GameArea_MouseMove(object sender, MouseEventArgs e)
		{
			gx = e.X;
			gy = e.Y;
		}

		private void GameArea_MouseLeave(object sender, EventArgs e)
		{
			gx = -64;
			gy = -64;
		}

		private void GameArea_MouseDown(object sender, MouseEventArgs e)
		{
			if (!GameLoop.Enabled)
				return;
			if (FreezeCP && gx != -64 && MouseInGameArea() && tv[px, py] == 0 && e.Button == MouseButtons.Left)
			{
				BuildTurret(ti, px, py);
				FreezeCP = false;
				gx = -64;
				ControlPanel_Paint(obj, pea);
			}
			else if (MouseInGameArea() && tv[px, py] == -8 && e.Button == MouseButtons.Left)
			{
				int j = 0;
				for (j = 0; j < Config.TMAX; j++)
				{
					if (ts[j].X == px && ts[j].Y == py)
						break;
				}
				tei = j;
				eei = -1;
				ControlPanel_Paint(obj, pea);
			}
			else if (MouseInGameArea() && tv[px, py] > 0 && e.Button == MouseButtons.Left) // All path tiles are greater than 0
			{
				int j = 0;
				for (j = 0; j <= eMax; j++)
				{
					if (j == eMax)
						break;
					if (es[j].Intersects(GameArea.CreateGraphics(), e.X, e.Y))
						break;
				}
				if (j != eMax)
					eei = j;
				tei = -1;
				ControlPanel_Paint(obj, pea);
			}
			else
			{
				tei = -1;
				eei = -1;
				FreezeCP = false;
				ControlPanel_Paint(obj, pea);
			}
		}

		private void ControlPanel_MouseLeave(object sender, EventArgs e)
		{
			if (!FreezeCP)
			{
				sx = 0;
				ControlPanel_Paint(obj, pea);
			}
		}

		private void GameArea_MouseUp(object sender, MouseEventArgs e)
		{
			if (!GameLoop.Enabled)
				return;
			if (e.Button != MouseButtons.Left)
				return;
			if (!MouseInGameArea())
				return;
			if (tv[px, py] == -8)
			{
				int j = 0;
				for (j = 0; j < Config.TMAX; j++)
				{
					if (ts[j].X == px && ts[j].Y == py)
						break;
				}
				tei = j;
				eei = -1;
				ControlPanel_Paint(obj, pea);
			}
		}

		private void SpeedLabel_MouseDown(object sender, MouseEventArgs e)
		{
			switch (GameLoop.Interval)
			{
				case Config.FAST:
					if (e.Button == MouseButtons.Left)
					{
						GameLoop.Interval = 60;
						SpeedLabel.Text = "Slow";
					}
					else
					{
						GameLoop.Interval = 40;
						SpeedLabel.Text = "Medium";
					}
					break;
				case 40:
					if (e.Button == MouseButtons.Left)
					{
						GameLoop.Interval = Config.FAST;
						SpeedLabel.Text = "Fast";
					}
					else
					{
						GameLoop.Interval = 60;
						SpeedLabel.Text = "Slow";
					}
					break;
				case 60:
					if (e.Button == MouseButtons.Left)
					{
						GameLoop.Interval = 40;
						SpeedLabel.Text = "Medium";
					}
					else
					{
						GameLoop.Interval = Config.FAST;
						SpeedLabel.Text = "Fast";
					}
					break;
			}
		}

		private void NextEnemy(object sender, EventArgs e)
		{
			if (GameLoop.Enabled && eci < CurrentWave.Count)
				next = 1;
		}

		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.N:
					NextEnemy(obj, ea);
					break;
				case Keys.U:
					mea = new MouseEventArgs(MouseButtons.Left, 1, 0, 0, -10);
					ControlPanel_MouseDown(obj, mea);
					break;
				case Keys.S:
					mea = new MouseEventArgs(MouseButtons.Left, 1, 0, 0, -15);
					ControlPanel_MouseDown(obj, mea);
					break;
				case Keys.P:
					if (GameLoop.Enabled && !paused)
					{
						GameLoop.Enabled = false;
						paused = true;
					}
					else if (paused)
					{
						GameLoop.Enabled = true;
						paused = false;
					} // Else do nothing whatsoever, etc. etc...
					break;
			}
		}

		private void StartEditor_Click(object sender, EventArgs e)
		{
			led = new LevelEditor();
			this.Hide();
			led.ShowDialog();
			this.Show();
		}

		private bool MouseInGameArea() => px >= 0 && px < tv.GetLength(0) && py >= 0 && py <= tv.GetLength(1);

	};
}
