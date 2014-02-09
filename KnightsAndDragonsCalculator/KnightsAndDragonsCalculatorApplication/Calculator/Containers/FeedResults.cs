using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KnightsAndDragonsCalculatorApplication.Calculator.Containers
{
    public class FeedResults
    {
        public int FeedCost { get; set; }
        public int FeedCount { get; set; }
        public int CraftCost { get; set; }
        public int MaterialCount { get; set; }
        public decimal CraftTime { get; set; }
        public string CraftTimeDescription { 
            get {
                string desc = string.Empty;
                int minutes = (int)CraftTime;
                int seconds = (int)Math.Ceiling((CraftTime - minutes) * 60);

                TimeSpan ts = new TimeSpan(0, minutes, seconds);
                if ((int)ts.TotalHours > 0)
                {
                    desc += (int)ts.TotalHours + "hr" + ((ts.TotalHours > 1) ? "s " : " ");
                }
                if (ts.Minutes > 0)
                {
                    desc += ts.Minutes + "m ";
                }
                if (ts.Seconds > 0)
                {
                    desc += ts.Seconds + "s ";
                }
                return desc; 
            } 
        }
        public int FusionCost { get; set; }
        public int FusionCount { get; set; }
        public int TotalCost { get { return FeedCost + CraftCost + FusionCost; } }
    }
}