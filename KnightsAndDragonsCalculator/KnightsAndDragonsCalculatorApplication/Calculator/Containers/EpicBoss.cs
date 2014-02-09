using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KnightsAndDragonsCalculatorApplication.Calculator.Containers
{
    public class EpicBoss
    {
        public int Level { get; set; }
        public Element Element1 { get; set; }
        public Element? Element2 { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Health { get; set; }
    }
}
