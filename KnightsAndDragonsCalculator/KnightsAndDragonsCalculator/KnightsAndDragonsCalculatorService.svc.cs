using KnightsAndDragonsCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace KnightsAndDragonsCalculator
{
    public class KnightsAndDragonsCalculatorService : IKnightsAndDragonsCalculatorService
    {
        public KnightsAndDragonsCalculatorService()
        {
            LevelTable.Initialize();
            ArmorList.Initialize();
        }

        //public CalculatorResults Calculate1(Armor armor, int targetLevel)
        //{
        //    return CalculateStats(armor, targetLevel);
        //}

        public CalculatorResults CalculateStats(string armorName, string targetLevel)
        {
            return Calculate(ArmorList.GetArmor(armorName), int.Parse(targetLevel));
        }

        //public CalculatorResults Calculate3(Armor armor, int targetLevel, Armor feedArmor)
        //{
        //    return CalculateStats(armor, targetLevel, feedArmor);
        //}

        public CalculatorResults CalculateStatsAndCost(string armorName, string targetLevel, string feedArmorName)
        {
            return Calculate(ArmorList.GetArmor(armorName), int.Parse(targetLevel), ArmorList.GetArmor(feedArmorName));
        }

        public List<string> GetArmorNames()
        {
            return ArmorList.GetArmorNames();
        }

        private CalculatorResults Calculate(Armor armor, int targetLevel)
        {
            if (armor == null) return new CalculatorResults(Strings.ArmorNotFound);

            CalculatorResults results = new CalculatorResults();
            if (armor.NormalStats != null)
            {
                results.NormalAttack = Calculate(armor.NormalStats.AttackStart, armor.NormalStats.AttackUp, targetLevel);
                results.NormalDefense = Calculate(armor.NormalStats.DefenseStart, armor.NormalStats.DefenseUp, targetLevel);
            }
            if (armor.PlusStats != null)
            {
                results.PlusAttack = Calculate(armor.PlusStats.AttackStart, armor.PlusStats.AttackUp, targetLevel);
                results.PlusDefense = Calculate(armor.PlusStats.DefenseStart, armor.PlusStats.DefenseUp, targetLevel);
            }
            return results;
        }

        private CalculatorResults Calculate(Armor armor, int targetLevel, Armor feedArmor)
        {
            if (armor == null || feedArmor == null) return new CalculatorResults(Strings.ArmorNotFound);

            CalculatorResults results = Calculate(armor, targetLevel);

            int feedCost = GetFeedCost(armor, feedArmor);
            if (feedCost <= 0) return new CalculatorResults(Strings.ArmorDataMissing);

            int totalFeedCost = LevelTable.GetTotalFeedCost(armor.Rarity, targetLevel);
            if (totalFeedCost <= 0) return new CalculatorResults(Strings.LevelDataMissing);

            int feedCount = (int)Math.Ceiling((decimal)(totalFeedCost / feedCost));

            results.FeedCount = feedCount;
            results.CraftCost = feedArmor.CraftCost * feedCount;
            results.MaterialCount = feedArmor.MaterialCount * feedCount;

            int currentLevel = 1;
            int cumulativeJump = 0;
            while (feedCount > 0)
            {
                Level level = LevelTable.GetLevel(currentLevel);
                if (level == null) break;

                int count = (feedCount > 4) ? 4 : feedCount;

                results.FeedCost += level.Gold * count;
                cumulativeJump += feedCost * count;
                currentLevel = LevelTable.GetLevelFromExperience(armor.Rarity, cumulativeJump);

                feedCount -= count;
            }

            return results;
        }

        private int Calculate(int start, int up, int level)
        {
            return start + (up * (level - 1));
        }

        private int GetFeedCost(Armor armor, Armor feedArmor)
        {
            int feedCost = feedArmor.FeedCost;
            if (armor.SameElementAs(feedArmor))
            {
                feedCost = (int)(feedCost * 1.25);
            }
            return feedCost;
        }
    }
}
