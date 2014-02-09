using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Web;

namespace KnightsAndDragonsCalculatorApplication.Calculator.Containers
{
    public enum Rarity 
    {
        Common,
        Uncommon,
        Rare,
        FusionBoost,
        SuperRare,
        UltraRare,
        Legendary,
        Epic,
        Nemesis
    }

    public enum Element
    {
        Fire,
        Water,
        Air,
        Earth,
        Spirit,
        All
    }

    public class Armor
    {
        public string Name { get; set; }
        public Rarity Rarity { get; set; }
        public Element Element1 { get; set; }
        public Element? Element2 { get; set; }
        public bool IsCraftable { get; set; }
        public int MaxLevel { get; set; }
        public int PlusLevel { get; set; }
        public int FeedCost { get; set; }
        public int CraftCost { get; set; }
        public int MaterialCount { get; set; }
        public int CraftTime { get; set; }
        public bool IsFusionResult { get; set; }
        public ArmorStats NormalStats { get; set; }
        public ArmorStats PlusStats { get; set; }

        public string RarityDescription { get { return GetRarityDescription(Rarity); } }
        public int RaritySortOrder { get { return (int)Rarity; } }
        public string Element1Description { get { return GetElementDescription(Element1); } }
        public string Element2Description { get { return GetElementDescription(Element2); } }
        public string ElementDescription { 
            get {
                string description1 = Element1Description;
                string description2 = Element2Description;
                if (string.IsNullOrEmpty(description2))
                {
                    return description1;
                }
                return description1 + "/" + description2;
            } 
        }
        public int MaxNormalAttack { get { return GetNormalAttackAt(MaxLevel); } }
        public int MaxNormalDefense { get { return GetNormalDefenseAt(MaxLevel); } }
        public int MaxNormalTotal { get { return MaxNormalAttack + MaxNormalDefense; } }
        public int MaxPlusAttack { get { return GetPlusAttackAt(MaxLevel); } }
        public int MaxPlusDefense { get { return GetPlusDefenseAt(MaxLevel); } }
        public int MaxPlusTotal { get { return MaxPlusAttack + MaxPlusDefense; } }
        public string SafeName { get { return Regex.Replace(Name, "[^a-zA-Z]+", "", RegexOptions.Compiled); } }
        public string SafeRarityDescription { get { return Regex.Replace(RarityDescription, "[^a-zA-Z]+", "", RegexOptions.Compiled); } }

        public Armor() { }

        public Armor(string name, Rarity rarity, bool isCraftable, int maxLevel, int plusLevel, int feedCost, int craftCost, int materialCount, int craftTime, bool isFusionResult, ArmorStats normalStats, ArmorStats plusStats, Element element1)
        {
            Name = name;
            Rarity = rarity;
            IsCraftable = isCraftable;
            MaxLevel = maxLevel;
            PlusLevel = plusLevel;
            FeedCost = feedCost;
            CraftCost = craftCost;
            MaterialCount = materialCount;
            CraftTime = craftTime;
            IsFusionResult = isFusionResult;
            NormalStats = normalStats;
            PlusStats = plusStats;
            Element1 = element1;
        }

        public Armor(string name, Rarity rarity, bool isCraftable, int maxLevel, int plusLevel, int feedCost, int craftCost, int materialCount, int craftTime, bool isFusionResult, ArmorStats normalStats, ArmorStats plusStats, Element element1, Element? element2)
            : this(name, rarity, isCraftable, maxLevel, plusLevel, feedCost, craftCost, materialCount, craftTime, isFusionResult, normalStats, plusStats, element1)
        {
            Element2 = element2;
        }

        public bool SameElementAs(Armor armor)
        {
            return (
                armor.Element1 == Element.All || 
                Element1 == Element.All ||
                armor.Element1 == Element1 || 
                (Element2 != null && armor.Element1 == Element2.Value) || 
                (armor.Element2 != null && armor.Element2.Value == Element1) ||
                (Element2 != null && armor.Element2 != null && armor.Element2.Value == Element2.Value)
            );
        }

        public bool HasElement(Element element)
        {
            return element == Element1 || (Element2 != null && element == Element2.Value);
        }

        public bool IsStrongAgainst(Element element)
        {
            switch (element)
            {
                case Element.Fire:
                    return HasElement(Element.Water);
                case Element.Spirit:
                    return HasElement(Element.Fire);
                case Element.Earth:
                    return HasElement(Element.Spirit);
                case Element.Air:
                    return HasElement(Element.Earth);
                case Element.Water:
                    return HasElement(Element.Air);
                case Element.All:
                    return true;
                default:
                    return false;
            }
        }

