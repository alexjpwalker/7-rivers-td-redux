namespace SevenRiversTD.Properties
{
    public static class Config
    {
        public const int GS = 32; // Grid size
        public const int AS = 736; // Area size
        public const double FD = 0.4; // Opening/exit fading effect distance
        public const double FDL = 0.37; // Fading effect gradient limiter
        public const double GSM = 0.64; // Grid size fading effect modifier
        public const int TMAX = 32; // Turret limit
        public const int TTMAX = 10; // Turret type limit
        public const int UCMAX = 800; // Upgrade cost limit
        public const sbyte CHARRED = -9; // Charred tile index
        public const int STD_TLIM = 10; // Highest positive tile index
        public const int FAST = 25; // Game loop interval on "Fast" setting
        public const int VICTORY = 0; // Victory message index
        public const int DEFEAT = 1; // Defeat message index
        public const int INIT_HP = 20; // Initial HP
        public const int INIT_GOLD = 60; // Initia-- Well, what do you think?
        public const int INIT_WAVE = 1; // No comment. (Useful for high-end wave testing.)
        public const int MASTER = 5; // Highest level a turret can reach
        public const int PA_MIN = 5; // Minimal path length (contains only start and end points, and initial direction)
        public const int PA_MAX = 50; // Maximum path length
        public const int PA_CMAX = 10; // Maximum path count
        public const int INFO_Y = 404; // Y-coordinate of information box on Control Panel
        public const int RELAX = 70; // "Relax time" between waves
        public const int SSMAX = 1024; // Maximum number of substrings an input file can be split into
        public const int MLMAX = 1024; // Maximum map/path file length
        public const int MPL = 48; // Maximum path length
        public const int MPC = 10; // Maximum path count

        // --- Enemy Traits and Modifiers ---
        public const int BOSS_DAMAGE = 5; // Damage dealt by any boss
        public const int EMAX = 400; // Enemy limit
        public const int ETMAX = 11; // Enemy type limit
        public const int WMAX = 150; // Wave limit
        public const int IMAX = 8; // Enemy image limit
        public const int ERR_RATE = 8; // Erratic enemy constant

        // --- Turret Build Costs, Upgrade Costs and Sale Values ---
        public const int MG_COST = 50;
        public const int MG_UP1 = 15;
        public const int MG_UP2 = 25;
        public const int MG_UP3 = 35;
        public const int MG_UP4 = 155;
        public const int MG_SELL1 = 45;
        public const int MG_SELL2 = 58;
        public const int MG_SELL3 = 81;
        public const int MG_SELL4 = 112;
        public const int MG_SELL5 = 252;
        public const int SN_COST = 80;
        public const int SN_UP1 = 30;
        public const int SN_UP2 = 55;
        public const int SN_UP3 = 100;
        public const int SN_UP4 = 170;
        public const int SN_SELL1 = 72;
        public const int SN_SELL2 = 99;
        public const int SN_SELL3 = 148;
        public const int SN_SELL4 = 238;
        public const int SN_SELL5 = 391;
        public const int LA_COST = 110;
        public const int LA_UP1 = 105;
        public const int LA_UP2 = 100;
        public const int LA_UP3 = 95;
        public const int LA_UP4 = 190;
        public const int LA_SELL1 = 99;
        public const int LA_SELL2 = 193;
        public const int LA_SELL3 = 283;
        public const int LA_SELL4 = 369;
        public const int LA_SELL5 = 540;
        public const int PL_COST = 150;
        public const int PL_UP1 = 85;
        public const int PL_UP2 = 100;
        public const int PL_UP3 = 140;
        public const int PL_UP4 = 325;
        public const int PL_SELL1 = 135;
        public const int PL_SELL2 = 211;
        public const int PL_SELL3 = 301;
        public const int PL_SELL4 = 429;
        public const int PL_SELL5 = 720;
    }
}
