using SevenRiversTD.Model;
using SevenRiversTD.Properties;
using System;
using System.Collections.Generic;
using System.Text;

namespace SevenRiversTD.Data
{
    public static class Waves
    {
		private static sbyte[,] p1_map1 = Map.ToPaths(Maps.map1);
		private static sbyte[,] p2_map1 = Map.ToPaths(Maps.map1, 2);
		private static sbyte[,] p1_map2 = Map.ToPaths(Maps.map2);
		private static sbyte[,] p2_map2 = Map.ToPaths(Maps.map2, 2);
		private static Wave[] ws = new Wave[Config.WMAX];
		//public static Wave * get_Wave1(){return w1;};//public static Wave * get_Wave2(){return w2;};//public static Wave * get_Wave3(){return w3;};//public static Wave * get_Wave4(){return w4;};
		public static WaveSequence get_Sequence(sbyte sn)
		{
			WaveSequence s;
			switch (sn)
			{
				case 1:
					s = new WaveSequence(20);
					break;
				case 2:
					s = new WaveSequence(30);
					break;
				default:
					sn = 1;
					s = new WaveSequence(20);
					break;
			}
			InitializeWaves(sn);
			for (int i = 0; i < ws.Length; i++)
				s.AddWave(ws[i]);
			return s;
		}

