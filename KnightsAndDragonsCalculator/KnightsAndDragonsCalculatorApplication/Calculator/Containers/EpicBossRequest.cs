using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KnightsAndDragonsCalculatorApplication.Calculator.Containers
{
    public class EpicBossRequest
    {
        public EpicBoss EpicBoss { get; set; }
        public Guild Guild { get; set; }
        public Player Player { get; set; }
    }
}