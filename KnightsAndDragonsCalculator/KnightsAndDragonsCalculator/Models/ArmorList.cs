using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KnightsAndDragonsCalculator.Models
{
    public class ArmorList
    {
        public static Dictionary<string, Armor> _armors;

        public static void Initialize()
        {
            _armors = GetArmors();
        }

        public static Armor GetArmor(string armorName)
        {
            if (_armors.ContainsKey(armorName))
            {
                return _armors[armorName];
            }
            return null;
        }

        public static List<string> GetArmorNames()
        {
            List<string> names = new List<string>();
            foreach (Armor armor in _armors.Values)
            {
                names.Add(armor.Name);
            }
            return names;
        }

        private static Dictionary<string, Armor> GetArmors()
        {
            Dictionary<string, Armor> armors = new Dictionary<string, Armor>();
            armors.Add(Strings.CommonAirArmor, Armor.GetCommonArmor(Strings.CommonAirArmor, Element.Air));
            armors.Add(Strings.CommonEarthArmor, Armor.GetCommonArmor(Strings.CommonEarthArmor, Element.Earth));
            armors.Add(Strings.CommonFireArmor, Armor.GetCommonArmor(Strings.CommonFireArmor, Element.Fire));
            armors.Add(Strings.CommonSpiritArmor, Armor.GetCommonArmor(Strings.CommonSpiritArmor, Element.Spirit));
            armors.Add(Strings.CommonWaterArmor, Armor.GetCommonArmor(Strings.CommonWaterArmor, Element.Water));
            armors.Add(Strings.UncommonAirArmor, Armor.GetUncommonArmor(Strings.UncommonAirArmor, Element.Air));
            armors.Add(Strings.UncommonEarthArmor, Armor.GetUncommonArmor(Strings.UncommonEarthArmor, Element.Earth));
            armors.Add(Strings.UncommonFireArmor, Armor.GetUncommonArmor(Strings.UncommonFireArmor, Element.Fire));
            armors.Add(Strings.UncommonSpiritArmor, Armor.GetUncommonArmor(Strings.UncommonSpiritArmor, Element.Spirit));
            armors.Add(Strings.UncommonWaterArmor, Armor.GetUncommonArmor(Strings.UncommonWaterArmor, Element.Water));
            armors.Add(Strings.WickerMantle, GetWickerMantle());
            armors.Add(Strings.SnakeskinArmor, GetSnakeskinArmor());
            return armors;
        }

        private static Armor GetWickerMantle()
        {
            return new Armor(
                Strings.WickerMantle,
                Rarity.Legendary,
                Type.Craftable,
                70,
                35,
                72,
                75000,
                50,
                new ArmorStats(442, 9, 377, 7),
                new ArmorStats(489, 13, 418, 11),
                Element.Fire);
        }

        private static Armor GetSnakeskinArmor()
        {
            return new Armor(
                Strings.SnakeskinArmor,
                Rarity.Rare,
                Type.Craftable,
                50,
                15,
                20,
                3000,
                8,
                new ArmorStats(153, 6, 170, 9),
                new ArmorStats(168, 7, 189, 12),
                Element.Water,
                Element.Earth);
        }
    }
}