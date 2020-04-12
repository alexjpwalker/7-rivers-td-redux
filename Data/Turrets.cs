using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace SevenRiversTD.Data
{
	public sealed class Turrets
	{
		public static void InitializeTurretImages()
		{
			//
			// MG Turret
			//
			Graphics g; GraphicsPath gp; LinearGradientBrush lgb;
			MGTurret = new Bitmap(32, 32);
			// t1 = __try_cast<Bitmap>(Image.FromFile(String.Concat(Environment.CurrentDirectory, "\\Merlin's Head.bmp")));
			g = Graphics.FromImage(MGTurret);
			gp = new GraphicsPath();
			lgb = new LinearGradientBrush(new Point(0, 0), new Point(32, 32), Color.FromArgb(70, 70, 70), Color.FromArgb(20, 20, 20));
			gp.AddArc(6, 12, 20, 16, 0, 180);
			gp.AddLine(26, 20, 26, 14);
			gp.AddArc(6, 6, 20, 16, 0, 180);
			gp.AddLine(7, 15, 7, 20);
			g.FillPath(lgb, gp);
			lgb = new LinearGradientBrush(new Point(0, 0), new Point(32, 32), Color.FromArgb(10, 10, 10), Color.Gray);
			g.FillEllipse(lgb, 6, 6, 20, 16);
			//
			// Sniper Turret
			//
			SniperTurret = new Bitmap(32, 32);
			g = Graphics.FromImage(SniperTurret);
			gp.Reset();
			lgb = new LinearGradientBrush(new Point(0, 0), new Point(32, 32), Color.FromArgb(70, 70, 70), Color.FromArgb(20, 20, 20));
			gp.AddArc(6, 12, 20, 16, 0, 180);
			gp.AddLine(26, 20, 26, 14);
			gp.AddArc(6, 6, 20, 16, 0, 180);
			gp.AddLine(7, 15, 7, 20);
			g.FillPath(lgb, gp);
			lgb = new LinearGradientBrush(new Point(0, 0), new Point(32, 32), Color.FromArgb(10, 10, 10), Color.Gray);
			g.FillEllipse(lgb, 6, 6, 20, 16);
			gp.Reset();
			lgb = new LinearGradientBrush(new Point(6, 6), new Point(24, 24), Color.FromArgb(100, 100, 100), Color.Black);
			gp.AddArc(10, 9, 12, 10, 0, 180);
			gp.AddLine(22, 14, 22, 10);
			gp.AddArc(10, 4, 12, 10, 0, 180);
			gp.AddLine(11, 11, 11, 14);
			g.FillPath(lgb, gp);
			lgb = new LinearGradientBrush(new Point(6, 6), new Point(24, 24), Color.Black, Color.Silver);
			g.FillEllipse(lgb, 10, 4, 12, 10);
			// 
			// Laser Turret
			//
			LaserTurret = new Bitmap(32, 32);
			g = Graphics.FromImage(LaserTurret);
			gp = new GraphicsPath();
			lgb = new LinearGradientBrush(new Point(0, 0), new Point(32, 32), Color.FromArgb(120, 70, 70), Color.FromArgb(40, 20, 20));
			gp.AddArc(6, 12, 20, 16, 0, 180);
			gp.AddLine(26, 20, 26, 14);
			gp.AddArc(6, 6, 20, 16, 0, 180);
			gp.AddLine(7, 15, 7, 20);
			g.FillPath(lgb, gp);
			lgb = new LinearGradientBrush(new Point(0, 0), new Point(32, 32), Color.FromArgb(30, 30, 10), Color.Red);
			g.FillEllipse(lgb, 6, 6, 20, 16);
			//
			// Plasma Cannon
			//
			PlasmaCannon = new Bitmap(32, 32);
			g = Graphics.FromImage(PlasmaCannon);
			gp.Reset();
			lgb = new LinearGradientBrush(new Point(0, 0), new Point(32, 32), Color.SteelBlue, Color.Black);
			gp.AddArc(6, 12, 20, 16, 0, 180);
			gp.AddLine(26, 20, 26, 14);
			gp.AddArc(6, 6, 20, 16, 0, 180);
			gp.AddLine(7, 15, 7, 20);
			g.FillPath(lgb, gp);
			lgb = new LinearGradientBrush(new Point(0, 0), new Point(32, 32), Color.Black, Color.SteelBlue);
			g.FillEllipse(lgb, 6, 6, 20, 16);
			gp.Reset();
			lgb = new LinearGradientBrush(new Point(6, 6), new Point(24, 24), Color.SteelBlue, Color.Black);
			gp.AddArc(10, 9, 12, 10, 0, 180);
			gp.AddLine(22, 14, 22, 10);
			gp.AddArc(10, 4, 12, 10, 0, 180);
			gp.AddLine(11, 11, 11, 14);
			g.FillPath(lgb, gp);
			lgb = new LinearGradientBrush(new Point(4, 4), new Point(20, 22), Color.SteelBlue, Color.Black);
			g.FillEllipse(lgb, 10, 4, 12, 10);
		}
		public static Bitmap MGTurret { get; private set; }
		public static Bitmap SniperTurret { get; private set; }
		public static Bitmap LaserTurret { get; private set; }
		public static Bitmap PlasmaCannon { get; private set; }
	}
}
