using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KnightsAndDragonsCalculatorApplication.Calculator.Containers
{
    public class EpicBossResultItem
    {
        public string ArmorName { get; set; }
        public string ArmorImageName { get; set; }
        public int PlayerDamageDone { get; set; }
        public int FollowerDamageDone { get; set; }
        public int PlayerDamageTaken { get; set; }
        public int FollowerDamageTaken { get; set; }
        public int PlayerHitsTaken { get; set; }
        public int FollowerHitsTaken { get; set; }
        public int PlayerTotalDamageDone { get { return PlayerDamageDone * (PlayerHitsTaken - 1); } }
        public int FollowerTotalDamageDone { get { return FollowerDamageDone * (FollowerHitsTaken - 1); } }

        public EpicBossResultItem(string armorName, string armorImageName, int playerDamageDone, int playerDamageTaken, int playerHitsTaken, int followerDamageDone, int followerDamageTaken, int followerHitsTaken)
        {
            ArmorName = armorName;
            ArmorImageName = armorImageName;
            PlayerDamageDone = playerDamageDone;
            PlayerDamageTaken = playerDamageTaken;
            PlayerHitsTaken = playerHitsTaken;
            FollowerDamageDone = followerDamageDone;
            FollowerDamageTaken = followerDamageTaken;
            FollowerHitsTaken = followerHitsTaken;
        }
    }
}