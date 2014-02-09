using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace KnightsAndDragonsCalculator.Models
{
    [DataContract]
    public class CalculatorResults
    {
        [DataMember]
        public int FeedCost { get; set; }
        [DataMember]
        public int CraftCost { get; set; }
        [DataMember]
        public int FeedCount { get; set; }
        [DataMember]
        public int MaterialCount { get; set; }
        [DataMember]
        public int NormalAttack { get; set; }
        [DataMember]
        public int NormalDefense { get; set; }
        [DataMember]
        public int PlusAttack { get; set; }
        [DataMember]
        public int PlusDefense { get; set; }
        [DataMember]
        public string ErrorMessage { get; set; }

        public CalculatorResults() {}

        public CalculatorResults(int normalAttack, int normalDefense, int plusAttack, int plusDefense)
            : this()
        {
            NormalAttack = normalAttack;
            NormalDefense = normalDefense;
            PlusAttack = plusAttack;
            PlusDefense = plusDefense;
        }

        public CalculatorResults(int normalAttack, int normalDefense, int plusAttack, int plusDefense, int feedCost, int craftCost, int feedCount)
            : this(normalAttack, normalDefense, plusAttack, plusDefense)
        {
            FeedCost = feedCost;
            CraftCost = craftCost;
            FeedCount = feedCount;
        }

        public CalculatorResults(string errorMessage)
            : this()
        {
            ErrorMessage = errorMessage;
        }
    }
}