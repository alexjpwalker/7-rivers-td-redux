using System;
using System.ComponentModel;
using System.Collections;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using SevenRiversTD.Model;
using SevenRiversTD.Properties;
using SevenRiversTD.Data;

namespace SevenRiversTD.Forms
{
	public partial class LevelSelect : Form
	{
		public LevelSelect(sbyte _i, int width, int height)
		{
			InitializeComponent();
			LevelIndex = _i;
			br = new SolidBrush(Color.LightGreen);
			tv = new sbyte[Map.MW + 8, Map.MH + 8];
			bmp = new Bitmap(width, height);
			hb = new HatchBrush(HatchStyle.LargeConfetti, Color.DimGray, Color.Black);
			hb2 = new HatchBrush(HatchStyle.SmallConfetti, Color.DarkRed, Color.Black);
		}

		public sbyte LevelIndex;
		private SolidBrush br;
		private HatchBrush hb, hb2;
		sbyte[,] tv;
		Bitmap bmp;
		private System.Windows.Forms.PictureBox LevelPreview;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.Button button3;

		private void DrawGrid(Graphics g)
		{
			Pen p = new Pen(Color.Black, 1.5f);
			for (int i = 0; i <= Config.AS; i += Config.GS)
			{
				g.DrawLine(p, new Point(i, 0), new Point(i, Config.AS));
				g.DrawLine(p, new Point(0, i), new Point(Config.AS, i));
			}
		}
		private void DrawTopLayer(Graphics g, sbyte _tv, int i, int j)
		{
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
					pts[0] = new Point(i * Config.GS, (int)((j + 1 - Config.FD) * Config.GS)); // [0] is the TRANSPARENT end
					pts[1] = new Point(i * Config.GS, j * Config.GS);
					lg = new LinearGradientBrush(pts[0], pts[1], Color.FromArgb(0, fec), Color.FromArgb(200, fec));
					g.FillRectangle(lg, i * Config.GS, j * Config.GS, Config.GS, (int)(Config.GS * Config.GSM));
					break;
				case 4:
				case 8:
					pts[0] = new Point((int)((i + 1 - Config.FD) * Config.GS), j * Config.GS);
					pts[1] = new Point(i * Config.GS, j * Config.GS);
					lg = new LinearGradientBrush(pts[0], pts[1], Color.FromArgb(0, fec), Color.FromArgb(200, fec));
					g.FillRectangle(lg, i * Config.GS, j * Config.GS, (int)(Config.GS * Config.GSM), Config.GS);
					break;
				case 5:
				case 9:
					pts[0] = new Point(i * Config.GS, (int)((j + Config.FD) * Config.GS));
					pts[1] = new Point(i * Config.GS, (j + 1) * Config.GS);
					lg = new LinearGradientBrush(pts[0], pts[1], Color.FromArgb(0, fec), Color.FromArgb(200, fec));
					g.FillRectangle(lg, i * Config.GS, (int)((j + Config.FD) * Config.GS), Config.GS, (int)(Config.GS * Config.GSM));
					break;
				case 6:
				case 10:
					pts[0] = new Point((int)((i + Config.FD) * Config.GS), j * Config.GS);
					pts[1] = new Point(i * Config.GS + Config.GS, j * Config.GS);
					lg = new LinearGradientBrush(pts[0], pts[1], Color.FromArgb(0, fec), Color.FromArgb(200, fec));
					g.FillRectangle(lg, (int)((i + Config.FD) * Config.GS), j * Config.GS, (int)(Config.GS * Config.GSM), Config.GS);
					break;
			}
		}

		private void DrawTiles(Graphics g, sbyte[] m)
		{
			Point[] pts = new Point[4];
			LinearGradientBrush lg;
			tv = Map.ToGrid(m);
			for (int i = 0; i < Map.MW; i++)
			{
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
							DrawTopLayer(g, tv[i, j], i, j);
							break;
						case 11: // TODO: River tiles (cases through to about 18 may be needed)
							break;
						default:
							MessageBox.Show(tv[i, j].ToString(), "Undefined tile");
							break;
					}
				}
			}
			DrawGrid(g);
		}
		private void PreviewMap(int mi)
		{
			Graphics g = Graphics.FromImage(bmp);
			switch (mi)
			{
				case 1:
					DrawTiles(g, Maps.map1);
					button2.Enabled = false;
					label1.Text = "Level 1: 20 Waves";
					break;
				case 2:
					DrawTiles(g, Maps.map2);
					button2.Enabled = true;
					label1.Text = "Level 2: 30 Waves";
					break;
				case 3:
					DrawTiles(g, Maps.map3);
					label1.Text = "Level 3";
					break;
				case 4:
					DrawTiles(g, Maps.map4);
					label1.Text = "Level 4";
					break;
				case 5:
					DrawTiles(g, Maps.map5);
					button3.Enabled = true;
					label1.Text = "Level 5";
					break;
				case 6:
					DrawTiles(g, Maps.map6);
					button3.Enabled = false;
					label1.Text = "Level 6";
					break;
				default:
					return;
			}
			LevelPreview.Image = (Image)(bmp);
		}
		private void LevelSelect_Load(object sender, EventArgs e)
		{
			Graphics g = Graphics.FromImage(bmp);
			DrawTiles(g, Maps.map1);
			LevelPreview.Image = (Image)(bmp);
		}

		private void LevelPreview_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			LevelIndex = 0;
			this.DialogResult = DialogResult.OK;
		}

		private void button3_Click(object sender, EventArgs e)
		{
			LevelIndex++;
			PreviewMap(LevelIndex);
		}

		private void button2_Click(object sender, EventArgs e)
		{
			LevelIndex--;
			PreviewMap(LevelIndex);
		}

	}
}
