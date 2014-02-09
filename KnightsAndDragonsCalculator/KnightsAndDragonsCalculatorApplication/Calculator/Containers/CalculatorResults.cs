using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace KnightsAndDragonsCalculatorApplication.Calculator.Containers
{
    public class CalculatorResults
    {
        public StatsResults Stats { get; set; }
        public FeedResults Feed { get; set; }
        public FusionResults Fusion { get; set; }
        public EpicBossResults EpicBoss { get; set; }
        public string ErrorMessage { get; set; }

        public CalculatorResults() {}

        public CalculatorResults(StatsResults stats)
            : this()
        {
            Stats = stats;
        }

        public CalculatorResults(FeedResults feed)
            : this()
        {
            Feed = feed;
        }

        public CalculatorResults(FusionResults fusion)
            : this()
        {
            Fusion = fusion;
        }

        public CalculatorResults(StatsResults stats, FeedResults feed)
            : this(stats)
        {
            Feed = feed;
        }

        public CalculatorResults(EpicBossResults epicBoss)
            : this()
        {
            EpicBoss = epicBoss;
        }

        public CalculatorResults(string errorMessage)
            : this()
        {
            ErrorMessage = errorMessage;
        }
    }
}