		static void InitializeWaves(sbyte sn)
		{
			switch (sn)
			{
				case 1:
					ws = new Wave[20];
					ws[00] = new Wave(count: 10, type: EnemyType.NORMAL,  HP: 8,    speed: 10, spawnTime: 30, gold: 1, paths: p1_map1);
					ws[01] = new Wave(count: 14, type: EnemyType.NORMAL,  HP: 10,   speed: 11, spawnTime: 24, gold: 1, paths: p1_map1);
					ws[02] = new Wave(count: 13, type: EnemyType.SPRINT,  HP: 11,   speed: 7,  spawnTime: 22, gold: 2, paths: p1_map1);
					ws[03] = new Wave(count: 8,  type: EnemyType.ARMOUR,  HP: 17,   speed: 16, spawnTime: 15, gold: 2, paths: p2_map1);
					ws[04] = new Wave(count: 15, type: EnemyType.NORMAL,  HP: 18,   speed: 11, spawnTime: 19, gold: 2, paths: p1_map1);
					ws[05] = new Wave(count: 32, type: EnemyType.SWARM,   HP: 5,    speed: 6,  spawnTime: 3,  gold: 1, paths: p1_map1);
					ws[06] = new Wave(count: 12, type: EnemyType.SPRINT,  HP: 19,   speed: 7,  spawnTime: 20, gold: 2, paths: p1_map1);
					ws[07] = new Wave(count: 10, type: EnemyType.ERRATIC, HP: 24,   speed: 5,  spawnTime: 10, gold: 2, paths: p1_map1);
					ws[08] = new Wave(count: 19, type: EnemyType.NORMAL,  HP: 29,   speed: 10, spawnTime: 25, gold: 2, paths: p1_map1);
					ws[09] = new Wave(count: 7,  type: EnemyType.REGEN,   HP: 22,   speed: 12, spawnTime: 14, gold: 3, paths: p2_map1);
					ws[10] = new Wave(count: 10, type: EnemyType.ARMOUR,  HP: 45,   speed: 18, spawnTime: 20, gold: 3, paths: p1_map1);
					ws[11] = new Wave(count: 14, type: EnemyType.NORMAL,  HP: 38,   speed: 10, spawnTime: 18, gold: 3, paths: p1_map1);
					ws[12] = new Wave(count: 42, type: EnemyType.SWARM,   HP: 9,    speed: 7,  spawnTime: 4,  gold: 1, paths: p1_map1);
					ws[13] = new Wave(count: 18, type: EnemyType.ERRATIC, HP: 49,   speed: 5,  spawnTime: 22, gold: 2, paths: p1_map1);
					ws[14] = new Wave(count: 15, type: EnemyType.SPRINT,  HP: 38,   speed: 6,  spawnTime: 9,  gold: 3, paths: p2_map1);
					ws[15] = new Wave(count: 9,  type: EnemyType.ARMOUR,  HP: 102,  speed: 16, spawnTime: 19, gold: 4, paths: p1_map1);
					ws[16] = new Wave(count: 16, type: EnemyType.REGEN,   HP: 47,   speed: 14, spawnTime: 30, gold: 3, paths: p1_map1);
					ws[17] = new Wave(count: 55, type: EnemyType.SWARM,   HP: 11,   speed: 6,  spawnTime: 5,  gold: 2, paths: p1_map1);
					ws[18] = new Wave(count: 6,  type: EnemyType.ARMOUR,  HP: 285,  speed: 18, spawnTime: 38, gold: 8, paths: p1_map1);
					ws[19] = new Wave(count: 1,  type: EnemyType.BOSS,    HP: 1800, speed: 26, spawnTime: 50, gold: 50, paths: p2_map1);
					break;
				case 2: // TODO: These waves and paths. Map can now be made using the Map Editor
					ws = new Wave[30];
					ws[00] = new Wave(count: 14, type: EnemyType.NORMAL, HP: 11, speed: 10, spawnTime: 30, gold: 2, paths: p1_map2);
					ws[01] = new Wave(count: 7,  type: EnemyType.ARMOUR, HP: 38, speed: 18, spawnTime: 42, gold: 4, paths: p2_map2);
					ws[02] = new Wave(count: 12, type: EnemyType.SPRINT, HP: 24, speed: 7, spawnTime: 24, gold: 3, paths: p1_map2);
					ws[03] = new Wave(count: 16, type: EnemyType.NORMAL, HP: 36, speed: 10, spawnTime: 30, gold: 4, paths: p1_map2);
					ws[04] = new Wave(count: 35, type: EnemyType.SWARM, HP: 7, speed: 6, spawnTime: 5, gold: 2, paths: p2_map2);
					ws[05] = new Wave(count: 9,  type: EnemyType.ARMOUR, HP: 102, speed: 19, spawnTime: 34, gold: 5, paths: p1_map2);
					ws[06] = new Wave(count: 1,  type: EnemyType.SPRINT_BOSS, HP: 250, speed: 7, spawnTime: 50, gold: 60, paths: p2_map2);
					ws[07] = new Wave(count: 13, type: EnemyType.NORMAL, HP: 54, speed: 10, spawnTime: 26, gold: 3, paths: p2_map2);
					ws[08] = new Wave(count: 16, type: EnemyType.REGEN, HP: 34, speed: 10, spawnTime: 19, gold: 4, paths: p1_map2);
					ws[09] = new Wave(count: 11, type: EnemyType.ARMOUR, HP: 162, speed: 17, spawnTime: 34, gold: 5, paths: p1_map2);
					ws[10] = new Wave(count: 20, type: EnemyType.ERRATIC, HP: 112, speed: 4, spawnTime: 24, gold: 3, paths: p1_map2);
					ws[11] = new Wave(count: 40, type: EnemyType.SWARM, HP: 14, speed: 5, spawnTime: 3, gold: 2, paths: p1_map2);
					ws[12] = new Wave(count: 15, type: EnemyType.NORMAL, HP: 120, speed: 9, spawnTime: 30, gold: 5, paths: p1_map2);
					ws[13] = new Wave(count: 1,  type: EnemyType.REGEN_BOSS, HP: 1000, speed: 14, spawnTime: 50, gold: 80, paths: p2_map2);
					ws[14] = new Wave(count: 12, type: EnemyType.NORMAL, HP: 140, speed: 10, spawnTime: 30, gold: 5, paths: p1_map2);
					ws[15] = new Wave(count: 9,  type: EnemyType.DISRUPTOR, HP: 142, speed: 10, spawnTime: 30, gold: 5, paths: p1_map2);
					ws[16] = new Wave(count: 14, type: EnemyType.SPRINT, HP: 162, speed: 6, spawnTime: 24, gold: 5, paths: p1_map2);
					ws[17] = new Wave(count: 7,  type: EnemyType.ARMOUR, HP: 438, speed: 18, spawnTime: 37, gold: 9, paths: p1_map2);
					ws[18] = new Wave(count: 20, type: EnemyType.ACCELERATOR, HP: 160, speed: 12, spawnTime: 20, gold: 4, paths: p1_map2);
					ws[19] = new Wave(count: 15, type: EnemyType.NORMAL, HP: 305, speed: 10, spawnTime: 25, gold: 5, paths: p2_map2);
					ws[20] = new Wave(count: 1,  type: EnemyType.ERRATIC_BOSS, HP: 2200, speed: 7, spawnTime: 50, gold: 90, paths: p1_map2);
					ws[21] = new Wave(count: 8,  type: EnemyType.NORMAL, HP: 353, speed: 10, spawnTime: 14, gold: 5, paths: p1_map2);
					ws[22] = new Wave(count: 50, type: EnemyType.SWARM, HP: 21, speed: 4, spawnTime: 2, gold: 2, paths: p1_map2);
					ws[23] = new Wave(count: 13, type: EnemyType.DISRUPTOR, HP: 331, speed: 10, spawnTime: 30, gold: 6, paths: p1_map2);
					ws[24] = new Wave(count: 16, type: EnemyType.REGEN, HP: 275, speed: 9, spawnTime: 16, gold: 5, paths: p1_map2);
					ws[25] = new Wave(count: 1,  type: EnemyType.BOSS, HP: 6500, speed: 28, spawnTime: 50, gold: 95, paths: p2_map2);
					ws[26] = new Wave(count: 14, type: EnemyType.NORMAL, HP: 525, speed: 10, spawnTime: 30, gold: 5, paths: p1_map2);
					ws[27] = new Wave(count: 25, type: EnemyType.ACCELERATOR, HP: 300, speed: 13, spawnTime: 22, gold: 5, paths: p1_map2);
					ws[28] = new Wave(count: 6,  type: EnemyType.DISRUPTOR, HP: 950, speed: 18, spawnTime: 50, gold: 18, paths: p1_map2);
					ws[29] = new Wave(count: 1,  type: EnemyType.COMMANDER, HP: 9999, speed: 32, spawnTime: 50, gold: 150, paths: p1_map2);
					break;
			}
		}
	}
}
