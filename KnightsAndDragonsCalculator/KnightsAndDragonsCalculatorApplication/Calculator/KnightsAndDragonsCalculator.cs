using KnightsAndDragonsCalculatorApplication.Calculator.Containers;
using KnightsAndDragonsCalculatorApplication.Calculator.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KnightsAndDragonsCalculatorApplication.Calculator
{
    public class KnightsAndDragonsCalculator
    {

        public CalculatorResults Calculate(string targetArmorName, int targetLevel)
        {
            Armor targetArmor = ArmorTable.Instance.GetArmor(targetArmorName);
            if (targetArmor == null) return new CalculatorResults(Strings.ErrorArmorNotFound);
            if (targetLevel < 1 || targetLevel > targetArmor.MaxLevel) return new CalculatorResults(Strings.ErrorInvalidTargetLevel);

            StatsResults stats = GetStatsResults(targetArmor, targetLevel);

            return new CalculatorResults(stats);
        }

        public CalculatorResults Calculate(string targetArmorName, int startLevel, int targetLevel, string feederArmorName, int armorsmithCount, int armorsmithLevel)
        {
            Armor targetArmor = ArmorTable.Instance.GetArmor(targetArmorName);
            Armor feederArmor = ArmorTable.Instance.GetArmor(feederArmorName);
            if (targetArmor == null || feederArmor == null) return new CalculatorResults(Strings.ErrorArmorNotFound);
            if (targetLevel < 1 || targetLevel > targetArmor.MaxLevel) return new CalculatorResults(Strings.ErrorInvalidTargetLevel);
            if (startLevel < 1 || startLevel > targetLevel) return new CalculatorResults(Strings.ErrorInvalidStartLevel);
            if (armorsmithCount < 1) return new CalculatorResults(Strings.ErrorInvalidArmorsmithCount);
            if (armorsmithLevel < 1 || armorsmithLevel > 3) return new CalculatorResults(Strings.ErrorInvalidArmorsmithLevel);

            StatsResults stats = GetStatsResults(targetArmor, targetLevel);
            FeedResults feed = GetFeedResults(targetArmor.MaxLevel, startLevel, targetLevel, feederArmor.FeedCost, targetArmor.SameElementAs(feederArmor), feederArmor.CraftCost, feederArmor.MaterialCount, feederArmor.CraftTime, armorsmithCount, armorsmithLevel);

            return new CalculatorResults(stats, feed);
        }

        public CalculatorResults Calculate(int targetArmorMaxLevel, int startLevel, int targetLevel, int baseFeedCost, bool isSameElement, int armorsmithCount, int armorsmithLevel)
        {  
            if (targetLevel < 1 || targetLevel > targetArmorMaxLevel) return new CalculatorResults(Strings.ErrorInvalidTargetLevel);
            if (startLevel < 1 || startLevel > targetLevel) return new CalculatorResults(Strings.ErrorInvalidStartLevel);
            if (armorsmithCount < 1) return new CalculatorResults(Strings.ErrorInvalidArmorsmithCount);
            if (armorsmithLevel < 1 || armorsmithLevel > 3) return new CalculatorResults(Strings.ErrorInvalidArmorsmithLevel);

            int craftCost = Armor.GetCraftCostFromFeedCost(baseFeedCost);
            int materialCount = Armor.GetMaterialCountFromFeedCost(baseFeedCost);
            int craftTime = Armor.GetCraftTimeFromFeedCost(baseFeedCost);

            FeedResults feed = GetFeedResults(targetArmorMaxLevel, startLevel, targetLevel, baseFeedCost, isSameElement, craftCost, materialCount, craftTime, armorsmithCount, armorsmithLevel);

            return new CalculatorResults(feed);
        }

        public CalculatorResults Combine(string armorName1, string armorName2)
        {
            Armor armor1 = ArmorTable.Instance.GetArmor(armorName1);
            Armor armor2 = ArmorTable.Instance.GetArmor(armorName2);
            if (armor1 == null || armor2 == null) return new CalculatorResults(Strings.ErrorArmorNotFound);
            if (armor1.SameElementAs(armor2)) return new CalculatorResults(Strings.ErrorArmorSameElement);

            FusionResults fusion = GetFusionResults(armor1, armor2);

            return new CalculatorResults(fusion);
        }

        public CalculatorResults Split(string targetArmorName)
        {
            Armor targetArmor = ArmorTable.Instance.GetArmor(targetArmorName);
            if (targetArmor == null) return new CalculatorResults(Strings.ErrorArmorNotFound);

            FusionResults fusion = GetFusableResults(targetArmor);

            return new CalculatorResults(fusion);
        }

        public CalculatorResults Calculate(EpicBossRequest request)
        {
            // validation
            string epicBossValidationMessage = GetEpicBossDataValidationMessage(request.EpicBoss);
            if (!string.IsNullOrEmpty(epicBossValidationMessage)) return new CalculatorResults(epicBossValidationMessage);
            string guildValidationMessage = GetGuildDataValidationMessage(request.Guild);
            if (!string.IsNullOrEmpty(guildValidationMessage)) return new CalculatorResults(guildValidationMessage);
            string playerValidationMessage = GetPlayerDataValidationMessage(request.Player);
            if (!string.IsNullOrEmpty(playerValidationMessage)) return new CalculatorResults(playerValidationMessage);

            EpicBossResults epicBoss = GetEpicBossResults(request.EpicBoss, request.Guild, request.Player);

            return new CalculatorResults(epicBoss);
        }

        #region Enhancement

        private StatsResults GetStatsResults(Armor armor, int level)
        {
            StatsResults results = new StatsResults();
            if (armor.NormalStats != null)
            {
                results.NormalAttack = armor.GetNormalAttackAt(level);
                results.NormalDefense = armor.GetNormalDefenseAt(level);
            }
            if (armor.PlusStats != null)
            {
                results.PlusAttack = armor.GetPlusAttackAt(level);
                results.PlusDefense = armor.GetPlusDefenseAt(level);
            }
            return results;
        }

        private FeedResults GetFeedResults(int targetArmorMaxLevel, int startLevel, int targetLevel, int baseFeedCost, bool isSameElement)
        {
            return GetFeedResults(targetArmorMaxLevel, startLevel, targetLevel, baseFeedCost, isSameElement, 0, 0, 0, 1, 1);
        }

        private FeedResults GetFeedResults(int targetArmorMaxLevel, int startLevel, int targetLevel, int baseFeedCost, bool isSameElement, int craftCost, int materialCount, int craftTime, int armorsmithCount, int armorsmithLevel)
        {
            FeedResults results = new FeedResults();

            int feedCost = (isSameElement) ? (int)(baseFeedCost * 1.25) : baseFeedCost;
            int totalFeedCost = LevelTable.Instance.GetTotalFeedCost(targetArmorMaxLevel, startLevel, targetLevel);
            if (feedCost <= 0 || totalFeedCost <= 0) return results;

            int feedCount = (int)Math.Ceiling(totalFeedCost / (decimal)feedCost);

            results.FeedCount = feedCount;

            // special case: non-craftable rare/super rare
            int craftCount = feedCount;
            if (baseFeedCost == 40 && craftCost == 0)
            {
                craftCost = Armor.GetCraftCostFromFeedCost(5);
                craftTime = Armor.GetCraftTimeFromFeedCost(5);
                materialCount = Armor.GetMaterialCountFromFeedCost(5);
                craftCount = feedCount * 2;

                results.FusionCount = feedCount;
                results.FusionCost = feedCount * 25000;
            }

            results.CraftCost = craftCost * craftCount;
            results.MaterialCount = materialCount * craftCount;
            results.CraftTime = GetCraftTime(craftTime, craftCount, armorsmithCount, armorsmithLevel);

            int currentLevel = startLevel;
            int cumulativeJump = LevelTable.Instance.GetExperienceFromLevel(targetArmorMaxLevel, startLevel);
            while (feedCount > 0)
            {
                Level level = LevelTable.Instance.GetLevel(currentLevel);
                if (level == null) break;

                int count = (feedCount > 4) ? 4 : feedCount;

                results.FeedCost += level.Gold * count;
                cumulativeJump += feedCost * count;
                currentLevel = LevelTable.Instance.GetLevelFromExperience(targetArmorMaxLevel, cumulativeJump);

                feedCount -= count;
            }
            return results;
        }

        private decimal GetCraftTime(int baseCraftTime, int craftCount, int armorsmithCount, int armorsmithLevel)
        {
            int craftRounds = craftCount / armorsmithCount;
            int craftLeftOvers = craftCount % armorsmithCount;

            decimal craftTime = baseCraftTime * (craftRounds + (craftLeftOvers > 0 ? 1 : 0)) * (1 - ((armorsmithLevel - 1) * 0.05m));
           
            return craftTime;
        }

        #endregion

        #region Fusion

        private FusionResults GetFusionResults(Armor armor1, Armor armor2)
        {
            FusionResults results = new FusionResults();

            results.Armors = ArmorTable.Instance.GetFusionArmors(armor1, armor2, GetFusionRarities(armor1.Rarity, armor2.Rarity));

            return results;
        }

        private FusionResults GetFusableResults(Armor targetArmor)
        {
            FusionResults results = new FusionResults();

            results.Armors = ArmorTable.Instance.GetFusableArmors(targetArmor, GetFusableRarities(targetArmor.Rarity));

            return results;
        }

        private List<Rarity> GetFusionRarities(Rarity rarity1, Rarity rarity2)
        {
            List<Rarity> rarities = new List<Rarity>();
            AddRarity(rarities, rarity1);
            AddRarity(rarities, GetPreviousRarity(rarity1));
            AddRarity(rarities, GetNextRarity(rarity1));
            AddRarity(rarities, GetNextRarity(GetNextRarity(rarity1)));
            if (rarity1 != rarity2)
            {
                AddRarity(rarities, rarity2);
                AddRarity(rarities, GetPreviousRarity(rarity2));
                AddRarity(rarities, GetNextRarity(rarity2));
                AddRarity(rarities, GetNextRarity(GetNextRarity(rarity2)));
            }
            return rarities;
        }

        private List<Rarity> GetFusableRarities(Rarity rarity)
        {
            List<Rarity> rarities = new List<Rarity>();
            AddRarity(rarities, rarity);
            AddRarity(rarities, GetPreviousRarity(rarity));
            AddRarity(rarities, GetPreviousRarity(GetPreviousRarity(rarity)));
            AddRarity(rarities, GetNextRarity(rarity));
            return rarities;
        }

        private void AddRarity(List<Rarity> rarities, Rarity rarity)
        {
            if (!rarities.Contains(rarity)) rarities.Add(rarity);
        }

        private Rarity GetNextRarity(Rarity current)
        {
            switch (current)
            {
                case Rarity.Common:
                    return Rarity.Uncommon;
                case Rarity.Uncommon:
                    return Rarity.Rare;
                case Rarity.Rare:
                    return Rarity.SuperRare;
                case Rarity.SuperRare:
                    return Rarity.UltraRare;
                case Rarity.UltraRare:
                    return Rarity.Legendary;
                case Rarity.Legendary:
                    return Rarity.Epic;
                default:
                    return current;
            }
        }

        private Rarity GetPreviousRarity(Rarity current)
        {
            switch (current)
            {
                case Rarity.Uncommon:
                    return Rarity.Common;
                case Rarity.Rare:
                    return Rarity.Uncommon;
                case Rarity.SuperRare:
                    return Rarity.Rare;
                case Rarity.UltraRare:
                    return Rarity.SuperRare;
                case Rarity.Legendary:
                    return Rarity.UltraRare;
                case Rarity.Epic:
                    return Rarity.Legendary;
                default:
                    return current;
            }
        }

        #endregion

        #region Epic Boss

        private EpicBossResults GetEpicBossResults(EpicBoss epicBoss, Guild guild, Player player)
        {
            EpicBossResults results = new EpicBossResults();
            results.Items = new List<EpicBossResultItem>();

            int playerHealth = GetPlayerHealth(player.Level);
            int playerStats = GetPlayerStats(player.Level);
            int followerHealth = GetFollowerHealth(player.Level);
            int followerStats = GetFollowerStats(player.Level);
            decimal baseAttack = GetBaseAttack(player.Level);
            decimal knightBonus = GetKnightBonus(player.KnightCount);

            foreach (PlayerArmor playerArmor in player.Armors)
            {
                Armor armor = ArmorTable.Instance.GetArmor(playerArmor.ArmorName);

                KeyValuePair<int, int> armorStats = GetArmorStats(armor, playerArmor.Level, playerArmor.IsPlus);
                decimal guildRankBonus = GetGuildRankBonus(guild.RankBonus);
                decimal guildElementBonus = GetGuildElementBonus(armor, guild);
                decimal playerBonus = GetPlayerBonus(epicBoss.Element1, epicBoss.Element2, armor, playerArmor.IsNemesis);
                decimal bossLevelBonus = GetBossLevelBonus(epicBoss.Level);
                decimal bossElementBonus = GetBossElementBonus(epicBoss, armor);

                int playerDamageDone = GetDamageDone(baseAttack, armorStats.Key + playerStats, epicBoss.Defense, guildRankBonus, guildElementBonus, playerBonus, knightBonus);
                int followerDamageDone = GetDamageDone(baseAttack, armorStats.Key + followerStats, epicBoss.Defense, guildRankBonus, guildElementBonus, playerBonus, knightBonus);

                int playerDamageTaken = GetDamageTaken(armorStats.Value + playerStats, epicBoss.Attack, guildRankBonus, guildElementBonus, bossLevelBonus, bossElementBonus);
                int followerDamageTaken = GetDamageTaken(armorStats.Value + followerStats, epicBoss.Attack, guildRankBonus, guildElementBonus, bossLevelBonus, bossElementBonus);

                int playerHitsTaken = (playerHealth / playerDamageTaken) + 1;
                int followerHitsTaken = (followerHealth / followerDamageTaken) + 1;

                results.Items.Add(new EpicBossResultItem(armor.Name, armor.SafeName, playerDamageDone, playerDamageTaken, playerHitsTaken, followerDamageDone, followerDamageTaken, followerHitsTaken));
            }
            results.Items.Sort((x, y) => -1 * x.PlayerTotalDamageDone.CompareTo(y.PlayerTotalDamageDone));

            return results;
        }

        private string GetEpicBossDataValidationMessage(EpicBoss epicBoss)
        {
            if (epicBoss == null) return Strings.ErrorBossDataNotProvided;
            if (epicBoss.Level < 1 || epicBoss.Level > 60) return Strings.ErrorInvalidBossLevel;
            if (epicBoss.Attack < 1 || epicBoss.Defense < 1 || epicBoss.Health < 1) return Strings.ErrorInvalidBossStats;


            return string.Empty;
        }

        private string GetGuildDataValidationMessage(Guild guild)
        {
            if (guild == null) return Strings.ErrorGuildDataNotProvided;

            return string.Empty;
        }

        private string GetPlayerDataValidationMessage(Player player)
        {
            if (player == null) return Strings.ErrorPlayerDataNotProvided;
            if (player.Level < 1 || player.Level > 100) return Strings.ErrorInvalidPlayerLevel;
            if (player.KnightCount < 1 || player.KnightCount > 5) return Strings.ErrorInvalidPlayerKnightCount;
            if (player.Armors == null || player.Armors.Count <= 0) return Strings.ErrorPlayerArmorsDataNotProvided;

            foreach (PlayerArmor playerArmor in player.Armors)
            {
                Armor armor = ArmorTable.Instance.GetArmor(playerArmor.ArmorName);
                if (armor == null) return string.Format(Strings.ErrorPlayerArmorNotFound, playerArmor.ArmorName);
                if (playerArmor.Level < 1 || playerArmor.Level > armor.MaxLevel) return string.Format(Strings.ErrorInvalidPlayerArmorLevel, playerArmor.ArmorName, armor.MaxLevel);
                if (playerArmor.IsNemesis && armor.Rarity != Rarity.Nemesis) return string.Format(Strings.ErrorInvalidIsNemesis, playerArmor.ArmorName);
                if ((playerArmor.IsPlus && armor.PlusStats == null) || (!playerArmor.IsPlus && armor.NormalStats == null)) return string.Format(Strings.ErrorArmorStatsNotAvailable, playerArmor.ArmorName);
            }

            return string.Empty;
        }

        private int GetPlayerHealth(int level)
        {
            return GetHealth(level, 607, 5.5m);
        }

        private int GetFollowerHealth(int level)
        {
            return GetHealth(level, 455, 4);
        }

        private int GetHealth(int level, int maxHealth, decimal jump)
        {
            return (int)(maxHealth - (jump * (100 - level)));
        }

        private int GetPlayerStats(int level)
        {
            return GetStats(level, 316); 
        }

        private int GetFollowerStats(int level)
        {
            return GetStats(level, 237);
        }

        private int GetStats(int level, int maxStats)
        {
            // TODO work out player stats level equation
            return maxStats;
        }

        private KeyValuePair<int, int> GetArmorStats(Armor armor, int level, bool isPlus)
        {
            int armorAttack = (isPlus) ? armor.GetPlusAttackAt(level) : armor.GetNormalAttackAt(level);
            int armorDefense = (isPlus) ? armor.GetPlusDefenseAt(level) : armor.GetNormalDefenseAt(level);

            return new KeyValuePair<int, int>(armorAttack, armorDefense);
        }

        private decimal GetGuildRankBonus(int rankBonus)
        {
            return 1 + (rankBonus / 100m);
        }

        private decimal GetGuildElementBonus(Armor armor, Guild guild)
        {
            decimal elementBonus = 1;
            if (armor.Element1 != Element.All)
            {
                if (armor.HasElement(Element.Air)) elementBonus += (guild.AirBonus / 100m);
                if (armor.HasElement(Element.Earth)) elementBonus += (guild.EarthBonus / 100m);
                if (armor.HasElement(Element.Fire)) elementBonus += (guild.FireBonus / 100m);
                if (armor.HasElement(Element.Spirit)) elementBonus += (guild.SpiritBonus / 100m);
                if (armor.HasElement(Element.Water)) elementBonus += (guild.WaterBonus / 100m);
            }

            return elementBonus;
        }

        private decimal GetPlayerBonus(Element bossElement1, Element? bossElement2, Armor armor, bool isNemesis)
        {
            if (isNemesis) return 4.5m;
            if (armor.Element1 == Element.All) return 1.5m;
            bool isStrongAgainstElement1 = armor.IsStrongAgainst(bossElement1);
            bool isStrongAgainstElement2 = bossElement2 != null && armor.IsStrongAgainst(bossElement2.Value);
            return 1m + (isStrongAgainstElement1 ? 0.5m : 0m) + (isStrongAgainstElement2 ? 0.5m : 0m);
        }

        private decimal GetKnightBonus(int count)
        {
            return 1 + ((count - 1) * 0.25m);
        }

        private decimal GetBossLevelBonus(int level)
        {
            decimal bossLevel = GetBossLevel(level);
            return (level < 20) ? 3 * (bossLevel + 4) : (1.6m * bossLevel) + 44;
        }

        private decimal GetBossElementBonus(EpicBoss epicBoss, Armor armor)
        {
            decimal elementBonus = 1;
            if (armor.IsWeakAgainst(epicBoss.Element1)) elementBonus += 0.5m;
            if (epicBoss.Element2 != null && armor.IsWeakAgainst(epicBoss.Element2.Value)) elementBonus += 0.5m;

            return elementBonus;
        }

        private decimal GetBossLevel(int level)
        {
            switch (level)
            {
                case 10:
                    return 11.5m;
                case 15:
                    return 18m;
                case 21:
                    return 24m;
                case 28:
                    return 31m;
                case 35:
                    return 39m;
                case 43:
                    return 47m;
                case 51:
                    return 55m;
                case 60:
                    return 67m;
                default:
                    return level;
            }
        }

        private int GetDamageDone(decimal baseAttack, int playerAttack, int bossDefense, decimal guildRankBonus, decimal guildElementBonus, decimal playerBonus, decimal knightBonus)
        {
            int knightStat = GetKnightStat(playerAttack, guildRankBonus, guildElementBonus);
            return (int)Math.Floor(Math.Floor(baseAttack * (knightStat / (decimal)bossDefense) * playerBonus) * knightBonus);
        }

        private decimal GetBaseAttack(int level)
        {
            return (1.6m * level) + 4;
        }

        private int GetDamageTaken(int playerDefense, int bossAttack, decimal guildRankBonus, decimal guildElementBonus, decimal bossLevelBonus, decimal bossElementBonus)
        {
            int knightStat = GetKnightStat(playerDefense, guildRankBonus, guildElementBonus);
            int damageTaken = (int)Math.Floor(Math.Floor((bossAttack / (decimal)knightStat) * bossLevelBonus) * bossElementBonus);
            return (damageTaken > 0) ? damageTaken : 1;
        }

        public int GetKnightStat(int stat, decimal guildRankBonus, decimal guildElementBonus)
        {
            return (int)Math.Ceiling(Math.Ceiling(stat * guildElementBonus) * guildRankBonus);
        }

        #endregion
    }
}