        public bool IsWeakAgainst(Element element)
        {
            switch (element)
            {
                case Element.Fire:
                    return HasElement(Element.Spirit);
                case Element.Spirit:
                    return HasElement(Element.Earth);
                case Element.Earth:
                    return HasElement(Element.Air);
                case Element.Air:
                    return HasElement(Element.Water);
                case Element.Water:
                    return HasElement(Element.Fire);
                case Element.All:
                default:
                    return false;
            }
        }

        public bool IsPossibleFusionResultByElement(Armor armor1, Armor armor2)
        {
            if (Element2 == null) return (SameElementAs(armor1) || SameElementAs(armor2));

            return ((armor1.HasElement(Element1) && !armor1.HasElement(Element2.Value) && !armor2.HasElement(Element1) && armor2.HasElement(Element2.Value)) ||
                    (!armor1.HasElement(Element1) && armor1.HasElement(Element2.Value) && armor2.HasElement(Element1) && !armor2.HasElement(Element2.Value)));
        }

        public bool IsPossibleFusableResultByElement(Armor targetArmor)
        {
            if (targetArmor.Element2 == null) return HasElement(targetArmor.Element1);

            return HasElement(targetArmor.Element1) ^ HasElement(targetArmor.Element2.Value);
        }

        public int GetNormalAttackAt(int level)
        {
            if (NormalStats != null)
            {
                return GetStatAtLevel(NormalStats.AttackStart, NormalStats.AttackUp, level);
            }
            return 0;
        }

        public int GetNormalDefenseAt(int level)
        {
            if (NormalStats != null)
            {
                return GetStatAtLevel(NormalStats.DefenseStart, NormalStats.DefenseUp, level);
            }
            return 0;
        }

        public int GetPlusAttackAt(int level)
        {
            if (PlusStats != null)
            {
                return GetStatAtLevel(PlusStats.AttackStart, PlusStats.AttackUp, level);
            }
            return 0;
        }

        public int GetPlusDefenseAt(int level)
        {
            if (PlusStats != null)
            {
                return GetStatAtLevel(PlusStats.DefenseStart, PlusStats.DefenseUp, level);
            }
            return 0;
        }

        public static int GetMaxLevelFromRarity(Rarity rarity)
        {
            switch (rarity)
            {
                case Rarity.Common:
                case Rarity.Uncommon:
                    return 30;
                case Rarity.Rare:
                case Rarity.SuperRare:
                case Rarity.Nemesis:
                    return 50;
                case Rarity.UltraRare:
                case Rarity.Legendary:
                    return 70;
                case Rarity.Epic:
                    return 99;
                case Rarity.FusionBoost:
                    return 1;
                default:
                    return 0;
            }
        }

        public static int GetCraftCostFromFeedCost(int baseFeedCost)
        {
            switch (baseFeedCost)
            {
                case 5:
                    return 300;
                case 8:
                    return 500;
                default:
                    return 0;
            }
        }

        public static int GetMaterialCountFromFeedCost(int baseFeedCost)
        {
            switch (baseFeedCost)
            {
                case 5:
                    return 3;
                case 8:
                    return 4;
                default:
                    return 0;
            }
        }

        public static int GetCraftTimeFromFeedCost(int baseFeedCost)
        {
            switch (baseFeedCost)
            {
                case 5:
                    return 5;
                case 8:
                    return 30;
                case 20:
                    return 120;
                default:
                    return 0;
            }
        }

        public static Armor GetCommonArmor(string name, Element element, ArmorStats normalStats, ArmorStats plusStats)
        {
            return new Armor(name, Rarity.Common, true, 30, 10, 5, 300, 3, 5, false, normalStats, plusStats, element);
        }

        public static Armor GetUncommonArmor(string name, Element element, ArmorStats normalStats, ArmorStats plusStats)
        {
            return new Armor(name, Rarity.Uncommon, true, 30, 10, 8, 500, 4, 30, false, normalStats, plusStats, element);
        }

        public static Armor GetCraftableRareArmor(string name, Element element1, Element element2, int craftCost, int materialCount, ArmorStats normalStats, ArmorStats plusStats)
        {
            return new Armor(name, Rarity.Rare, true, 50, 15, 20, craftCost, materialCount, 120, true, normalStats, plusStats, element1, element2);
        }

