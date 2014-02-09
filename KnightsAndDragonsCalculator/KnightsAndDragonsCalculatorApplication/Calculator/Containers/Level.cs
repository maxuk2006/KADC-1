using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace KnightsAndDragonsCalculatorApplication.Calculator.Containers
{
    public class Level
    {
        public int Gold { get; set; }
        public int Jump30 { get; set; }
        public int Jump50 { get; set; }
        public int Jump70 { get; set; }
        public int Jump99 { get; set; }

        public Level() { }

        public Level(int gold, int jump30, int jump50, int jump70, int jump99)
        {
            Gold = gold;
            Jump30 = jump30;
            Jump50 = jump50;
            Jump70 = jump70;
            Jump99 = jump99;
        }

        public int GetJump(int maxLevel)
        {
            switch (maxLevel)
            {
                case 30:
                    return Jump30;
                case 50:
                    return Jump50;
                case 70:
                    return Jump70;
                case 99:
                    return Jump99;
            }
            return 0;
        }
    }
}