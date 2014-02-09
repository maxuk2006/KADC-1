using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KnightsAndDragonsCalculatorApplication.Calculator.Containers
{
    public class Player
    {
        public int Level { get; set; }
        public int KnightCount { get; set; }
        public List<PlayerArmor> Armors { get; set; }
    }
}
