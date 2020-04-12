using SevenRiversTD.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SevenRiversTD.Data
{
    public static class Enemies
    {
        public static void Initialize()
        {
            ColorCode = new Dictionary<EnemyType, Color>();
            ColorCode[EnemyType.NORMAL] = Color.LightGray;
            ColorCode[EnemyType.ARMOUR] = Color.MediumPurple;
            ColorCode[EnemyType.SPRINT] = Color.YellowGreen;
            ColorCode[EnemyType.SWARM] = Color.Brown;
            ColorCode[EnemyType.REGEN] = Color.Orange;
            ColorCode[EnemyType.ERRATIC] = Color.SkyBlue;
            ColorCode[EnemyType.BOSS] = Color.Red;
            ColorCode[EnemyType.SPRINT_BOSS] = Color.Lime;
            ColorCode[EnemyType.REGEN_BOSS] = Color.Yellow;
            ColorCode[EnemyType.ERRATIC_BOSS] = Color.Aqua;
            ColorCode[EnemyType.DISRUPTOR] = Color.LightYellow;
            ColorCode[EnemyType.ACCELERATOR] = Color.PaleVioletRed;
            ColorCode[EnemyType.COMMANDER] = Color.RoyalBlue;
            DefaultColorCode = Color.LightGray;
        }

        public static Dictionary<EnemyType, Color> ColorCode { get; private set; }
        public static Color DefaultColorCode { get; private set; }
    }
}
