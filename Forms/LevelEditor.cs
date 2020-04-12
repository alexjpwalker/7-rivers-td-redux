using System;
using System.ComponentModel;
using System.Collections;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using SevenRiversTD.Model;
using System.IO;
using SevenRiversTD.Properties;

namespace SevenRiversTD.Forms
{
	public partial class LevelEditor : Form
	{
		public LevelEditor()
		{
			InitializeComponent();
			tpi = new Bitmap(TilePanel.Width, TilePanel.Height);
			bmp = new Bitmap(TilePanel.Width, TilePanel.Height);
			area = new Bitmap(GameArea.Width, GameArea.Height);
			br = new SolidBrush(GameArea.BackColor);
			tv = new sbyte[Map.MW + 8, Map.MH + 8];
			paths = new sbyte[Config.MPC, Config.MPL];
			for (int i = 0; i < Config.MPC; i++)
			{
				for (int j = 0; j < Config.MPL; j++)
					paths[i, j] = -1;
			}
			map = new sbyte[Config.MLMAX];
			sf = new StringFormat();
			sf.Alignment = StringAlignment.Center;
			sf.LineAlignment = StringAlignment.Center;
			MD = false;
			obj = new Object();
			pea = new PaintEventArgs(TilePanel.CreateGraphics(), TilePanel.ClientRectangle);
			gpea = new PaintEventArgs(GameArea.CreateGraphics(), GameArea.ClientRectangle);
			hb = new HatchBrush(HatchStyle.LargeConfetti, Color.DimGray, Color.Black);
			hb2 = new HatchBrush(HatchStyle.SmallConfetti, Color.DarkRed, Color.Black);
			ti = 0; pap = 0; pc = 0;
			WaveFile = String.Empty;
		}

		private Bitmap tpi, area, bmp;
		SolidBrush br;
		sbyte[,] tv;
		sbyte[] map;
		sbyte[,] paths;
		int sx, sy, pap, pc; // x,y - path add phase - path count
		sbyte ti, px, py, cp; // tile index - path point x,y - current point
		int seq; // sequence length
		bool MD; // mouse down
		StringFormat sf;
		Object obj;
		WaveSequence waves;
		PaintEventArgs pea, gpea;
		HatchBrush hb, hb2;
		String WaveFile;

