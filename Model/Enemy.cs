using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SevenRiversTD.Properties;
using SevenRiversTD.Data;

namespace SevenRiversTD.Model
{
	public class Enemy
	{
		public Enemy() // Creates a new undefined enemy
		{
			Type = null;
			hp = 0; Speed = 0; Direction = 0; X = 0; Y = 0; _ph = 0; pm = 0; PathPoint = 0; SpeedPhase = 0;
		}
		public Enemy(EnemyType _t, int _x, int _y, int _d) // Sets enemy properties as default values
		{
			Type = _t;
			hp = 0; Speed = 0; Direction = _d; X = _x; Y = _y; _ph = 0; pm = 0; PathPoint = 0; SpeedPhase = 0;
			InitializeEnemyImages(_t);
		}
		public Enemy(EnemyType _t, int _hp, int _s, int _tt, int _gg, sbyte[,] paths)
		{
			Direction = 0; X = 0; Y = 0;
			_ph = 0; PathPoint = 4; SpeedPhase = 0; pm = 4;
			hp = _hp; Speed = _s; Trigger = _tt; Gold = _gg;
			hpi = hp; InitialSpeed = Speed;
			Name = Strings.EnemyNames[_t];
			InitializeEnemyImages(_t);
			SetPath(paths);
			// path[0] (i.e. path length) is used in GameLoop rather than here. (It feels more at home.)
			X = path[1] * Config.GS;
			Y = path[2] * Config.GS;
			Direction = path[3];
			Type = null; // Doesn't this defeat the object of assigning a type...?
		}
		private int hp, hpi;
		double _ph, pm;
		sbyte[] path;
		Bitmap[] img;
		static Random r = new Random();
		// Read/write properties
		public int HP
		{
			get
			{
				if (hp > 0)
					return hp;
				else
					return 0;
			}
			set
			{
				if (value > hpi)
					hp = hpi;
				else if (value > 0)
					hp = value;
				else
				{
					hp = 0;
					Type = null;
				}
			}
		}
		public int MaxHP => hpi;

		public EnemyType? Type { get; set; }
		public int Speed { get; set; }
		public int SpeedPhase { get; set; }
		public int Direction { get; set; }
		public int PathPoint { get; set; }
		public double X { get; set; } // Note the X/Y properties mean different things to turrets.
		public double Y { get; set; }
		public int Gold { get; set; }
		public Bitmap get_Image(int _i) { return img[_i]; }
		public double Phase
		{
			get
			{
				return _ph;
			}
			set
			{
				if (value >= pm)
					value -= pm;
				else if (value < 0)
					value += pm;
				_ph = value;
			}
		}
		public double get_MaxPhase() { return pm; }
		public String Name { get; set; }
		public int Trigger { get; set; }
		public void set_MaxPhase(double _pm) { pm = _pm; }
		public void set_Image(int _i, Bitmap _img) { img[_i] = _img; }
		// Read-only properties
		public sbyte get_Path(int _i) { return path[_i]; }
		public int InitialSpeed { get; set; }
		public Region Region
		{
			get
			{
				GraphicsPath gp = new GraphicsPath();
				gp.AddEllipse((int)X + 8, (int)Y + 8, 16, 16);
				// This rectangle could also be defined using GS, but the image is defined using these numbers.
				return new Region(gp);
			}
		}
		public bool IsBoss
		{
			get
			{
				return Type == EnemyType.BOSS
					|| Type == EnemyType.ERRATIC_BOSS
					|| Type == EnemyType.REGEN_BOSS
					|| Type == EnemyType.SPRINT_BOSS
					|| Type == EnemyType.COMMANDER;
			}
		}

