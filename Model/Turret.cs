using SevenRiversTD.Data;
using SevenRiversTD.Properties;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SevenRiversTD.Model
{
	public class Turret
	{
		public Turret() // Creates a new undefined turret
		{
			Type = -1;
			X = 0; Y = 0; Damage = 0; Range = 0; CurrentDelay = 0; ShotDelay = 0; a = 0; Level = 0;
		}

		public Turret(sbyte _t, short _x, short _y, bool warn) // Sets turret properties as default values
		{
			Type = _t;
			X = _x;
			Y = _y;
			a = 0; ShotCount = 0;
			Level = 1;
			switch (_t)
			{
				case -1: // Undefined
					Damage = 0;
					Range = 0;
					CurrentDelay = 0;
					ShotDelay = 0;
					break;
				case 0: // MG Turret
					Damage = 3;
					Range = 100;
					CurrentDelay = 6;
					ShotDelay = 6;
					ShotColor = Color.OrangeRed;
					ShotWidth = 2.5f;
					Image = Turrets.MGTurret;
					break;
				case 1: // Sniper Turret
					Damage = 17;
					Range = 200;
					CurrentDelay = 28;
					ShotDelay = 28;
					ShotColor = Color.Firebrick;
					ShotWidth = 3;
					Image = Turrets.SniperTurret;
					break;
				case 2: // Laser Turret
					Damage = 1;
					Range = 120;
					CurrentDelay = 0;
					ShotDelay = 0;
					ShotColor = Color.Orange;
					ShotWidth = 2;
					Image = Turrets.LaserTurret;
					break;
				case 3: // Plasma Cannon
					Damage = 46;
					Range = 140;
					CurrentDelay = 35;
					ShotDelay = 35;
					ShotColor = Color.RoyalBlue;
					ShotWidth = 4.5f;
					Image = Turrets.PlasmaCannon;
					break;
				default:
					if (warn)
						MessageBox.Show(_t.ToString("Error creating turret of index #: Undefined index!"), "7 Towers TD", MessageBoxButtons.OK, MessageBoxIcon.Error);
					Damage = 0;
					Range = 0;
					CurrentDelay = 0;
					ShotDelay = 0;
					Type = -1; // Actually undefine it, otherwise this'll produce a runtime error.
					break;
			}
		}
		private double a;

		// Read/write properties
		public sbyte Type { get; set; }
		public short X { get; set; } // Note the X/Y properties mean different things to enemies.
		public short Y { get; set; }

		public short Damage { get; set; }
		public ushort Range { get; set; }
		public ushort ShotDelay { get; set; }
		public sbyte Level { get; set; }

		public ushort CurrentDelay { get; set; }

		public double get_Angle() { return a; }
		public Color ShotColor { get; set; }
		public float ShotWidth { get; set; }
		public uint ShotCount { get; set; }
		public Bitmap Image { get; private set; }

		public void set_Angle(double _a) { a = _a; }

		// Read-only properties
		public string Name
		{
			get
			{
				if (Type == -1)
					return Strings.Undefined;
				else if (Level < Config.MASTER)
					return Strings.Turret[Type];
				else
					return Strings.MasterTurret[Type];
			}
		}

		// Internal methods
		private int InternalSell(int funds)
		{
			switch (this.Type)
			{
				case 0: // MG Turret
					switch (this.Level)
					{
						case 1: return funds + Config.MG_SELL1;
						case 2: return funds + Config.MG_SELL2;
						case 3: return funds + Config.MG_SELL3;
						case 4: return funds + Config.MG_SELL4;
						case 5: return funds + Config.MG_SELL5;
						default: return funds;
					}
				case 1: // Sniper Turret
					switch (this.Level)
					{
						case 1: return funds + Config.SN_SELL1;
						case 2: return funds + Config.SN_SELL2;
						case 3: return funds + Config.SN_SELL3;
						case 4: return funds + Config.SN_SELL4;
						case 5: return funds + Config.SN_SELL5;
						default: return funds;
					}
				case 2: // Laser Turret
					switch (this.Level)
					{
						case 1: return funds + Config.LA_SELL1;
						case 2: return funds + Config.LA_SELL2;
						case 3: return funds + Config.LA_SELL3;
						case 4: return funds + Config.LA_SELL4;
						case 5: return funds + Config.LA_SELL5;
						default: return funds;
					}
				case 3: // Plasma Turret
					switch (this.Level)
					{
						case 1: return funds + Config.PL_SELL1;
						case 2: return funds + Config.PL_SELL2;
						case 3: return funds + Config.PL_SELL3;
						case 4: return funds + Config.PL_SELL4;
						case 5: return funds + Config.PL_SELL5;
						default: return funds;
					}
				default:
					return funds;
			}
		}
		// External methods
		public int Upgrade(int funds)
		{
			switch (this.Type)
			{
				case 0: // MG Turret
					switch (this.Level)
					{
						case 1:
							if (funds < Config.MG_UP1)
								return -1;
							funds -= Config.MG_UP1;
							this.Damage = 4;
							this.Range = 104;
							Level++;
							return funds;
						case 2:
							if (funds < Config.MG_UP2)
								return -1;
							funds -= Config.MG_UP2;
							this.Damage = 5;
							this.ShotDelay = 5;
							this.Range = 108;
							Level++;
							return funds;
						case 3:
							if (funds < Config.MG_UP3)
								return -1;
							funds -= Config.MG_UP3;
							this.Damage = 6;
							this.Range = 113;
							this.ShotDelay = 4;
							Level++;
							return funds;
						case 4:
						default:
							if (funds < Config.MG_UP4)
								return -1;
							funds -= Config.MG_UP4;
							this.Damage = 8;
							this.Range = 118;
							this.ShotDelay = 2;
							Level++;
							return funds;
					}
				case 1: // Sniper Turret
					switch (this.Level)
					{
						case 1:
							if (funds < Config.SN_UP1)
								return -1;
							funds -= Config.SN_UP1;
							this.Damage = 21;
							this.ShotDelay = 26;
							this.Range = 208;
							Level++;
							return funds;
						case 2:
							if (funds < Config.SN_UP2)
								return -1;
							funds -= Config.SN_UP2;
							this.Damage = 26;
							this.ShotDelay = 24;
							this.Range = 216;
							Level++;
							return funds;
						case 3:
							if (funds < Config.SN_UP3)
								return -1;
							funds -= Config.SN_UP3;
							this.Damage = 33;
							this.Range = 224;
							this.ShotDelay = 21;
							Level++;
							return funds;
						case 4:
						default:
							if (funds < Config.SN_UP4)
								return -1;
							funds -= Config.SN_UP4;
							this.Damage = 40;
							this.Range = 999;
							this.ShotDelay = 19;
							Level++;
							return funds;
					}
				case 2: // Laser Turret
					switch (this.Level)
					{
						case 1:
							if (funds < Config.LA_UP1)
								return -1;
							funds -= Config.LA_UP1;
							this.Damage = 2;
							this.Range = 123;
							Level++;
							return funds;
						case 2:
							if (funds < Config.LA_UP2)
								return -1;
							funds -= Config.LA_UP2;
							this.Damage = 3;
							this.Range = 126;
							Level++;
							return funds;
						case 3:
							if (funds < Config.LA_UP3)
								return -1;
							funds -= Config.LA_UP3;
							this.Damage = 4;
							this.Range = 130;
							Level++;
							return funds;
						case 4:
						default:
							if (funds < Config.LA_UP4)
								return -1;
							funds -= Config.LA_UP4;
							this.Damage = 2;
							this.Range = 142;
							this.ShotColor = Color.Lime;
							Level++;
							return funds;
					}
				case 3: // Plasma Turret
					switch (this.Level)
					{
						case 1:
							if (funds < Config.PL_UP1)
								return -1;
							funds -= Config.PL_UP1;
							this.Damage = 71;
							this.Range = 150;
							this.ShotDelay = 33;
							Level++;
							return funds;
						case 2:
							if (funds < Config.PL_UP2)
								return -1;
							funds -= Config.PL_UP2;
							this.Damage = 98;
							this.Range = 160;
							this.ShotDelay = 31;
							Level++;
							return funds;
						case 3:
							if (funds < Config.PL_UP3)
								return -1;
							funds -= Config.PL_UP3;
							this.Damage = 137;
							this.Range = 168;
							this.ShotDelay = 29;
							Level++;
							return funds;
						case 4:
						default:
							if (funds < Config.PL_UP4)
								return -1;
							funds -= Config.PL_UP4;
							this.Damage = 240;
							this.Range = 175;
							this.ShotDelay = 28;
							this.ShotWidth = 5;
							Level++;
							return funds;
					}
				default:
					return -1;
			}
		}

		public int Sell(int funds)
		{
			funds = InternalSell(funds);
			this.Type = -1;
			return funds;
		}

		public int SellValue => InternalSell(0);

		public int UpgradeCost
		{
			get
			{
				switch (this.Type)
				{
					case 0: // MG Turret
						switch (this.Level)
						{
							case 1: return Config.MG_UP1;
							case 2: return Config.MG_UP2;
							case 3: return Config.MG_UP3;
							case 4: return Config.MG_UP4;
							default: return 0;
						}
					case 1: // Sniper Turret
						switch (this.Level)
						{
							case 1: return Config.SN_UP1;
							case 2: return Config.SN_UP2;
							case 3: return Config.SN_UP3;
							case 4: return Config.SN_UP4;
							default: return 0;
						}
					case 2: // Laser Turret
						switch (this.Level)
						{
							case 1: return Config.LA_UP1;
							case 2: return Config.LA_UP2;
							case 3: return Config.LA_UP3;
							case 4: return Config.LA_UP4;
							default: return 0;
						}
					case 3: // Plasma Turret
						switch (this.Level)
						{
							case 1: return Config.PL_UP1;
							case 2: return Config.PL_UP2;
							case 3: return Config.PL_UP3;
							case 4: return Config.PL_UP4;
							default: return 0;
						}
					default:
						return 0;
				}
			}
		}
		public static int BuildCost(int _t)
		{
			switch (_t)
			{
				case 0: return Config.MG_COST;
				case 1: return Config.SN_COST;
				case 2: return Config.LA_COST;
				case 3: return Config.PL_COST;
				default: return 0;
			}
		}
	};
};