		private System.Windows.Forms.Panel GameArea;
		private System.Windows.Forms.Panel TilePanel;
		private System.Windows.Forms.SaveFileDialog SaveLevel;
		private System.Windows.Forms.OpenFileDialog LoadLevel;
		private System.Windows.Forms.Button AddPath;
		private System.Windows.Forms.MenuStrip mainMenu1;
		private System.Windows.Forms.ToolStripMenuItem mFile;
		private System.Windows.Forms.ToolStripMenuItem NewMap;
		private System.Windows.Forms.ToolStripMenuItem mOpen;
		private System.Windows.Forms.ToolStripMenuItem SaveMap;
		private System.Windows.Forms.ToolStripMenuItem SaveAs;
		private System.Windows.Forms.ToolStripMenuItem Exit;
		private System.Windows.Forms.ToolStripMenuItem mEdit;
		private System.Windows.Forms.ToolStripMenuItem mAddPath;
		private System.Windows.Forms.ToolStripMenuItem menuItem9;
		private System.Windows.Forms.ToolStripMenuItem FillArea;
		private System.Windows.Forms.ToolStripMenuItem OpenMap;
		private System.Windows.Forms.ToolStripMenuItem OpenWaves;
		private System.Windows.Forms.Label WaveHeader;
		private System.Windows.Forms.OpenFileDialog LoadWaves;
		private System.Windows.Forms.StatusBar Status;
		private System.Windows.Forms.Label t_path;
		private System.Windows.Forms.Label PathHeader;
		private System.Windows.Forms.NumericUpDown Path;
		private System.Windows.Forms.ToolStripMenuItem SaveMapOnly;

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
					break;
				case 2: // Path point - invisible in main app
					br.Color = Color.DimGray;
					g.FillRectangle(br, i * Config.GS + 4, j * Config.GS + Config.GS / 2 - 2, Config.GS - 8, 4);
					g.FillRectangle(br, i * Config.GS + Config.GS / 2 - 2, j * Config.GS + 4, 4, Config.GS - 8);
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
					pts[0] = new Point(i * Config.GS, (int)(j + Config.FDL) * Config.GS);
					pts[1] = new Point(i * Config.GS, (j + 1) * Config.GS);
					lg = new LinearGradientBrush(pts[0], pts[1], Color.FromArgb(0, fec), Color.FromArgb(200, fec));
					g.FillRectangle(lg, i * Config.GS, (int)(j + Config.FD) * Config.GS, Config.GS, (int)(Config.GS * Config.GSM));
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
		private void DrawTiles(Graphics g)
		{
			br.Color = GameArea.BackColor;
			Point[] pts = new Point[4];
			LinearGradientBrush lg;
			Color fec; // Fading effect color
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
						case -10: // Charred
							g.FillRectangle(br, i * Config.GS, j * Config.GS, Config.GS, Config.GS);
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
							break;
					}
				}
			}
		}
		private void TilePanel_Paint(System.Object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Graphics g = Graphics.FromImage(bmp);
			g.DrawImageUnscaled(tpi, 0, 0);
			if (ti != -1)
				g.DrawRectangle(Pens.Red, sx * Config.GS + 1, sy * Config.GS + 1, Config.GS - 2, Config.GS - 2);
			e.Graphics.DrawImageUnscaled(bmp, 0, 0);
		}
		private void GameArea_Paint(System.Object sender, System.Windows.Forms.PaintEventArgs e)
		{
			e.Graphics.DrawImageUnscaled(area, 0, 0);
		}
		private void LevelEditor_Load(System.Object sender, System.EventArgs e)
		{
			LinearGradientBrush lg = new LinearGradientBrush(new Point(Config.GS, 0), new Point(Config.GS * 2, Config.GS), Color.LightGray, Color.DarkGray);
			Color fec; // Fading effect color
			Graphics g = Graphics.FromImage(tpi);
			g.FillRectangle(br, 0, 0, Config.GS, Config.GS);
			g.FillRectangle(lg, Config.GS, 0, Config.GS, Config.GS);
			lg = new LinearGradientBrush(new Point(Config.GS * 2, 0), new Point(Config.GS * 3, Config.GS), Color.LightGray, Color.DarkGray);
			g.FillRectangle(lg, Config.GS * 2, 0, Config.GS, Config.GS);
			for (int i = 0; i < 3; i++)
			{
				for (int j = 1; j < 4; j++)
				{
					lg = new LinearGradientBrush(new Point(Config.GS * i, Config.GS * j), new Point(Config.GS * (i + 1), Config.GS * (j + 1)), Color.LightGray, Color.DarkGray);
					g.FillRectangle(lg, Config.GS * i, Config.GS * j, Config.GS, Config.GS);
				}
			}
			g.FillRectangle(Brushes.Black, Config.GS * 2, Config.GS * 3, Config.GS, Config.GS);
			g.FillRectangle(hb, 2 * Config.GS + 3, 3 * Config.GS + 3, Config.GS - 6, Config.GS - 6);
			g.FillEllipse(hb2, 2 * Config.GS + 1, 3 * Config.GS + 1, Config.GS - 2, Config.GS - 2);
			for (int i = 0; i < 4; i++)
				g.DrawLine(Pens.Black, Config.GS * i, 0, Config.GS * i, Config.GS * 4);
			for (int i = 0; i < 5; i++)
				g.DrawLine(Pens.Black, 0, Config.GS * i, Config.GS * 3, Config.GS * i);
			int _tv = -1;
			Point[] pts = new Point[4];
			for (int j = 0; j < 4; j++)
			{
				for (int i = 0; i < 3; i++)
				{
					_tv++;
					if (_tv <= 6)
						fec = Color.Lime;
					else
						fec = Color.DarkRed;
					switch (_tv) // For all tiles inherited from enemy path
					{
						case 2:
							g.FillRectangle(Brushes.DimGray, i * Config.GS + 4, j * Config.GS + Config.GS / 2 - 2, Config.GS - 8, 4);
							g.FillRectangle(Brushes.DimGray, i * Config.GS + Config.GS / 2 - 2, j * Config.GS + 4, 4, Config.GS - 8);
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
						default:
							break;
					}
				}
			}
			g = Graphics.FromImage(area);
			DrawTiles(g);
			DrawGrid(g);
		}
		private void GameArea_MouseDown(System.Object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (pap == 0)
			{
				MD = true;
				GameArea_MouseMove(obj, e);
			}
			else if (pap == 1)
			{
				px = (sbyte)(e.X / Config.GS);
				py = (sbyte)(e.Y / Config.GS);
				paths[(int)Path.Value, cp] = px;
				paths[(int)Path.Value, cp + 1] = py;
				cp += 2;
				Status.Text = String.Concat("Select path point ", (cp - 2).ToString());
				RectangleF rectf = new RectangleF(px * Config.GS, py * Config.GS, Config.GS, Config.GS);
				GameArea.CreateGraphics().DrawRectangle(Pens.Red, px * Config.GS, py * Config.GS, Config.GS, Config.GS);
				GameArea.CreateGraphics().DrawString("S", this.Font, Brushes.Red, rectf, sf);
				pap = 2;
			}
			else if (pap == 2)
			{
				if (cp > Config.MPL - 7)
				{
					pap = 0;
					paths[(int)Path.Value, cp] = (sbyte)(e.X / Config.GS);
					paths[(int)Path.Value, cp + 1] = (sbyte)(e.Y / Config.GS);
					paths[(int)Path.Value, cp + 2] = -1;
					paths[(int)Path.Value, 0] = (sbyte)(cp + 3);
					Status.Text = "Maximum path length reached";
					Path.Enabled = true;
					pc++;
					PathHeader.Text = String.Concat("Paths: ", pc.ToString());
					return;
				}
				if (e.X / Config.GS == px && e.Y / Config.GS < py)
					paths[(int)Path.Value, cp] = 1;
				else if (e.X / Config.GS < px && e.Y / Config.GS == py)
					paths[(int)Path.Value, cp] = 2;
				else if (e.X / Config.GS == px && e.Y / Config.GS > py)
					paths[(int)Path.Value, cp] = 3;
				else if (e.X / Config.GS > px && e.Y / Config.GS == py)
					paths[(int)Path.Value, cp] = 4;
				else // Either path point is the same as last point, or it is relatively diagonal
					return;
				px = (sbyte)(e.X / Config.GS);
				py = (sbyte)(e.Y / Config.GS);
				GameArea.CreateGraphics().DrawRectangle(Pens.Red, px * Config.GS, py * Config.GS, Config.GS, Config.GS);
				RectangleF rectf = new RectangleF(px * Config.GS, py * Config.GS, Config.GS, Config.GS);
				GameArea.CreateGraphics().DrawString((cp - 2).ToString(), this.Font, Brushes.Red, rectf, sf);
				cp++;
				if (e.Button == MouseButtons.Left)
				{
					Status.Text = String.Concat("Select path point ", (cp - 2).ToString());
					return;
				}
				else
				{
					paths[(int)Path.Value, cp] = (sbyte)(e.X / Config.GS);
					paths[(int)Path.Value, cp + 1] = (sbyte)(e.Y / Config.GS);
					paths[(int)Path.Value, cp + 2] = -1;
					paths[(int)Path.Value, 0] = (sbyte)(cp + 3);
					Status.Text = "Ready";
					Path.Enabled = true;
					pap = 0;
					pc++;
					PathHeader.Text = String.Concat("Paths: ", pc.ToString());
				}
			}
		}

		private void GameArea_MouseUp(System.Object sender, System.Windows.Forms.MouseEventArgs e)
		{
			MD = false;
		}

		private void GameArea_MouseMove(System.Object sender, System.Windows.Forms.MouseEventArgs e)
		{
			Graphics g;
			if (!MD)
				return;
			if (ti == tv[e.X / Config.GS, e.Y / Config.GS])
				return; // Not much point changing a tile value if it's already that value.
			tv[e.X / Config.GS, e.Y / Config.GS] = ti;
			map[e.Y / Config.GS * Map.MW + e.X / Config.GS] = ti;
			g = Graphics.FromImage(area);
			DrawTiles(g);
			DrawGrid(g);
			GameArea_Paint(obj, gpea);
		}

		private void TilePanel_MouseDown(System.Object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (pap != 0)
				return;
			sx = (int)e.X / Config.GS;
			sy = (int)e.Y / Config.GS;
			ti = (sbyte)(sy * 3 + sx);
			if (ti > Config.STD_TLIM)
			{
				switch (ti)
				{
					case 11:
						ti = Config.CHARRED;
						break;
					default:
						ti = 0;
						break;
				}
			}
			TilePanel_Paint(obj, pea); // This function reads sx and sy. ti is read somewhere else.
		}

		private void OpenWaves_Click(System.Object sender, System.EventArgs e)
		{
			if (LoadWaves.ShowDialog() == DialogResult.OK)
			{
				waves = new WaveSequence();
				try
				{
					seq = WaveSequence.get_FileSequenceCount(LoadWaves.FileName, map);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
					seq = 0;
				}
				WaveHeader.Text = String.Concat("Waves: ", seq.ToString());
				if (seq > 0)
					this.WaveFile = LoadWaves.FileName;
			}
		}

		private void SaveMap_Click(System.Object sender, System.EventArgs e)
		{
			if (WaveFile.Length == 0)
			{
				MessageBox.Show("No wave sequence file defined for this level. Successfully load a WaveSequence file to save this level.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (SaveLevel.ShowDialog() == DialogResult.OK)
			{
				FileStream fs = new FileStream(SaveLevel.FileName, FileMode.Create, FileAccess.Write);
				StreamWriter sw = new StreamWriter(fs);
				String nameS = SaveLevel.FileName.Remove(0, SaveLevel.FileName.LastIndexOf("\\") + 1);
				String nameL = SaveLevel.FileName;
				if (nameS.EndsWith(".txt"))
				{
					nameS = nameS.Remove(nameS.Length - 4, 4);
					nameL = nameL.Remove(nameL.Length - 4, 4);
				}
				nameS = String.Concat(nameS, "_Map");
				nameL = String.Concat(nameL, "_Map.txt");
				sw.Write(nameS);
				sw.Write(",");
				nameS = WaveFile.Remove(0, WaveFile.LastIndexOf("\\") + 1);
				if (nameS.EndsWith(".txt"))
					nameS = nameS.Remove(nameS.Length - 4, 4);
				sw.Write(nameS);
				sw.Write(",");
				sw.Write(".txt");
				sw.Close();
				fs = new FileStream(nameL, FileMode.Create, FileAccess.Write);
				sw = new StreamWriter(fs);
				StringWriter stw = new StringWriter();
				int ti = -1; // 1-dimensional tile index
				for (int i = 0; i < Map.MH; i++)
				{
					for (int j = 0; j < Map.MW; j++)
					{
						ti++;
						stw.Write(map[ti].ToString());
						if (map[ti].ToString().Length == 2)
							stw.Write(",");
						else
							stw.Write(", ");
					}
					stw.WriteLine();
				}
				stw.WriteLine();
				stw.Write(pc.ToString());
				stw.WriteLine(",");
				for (int i = 0; i < pc; i++)
				{
					if (paths[i, 0] == -1)
						break; // Paths are stacked, so no higher order needs checking.
					for (int j = 0; j < paths.GetLength(1) - 1; j++)
					{
						stw.Write(paths[i, j].ToString());
						if (paths[i, j].ToString().Length == 2)
							stw.Write(",");
						else
							stw.Write(", ");
						if (paths[i, j] == -1)
							break; // This actually continues to the next path.
					}
				}
				String s = stw.ToString();
				sw.Write(s.TrimEnd(",".ToCharArray()));
				stw.Close();
				sw.Close();
			}
		}

		private void mAddPath_Click(System.Object sender, System.EventArgs e)
		{
			pap = 1;
			cp = 1;
			ti = -1;
			if (Path.Value > 0)
			{
				while (paths[(int)Path.Value - 1, 0] == -1 && Path.Value > 0)
				{
					Path.Value--;
					if (Path.Value == 0)
						break;
				}
			}
			for (int i = 0; i < Config.MPL; i++)
				paths[(int)Path.Value, i] = -1;
			Path.Enabled = false;
			Status.Text = "Select start point";
		}

		private void SaveMapOnly_Click(System.Object sender, System.EventArgs e)
		{
			if (SaveLevel.ShowDialog() == DialogResult.OK)
			{
				FileStream fs = new FileStream(SaveLevel.FileName, FileMode.Create, FileAccess.Write);
				StreamWriter sw = new StreamWriter(fs);
				StringWriter stw = new StringWriter();
				int ti = -1; // 1-dimensional tile index
				for (int i = 0; i < Map.MH; i++)
				{
					for (int j = 0; j < Map.MW; j++)
					{
						ti++;
						stw.Write(map[ti].ToString());
						if (map[ti].ToString().Length == 2)
							stw.Write(",");
						else
							stw.Write(", ");
					}
					stw.WriteLine();
				}
				stw.WriteLine();
				String s = stw.ToString();
				sw.Write(s.TrimEnd(",".ToCharArray()));
				stw.Close();
				sw.Close();
			}
		}

	};
}
