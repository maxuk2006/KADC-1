using KnightsAndDragonsCalculatorApplication.Calculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KnightsAndDragonsCalculatorApplication.Models
{
    public class EnhancementModel
    {
        public List<string> TargetArmorNames { get; set; }
        public List<string> FeederArmorNames { get; set; }
        public List<KeyValuePair<string, int>> TargetArmorMaxLevels { get; set; }
        public List<KeyValuePair<string, int>> BaseFeedCosts { get; set; }
    }
}