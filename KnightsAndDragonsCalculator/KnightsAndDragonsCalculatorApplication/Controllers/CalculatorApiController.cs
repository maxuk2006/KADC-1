using KnightsAndDragonsCalculatorApplication.Calculator;
using KnightsAndDragonsCalculatorApplication.Calculator.Containers;
using KnightsAndDragonsCalculatorApplication.Calculator.Tables;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace KnightsAndDragonsCalculatorApplication.Controllers
{
    public class CalculatorApiController : ApiController
    {
        KnightsAndDragonsCalculator _calculator;

        /// <summary>
        /// Calculate result of enhancing target armor to target level.
        /// </summary>
        /// <param name="targetArmorName">Name of armor to enhance</param>
        /// <param name="targetLevel">Level to enhance to</param>
        /// <returns>Results of calculation</returns>
        [HttpGet]
        [Route("api/calculate/{targetArmorName}/{targetLevel}")]
        public CalculatorResults Calculate(string targetArmorName, int targetLevel)
        {
            return GetCalculator().Calculate(targetArmorName, targetLevel);
        }

        /// <summary>
        /// Calculate result of enhancing target armor to target level using another armor. 
        /// </summary>
        /// <param name="targetArmorName">Name of armor to enhance</param>
        /// <param name="startLevel">Level to enhance from</param>
        /// <param name="targetLevel">Level to enhance to</param>
        /// <param name="feederArmorName">Name of armor used to enhance target armor</param>
        /// <returns>Results of calculation</returns>
        /// 
        [HttpGet]
        [Route("api/calculate/{targetArmorName}/{startLevel}/{targetLevel}/{feederArmorName}/{armorSmithCount}/{armorSmithLevel}")]
        public CalculatorResults Calculate(string targetArmorName, int startLevel, int targetLevel, string feederArmorName, int armorsmithCount, int armorsmithLevel)
        {
            return GetCalculator().Calculate(targetArmorName, startLevel, targetLevel, feederArmorName, armorsmithCount, armorsmithLevel);
        }

        /// <summary>
        /// Calculate result of enhancing an armor of target rarity using provided feed parameters
        /// </summary>
        /// <param name="targetArmorMaxLevel">Maximum level of target armor</param>
        /// <param name="startLevel">Level to enhance from</param>
        /// <param name="targetLevel">Level to enhance to</param>
        /// <param name="baseFeedCost">Base feed EP to enhance with</param>
        /// <param name="isSameElement">Do armors used in enhancement have same element</param>
        /// <param name="armorsmithCount">Number of armorsmiths</param>
        /// <param name="armorsmithLevel">Level of armorsmiths</param>
        /// <returns>Results of calculation</returns>
        [HttpGet]
        [Route("api/calculate/{targetArmorMaxLevel}/{startLevel}/{targetLevel}/{baseFeedCost}/{isSameElement}/{armorSmithCount}/{armorSmithLevel}")]
        public CalculatorResults Calculate(int targetArmorMaxLevel, int startLevel, int targetLevel, int baseFeedCost, bool isSameElement, int armorsmithCount, int armorsmithLevel)
        {
            return GetCalculator().Calculate(targetArmorMaxLevel, startLevel, targetLevel, baseFeedCost, isSameElement, armorsmithCount, armorsmithLevel);
        }

        /// <summary>
        /// Calculate damage done/taken against epic boss using specific armors
        /// </summary>
        /// <param name="request">Request data</param>
        /// <returns>Results of calculation</returns>
        [HttpPost]
        [Route("api/calculate/epicBoss")]
        public CalculatorResults Calculate(EpicBossRequest request)
        {
            return GetCalculator().Calculate(request);
        }

        /// <summary>
        /// Calculate the result of combining 2 armors.
        /// </summary>
        /// <param name="armorName1">Name of 1st armor</param>
        /// <param name="armorName2">Name of 2nd armor</param>
        /// <returns>Results of fusion combination</returns>
        [HttpGet]
        [Route("api/combine/{armorName1}/{armorName2}")]
        public CalculatorResults Combine(string armorName1, string armorName2)
        {
            return GetCalculator().Combine(armorName1, armorName2);
        }

        /// <summary>
        /// Calculate the armors that can be use to combine to get target armor
        /// </summary>
        /// <param name="targetArmorName">Target armor name</param>
        /// <returns>Results of fusion split</returns>
        [HttpGet]
        [Route("api/split/{targetArmorName}")]
        public CalculatorResults Split(string targetArmorName)
        {
            return GetCalculator().Split(targetArmorName);
        }
 
        /// <summary>
        /// Gets armor from armor name.
        /// </summary>
        /// <param name="armorName">Name of armor</param>
        /// <returns>Matched armor</returns>
        [HttpGet]
        [Route("api/armorData/{armorName}")]
        public Armor GetArmor(string armorName)
        {
            return ArmorTable.Instance.GetArmor(armorName);
        }

        /// <summary>
        /// Get all level data
        /// </summary>
        /// <returns>List of level data</returns>
        [HttpGet]
        [Route("api/levelData")]
        public List<Level> GetLevelData()
        {
            return LevelTable.Instance.GetLevels();
        }

        /// <summary>
        /// Get all armor data
        /// </summary>
        /// <returns>List of armor data</returns>
        [HttpGet]
        [Route("api/armorData")]
        public List<Armor> GetArmorData()
        {
            return ArmorTable.Instance.GetArmors();
        }

        private KnightsAndDragonsCalculator GetCalculator()
        {
            if (_calculator == null)
            {
                _calculator = new KnightsAndDragonsCalculator();
            }
            return _calculator;
        }
    }
}
