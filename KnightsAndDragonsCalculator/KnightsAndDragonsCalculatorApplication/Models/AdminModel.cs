using KnightsAndDragonsCalculatorApplication.Calculator;
using KnightsAndDragonsCalculatorApplication.Calculator.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KnightsAndDragonsCalculatorApplication.Models
{
    public class AdminModel
    {
        public List<KeyValuePair<string, Element>> Elements { get; set; }
        public List<KeyValuePair<string, Rarity>> Rarities { get; set; }
    }
}