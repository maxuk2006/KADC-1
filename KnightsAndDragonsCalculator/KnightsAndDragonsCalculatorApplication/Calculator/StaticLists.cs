using KnightsAndDragonsCalculatorApplication.Calculator.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KnightsAndDragonsCalculatorApplication.Calculator
{
    public static class StaticLists
    {
        public static List<KeyValuePair<string, int>> GetTargetArmorMaxLevels()
        {
            List<KeyValuePair<string, int>> rarities = new List<KeyValuePair<string, int>>();
            rarities.Add(new KeyValuePair<string, int>(string.Format("{0}/{1}/Old {2}", Strings.RarityCommon, Strings.RarityUncommon, Strings.RarityNemesis), 30));
            rarities.Add(new KeyValuePair<string, int>(string.Format("{0}/{1}/New {2}", Strings.RarityRare, Strings.RaritySuperRare, Strings.RarityNemesis), 50));
            rarities.Add(new KeyValuePair<string, int>(string.Format("{0}/{1}", Strings.RarityUltraRare, Strings.RarityLegendary), 70));
            rarities.Add(new KeyValuePair<string, int>(Strings.RarityEpic, 99));
            return rarities;
        }

        public static List<KeyValuePair<string, int>> GetBaseFeedCosts()
        {
            List<KeyValuePair<string, int>> costs = new List<KeyValuePair<string, int>>();
            costs.Add(new KeyValuePair<string, int>(Strings.RarityCommon, 5));
            costs.Add(new KeyValuePair<string, int>(Strings.RarityUncommon, 8));
            costs.Add(new KeyValuePair<string, int>(string.Format("{0} {1}", Strings.TypeCraftable, Strings.RarityRare), 20));
            costs.Add(new KeyValuePair<string, int>(string.Format("{0} {1}/{2}", Strings.TypeNonCraftable, Strings.RarityRare, Strings.RaritySuperRare), 40));
            costs.Add(new KeyValuePair<string, int>(string.Format("{0}/{1}", Strings.RarityUltraRare, Strings.RarityLegendary), 72));
            costs.Add(new KeyValuePair<string, int>(string.Format("{0}/{1}", Strings.RarityEpic, Strings.RarityFusionBoost), 150));
            return costs;
        }

        public static List<KeyValuePair<string, Element>> GetElements()
        {
            List<KeyValuePair<string, Element>> elements = new List<KeyValuePair<string, Element>>();
            elements.Add(new KeyValuePair<string, Element>(Strings.ElementAir, Element.Air));
            elements.Add(new KeyValuePair<string, Element>(Strings.ElementEarth, Element.Earth));
            elements.Add(new KeyValuePair<string, Element>(Strings.ElementFire, Element.Fire));
            elements.Add(new KeyValuePair<string, Element>(Strings.ElementSpirit, Element.Spirit));
            elements.Add(new KeyValuePair<string, Element>(Strings.ElementWater, Element.Water));
            return elements;
        }

        public static List<KeyValuePair<string, Element>> GetElementsIncludingAll()
        {
            List<KeyValuePair<string, Element>> elements = GetElements();
            elements.Add(new KeyValuePair<string, Element>(Strings.ElementAll, Element.All));
            return elements;
        }

        public static List<KeyValuePair<string, Rarity>> GetRarities()
        {
            List<KeyValuePair<string, Rarity>> rarities = new List<KeyValuePair<string, Rarity>>();
            rarities.Add(new KeyValuePair<string, Rarity>(Strings.RarityCommon, Rarity.Common));
            rarities.Add(new KeyValuePair<string, Rarity>(Strings.RarityUncommon, Rarity.Uncommon));
            rarities.Add(new KeyValuePair<string, Rarity>(Strings.RarityRare, Rarity.Rare));
            rarities.Add(new KeyValuePair<string, Rarity>(Strings.RaritySuperRare, Rarity.SuperRare));
            rarities.Add(new KeyValuePair<string, Rarity>(Strings.RarityUltraRare, Rarity.UltraRare));
            rarities.Add(new KeyValuePair<string, Rarity>(Strings.RarityLegendary, Rarity.Legendary));
            rarities.Add(new KeyValuePair<string, Rarity>(Strings.RarityEpic, Rarity.Epic));
            rarities.Add(new KeyValuePair<string, Rarity>(Strings.RarityNemesis, Rarity.Nemesis));
            rarities.Add(new KeyValuePair<string, Rarity>(Strings.RarityFusionBoost, Rarity.FusionBoost));
            return rarities;
        }

        public static List<KeyValuePair<string, int>> GetGuildRanks()
        {
            List<KeyValuePair<string, int>> costs = new List<KeyValuePair<string, int>>();
            costs.Add(new KeyValuePair<string, int>(Strings.GuildRankCommander, 0));
            costs.Add(new KeyValuePair<string, int>(Strings.GuildRankHighCommander, 5));
            costs.Add(new KeyValuePair<string, int>(Strings.GuildRankChampion, 7));
            costs.Add(new KeyValuePair<string, int>(Strings.GuildRankSentinel, 7));
            costs.Add(new KeyValuePair<string, int>(Strings.GuildRankMaster, 10));
            return costs;
        }
    }
}