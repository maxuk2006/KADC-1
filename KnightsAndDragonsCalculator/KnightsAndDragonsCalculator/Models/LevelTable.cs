using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KnightsAndDragonsCalculator.Models
{
    public class LevelTable
    {
        public static List<Level> _levels;

        public static void Initialize()
        {
            _levels = GetLevels();
        }

        public static Level GetLevel(int level)
        {
            if (level <= 0 || level > _levels.Count) return null;

            return _levels[level - 1];
        }

        public static int GetTotalFeedCost(Rarity rarity, int targetLevel)
        {
            int total = 0;
            for (int i = 1; i <= targetLevel; i++)
            {
                Level level = GetLevel(i);
                if (level == null)
                {
                    return total;
                }
                total += level.GetJump(rarity);
            }
            return total;
        }

        public static int GetLevelFromExperience(Rarity rarity, int experience)
        {
            int e = experience;
            for (int i = 0; i < _levels.Count; i++)
            {
                Level level = _levels[i];
                e -= level.GetJump(rarity);
                if (e < 0) return i;
                if (e == 0) return i + 1;
            }
            return _levels.Count;
        }

        private static List<Level> GetLevels()
        {
            return new List<Level>()
                {
                    new Level(150, 0, 0, 0, 0, 0, 0, 0),
                    new Level(200, 0, 0, 0, 0, 9, 9, 0),
                    new Level(250, 0, 0, 0, 0, 12, 12, 0),
                    new Level(300, 0, 0, 0, 0, 15, 15, 0),
                    new Level(350, 0, 0, 0, 0, 18, 18, 0),
                    new Level(400, 0, 0, 0, 0, 21, 21, 0),
                    new Level(450, 0, 0, 0, 0, 24, 24, 0),
                    new Level(500, 0, 0, 0, 0, 27, 27, 0),
                    new Level(600, 0, 0, 0, 0, 30, 30, 0),
                    new Level(700, 0, 0, 0, 0, 33, 33, 0),
                    new Level(800, 0, 0, 0, 0, 36, 36, 0),
                    new Level(900, 0, 0, 0, 0, 39, 39, 0),
                    new Level(1000, 0, 0, 0, 0, 42, 42, 0),
                    new Level(1100, 0, 0, 0, 0, 45, 45, 0),
                    new Level(1200, 0, 0, 0, 0, 47, 47, 0),
                    new Level(1300, 0, 0, 0, 0, 48, 48, 0),
                    new Level(1400, 0, 0, 0, 0, 50, 50, 0),
                    new Level(1500, 0, 0, 0, 0, 51, 51, 0),
                    new Level(1600, 0, 0, 0, 0, 53, 53, 0),
                    new Level(1700, 0, 0, 0, 0, 55, 55, 0),
                    new Level(1800, 0, 0, 0, 0, 56, 56, 0),
                    new Level(1900, 0, 0, 0, 0, 58, 58, 0),
                    new Level(2000, 0, 0, 0, 0, 59, 59, 0),
                    new Level(2250, 0, 0, 0, 0, 61, 61, 0),
                    new Level(2500, 0, 0, 0, 0, 63, 63, 0),
                    new Level(2750, 0, 0, 0, 0, 64, 64, 0),
                    new Level(3000, 0, 0, 0, 0, 66, 66, 0),
                    new Level(3250, 0, 0, 0, 0, 67, 67, 0),
                    new Level(3500, 0, 0, 0, 0, 69, 69, 0),
                    new Level(3750, 0, 0, 0, 0, 70, 70, 0),
                    new Level(4000, 0, 0, 0, 0, 71, 71, 0),
                    new Level(4250, 0, 0, 0, 0, 73, 73, 0),
                    new Level(4500, 0, 0, 0, 0, 74, 74, 0),
                    new Level(5000, 0, 0, 0, 0, 75, 75, 0),
                    new Level(5500, 0, 0, 0, 0, 76, 76, 0),
                    new Level(6000, 0, 0, 0, 0, 77, 77, 0),
                    new Level(6500, 0, 0, 0, 0, 78, 78, 0),
                    new Level(7000, 0, 0, 0, 0, 79, 79, 0),
                    new Level(7500, 0, 0, 0, 0, 80, 80, 0),
                    new Level(8000, 0, 0, 0, 0, 81, 81, 0),
                    new Level(8500, 0, 0, 0, 0, 82, 82, 0),
                    new Level(9000, 0, 0, 0, 0, 84, 84, 0),
                    new Level(9500, 0, 0, 0, 0, 85, 85, 0),
                    new Level(10000, 0, 0, 0, 0, 87, 87, 0),
                    new Level(10500, 0, 0, 0, 0, 90, 90, 0),
                    new Level(11000, 0, 0, 0, 0, 90, 90, 0),
                    new Level(11500, 0, 0, 0, 0, 90, 90, 0),
                    new Level(12000, 0, 0, 0, 0, 94, 94, 0),
                    new Level(12500, 0, 0, 0, 0, 94, 94, 0),
                    new Level(13000, 0, 0, 0, 0, 94, 94, 0),
                    new Level(13500, 0, 0, 0, 0, 94, 94, 0),
                    new Level(14000, 0, 0, 0, 0, 0, 0, 0),
                    new Level(14500, 0, 0, 0, 0, 0, 0, 0),
                    new Level(15000, 0, 0, 0, 0, 0, 0, 0),
                    new Level(15500, 0, 0, 0, 0, 0, 0, 0),
                    new Level(16000, 0, 0, 0, 0, 0, 0, 0),
                    new Level(16500, 0, 0, 0, 0, 0, 0, 0),
                    new Level(17000, 0, 0, 0, 0, 0, 0, 0),
                    new Level(17500, 0, 0, 0, 0, 0, 0, 0),
                    new Level(18000, 0, 0, 0, 0, 0, 0, 0),
                    new Level(18500, 0, 0, 0, 0, 0, 0, 0),
                    new Level(19000, 0, 0, 0, 0, 0, 0, 0),
                    new Level(19500, 0, 0, 0, 0, 0, 0, 0),
                    new Level(20000, 0, 0, 0, 0, 0, 0, 0),
                    new Level(21000, 0, 0, 0, 0, 0, 0, 0),
                    new Level(22000, 0, 0, 0, 0, 0, 0, 0),
                    new Level(23000, 0, 0, 0, 0, 0, 0, 0),
                    new Level(24000, 0, 0, 0, 0, 0, 0, 0),
                    new Level(25000, 0, 0, 0, 0, 0, 0, 0)
                };
        }
    }
}