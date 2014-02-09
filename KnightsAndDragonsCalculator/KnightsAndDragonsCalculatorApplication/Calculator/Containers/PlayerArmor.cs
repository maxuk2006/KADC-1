using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KnightsAndDragonsCalculatorApplication.Calculator.Containers
{
    public class PlayerArmor
    {
        public string ArmorName { get; set; }
        public int Level { get; set; }
        public bool IsPlus { get; set; }
        public bool IsNemesis { get; set; }
    }
}