        public static Armor GetNonCraftableRareArmor(string name, Element element1, Element element2, ArmorStats normalStats, ArmorStats plusStats)
        {
            return new Armor(name, Rarity.Rare, false, 50, 15, 40, 0, 0, 0, true, normalStats, plusStats, element1, element2);
        }

        public static Armor GetSuperRareArmor(string name, Element element1, Element element2, ArmorStats normalStats, ArmorStats plusStats)
        {
            return new Armor(name, Rarity.SuperRare, false, 50, 15, 40, 0, 0, 0, true, normalStats, plusStats, element1, element2);
        }

        public static Armor GetCraftableUltraRareArmor(string name, Element element1, Element element2, int craftCost, int materialCount, bool isFusionResult, ArmorStats normalStats, ArmorStats plusStats)
        {
            return new Armor(name, Rarity.UltraRare, true, 70, 20, 72, craftCost, materialCount, 1440, isFusionResult, normalStats, plusStats, element1, element2);
        }

        public static Armor GetNonCraftableUltraRareArmor(string name, Element element1, Element element2, bool isFusionResult, ArmorStats normalStats, ArmorStats plusStats)
        {
            return new Armor(name, Rarity.UltraRare, false, 70, 20, 72, 0, 0, 0, isFusionResult, normalStats, plusStats, element1, element2);
        }

        public static Armor GetCraftableLegendaryArmor(string name, Element element1, Element? element2, int craftCost, int materialCount, int craftTime, bool isFusionResult, ArmorStats normalStats, ArmorStats plusStats)
        {
            return new Armor(name, Rarity.Legendary, true, 70, 35, 72, craftCost, materialCount, craftTime, isFusionResult, normalStats, plusStats, element1, element2);
        }

        public static Armor GetNonCraftableLegendaryArmor(string name, Element element1, Element? element2, bool isFusionResult, ArmorStats normalStats, ArmorStats plusStats)
        {
            return new Armor(name, Rarity.Legendary, false, 70, 35, 72, 0, 0, 0, isFusionResult, normalStats, plusStats, element1, element2);
        }

        public static Armor GetEpicArmor(string name, Element element1, Element? element2, bool isFusionResult, ArmorStats normalStats, ArmorStats plusStats)
        {
            return new Armor(name, Rarity.Epic, false, 99, 0, 150, 0, 0, 0, isFusionResult, normalStats, plusStats, element1, element2);
        }

        public static Armor GetNemesisArmor(string name, Element element1, int maxLevel, int feedCost, ArmorStats normalStats, ArmorStats plusStats)
        {
            return new Armor(name, Rarity.Nemesis, false, maxLevel, 0, feedCost, 0, 0, 0, false, normalStats, plusStats, element1);
        }

        public static Armor GetFusionBoostArmor(string name, Element element1)
        {
            return new Armor(name, Rarity.FusionBoost, false, 1, 0, 150, 0, 0, 0, false, null, null, element1);
        }

        private string GetRarityDescription(Rarity rarity)
        {
            switch (rarity)
            {
                case Rarity.Common:
                    return Strings.RarityCommon;
                case Rarity.Uncommon:
                    return Strings.RarityUncommon;
                case Rarity.Rare:
                    return Strings.RarityRare;
                case Rarity.SuperRare:
                    return Strings.RaritySuperRare;
                case Rarity.UltraRare:
                    return Strings.RarityUltraRare;
                case Rarity.Legendary:
                    return Strings.RarityLegendary;
                case Rarity.Epic:
                    return Strings.RarityEpic;
                case Rarity.Nemesis:
                    return Strings.RarityNemesis;
                case Rarity.FusionBoost:
                    return Strings.RarityFusionBoost;
                default:
                    return string.Empty;
            }
        }

        private string GetElementDescription(Element? element)
        {
            switch (element)
            {
                case Element.Fire:
                    return Strings.ElementFire;
                case Element.Spirit:
                    return Strings.ElementSpirit;
                case Element.Earth:
                    return Strings.ElementEarth;
                case Element.Air:
                    return Strings.ElementAir;
                case Element.Water:
                    return Strings.ElementWater;
                case Element.All:
                    return Strings.ElementAll;
                default:
                    return string.Empty;
            }
        }

        private int GetStatAtLevel(int start, int up, int level)
        {
            return start + (up * (level - 1));
        }
    }
}