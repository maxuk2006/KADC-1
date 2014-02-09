using KnightsAndDragonsCalculatorApplication.Calculator;
using KnightsAndDragonsCalculatorApplication.Calculator.Containers;
using KnightsAndDragonsCalculatorApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KnightsAndDragonsCalculatorApplication.Controllers
{
    public class EpicBossTestController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            EpicBossTestModel model = new EpicBossTestModel();
            model.Differences = GetDifferences();

            return View(model);
        }

        private List<KeyValuePair<int, int>> GetDifferences()
        {
            List<KeyValuePair<int, int>> differences = new List<KeyValuePair<int, int>>();
            differences.Add(TestMethod24());
            differences.Add(TestMethod25());
            differences.Add(TestMethod26());
            differences.Add(TestMethod27());
            differences.Add(TestMethod28());
            differences.Add(TestMethod29());
            differences.Add(TestMethod30());
            differences.Add(TestMethod31());
            differences.Add(TestMethod32());
            differences.Add(TestMethod33());
            differences.Add(TestMethod34());
            differences.Add(TestMethod35());
            differences.Add(TestMethod36());
            differences.Add(TestMethod37());
            differences.Add(TestMethod38());
            differences.Add(TestMethod39());
            differences.Add(TestMethod40());
            differences.Add(TestMethod41());
            differences.Add(TestMethod42());
            differences.Add(TestMethod43());
            return differences;
        }

        public KeyValuePair<int, int> TestMethod24()
        {

            EpicBossRequest request = GetJackFrostGuardiansBattlegearRequest(24, 1158, 502, 3, 63);

            CalculatorResults results = new KnightsAndDragonsCalculator().Calculate(request);

            return new KeyValuePair<int, int>(results.EpicBoss.Items[0].PlayerDamageDone - 1165, results.EpicBoss.Items[0].PlayerDamageTaken - 56);
        }

        public KeyValuePair<int, int> TestMethod25()
        {

            EpicBossRequest request = GetJackFrostGuardiansBattlegearRequest(25, 1189, 511, 3, 63);

            CalculatorResults results = new KnightsAndDragonsCalculator().Calculate(request);

            return new KeyValuePair<int, int>(results.EpicBoss.Items[0].PlayerDamageDone - 1144, results.EpicBoss.Items[0].PlayerDamageTaken - 59);
        }

        public KeyValuePair<int, int> TestMethod26()
        {

            EpicBossRequest request = GetJackFrostGuardiansBattlegearRequest(26, 1216, 516, 3, 63);

            CalculatorResults results = new KnightsAndDragonsCalculator().Calculate(request);

            return new KeyValuePair<int, int>(results.EpicBoss.Items[0].PlayerDamageDone - 1134, results.EpicBoss.Items[0].PlayerDamageTaken - 61);
        }

        public KeyValuePair<int, int> TestMethod27()
        {

            EpicBossRequest request = GetJackFrostGuardiansBattlegearRequest(27, 1244, 522, 4, 63);

            CalculatorResults results = new KnightsAndDragonsCalculator().Calculate(request);

            return new KeyValuePair<int, int>(results.EpicBoss.Items[0].PlayerDamageDone - 1307, results.EpicBoss.Items[0].PlayerDamageTaken - 64);
        }

        public KeyValuePair<int, int> TestMethod28()
        {

            EpicBossRequest request = GetJackFrostGuardiansBattlegearRequest(28, 1352, 544, 4, 63);

            CalculatorResults results = new KnightsAndDragonsCalculator().Calculate(request);

            return new KeyValuePair<int, int>(results.EpicBoss.Items[0].PlayerDamageDone - 1254, results.EpicBoss.Items[0].PlayerDamageTaken - 73);
        }

        public KeyValuePair<int, int> TestMethod29()
        {

            EpicBossRequest request = GetJackFrostGuardiansBattlegearRequest(29, 1297, 534, 4, 63);

            CalculatorResults results = new KnightsAndDragonsCalculator().Calculate(request);

            return new KeyValuePair<int, int>(results.EpicBoss.Items[0].PlayerDamageDone - 1279, results.EpicBoss.Items[0].PlayerDamageTaken - 69);
        }

        public KeyValuePair<int, int> TestMethod30()
        {

            EpicBossRequest request = GetJackFrostGuardiansBattlegearRequest(30, 1325, 539, 4, 64);

            CalculatorResults results = new KnightsAndDragonsCalculator().Calculate(request);

            return new KeyValuePair<int, int>(results.EpicBoss.Items[0].PlayerDamageDone - 1277, results.EpicBoss.Items[0].PlayerDamageTaken - 71);
        }

        public KeyValuePair<int, int> TestMethod31()
        {

            EpicBossRequest request = GetJackFrostGuardiansBattlegearRequest(31, 1352, 544, 4, 64);

            CalculatorResults results = new KnightsAndDragonsCalculator().Calculate(request);

            return new KeyValuePair<int, int>(results.EpicBoss.Items[0].PlayerDamageDone - 1265, results.EpicBoss.Items[0].PlayerDamageTaken - 74);
        }

        public KeyValuePair<int, int> TestMethod32()
        {

            EpicBossRequest request = GetJackFrostGuardiansBattlegearRequest(32, 1379, 551, 5, 64);

            CalculatorResults results = new KnightsAndDragonsCalculator().Calculate(request);

            return new KeyValuePair<int, int>(results.EpicBoss.Items[0].PlayerDamageDone - 1428, results.EpicBoss.Items[0].PlayerDamageTaken - 77);
        }

        public KeyValuePair<int, int> TestMethod33()
        {

            EpicBossRequest request = GetJackFrostGuardiansBattlegearRequest(33, 1405, 556, 5, 64);

            CalculatorResults results = new KnightsAndDragonsCalculator().Calculate(request);

            return new KeyValuePair<int, int>(results.EpicBoss.Items[0].PlayerDamageDone - 1414, results.EpicBoss.Items[0].PlayerDamageTaken - 80);
        }

        public KeyValuePair<int, int> TestMethod34()
        {

            EpicBossRequest request = GetJackFrostGuardiansBattlegearRequest(34, 1433, 562, 5, 64);

            CalculatorResults results = new KnightsAndDragonsCalculator().Calculate(request);

            return new KeyValuePair<int, int>(results.EpicBoss.Items[0].PlayerDamageDone - 1400, results.EpicBoss.Items[0].PlayerDamageTaken - 83);
        }

        public KeyValuePair<int, int> TestMethod35()
        {

            EpicBossRequest request = GetJackFrostGuardiansBattlegearRequest(35, 1568, 591, 5, 64);

            CalculatorResults results = new KnightsAndDragonsCalculator().Calculate(request);

            return new KeyValuePair<int, int>(results.EpicBoss.Items[0].PlayerDamageDone - 1330, results.EpicBoss.Items[0].PlayerDamageTaken - 98);
        }

        public KeyValuePair<int, int> TestMethod36()
        {

            EpicBossRequest request = GetJackFrostGuardiansBattlegearRequest(36, 1487, 573, 5, 68);

            CalculatorResults results = new KnightsAndDragonsCalculator().Calculate(request);

            return new KeyValuePair<int, int>(results.EpicBoss.Items[0].PlayerDamageDone - 1412, results.EpicBoss.Items[0].PlayerDamageTaken - 86);
        }

        public KeyValuePair<int, int> TestMethod37()
        {

            EpicBossRequest request = GetJackFrostGuardiansBattlegearRequest(37, 1515, 579, 5, 68);

            CalculatorResults results = new KnightsAndDragonsCalculator().Calculate(request);

            return new KeyValuePair<int, int>(results.EpicBoss.Items[0].PlayerDamageDone - 1398, results.EpicBoss.Items[0].PlayerDamageTaken - 89);
        }

        public KeyValuePair<int, int> TestMethod38()
        {

            EpicBossRequest request = GetJackFrostGuardiansBattlegearRequest(38, 1541, 584, 5, 68);

            CalculatorResults results = new KnightsAndDragonsCalculator().Calculate(request);

            return new KeyValuePair<int, int>(results.EpicBoss.Items[0].PlayerDamageDone - 1386, results.EpicBoss.Items[0].PlayerDamageTaken - 92);
        }

        public KeyValuePair<int, int> TestMethod39()
        {

            EpicBossRequest request = GetJackFrostGuardiansBattlegearRequest(39, 1568, 591, 5, 68);

            CalculatorResults results = new KnightsAndDragonsCalculator().Calculate(request);

            return new KeyValuePair<int, int>(results.EpicBoss.Items[0].PlayerDamageDone - 1370, results.EpicBoss.Items[0].PlayerDamageTaken - 95);
        }

        public KeyValuePair<int, int> TestMethod40()
        {

            EpicBossRequest request = GetJackFrostGuardiansBattlegearRequest(40, 1595, 596, 5, 70);

            CalculatorResults results = new KnightsAndDragonsCalculator().Calculate(request);

            return new KeyValuePair<int, int>(results.EpicBoss.Items[0].PlayerDamageDone - 1378, results.EpicBoss.Items[0].PlayerDamageTaken - 97);
        }

        public KeyValuePair<int, int> TestMethod41()
        {

            EpicBossRequest request = GetJackFrostGuardiansBattlegearRequest(41, 1623, 601, 5, 70);

            CalculatorResults results = new KnightsAndDragonsCalculator().Calculate(request);

            return new KeyValuePair<int, int>(results.EpicBoss.Items[0].PlayerDamageDone - 1366, results.EpicBoss.Items[0].PlayerDamageTaken - 100);
        }

        public KeyValuePair<int, int> TestMethod42()
        {

            EpicBossRequest request = GetJackFrostGuardiansBattlegearRequest(42, 1650, 607, 5, 70);

            CalculatorResults results = new KnightsAndDragonsCalculator().Calculate(request);

            return new KeyValuePair<int, int>(results.EpicBoss.Items[0].PlayerDamageDone - 1352, results.EpicBoss.Items[0].PlayerDamageTaken - 103);
        }

        public KeyValuePair<int, int> TestMethod43()
        {

            EpicBossRequest request = GetJackFrostGuardiansBattlegearRequest(43, 1786, 636, 5, 70);

            CalculatorResults results = new KnightsAndDragonsCalculator().Calculate(request);

            return new KeyValuePair<int, int>(results.EpicBoss.Items[0].PlayerDamageDone - 1290, results.EpicBoss.Items[0].PlayerDamageTaken - 120);
        }

        private EpicBossRequest GetJackFrostGuardiansBattlegearRequest(int bossLevel, int bossAttack, int bossDefense, int knightCount, int armorLevel)
        {
            EpicBossRequest request = new EpicBossRequest();
            request.EpicBoss = GetJackFrostBoss(bossLevel, bossAttack, bossDefense);
            request.Guild = GetGuild();
            request.Player = GetPlayer(knightCount, GetGuardiansBattlegear(armorLevel));
            return request;
        }

        private EpicBoss GetJackFrostBoss(int level, int attack, int defense)
        {
            EpicBoss boss = new EpicBoss();
            boss.Element1 = Element.Air;
            boss.Level = level;
            boss.Attack = attack;
            boss.Defense = defense;
            boss.Health = 1;
            return boss;
        }

        private Guild GetGuild()
        {
            Guild guild = new Guild();
            guild.RankBonus = 5;
            guild.AirBonus = 6;
            guild.EarthBonus = 6;
            guild.FireBonus = 6;
            guild.SpiritBonus = 6;
            guild.WaterBonus = 6;
            return guild;
        }

        private Player GetPlayer(int knightCount, PlayerArmor armor)
        {
            Player player = new Player();
            player.Level = 100;
            player.KnightCount = knightCount;
            player.Armors = new List<PlayerArmor>();
            player.Armors.Add(armor);
            return player;
        }

        private PlayerArmor GetArmorOfInfernalLord()
        {
            PlayerArmor armor = new PlayerArmor();
            armor.ArmorName = "Armor of Infernal Lord";
            armor.Level = 70;
            armor.IsPlus = true;
            return armor;
        }

        private PlayerArmor GetGuardiansBattlegear(int level)
        {
            PlayerArmor armor = new PlayerArmor();
            armor.ArmorName = "Guardian's Battlegear";
            armor.Level = level;
            return armor;
        }

        private PlayerArmor GetCriusArmor()
        {
            PlayerArmor armor = new PlayerArmor();
            armor.ArmorName = "Crius Armor";
            armor.Level = 50;
            armor.IsPlus = true;
            return armor;
        }
	}
}