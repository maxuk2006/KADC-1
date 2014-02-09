using KnightsAndDragonsCalculatorApplication.Calculator.Containers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace KnightsAndDragonsCalculatorApplication.Calculator.Tables
{
    public sealed class ArmorTable
    {
        private static volatile ArmorTable instance;
        private static object syncRoot = new Object();

        private List<Armor> _armors;

        private ArmorTable() 
        {
            Initialize();
        }

        public static ArmorTable Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new ArmorTable();
                    }
                }

                return instance;
            }
        }

        public List<Armor> GetArmors()
        {
            return _armors;
        }

        public Armor GetArmor(string armorName)
        {
            return _armors.Find(a => a.Name == armorName);
        }

        public List<string> GetTargetArmorNames()
        {
            var armorNames = from a in _armors
                               where a.Rarity == Rarity.Rare || a.Rarity == Rarity.SuperRare || a.Rarity == Rarity.UltraRare || a.Rarity == Rarity.Legendary || a.Rarity == Rarity.Epic || a.Rarity == Rarity.Nemesis
                               select a.Name;

            return armorNames.ToList();
        }

        public List<string> GetFeederArmorNames()
        {
            var armorNames = from a in _armors
                               where a.Rarity == Rarity.Common || a.Rarity == Rarity.Uncommon || a.Rarity == Rarity.Rare || a.Rarity == Rarity.FusionBoost
                               select a.Name;

            return armorNames.ToList();
        }

        public List<string> GetFusableArmorNames()
        {
            var armorNames = from a in _armors
                               where a.Rarity != Rarity.FusionBoost && a.Rarity != Rarity.Nemesis
                               select a.Name;

            return armorNames.ToList();
        }

        public List<string> GetFusionArmorNames()
        {
            var armorNames = from a in _armors
                               where a.IsFusionResult
                               select a.Name;

            return armorNames.ToList();
        }

        public List<string> GetEpicBossArmorNames()
        {
            var armorNames = from a in _armors
                               where a.NormalStats != null || a.PlusStats != null
                               select a.Name;

            return armorNames.ToList();
        }

        public List<Armor> GetFusionArmors(Armor armor1, Armor armor2, List<Rarity> rarities)
        {
            var fusionArmors = from a in _armors
                               where a.IsFusionResult && a.Name != armor1.Name && a.Name != armor2.Name && a.IsPossibleFusionResultByElement(armor1, armor2) && rarities.Contains(a.Rarity)
                               select a;

            return fusionArmors.ToList();
        }

        public List<Armor> GetFusableArmors(Armor targetArmor, List<Rarity> rarities)
        {
            var fusionArmors = from a in _armors
                               where a.Rarity != Rarity.FusionBoost && a.Rarity != Rarity.Nemesis && a.Name != targetArmor.Name && a.IsPossibleFusableResultByElement(targetArmor) && rarities.Contains(a.Rarity)
                               select a;

            return fusionArmors.ToList();
        }

        private List<string> GetArmorNames(IEnumerable<Armor> armors)
        {
            List<string> names = new List<string>();
            foreach (Armor armor in armors)
            {
                names.Add(armor.Name);
            }
            return names;
        }

        private void Initialize()
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<Armor>));
            using (var fs = new FileStream(HttpContext.Current.Server.MapPath(Strings.FileNameArmors), FileMode.Open))
            {
                _armors = xs.Deserialize(fs) as List<Armor>;
            }
            _armors.Sort((x, y) => x.Name.CompareTo(y.Name));
        }

        #region Old Initialization

        private void InitializeOld()
        {
            _armors = new List<Armor>();
            _armors.AddRange(GetCommonArmors());
            _armors.AddRange(GetUncommonArmors());
            _armors.AddRange(GetRareArmors());
            _armors.AddRange(GetSuperRareArmors());
            _armors.AddRange(GetUltraRareArmors());
            _armors.AddRange(GetLegendaryArmors());
            _armors.AddRange(GetEpicArmors());
            _armors.AddRange(GetNemesisArmors());
            _armors.AddRange(GetFusionBoostArmors());
            _armors.Sort((x, y) => x.Name.CompareTo(y.Name));

            XmlSerializer xs = new XmlSerializer(typeof(List<Armor>));
            using (var fs = new FileStream(@"d:\armors.xml", FileMode.Create))
            {
                xs.Serialize(fs, _armors);
            }
        }

        #region Fusion Boost

        private List<Armor> GetFusionBoostArmors()
        {
            List<Armor> armors = new List<Armor>();

            armors.Add(Armor.GetFusionBoostArmor(Strings.AirFusionBoostArmor, Element.Air));
            armors.Add(Armor.GetFusionBoostArmor(Strings.EarthFusionBoostArmor, Element.Earth));
            armors.Add(Armor.GetFusionBoostArmor(Strings.FireFusionBoostArmor, Element.Fire));
            armors.Add(Armor.GetFusionBoostArmor(Strings.SpiritFusionBoostArmor, Element.Spirit));
            armors.Add(Armor.GetFusionBoostArmor(Strings.WaterFusionBoostArmor, Element.Water));

            return armors;
        }

        #endregion

        #region Common

        private List<Armor> GetCommonArmors()
        {
            List<Armor> armors = new List<Armor>();

            armors.Add(Armor.GetCommonArmor(Strings.CommonAirArmor, Element.Air, new ArmorStats(72, 2, 88, 2), new ArmorStats(80, 2, 97, 2)));
            armors.Add(Armor.GetCommonArmor(Strings.CommonEarthArmor, Element.Earth, new ArmorStats(80, 2, 80, 2), new ArmorStats(88, 3, 88, 3)));
            armors.Add(Armor.GetCommonArmor(Strings.CommonFireArmor, Element.Fire, new ArmorStats(80, 2, 64, 2), new ArmorStats(88, 5, 71, 3)));
            armors.Add(Armor.GetCommonArmor(Strings.CommonSpiritArmor, Element.Spirit, new ArmorStats(88, 1, 72, 2), new ArmorStats(97, 2, 80, 3)));
            armors.Add(Armor.GetCommonArmor(Strings.CommonWaterArmor, Element.Water, new ArmorStats(84, 2, 84, 2), new ArmorStats(93, 3, 93, 3)));

            return armors;
        }

        #endregion

        #region Uncommon

        private List<Armor> GetUncommonArmors()
        {
            List<Armor> armors = new List<Armor>();

            armors.Add(Armor.GetUncommonArmor(Strings.UncommonAirArmor, Element.Air, new ArmorStats(104, 4, 127, 7), new ArmorStats(115, 5, 141, 9)));
            armors.Add(Armor.GetUncommonArmor(Strings.UncommonEarthArmor, Element.Earth, new ArmorStats(115, 5, 115, 6), new ArmorStats(127, 6, 127, 8)));
            armors.Add(Armor.GetUncommonArmor(Strings.UncommonFireArmor, Element.Fire, new ArmorStats(132, 6, 92, 4), new ArmorStats(146, 8, 101, 5)));
            armors.Add(Armor.GetUncommonArmor(Strings.UncommonSpiritArmor, Element.Spirit, new ArmorStats(127, 5, 104, 4), new ArmorStats(141, 7, 115, 5)));
            armors.Add(Armor.GetUncommonArmor(Strings.UncommonWaterArmor, Element.Water, new ArmorStats(121, 6, 121, 6), new ArmorStats(133, 7, 133, 7)));

            return armors;
        }

        #endregion

        #region Rare

        private List<Armor> GetRareArmors()
        {
            List<Armor> armors = new List<Armor>();

            armors.Add(Armor.GetCraftableRareArmor(Strings.SnakeskinArmor, Element.Water, Element.Earth, 3000, 8, new ArmorStats(153, 6, 170, 9), new ArmorStats(168, 7, 189, 12)));
            armors.Add(Armor.GetCraftableRareArmor(Strings.AtlanteanAvengerArmor, Element.Water, Element.Air, 6000, 8, new ArmorStats(187, 8, 170, 9), new ArmorStats(207, 11, 189, 12)));
            armors.Add(Armor.GetCraftableRareArmor(Strings.CriusArmor, Element.Earth, Element.Air, 9000, 12, new ArmorStats(187, 8, 187, 10), new ArmorStats(206, 10, 206, 12)));
            armors.Add(Armor.GetCraftableRareArmor(Strings.ChimeraCorpsUniform, Element.Spirit, Element.Earth, 12000, 12, new ArmorStats(153, 6, 179, 9), new ArmorStats(168, 7, 198, 11)));
            armors.Add(Armor.GetCraftableRareArmor(Strings.LivingFlameArmor, Element.Fire, Element.Spirit, 15000, 12, new ArmorStats(170, 9, 179, 9), new ArmorStats(189, 12, 198, 11)));
            armors.Add(Armor.GetCraftableRareArmor(Strings.HydraHuntersMail, Element.Water, Element.Fire, 18000, 8, new ArmorStats(170, 9, 187, 9), new ArmorStats(189, 12, 207, 12)));
            armors.Add(Armor.GetNonCraftableRareArmor(Strings.FlamestormFinery, Element.Air, Element.Fire, new ArmorStats(273, 7, 210, 6), new ArmorStats(302, 10, 231, 7)));
            armors.Add(Armor.GetNonCraftableRareArmor(Strings.VolcanicMantle, Element.Earth, Element.Fire, new ArmorStats(177, 4, 150, 4), new ArmorStats(196, 6, 165, 5)));
            armors.Add(Armor.GetNonCraftableRareArmor(Strings.WaveCharmersMantle, Element.Water, Element.Spirit, new ArmorStats(231, 6, 242, 7), new ArmorStats(254, 7, 267, 9)));
            armors.Add(Armor.GetNonCraftableRareArmor(Strings.WindMonarchsRobes, Element.Spirit, Element.Air, new ArmorStats(252, 7, 231, 6), new ArmorStats(278, 9, 254, 7)));

            return armors;
        }

        #endregion

        #region Super Rare

        private List<Armor> GetSuperRareArmors()
        {
            List<Armor> armors = new List<Armor>();

            armors.Add(Armor.GetSuperRareArmor(Strings.SteamWizardsRobes, Element.Fire, Element.Water, new ArmorStats(210, 7, 210, 7), new ArmorStats(233, 10, 233, 10)));
            armors.Add(Armor.GetSuperRareArmor(Strings.MonstrousGarb, Element.Air, Element.Earth, new ArmorStats(231, 6, 231, 7), new ArmorStats(255, 8, 256, 10)));
            armors.Add(Armor.GetSuperRareArmor(Strings.BrawlersArmor, Element.Earth, Element.Spirit, new ArmorStats(189, 6, 231, 7), new ArmorStats(208, 7, 256, 10)));
            armors.Add(Armor.GetSuperRareArmor(Strings.AsuraArmor, Element.Earth, Element.Water, new ArmorStats(221, 6, 221, 7), new ArmorStats(243, 7, 245, 10)));
            armors.Add(Armor.GetSuperRareArmor(Strings.GlacierArmor, Element.Water, Element.Spirit, new ArmorStats(210, 6, 221, 6), new ArmorStats(231, 7, 244, 8)));
            armors.Add(Armor.GetSuperRareArmor(Strings.LightningLordArmor, Element.Air, Element.Water, new ArmorStats(231, 6, 210, 7), new ArmorStats(256, 9, 232, 9)));
            armors.Add(Armor.GetSuperRareArmor(Strings.RoyalFlameArmor, Element.Fire, Element.Spirit, new ArmorStats(231, 8, 221, 6), new ArmorStats(256, 11, 244, 8)));
            armors.Add(Armor.GetSuperRareArmor(Strings.ExorcistsVestments, Element.Air, Element.Spirit, new ArmorStats(231, 6, 221, 6), new ArmorStats(247, 9, 230, 8)));
            armors.Add(Armor.GetSuperRareArmor(Strings.ForgemastersGarb, Element.Fire, Element.Earth, new ArmorStats(210, 6, 231, 7), new ArmorStats(233, 9, 256, 10)));
            armors.Add(Armor.GetSuperRareArmor(Strings.EmbersteelArmor, Element.Spirit, Element.Fire, new ArmorStats(252, 7, 221, 6), new ArmorStats(278, 9, 243, 7)));
            armors.Add(Armor.GetSuperRareArmor(Strings.FlowstoneBattlegear, Element.Earth, Element.Air, new ArmorStats(269, 8, 307, 9), new ArmorStats(297, 10, 339, 12)));
            armors.Add(Armor.GetSuperRareArmor(Strings.MoltenShroud, Element.Fire, Element.Earth, new ArmorStats(273, 6, 231, 6), new ArmorStats(301, 8, 254, 7)));
            armors.Add(Armor.GetSuperRareArmor(Strings.MonksVestments, Element.Earth, Element.Spirit, new ArmorStats(231, 6, 242, 7), new ArmorStats(254, 7, 267, 9)));
            armors.Add(Armor.GetSuperRareArmor(Strings.RageborneRaiment, Element.Fire, Element.Water, new ArmorStats(315, 8, 265, 9), null));
            armors.Add(Armor.GetSuperRareArmor(Strings.RiverstoneMantle, Element.Earth, Element.Water, new ArmorStats(284, 8, 312, 10), new ArmorStats(312, 9, 343, 11)));
            armors.Add(Armor.GetSuperRareArmor(Strings.StormrageArmor, Element.Water, Element.Air, new ArmorStats(295, 8, 282, 8), new ArmorStats(326, 11, 311, 10)));
            armors.Add(Armor.GetSuperRareArmor(Strings.TorchflameMantle, Element.Fire, Element.Spirit, new ArmorStats(252, 8, 302, 8), new ArmorStats(277, 9, 332, 9)));
            armors.Add(Armor.GetSuperRareArmor(Strings.VinewoodCarapace, Element.Earth, Element.Water, new ArmorStats(231, 6, 254, 8), new ArmorStats(254, 7, 280, 10)));

            return armors;
        }

        #endregion

        #region Ultra Rare

        private List<Armor> GetUltraRareArmors()
        {
            List<Armor> armors = new List<Armor>();

            armors.Add(Armor.GetCraftableUltraRareArmor(Strings.SpectralCaptainsUniform, Element.Water, Element.Spirit, 50000, 18, true, new ArmorStats(264, 6, 252, 7), new ArmorStats(290, 7, 278, 9)));
            armors.Add(Armor.GetCraftableUltraRareArmor(Strings.SwampShamanRobes, Element.Water, Element.Earth, 60000, 18, true, new ArmorStats(240, 7, 264, 9), new ArmorStats(264, 8, 292, 12)));
            armors.Add(Armor.GetCraftableUltraRareArmor(Strings.RocfeatherRobes, Element.Spirit, Element.Air, 70000, 18, true, new ArmorStats(264, 7, 252, 7), new ArmorStats(292, 10, 278, 9)));
            armors.Add(Armor.GetCraftableUltraRareArmor(Strings.ArmorOfTheInfernalLord, Element.Fire, Element.Earth, 80000, 18, true, new ArmorStats(240, 8, 264, 9), new ArmorStats(266, 11, 291, 11)));
            armors.Add(Armor.GetCraftableUltraRareArmor(Strings.DarkPrincesRoyalArmor, Element.Fire, Element.Spirit, 100000, 22, false, new ArmorStats(264, 9, 252, 7), new ArmorStats(292, 12, 278, 9)));
            armors.Add(Armor.GetNonCraftableUltraRareArmor(Strings.SkyGuardian, Element.Spirit, Element.Air, true, new ArmorStats(295, 9, 337, 10), new ArmorStats(325, 11, 372, 13)));
            armors.Add(Armor.GetNonCraftableUltraRareArmor(Strings.StormSorcerer, Element.Water, Element.Air, true, new ArmorStats(331, 10, 290, 8), new ArmorStats(366, 13, 320, 10)));
            armors.Add(Armor.GetNonCraftableUltraRareArmor(Strings.WickedWraith, Element.Spirit, Element.Earth, false, new ArmorStats(284, 8, 331, 11), new ArmorStats(313, 10, 366, 14)));

            return armors;
        }

        #endregion

        #region Legendary

        private List<Armor> GetLegendaryArmors()
        {
            List<Armor> armors = new List<Armor>();

            armors.Add(Armor.GetCraftableLegendaryArmor(Strings.AbominablePlatemail, Element.Air, Element.Water, 75000, 50, 4320, false, new ArmorStats(378, 7, 458, 9), new ArmorStats(419, 11, 506, 13)));
            armors.Add(Armor.GetCraftableLegendaryArmor(Strings.AdmiralsBattlewornRegalia, Element.Fire, Element.Spirit, 50000, 50, 2880, true, new ArmorStats(345, 9, 330, 6), new ArmorStats(381, 12, 363, 7)));
            armors.Add(Armor.GetNonCraftableLegendaryArmor(Strings.AegisOfTheDragon, Element.Earth, Element.Air, false, new ArmorStats(470, 10, 493, 12), new ArmorStats(517, 11, 542, 13)));
            armors.Add(Armor.GetCraftableLegendaryArmor(Strings.AegisOfTheFallen, Element.Fire, Element.Spirit, 200000, 50, 4320, true, new ArmorStats(308, 6, 425, 10), new ArmorStats(341, 9, 469, 13)));
            armors.Add(Armor.GetCraftableLegendaryArmor(Strings.AegisOfTheWyvern, Element.Earth, Element.Spirit, 75000, 50, 4320, false, new ArmorStats(369, 6, 443, 10), new ArmorStats(408, 9, 491, 15)));
            armors.Add(Armor.GetCraftableLegendaryArmor(Strings.ArmorOfBoreas, Element.Air, Element.Water, 200000, 50, 4320, false, new ArmorStats(379, 9, 327, 7), new ArmorStats(419, 12, 362, 10)));
            armors.Add(Armor.GetCraftableLegendaryArmor(Strings.ArmorOfEurus, Element.Water, Element.Spirit, 200000, 50, 4320, false, new ArmorStats(340, 8, 356, 8), new ArmorStats(376, 11, 393, 11)));
            armors.Add(Armor.GetCraftableLegendaryArmor(Strings.ArmorOfNotus, Element.Spirit, Element.Air, 200000, 50, 4320, false, new ArmorStats(301, 6, 423, 10), new ArmorStats(333, 9, 467, 13)));
            armors.Add(Armor.GetCraftableLegendaryArmor(Strings.ArmorOfPhyrus, Element.Fire, Element.Earth, 200000, 50, 4320, false, new ArmorStats(415, 10, 324, 6), new ArmorStats(458, 13, 358, 9)));
            armors.Add(Armor.GetCraftableLegendaryArmor(Strings.ArmorOfTheBear, Element.Earth, Element.Spirit, 75000, 50, 4320, false, new ArmorStats(428, 9, 366, 6), new ArmorStats(474, 14, 406, 10)));
            armors.Add(Armor.GetCraftableLegendaryArmor(Strings.ArmorOfTheWolf, Element.Earth, Element.Fire, 75000, 50, 4320, false, new ArmorStats(340, 5, 444, 10), new ArmorStats(377, 9, 492, 15)));
            armors.Add(Armor.GetCraftableLegendaryArmor(Strings.AsherahsArmor, Element.Water, Element.Spirit, 75000, 50, 4320, false, new ArmorStats(411, 8, 411, 8), new ArmorStats(455, 12, 455, 12)));
            armors.Add(Armor.GetCraftableLegendaryArmor(Strings.AssassinsShroud, Element.Spirit, Element.Earth, 200000, 50, 4320, true, new ArmorStats(324, 8, 339, 8), new ArmorStats(358, 12, 375, 11)));
            armors.Add(Armor.GetCraftableLegendaryArmor(Strings.BarbarianRaidingGear, Element.Air, Element.Fire, 30000, 50, 1440, true, new ArmorStats(300, 7, 225, 5), new ArmorStats(332, 10, 248, 6)));
            armors.Add(Armor.GetCraftableLegendaryArmor(Strings.BattlesuitRemnants, Element.Earth, null, 50000, 50, 2880, true, new ArmorStats(236, 4, 315, 7), new ArmorStats(260, 5, 348, 10)));
            armors.Add(Armor.GetCraftableLegendaryArmor(Strings.BehemothsVanguard, Element.Fire, Element.Spirit, 75000, 50, 4320, false, new ArmorStats(421, 9, 377, 7), new ArmorStats(466, 13, 418, 11)));
            armors.Add(Armor.GetNonCraftableLegendaryArmor(Strings.BlackKaleidoscopicRaiment, Element.All, null, false, new ArmorStats(441, 9, 378, 8), new ArmorStats(485, 10, 416, 9)));
            armors.Add(Armor.GetNonCraftableLegendaryArmor(Strings.BlackfrostRaiment, Element.Water, Element.Spirit, true, new ArmorStats(431, 9, 431, 9), new ArmorStats(474, 10, 474, 10)));
            armors.Add(Armor.GetNonCraftableLegendaryArmor(Strings.BoilerplateArmor, Element.Fire, Element.Water, true, new ArmorStats(420, 7, 420, 12), new ArmorStats(462, 8, 461, 13)));
            armors.Add(Armor.GetCraftableLegendaryArmor(Strings.BoneHarvestersGarb, Element.Spirit, null, 50000, 50, 2880, true, new ArmorStats(289, 7, 276, 6), new ArmorStats(320, 10, 305, 8)));
            armors.Add(Armor.GetCraftableLegendaryArmor(Strings.ChitinousArmor, Element.Water, null, 50000, 50, 2880, true, new ArmorStats(263, 5, 263, 7), new ArmorStats(289, 6, 291, 10)));
            armors.Add(Armor.GetNonCraftableLegendaryArmor(Strings.ClayplateMantle, Element.Water, Element.Earth, true, new ArmorStats(324, 10, 409, 14), new ArmorStats(356, 11, 449, 15)));
            armors.Add(Armor.GetCraftableLegendaryArmor(Strings.CloudKingsFinery, Element.Air, null, 50000, 50, 2880, true, new ArmorStats(302, 5, 263, 5), new ArmorStats(332, 6, 289, 6)));
            armors.Add(Armor.GetNonCraftableLegendaryArmor(Strings.CombustionArmor, Element.Fire, Element.Air, true, new ArmorStats(373, 13, 339, 12), new ArmorStats(410, 14, 372, 13)));
            armors.Add(Armor.GetCraftableLegendaryArmor(Strings.DeepDragonMail, Element.Water, Element.Earth, 50000, 50, 2880, true, new ArmorStats(268, 6, 357, 8), new ArmorStats(295, 7, 395, 12)));
            armors.Add(Armor.GetCraftableLegendaryArmor(Strings.DragonTamerCostume, Element.Water, Element.Earth, 120000, 50, 1440, false, new ArmorStats(257, 7, 311, 10), new ArmorStats(284, 7, 344, 10)));
            armors.Add(Armor.GetCraftableLegendaryArmor(Strings.EldritchKeepersRobes, Element.Spirit, null, 200000, 50, 4320, true, new ArmorStats(423, 9, 228, 6), new ArmorStats(468, 13, 252, 8)));
            armors.Add(Armor.GetCraftableLegendaryArmor(Strings.EtherealGarment, Element.Spirit, null, 50000, 50, 2880, true, new ArmorStats(270, 6, 289, 7), new ArmorStats(305, 8, 320, 10)));
            armors.Add(Armor.GetCraftableLegendaryArmor(Strings.FeatherbladeBattlegear, Element.Air, null, 200000, 50, 4320, true, new ArmorStats(386, 7, 440, 8), new ArmorStats(427, 11, 487, 12)));
            armors.Add(Armor.GetCraftableLegendaryArmor(Strings.FlameSummonersShroud, Element.Fire, Element.Air, 200000, 50, 2880, true, new ArmorStats(293, 6, 423, 10), new ArmorStats(323, 8, 467, 13)));
            armors.Add(Armor.GetNonCraftableLegendaryArmor(Strings.FlamehuntersGarb, Element.Fire, Element.Water, true, new ArmorStats(438, 8, 368, 9), new ArmorStats(482, 9, 404, 10)));
            armors.Add(Armor.GetNonCraftableLegendaryArmor(Strings.GuardiansBattlegear, Element.Spirit, Element.Earth, true, new ArmorStats(413, 10, 431, 11), new ArmorStats(454, 11, 474, 12)));
            armors.Add(Armor.GetCraftableLegendaryArmor(Strings.HalfDragonWarriorsArmor, Element.Earth, Element.Fire, 50000, 50, 2880, true, new ArmorStats(333, 8, 333, 7), new ArmorStats(368, 11, 367, 9)));
            armors.Add(Armor.GetCraftableLegendaryArmor(Strings.HorribleWurmCostume, Element.Air, Element.Earth, 50000, 50, 2880, true, new ArmorStats(369, 7, 325, 6), new ArmorStats(408, 10, 358, 8)));
            armors.Add(Armor.GetCraftableLegendaryArmor(Strings.HorsemansBattlegear, Element.Fire, Element.Water, 75000, 50, 4320, false, new ArmorStats(393, 8, 404, 8), new ArmorStats(434, 11, 448, 13)));
            armors.Add(Armor.GetCraftableLegendaryArmor(Strings.HuntersGarb, Element.Spirit, Element.Earth, 50000, 50, 2880, true, new ArmorStats(249, 6, 289, 7), new ArmorStats(274, 7, 318, 8)));
            armors.Add(Armor.GetCraftableLegendaryArmor(Strings.HydromancersMantle, Element.Water, null, 50000, 50, 2880, true, new ArmorStats(302, 8, 288, 7), new ArmorStats(334, 11, 317, 8)));
            armors.Add(Armor.GetCraftableLegendaryArmor(Strings.JackalopesChaingarb, Element.Water, Element.Earth, 200000, 50, 4320, true, new ArmorStats(391, 8, 375, 8), new ArmorStats(431, 10, 413, 10)));
            armors.Add(Armor.GetCraftableLegendaryArmor(Strings.JiansBattlegear, Element.Fire, Element.Water, 200000, 50, 4320, false, new ArmorStats(366, 10, 315, 7), new ArmorStats(403, 12, 348, 10)));
            armors.Add(Armor.GetCraftableLegendaryArmor(Strings.KaleidoscopicStarmetalRaiment, Element.All, null, 150000, 70, 4320, false, new ArmorStats(196, 7, 210, 3), new ArmorStats(216, 9, 197, 5)));
            armors.Add(Armor.GetCraftableLegendaryArmor(Strings.LeviathansPlatemail, Element.Water, null, 200000, 50, 4320, true, new ArmorStats(389, 10, 354, 8), new ArmorStats(429, 13, 391, 11)));
            armors.Add(Armor.GetCraftableLegendaryArmor(Strings.MantleOfTheBeast, Element.Spirit, Element.Earth, 200000, 50, 2880, true, new ArmorStats(366, 9, 315, 7), new ArmorStats(404, 12, 347, 9)));
            armors.Add(Armor.GetCraftableLegendaryArmor(Strings.MountainvineShroud, Element.Earth, null, 50000, 50, 2880, true, new ArmorStats(289, 6, 302, 6), new ArmorStats(319, 8, 334, 9)));
            armors.Add(Armor.GetCraftableLegendaryArmor(Strings.PyromancersMantle, Element.Fire, Element.Spirit, 200000, 50, 4320, true, new ArmorStats(371, 9, 354, 7), new ArmorStats(410, 12, 390, 9)));
            armors.Add(Armor.GetCraftableLegendaryArmor(Strings.RavenlordsMantle, Element.Spirit, Element.Air, 75000, 50, 4320, false, new ArmorStats(456, 10, 328, 5), new ArmorStats(505, 15, 364, 9)));
            armors.Add(Armor.GetCraftableLegendaryArmor(Strings.RubyPlateMail, Element.Fire, null, 50000, 50, 2880, true, new ArmorStats(315, 7, 236, 5), new ArmorStats(348, 10, 261, 7)));
            armors.Add(Armor.GetCraftableLegendaryArmor(Strings.ScorchstoneAegis, Element.Fire, null, 75000, 50, 4320, true, new ArmorStats(376, 5, 454, 10), new ArmorStats(417, 9, 502, 14)));
            armors.Add(Armor.GetCraftableLegendaryArmor(Strings.SlimebaneBattlegear, Element.Earth, Element.Water, 200000, 50, 4320, true, new ArmorStats(356, 10, 305, 7), new ArmorStats(393, 13, 337, 10)));
            armors.Add(Armor.GetCraftableLegendaryArmor(Strings.SoulshardRaiment, Element.Spirit, null, 75000, 50, 4320, true, new ArmorStats(467, 9, 362, 6), new ArmorStats(516, 13, 401, 10)));
            armors.Add(Armor.GetNonCraftableLegendaryArmor(Strings.SpartansWargear, Element.Air, Element.Earth, true, new ArmorStats(405, 14, 273, 12), null));
            armors.Add(Armor.GetCraftableLegendaryArmor(Strings.StarsongShroud, Element.Spirit, Element.Water, 200000, 50, 4320, true, new ArmorStats(345, 6, 366, 9), new ArmorStats(380, 8, 404, 12)));
            armors.Add(Armor.GetCraftableLegendaryArmor(Strings.SteampoweredExoskeleton, Element.Water, Element.Fire, 120000, 50, 1440, false, new ArmorStats(270, 6, 270, 6), new ArmorStats(299, 9, 299, 9)));
            armors.Add(Armor.GetCraftableLegendaryArmor(Strings.SwampstonePlatemail, Element.Water, Element.Earth, 75000, 50, 4320, false, new ArmorStats(404, 8, 385, 7), new ArmorStats(447, 12, 427, 12)));
            armors.Add(Armor.GetNonCraftableLegendaryArmor(Strings.TemperedBattlegear, Element.Fire, Element.Earth, true, new ArmorStats(455, 12, 385, 8), new ArmorStats(500, 13, 423, 9)));
            armors.Add(Armor.GetCraftableLegendaryArmor(Strings.TortoiseshellAegis, Element.Earth, Element.Air, 200000, 50, 4320, true, new ArmorStats(250, 6, 406, 9), new ArmorStats(276, 8, 448, 12)));
            armors.Add(Armor.GetCraftableLegendaryArmor(Strings.TrollforgePlatemail, Element.Earth, null, 75000, 50, 4320, true, new ArmorStats(498, 10, 328, 5), new ArmorStats(550, 14, 364, 9)));
            armors.Add(Armor.GetCraftableLegendaryArmor(Strings.WanderersShroud, Element.Fire, null, 200000, 50, 4320, true, new ArmorStats(371, 7, 388, 9), new ArmorStats(409, 9, 429, 11)));
            armors.Add(Armor.GetCraftableLegendaryArmor(Strings.WickerMantle, Element.Fire, null, 75000, 50, 4320, false, new ArmorStats(442, 9, 377, 7), new ArmorStats(489, 13, 417, 11)));
            armors.Add(Armor.GetCraftableLegendaryArmor(Strings.WitchsRobes, Element.Air, Element.Earth, 75000, 50, 4320, false, new ArmorStats(407, 8, 386, 8), new ArmorStats(450, 12, 427, 12)));

            return armors;
        }

        #endregion

        #region Epic

        private List<Armor> GetEpicArmors()
        {
            List<Armor> armors = new List<Armor>();

            armors.Add(Armor.GetEpicArmor(Strings.AegisOfSkyMajesty, Element.Fire, Element.Air, false, new ArmorStats(572, 11, 570, 9), new ArmorStats(630, 13, 627, 10)));
            armors.Add(Armor.GetEpicArmor(Strings.ArborsteelVanguard, Element.Water, Element.Earth, false, new ArmorStats(567, 8, 586, 12), new ArmorStats(624, 10, 644, 13)));
            armors.Add(Armor.GetEpicArmor(Strings.ArmorOfTheDevoted, Element.Spirit, null, false, new ArmorStats(672, 11, 614, 9), new ArmorStats(739, 12, 676, 11)));
            armors.Add(Armor.GetEpicArmor(Strings.BeastmasterBattlegear, Element.Spirit, Element.Air, false, new ArmorStats(572, 10, 572, 10), new ArmorStats(618, 11, 618, 12)));
            armors.Add(Armor.GetEpicArmor(Strings.BlazeborneVanguard, Element.Fire, null, true, new ArmorStats(576, 11, 479, 10), new ArmorStats(633, 12, 526, 11)));
            armors.Add(Armor.GetEpicArmor(Strings.BlazestoneMantle, Element.Water, Element.Fire, false, new ArmorStats(706, 12, 601, 8), null));
            armors.Add(Armor.GetEpicArmor(Strings.CloudrangePlatemail, Element.Water, Element.Air, false, new ArmorStats(466, 9, 601, 10), new ArmorStats(512, 10, 661, 11)));
            armors.Add(Armor.GetEpicArmor(Strings.ForgestoneAegis, Element.Fire, Element.Spirit, true, new ArmorStats(571, 11, 541, 10), new ArmorStats(628, 12, 595, 11)));
            armors.Add(Armor.GetEpicArmor(Strings.KerstmansShroud, Element.Fire, Element.Air, false, null, null));
            armors.Add(Armor.GetEpicArmor(Strings.MaelstromIrons, Element.Water, Element.Air, true, new ArmorStats(610, 12, 506, 9), new ArmorStats(670, 13, 556, 10)));
            armors.Add(Armor.GetEpicArmor(Strings.MoontidePlatemail, Element.Water, Element.Spirit, true, new ArmorStats(552, 9, 503, 8), new ArmorStats(607, 10, 553, 9)));
            armors.Add(Armor.GetEpicArmor(Strings.NorthernersBattlegear, Element.Water, null, false, new ArmorStats(728, 12, 573, 8), new ArmorStats(800, 13, 631, 10)));
            armors.Add(Armor.GetEpicArmor(Strings.SoulshardNecromantle, Element.Spirit, Element.Water, false, new ArmorStats(650, 12, 542, 8), new ArmorStats(715, 14, 596, 9)));
            armors.Add(Armor.GetEpicArmor(Strings.TectonicMantle, Element.Earth, null, true, new ArmorStats(528, 8, 581, 13), new ArmorStats(581, 9, 638, 14)));

            return armors;
        }

        #endregion

        #region Nemesis

        private List<Armor> GetNemesisArmors()
        {
            List<Armor> armors = new List<Armor>();

            armors.Add(Armor.GetNemesisArmor(Strings.AdmiralsNemesisUniform, Element.Water, 30, 8, null, null));
            armors.Add(Armor.GetNemesisArmor(Strings.AsherahsNemesis, Element.Air, 50, 20, null, null));
            armors.Add(Armor.GetNemesisArmor(Strings.BansheesNemesisWrappings, Element.Fire, 30, 8, new ArmorStats(196, 9, 139, 6), null));
            armors.Add(Armor.GetNemesisArmor(Strings.BattlesuitsNemesisGarb, Element.Spirit, 30, 8, new ArmorStats(196, 10, 196, 10), null));
            armors.Add(Armor.GetNemesisArmor(Strings.BehemothsNemesis, Element.Water, 50, 20, null, null));
            armors.Add(Armor.GetNemesisArmor(Strings.BeringarsNemesis, Element.Fire, 50, 20, new ArmorStats(286, 8, 246, 7), null));
            armors.Add(Armor.GetNemesisArmor(Strings.BoreassNemesis, Element.Earth, 30, 8, new ArmorStats(180, 9, 184, 13), null));
            armors.Add(Armor.GetNemesisArmor(Strings.CircesNemesis, Element.Spirit, 50, 20, null, null));
            armors.Add(Armor.GetNemesisArmor(Strings.CorvussNemesis, Element.Earth, 50, 20, null, new ArmorStats(264, 275, 13, 14)));
            armors.Add(Armor.GetNemesisArmor(Strings.CrabsNemesisUniform, Element.Air, 30, 8, new ArmorStats(179, 8, 170, 8), null));
            armors.Add(Armor.GetNemesisArmor(Strings.DeepDragonsNemesisArmor, Element.Spirit, 30, 8, new ArmorStats(196, 10, 187, 9), null));
            armors.Add(Armor.GetNemesisArmor(Strings.DevasNemesis, Element.Water, 30, 8, new ArmorStats(214, 10, 204, 10), null));
            armors.Add(Armor.GetNemesisArmor(Strings.DorarsNemesis, Element.Water, 50, 20, new ArmorStats(257, 8, 239, 7), null));
            armors.Add(Armor.GetNemesisArmor(Strings.EmberLizardsNemesis, Element.Water, 50, 20, new ArmorStats(233, 7, 258, 8), null));
            armors.Add(Armor.GetNemesisArmor(Strings.EurussNemesis, Element.Air, 30, 8, new ArmorStats(189, 11, 193, 10), null));
            armors.Add(Armor.GetNemesisArmor(Strings.FaerieDragonsNemesisArmor, Element.Fire, 30, 8, null, null));
            armors.Add(Armor.GetNemesisArmor(Strings.FenrissNemesis, Element.Water, 50, 20, new ArmorStats(273, 7, 279, 8), new ArmorStats(305, 312, 13, 14)));
            armors.Add(Armor.GetNemesisArmor(Strings.FireEatersNemesis, Element.Earth, 30, 8, new ArmorStats(184, 7, 224, 12), null));
            armors.Add(Armor.GetNemesisArmor(Strings.FlamegemNemesisArmor, Element.Water, 30, 8, null, null));
            armors.Add(Armor.GetNemesisArmor(Strings.ForgemastersNemesisArmor, Element.Water, 30, 8, null, null));
            armors.Add(Armor.GetNemesisArmor(Strings.GlorgsNemesis, Element.Spirit, 30, 8, new ArmorStats(252, 11, 239, 10), null));
            armors.Add(Armor.GetNemesisArmor(Strings.GorlogsNemesisArmor, Element.Spirit, 30, 8, new ArmorStats(196, 10, 196, 10), null));
            armors.Add(Armor.GetNemesisArmor(Strings.HalfDragonsNemesisCostume, Element.Fire, 30, 8, null, null));
            armors.Add(Armor.GetNemesisArmor(Strings.HorsemansNemesis, Element.Air, 50, 20, new ArmorStats(289, 8, 264, 7), new ArmorStats(323, 295, 14, 13)));
            armors.Add(Armor.GetNemesisArmor(Strings.HuntresssNemesisArmor, Element.Fire, 30, 8, new ArmorStats(215, 9, 150, 7), null));
            armors.Add(Armor.GetNemesisArmor(Strings.HydromancersNemesisFinery, Element.Air, 30, 8, new ArmorStats(196, 9, 187, 9), null));
            armors.Add(Armor.GetNemesisArmor(Strings.IceLichsNemesis, Element.Fire, 50, 20, null, null));
            armors.Add(Armor.GetNemesisArmor(Strings.JackalopesNemesis, Element.Spirit, 30, 8, new ArmorStats(214, 11, 214, 11), null));
            armors.Add(Armor.GetNemesisArmor(Strings.JiansNemesisMail, Element.Air, 30, 8, new ArmorStats(422, 14, 402, 14), null));
            armors.Add(Armor.GetNemesisArmor(Strings.LeviathansNemesis, Element.Air, 50, 20, new ArmorStats(247, 6, 253, 7), null));
            armors.Add(Armor.GetNemesisArmor(Strings.NerezzasNemesis, Element.Fire, 30, 8, null, null));
            armors.Add(Armor.GetNemesisArmor(Strings.NimbossNemesisArmor, Element.Earth, 30, 8, null, null));
            armors.Add(Armor.GetNemesisArmor(Strings.NotussNemesis, Element.Fire, 30, 8, new ArmorStats(180, 12, 177, 9), new ArmorStats(197, 194, 13, 10)));
            armors.Add(Armor.GetNemesisArmor(Strings.PhyrussNemesis, Element.Water, 30, 8, null, null));
            armors.Add(Armor.GetNemesisArmor(Strings.RaidersNemesisArmor, Element.Earth, 30, 8, new ArmorStats(153, 6, 187, 10), null));
            armors.Add(Armor.GetNemesisArmor(Strings.RavenlordsNemesis, Element.Fire, 50, 20, null, null));
            armors.Add(Armor.GetNemesisArmor(Strings.SasquatchsNemesis, Element.Fire, 30, 8, new ArmorStats(235, 10, 163, 8), null));
            armors.Add(Armor.GetNemesisArmor(Strings.SirensNemesisArmor, Element.Air, 30, 8, new ArmorStats(214, 10, 204, 10), null));
            armors.Add(Armor.GetNemesisArmor(Strings.SnowmansNemesis, Element.Air, 50, 20, new ArmorStats(283, 8, 251, 7), null));
            armors.Add(Armor.GetNemesisArmor(Strings.SwampDemonsNemesis, Element.Spirit, 50, 20, new ArmorStats(278, 8, 272, 7), null));
            armors.Add(Armor.GetNemesisArmor(Strings.TenTonsNemesisRobes, Element.Fire, 30, 8, new ArmorStats(215, 9, 150, 7), null));
            armors.Add(Armor.GetNemesisArmor(Strings.TortoisesNemesisArmor, Element.Spirit, 30, 8, new ArmorStats(214, 11, 214, 11), null));
            armors.Add(Armor.GetNemesisArmor(Strings.TrollKingsNemesis, Element.Spirit, 50, 20, new ArmorStats(264, 8, 220, 7), null));
            armors.Add(Armor.GetNemesisArmor(Strings.WanderersNemesis, Element.Water, 30, 8, null, null));
            armors.Add(Armor.GetNemesisArmor(Strings.WarriorsNemesis, Element.Water, 50, 20, null, null));
            armors.Add(Armor.GetNemesisArmor(Strings.WingWurmsNemesisArmor, Element.Spirit, 30, 8, new ArmorStats(196, 10, 225, 9), null));
            armors.Add(Armor.GetNemesisArmor(Strings.WyvernsNemesis, Element.Fire, 50, 20, new ArmorStats(252, 7, 300, 8), new ArmorStats(282, 335, 13, 14)));

            return armors;
        }

        #endregion

        #endregion
    }
}