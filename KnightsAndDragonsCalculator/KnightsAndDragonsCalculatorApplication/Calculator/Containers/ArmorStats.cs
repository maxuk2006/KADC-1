using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace KnightsAndDragonsCalculatorApplication.Calculator.Containers
{
    public class ArmorStats
    {
        public int AttackStart { get; set; }
        public int AttackUp { get; set; }
        public int DefenseStart { get; set; }
        public int DefenseUp { get; set; }

        public ArmorStats() { }

        public ArmorStats(int attackStart, int attackUp, int defenseStart, int defenseUp)
        {
            AttackStart = attackStart;
            AttackUp = attackUp;
            DefenseStart = defenseStart;
            DefenseUp = defenseUp;
        }
    }
}