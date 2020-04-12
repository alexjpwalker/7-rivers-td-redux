using SevenRiversTD.Model;
using SevenRiversTD.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace SevenRiversTD.Data
{
	public sealed class Strings
	{
		public static void Initialize()
		{
			GameOver = new String[4];
			EnemyNames = new Dictionary<EnemyType, string>();
			Special = new String[Config.TTMAX];
			Turret = new String[Config.TTMAX];
			MasterTurret = new String[Config.TTMAX];
			for (int i = 0; i < Turret.Length; i++)
				Turret[i] = Undefined;
			for (int i = 0; i < GameOver.Length; i++)
				GameOver[i] = Undefined;
			EnemyNames[EnemyType.NORMAL] = "Normal";
			EnemyNames[EnemyType.ARMOUR] = "Armoured";
			EnemyNames[EnemyType.SPRINT] = "Sprinter";
			EnemyNames[EnemyType.SWARM] = "Swarm";
			EnemyNames[EnemyType.REGEN] = "Regenerator";
			EnemyNames[EnemyType.ERRATIC] = "Erratic";
			EnemyNames[EnemyType.BOSS] = "Armour Boss";
			EnemyNames[EnemyType.SPRINT_BOSS] = "Sprint Boss";
			EnemyNames[EnemyType.REGEN_BOSS] = "Regen Boss";
			EnemyNames[EnemyType.ERRATIC_BOSS] = "Erratic Boss";
			EnemyNames[EnemyType.DISRUPTOR] = "Disruptor";
			EnemyNames[EnemyType.ACCELERATOR] = "Accelerator";
			EnemyNames[EnemyType.COMMANDER] = "Commander";
			Turret[0] = "MG Turret"; MasterTurret[0] = "Chaingun";
			Turret[1] = "Sniper Turret"; MasterTurret[1] = "Sniper Tower";
			Turret[2] = "Laser Turret"; MasterTurret[2] = "Laser Pulser";
			Turret[3] = "Plasma Turret"; MasterTurret[3] = "Plasma Cannon";
			Special[0] = "Rapid Fire";
			Special[1] = "Infinite Range";
			Special[2] = "Multi Hit";
			Special[3] = "Plasma Burst";
			GameOver[0] = "You have killed all of the enemies.\nCongratulations, you win!";
			GameOver[1] = "You allowed too many enemies to reach your base and ran out of HP.\nYou have lost the game!";
		}

		public static Dictionary<EnemyType, string> EnemyNames { get; private set; }
		public static String[] Turret { get; private set; }
		public static String[] MasterTurret { get; private set; }
		public static String[] Special { get; private set; }
		public static string None => "None";
		public static String Undefined => "Undefined";
		public static String[] GameOver { get; private set; }
	};
};
