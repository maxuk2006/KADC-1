using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace KnightsAndDragonsCalculator.Models
{
    public enum Rarity 
    {
        Common,
        Uncommon,
        Rare,
        SuperRare,
        UltraRare,
        Legendary,
        Epic
    }

    public enum Type 
    {
        Craftable,
        NonCraftable,
        Fusion
    }
    public enum Element
    {
        Fire,
        Water,
        Air,
        Earth,
        Spirit
    }

    [DataContract]
    public class Armor
    {
        [DataMember]
        public string Name { get; protected set; }
        [DataMember]
        public Rarity Rarity { get; protected set; }
        [DataMember]
        public Element Element1 { get; protected set; }
        [DataMember]
        public Element? Element2 { get; protected set; }
        [DataMember]
        public Type Type { get; protected set; }
        [DataMember]
        public int MaxLevel { get; protected set; }
        [DataMember]
        public int PlusLevel { get; protected set; }
        [DataMember]
        public int FeedCost { get; protected set; }
        [DataMember]
        public int CraftCost { get; protected set; }
        [DataMember]
        public int MaterialCount { get; protected set; }
        [DataMember]
        public ArmorStats NormalStats { get; protected set; }
        [DataMember]
        public ArmorStats PlusStats { get; protected set; }

        public Armor(string name, Rarity rarity, Type type, int maxLevel, int plusLevel, int feedCost, int craftCost, int materialCount, ArmorStats normalStats, ArmorStats plusStats, Element element1)
        {
            Name = name;
            Rarity = rarity;
            Type = type;
            MaxLevel = maxLevel;
            PlusLevel = plusLevel;
            FeedCost = feedCost;
            CraftCost = craftCost;
            MaterialCount = materialCount;
            NormalStats = normalStats;
            PlusStats = plusStats;
            Element1 = element1;
        }

        public Armor(string name, Rarity rarity, Type type, int maxLevel, int plusLevel, int feedCost, int craftCost, int materialCount, ArmorStats normalStats, ArmorStats plusStats, Element element1, Element element2)
            : this(name, rarity, type, maxLevel, plusLevel, feedCost, craftCost, materialCount, normalStats, plusStats, element1)
        {
            Element2 = element2;
        }

        public bool SameElementAs(Armor armor)
        {
            return (armor.Element1 == Element1 || 
                    (Element2 != null && armor.Element1 == Element2.Value) || 
                    (armor.Element2 != null && armor.Element2.Value == Element1) ||
                    (Element2 != null && armor.Element2 != null && armor.Element2.Value == Element2.Value));
        }

        public static Armor GetCommonArmor(string name, Element element)
        {
            return new Armor(name, Rarity.Common, Type.Craftable, 30, 10, 5, 300, 3, null, null, element);
        }

        public static Armor GetUncommonArmor(string name, Element element)
        {
            return new Armor(name, Rarity.Uncommon, Type.Craftable, 30, 10, 8, 500, 4, null, null, element);
        }
    }
}