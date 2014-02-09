using KnightsAndDragonsCalculatorApplication.Calculator.Containers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace KnightsAndDragonsCalculatorApplication.Calculator.Tables
{
    public sealed class LevelTable
    {
        private static volatile LevelTable instance;
        private static object syncRoot = new Object();

        private List<Level> _levels;

        private LevelTable()
        {
            Initialize();
        }

        public static LevelTable Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new LevelTable();
                    }
                }

                return instance;
            }
        }

        public List<Level> GetLevels()
        {
            return _levels;
        }

        public Level GetLevel(int level)
        {
            if (level <= 0 || level > _levels.Count) return null;

            return _levels[level - 1];
        }

        public int GetTotalFeedCost(int maxLevel, int startLevel, int targetLevel)
        {
            int total = 0;
            for (int i = startLevel + 1; i <= targetLevel; i++)
            {
                Level level = GetLevel(i);
                if (level == null)
                {
                    return total;
                }
                total += level.GetJump(maxLevel);
            }
            return total;
        }

        public int GetLevelFromExperience(int maxLevel, int experience)
        {
            int e = experience;
            for (int i = 0; i < _levels.Count; i++)
            {
                Level level = _levels[i];
                e -= level.GetJump(maxLevel);
                if (e < 0) return i;
                if (e == 0) return i + 1;
            }
            return _levels.Count;
        }

        public int GetExperienceFromLevel(int maxLevel, int targetLevel)
        {
            int experience = 0;
            for (int i = 0; i < targetLevel; i++)
            {
                Level level = _levels[i];
                experience += level.GetJump(maxLevel);
            }
            return experience;
        }

        private void Initialize()
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<Level>));
            using (var fs = new FileStream(HttpContext.Current.Server.MapPath(Strings.FileNameLevels), FileMode.Open))
            {
                _levels = xs.Deserialize(fs) as List<Level>;
            }
        }

        #region Old Initialization

        private void InitializeOld()
        {
            _levels = new List<Level>()
                {
                    new Level(150, 0, 0, 0, 0),
                    new Level(200, 4, 6, 9, 9),
                    new Level(250, 4, 10, 12, 12),
                    new Level(300, 4, 15, 15, 15),
                    new Level(350, 4, 15, 18, 18),
                    new Level(400, 4, 20, 21, 21),
                    new Level(450, 5, 20, 24, 24),
                    new Level(500, 5, 24, 27, 27),
                    new Level(600, 5, 24, 30, 30),
                    new Level(700, 5, 25, 33, 33),
                    new Level(800, 5, 25, 36, 36),
                    new Level(900, 6, 26, 39, 39),
                    new Level(1000, 6, 26, 42, 42),
                    new Level(1100, 6, 27, 45, 45),
                    new Level(1200, 6, 28, 47, 47),
                    new Level(1300, 6, 29, 48, 48),
                    new Level(1400, 7, 29, 50, 50),
                    new Level(1500, 7, 30, 51, 51),
                    new Level(1600, 7, 30, 53, 53),
                    new Level(1700, 7, 31, 55, 55),
                    new Level(1800, 8, 32, 56, 56),
                    new Level(1900, 8, 32, 58, 58),
                    new Level(2000, 8, 33, 59, 59),
                    new Level(2250, 9, 34, 61, 61),
                    new Level(2500, 9, 34, 63, 63),
                    new Level(2750, 9, 35, 64, 64),
                    new Level(3000, 10, 35, 66, 66),
                    new Level(3250, 10, 35, 67, 67),
                    new Level(3500, 10, 35, 69, 69),
                    new Level(3750, 10, 35, 70, 70),
                    new Level(4000, 0, 36, 71, 71),
                    new Level(4250, 0, 36, 73, 73),
                    new Level(4500, 0, 36, 74, 74),
                    new Level(5000, 0, 36, 75, 75),
                    new Level(5500, 0, 36, 76, 76),
                    new Level(6000, 0, 37, 77, 77),
                    new Level(6500, 0, 37, 78, 78),
                    new Level(7000, 0, 37, 79, 79),
                    new Level(7500, 0, 38, 80, 80),
                    new Level(8000, 0, 38, 81, 81),
                    new Level(8500, 0, 38, 82, 82),
                    new Level(9000, 0, 38, 83, 83),
                    new Level(9500, 0, 39, 85, 85),
                    new Level(10000, 0, 39, 86, 86),
                    new Level(10500, 0, 39, 87, 87),
                    new Level(11000, 0, 40, 88, 88),
                    new Level(11500, 0, 40, 89, 89),
                    new Level(12000, 0, 40, 90, 90),
                    new Level(12500, 0, 40, 91, 91),
                    new Level(13000, 0, 40, 92, 92),
                    new Level(13500, 0, 0, 93, 93),
                    new Level(14000, 0, 0, 95, 95),
                    new Level(14500, 0, 0, 97, 97),
                    new Level(15000, 0, 0, 98, 98),
                    new Level(15500, 0, 0, 99, 99),
                    new Level(16000, 0, 0, 100, 100),
                    new Level(16500, 0, 0, 101, 101),
                    new Level(17000, 0, 0, 102, 102),
                    new Level(17500, 0, 0, 103, 103),
                    new Level(18000, 0, 0, 104, 104),
                    new Level(18500, 0, 0, 106, 106),
                    new Level(19000, 0, 0, 109, 109),
                    new Level(19500, 0, 0, 110, 110),
                    new Level(20000, 0, 0, 111, 111),
                    new Level(21000, 0, 0, 112, 112),
                    new Level(22000, 0, 0, 113, 113),
                    new Level(23000, 0, 0, 114, 114),
                    new Level(24000, 0, 0, 115, 115),
                    new Level(25000, 0, 0, 116, 116),
                    new Level(26000, 0, 0, 117, 117),
                    new Level(27000, 0, 0, 0, 120),
                    new Level(28000, 0, 0, 0, 122),
                    new Level(29000, 0, 0, 0, 124),
                    new Level(30000, 0, 0, 0, 124),
                    new Level(30000, 0, 0, 0, 126),
                    new Level(30000, 0, 0, 0, 126),
                    new Level(30000, 0, 0, 0, 128),
                    new Level(30000, 0, 0, 0, 128),
                    new Level(30000, 0, 0, 0, 130),
                    new Level(30000, 0, 0, 0, 130),
                    new Level(30000, 0, 0, 0, 132),
                    new Level(30000, 0, 0, 0, 132),
                    new Level(30000, 0, 0, 0, 134),
                    new Level(30000, 0, 0, 0, 134),
                    new Level(30000, 0, 0, 0, 136),
                    new Level(30000, 0, 0, 0, 138),
                    new Level(30000, 0, 0, 0, 138),
                    new Level(30000, 0, 0, 0, 138),
                    new Level(30000, 0, 0, 0, 140),
                    new Level(30000, 0, 0, 0, 140),
                    new Level(30000, 0, 0, 0, 140),
                    new Level(30000, 0, 0, 0, 140),
                    new Level(30000, 0, 0, 0, 140),
                    new Level(30000, 0, 0, 0, 145),
                    new Level(30000, 0, 0, 0, 145),
                    new Level(30000, 0, 0, 0, 150),
                    new Level(30000, 0, 0, 0, 155),
                    new Level(30000, 0, 0, 0, 155),
                    new Level(30000, 0, 0, 0, 160)
                };


            XmlSerializer xs = new XmlSerializer(typeof(List<Level>));
            using (var fs = new FileStream(@"d:\levels.xml", FileMode.Create))
            {
                xs.Serialize(fs, _levels);
            }
        }

        #endregion
    }
}