		// Public methods
		public void InitializeEnemyImages(EnemyType _t)
		{
			img = new Bitmap[Config.IMAX];
			for (int i = 0; i < 6; i++)
				img[i] = new Bitmap(32, 32);
			Graphics g;
			Rectangle rect = new Rectangle(8, 8, 16, 16);
			Rectangle rectS = new Rectangle(10, 10, 12, 12);
			Rectangle rectL = new Rectangle(4, 4, 24, 24);
			var outerStar = new[] { new Point(16, 8), new Point(18, 14), new Point(24, 16), new Point(18, 18),
						new Point(16, 24), new Point(14, 18), new Point(8, 16), new Point(14, 14) };
			LinearGradientBrush lgb;
			switch (_t)
			{
				case EnemyType.ARMOUR:
					lgb = new LinearGradientBrush(rect, Color.Gainsboro, Color.Black, LinearGradientMode.ForwardDiagonal);
					g = Graphics.FromImage(img[0]);
					g.FillEllipse(lgb, rect);
					g.DrawEllipse(Pens.Black, rect);
					g = Graphics.FromImage(img[1]);
					lgb = new LinearGradientBrush(rect, Color.LightGray, Color.Black, LinearGradientMode.ForwardDiagonal);
					g.FillEllipse(lgb, rect);
					g.DrawEllipse(Pens.Black, rect);
					g = Graphics.FromImage(img[2]);
					lgb = new LinearGradientBrush(rect, Color.Silver, Color.Black, LinearGradientMode.ForwardDiagonal);
					g.FillEllipse(lgb, rect);
					g.DrawEllipse(Pens.Black, rect);
					g = Graphics.FromImage(img[3]);
					lgb = new LinearGradientBrush(rect, Color.DarkGray, Color.Black, LinearGradientMode.ForwardDiagonal);
					g.FillEllipse(lgb, rect);
					g.DrawEllipse(Pens.Black, rect);
					g = Graphics.FromImage(img[4]);
					lgb = new LinearGradientBrush(rect, Color.Silver, Color.Black, LinearGradientMode.ForwardDiagonal);
					g.FillEllipse(lgb, rect);
					g.DrawEllipse(Pens.Black, rect);
					g = Graphics.FromImage(img[5]);
					lgb = new LinearGradientBrush(rect, Color.LightGray, Color.Black, LinearGradientMode.ForwardDiagonal);
					g.FillEllipse(lgb, rect);
					g.DrawEllipse(Pens.Black, rect);
					break;
				case EnemyType.SWARM:
					g = Graphics.FromImage(img[0]);
					g.FillEllipse(Brushes.DarkViolet, rectS);
					g = Graphics.FromImage(img[1]);
					g.FillEllipse(Brushes.BlueViolet, rectS);
					g = Graphics.FromImage(img[2]);
					g.FillEllipse(Brushes.MediumPurple, rectS);
					g = Graphics.FromImage(img[3]);
					g.FillEllipse(Brushes.MediumSlateBlue, rectS);
					g = Graphics.FromImage(img[4]);
					g.FillEllipse(Brushes.MediumPurple, rectS);
					g = Graphics.FromImage(img[5]);
					g.FillEllipse(Brushes.BlueViolet, rectS);
					break;
				case EnemyType.REGEN:
					g = Graphics.FromImage(img[0]);
					g.FillPie(Brushes.YellowGreen, rect, 0, 90);
					g.FillPie(Brushes.LimeGreen, rect, 90, 90);
					g.FillPie(Brushes.ForestGreen, rect, 180, 90);
					g.FillPie(Brushes.OliveDrab, rect, 270, 90);
					g.DrawLine(Pens.Green, 8, 16, 24, 16);
					g.DrawLine(Pens.Green, 16, 8, 16, 24);
					g = Graphics.FromImage(img[1]);
					g.FillPie(Brushes.YellowGreen, rect, 90, 90);
					g.FillPie(Brushes.LimeGreen, rect, 180, 90);
					g.FillPie(Brushes.ForestGreen, rect, 270, 90);
					g.FillPie(Brushes.OliveDrab, rect, 0, 90);
					g.DrawLine(Pens.Green, 8, 16, 24, 16);
					g.DrawLine(Pens.Green, 16, 8, 16, 24);
					g = Graphics.FromImage(img[2]);
					g.FillPie(Brushes.YellowGreen, rect, 180, 90);
					g.FillPie(Brushes.LimeGreen, rect, 270, 90);
					g.FillPie(Brushes.ForestGreen, rect, 0, 90);
					g.FillPie(Brushes.OliveDrab, rect, 90, 90);
					g.DrawLine(Pens.Green, 8, 16, 24, 16);
					g.DrawLine(Pens.Green, 16, 8, 16, 24);
					g = Graphics.FromImage(img[3]);
					g.FillPie(Brushes.YellowGreen, rect, 270, 90);
					g.FillPie(Brushes.LimeGreen, rect, 0, 90);
					g.FillPie(Brushes.ForestGreen, rect, 90, 90);
					g.FillPie(Brushes.OliveDrab, rect, 180, 90);
					g.DrawLine(Pens.Green, 8, 16, 24, 16);
					g.DrawLine(Pens.Green, 16, 8, 16, 24);
					break;
				case EnemyType.ERRATIC:
					g = Graphics.FromImage(img[0]);
					g.FillEllipse(Brushes.Aquamarine, rect);
					g = Graphics.FromImage(img[1]);
					g.FillEllipse(Brushes.Turquoise, rect);
					g = Graphics.FromImage(img[2]);
					g.FillEllipse(Brushes.MediumAquamarine, rect);
					g = Graphics.FromImage(img[3]);
					g.FillEllipse(Brushes.DarkTurquoise, rect);
					g = Graphics.FromImage(img[4]);
					g.FillEllipse(Brushes.MediumAquamarine, rect);
					g = Graphics.FromImage(img[5]);
					g.FillEllipse(Brushes.Turquoise, rect);
					break;
				case EnemyType.BOSS:
					lgb = new LinearGradientBrush(rectL, Color.Gainsboro, Color.Black, LinearGradientMode.ForwardDiagonal);
					g = Graphics.FromImage(img[0]);
					g.FillEllipse(lgb, rectL);
					g.DrawEllipse(Pens.Black, rectL);
					g = Graphics.FromImage(img[1]);
					lgb = new LinearGradientBrush(rectL, Color.LightGray, Color.Black, LinearGradientMode.ForwardDiagonal);
					g.FillEllipse(lgb, rectL);
					g.DrawEllipse(Pens.Black, rectL);
					g = Graphics.FromImage(img[2]);
					lgb = new LinearGradientBrush(rectL, Color.Silver, Color.Black, LinearGradientMode.ForwardDiagonal);
					g.FillEllipse(lgb, rectL);
					g.DrawEllipse(Pens.Black, rectL);
					g = Graphics.FromImage(img[3]);
					lgb = new LinearGradientBrush(rectL, Color.DarkGray, Color.Black, LinearGradientMode.ForwardDiagonal);
					g.FillEllipse(lgb, rectL);
					g.DrawEllipse(Pens.Black, rectL);
					g = Graphics.FromImage(img[4]);
					lgb = new LinearGradientBrush(rectL, Color.Silver, Color.Black, LinearGradientMode.ForwardDiagonal);
					g.FillEllipse(lgb, rectL);
					g.DrawEllipse(Pens.Black, rectL);
					g = Graphics.FromImage(img[5]);
					lgb = new LinearGradientBrush(rectL, Color.LightGray, Color.Black, LinearGradientMode.ForwardDiagonal);
					g.FillEllipse(lgb, rectL);
					g.DrawEllipse(Pens.Black, rectL);
					break;
				case EnemyType.SPRINT_BOSS:
					g = Graphics.FromImage(img[0]);
					g.FillEllipse(Brushes.Red, rectL);
					g = Graphics.FromImage(img[1]);
					g.FillEllipse(Brushes.OrangeRed, rectL);
					g = Graphics.FromImage(img[2]);
					g.FillEllipse(Brushes.DarkOrange, rectL);
					g = Graphics.FromImage(img[3]);
					g.FillEllipse(Brushes.Orange, rectL);
					g = Graphics.FromImage(img[4]);
					g.FillEllipse(Brushes.DarkOrange, rectL);
					g = Graphics.FromImage(img[5]);
					g.FillEllipse(Brushes.OrangeRed, rectL);
					break;
				case EnemyType.REGEN_BOSS:
					g = Graphics.FromImage(img[0]);
					g.FillPie(Brushes.YellowGreen, rectL, 0, 90);
					g.FillPie(Brushes.LimeGreen, rectL, 90, 90);
					g.FillPie(Brushes.ForestGreen, rectL, 180, 90);
					g.FillPie(Brushes.OliveDrab, rectL, 270, 90);
					g.DrawLine(Pens.Green, 4, 16, 28, 16);
					g.DrawLine(Pens.Green, 16, 4, 16, 28);
					g = Graphics.FromImage(img[1]);
					g.FillPie(Brushes.YellowGreen, rectL, 90, 90);
					g.FillPie(Brushes.LimeGreen, rectL, 180, 90);
					g.FillPie(Brushes.ForestGreen, rectL, 270, 90);
					g.FillPie(Brushes.OliveDrab, rectL, 0, 90);
					g.DrawLine(Pens.Green, 4, 16, 28, 16);
					g.DrawLine(Pens.Green, 16, 4, 16, 28);
					g = Graphics.FromImage(img[2]);
					g.FillPie(Brushes.YellowGreen, rectL, 180, 90);
					g.FillPie(Brushes.LimeGreen, rectL, 270, 90);
					g.FillPie(Brushes.ForestGreen, rectL, 0, 90);
					g.FillPie(Brushes.OliveDrab, rectL, 90, 90);
					g.DrawLine(Pens.Green, 4, 16, 28, 16);
					g.DrawLine(Pens.Green, 16, 4, 16, 28);
					g = Graphics.FromImage(img[3]);
					g.FillPie(Brushes.YellowGreen, rectL, 270, 90);
					g.FillPie(Brushes.LimeGreen, rectL, 0, 90);
					g.FillPie(Brushes.ForestGreen, rectL, 90, 90);
					g.FillPie(Brushes.OliveDrab, rectL, 180, 90);
					g.DrawLine(Pens.Green, 4, 16, 28, 16);
					g.DrawLine(Pens.Green, 16, 4, 16, 28);
					break;
				case EnemyType.ERRATIC_BOSS:
					g = Graphics.FromImage(img[0]);
					g.FillEllipse(Brushes.Aquamarine, rectL);
					g = Graphics.FromImage(img[1]);
					g.FillEllipse(Brushes.Turquoise, rectL);
					g = Graphics.FromImage(img[2]);
					g.FillEllipse(Brushes.MediumAquamarine, rectL);
					g = Graphics.FromImage(img[3]);
					g.FillEllipse(Brushes.DarkTurquoise, rectL);
					g = Graphics.FromImage(img[4]);
					g.FillEllipse(Brushes.MediumAquamarine, rectL);
					g = Graphics.FromImage(img[5]);
					g.FillEllipse(Brushes.Turquoise, rectL);
					break;
				case EnemyType.DISRUPTOR:
					g = Graphics.FromImage(img[0]);
					g.FillEllipse(Brushes.Orange, rect);
					g.FillEllipse(Brushes.Black, rectS);
					g = Graphics.FromImage(img[1]);
					g.FillEllipse(Brushes.Gold, rect);
					g.FillEllipse(Brushes.Black, rectS);
					g = Graphics.FromImage(img[2]);
					g.FillEllipse(Brushes.Yellow, rect);
					g.FillEllipse(Brushes.Black, rectS);
					g = Graphics.FromImage(img[3]);
					g.FillEllipse(Brushes.LightYellow, rect);
					g.FillEllipse(Brushes.Black, rectS);
					g = Graphics.FromImage(img[4]);
					g.FillEllipse(Brushes.Yellow, rect);
					g.FillEllipse(Brushes.Black, rectS);
					g = Graphics.FromImage(img[5]);
					g.FillEllipse(Brushes.Gold, rect);
					g.FillEllipse(Brushes.Black, rectS);
					break;
				case EnemyType.ACCELERATOR:
					var br2 = new SolidBrush(Color.FromArgb(215, Color.Red));
					var br3 = new SolidBrush(Color.FromArgb(175, Color.Red));
					var br4 = new SolidBrush(Color.FromArgb(135, Color.Red));
					g = Graphics.FromImage(img[0]);
					g.FillPolygon(Brushes.Red, outerStar);
					g = Graphics.FromImage(img[1]);
					g.FillPolygon(br2, outerStar);
					g = Graphics.FromImage(img[2]);
					g.FillPolygon(br3, outerStar);
					g = Graphics.FromImage(img[3]);
					g.FillPolygon(br4, outerStar);
					g = Graphics.FromImage(img[4]);
					g.FillPolygon(br3, outerStar);
					g = Graphics.FromImage(img[5]);
					g.FillPolygon(br2, outerStar);
					break;
				case EnemyType.COMMANDER:
					var br5 = new SolidBrush(Color.FromArgb(128, Color.White));
					var br6 = new SolidBrush(Color.FromArgb(100, Color.White));
					lgb = new LinearGradientBrush(rectL, Color.Brown, Color.Black, LinearGradientMode.ForwardDiagonal);
					g = Graphics.FromImage(img[0]);
					g.FillEllipse(lgb, rectL);
					g.DrawEllipse(Pens.Black, rectL);
					g.FillPolygon(br5, outerStar);
					g = Graphics.FromImage(img[1]);
					lgb = new LinearGradientBrush(rectL, Color.RosyBrown, Color.DarkSlateGray, LinearGradientMode.ForwardDiagonal);
					g.FillEllipse(lgb, rectL);
					g.DrawEllipse(Pens.Black, rectL);
					g.FillPolygon(br6, outerStar);
					g = Graphics.FromImage(img[2]);
					lgb = new LinearGradientBrush(rectL, Color.SaddleBrown, Color.DarkSlateGray, LinearGradientMode.ForwardDiagonal);
					g.FillEllipse(lgb, rectL);
					g.DrawEllipse(Pens.Black, rectL);
					g.FillPolygon(br5, outerStar);
					g = Graphics.FromImage(img[3]);
					lgb = new LinearGradientBrush(rectL, Color.SandyBrown, Color.DarkSlateGray, LinearGradientMode.ForwardDiagonal);
					g.FillEllipse(lgb, rectL);
					g.DrawEllipse(Pens.Black, rectL);
					g.FillPolygon(br6, outerStar);
					g = Graphics.FromImage(img[4]);
					lgb = new LinearGradientBrush(rectL, Color.SaddleBrown, Color.Black, LinearGradientMode.ForwardDiagonal);
					g.FillEllipse(lgb, rectL);
					g.DrawEllipse(Pens.Black, rectL);
					g.FillPolygon(br5, outerStar);
					g = Graphics.FromImage(img[5]);
					lgb = new LinearGradientBrush(rectL, Color.RosyBrown, Color.Black, LinearGradientMode.ForwardDiagonal);
					g.FillEllipse(lgb, rectL);
					g.DrawEllipse(Pens.Black, rectL);
					g.FillPolygon(br6, outerStar);
					break;
				default: // Normal enemies will look like this
					g = Graphics.FromImage(img[0]);
					g.FillEllipse(Brushes.Red, rect);
					g = Graphics.FromImage(img[1]);
					g.FillEllipse(Brushes.OrangeRed, rect);
					g = Graphics.FromImage(img[2]);
					g.FillEllipse(Brushes.DarkOrange, rect);
					g = Graphics.FromImage(img[3]);
					g.FillEllipse(Brushes.Orange, rect);
					g = Graphics.FromImage(img[4]);
					g.FillEllipse(Brushes.DarkOrange, rect);
					g = Graphics.FromImage(img[5]);
					g.FillEllipse(Brushes.OrangeRed, rect);
					break;
			}

			for (int i = 0; i < 6; i++)
				img[i].MakeTransparent(Color.White);
		}

		public void Draw(Graphics g, Rectangle rect)
		{
			g.DrawImageUnscaled(img[(int)_ph], rect);
		}

		public void Draw(Graphics g, int _x, int _y)
		{
			g.DrawImageUnscaled(img[(int)_ph], _x, _y);
		}

		public void Draw(Graphics g)
		{
			g.DrawImageUnscaled(img[(int)_ph], (int)X, (int)Y);
		}

		public void SetPath(sbyte[,] m)
		{
			path = new sbyte[m.GetLength(1)]; // Highest path length
			int pc = r.Next(0, m.GetLength(0)); // Path count
			for (int i = 0; i < m.GetLength(1); i++)
				path[i] = m[pc, i];
		}

		public bool Intersects(Graphics g, int mx, int my)
		{
			if (Type == null)
				return false;
			Region re = Region;
			re.Intersect(new RectangleF(mx - 1, my - 1, 3, 3));
			return !re.IsEmpty(g);
		}
	};
};
