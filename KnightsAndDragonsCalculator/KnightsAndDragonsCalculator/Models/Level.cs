using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace KnightsAndDragonsCalculator.Models
{
    [DataContract]
    public class Level
    {
        [DataMember]
        public int Gold { get; set; }
        [DataMember]
        public int CommonJump { get; set; }
        [DataMember]
        public int UncommonJump { get; set; }
        [DataMember]
        public int RareJump { get; set; }
        [DataMember]
        public int SuperRareJump { get; set; }
        [DataMember]
        public int UltraRareJump { get; set; }
        [DataMember]
        public int LegendaryJump { get; set; }
        [DataMember]
        public int EpicJump { get; set; }

        public Level(int gold, int commonJump, int uncommonJump, int rareJump, int superRareJump, int ultraRareJump, int legendaryJump, int epicJump)
        {
            Gold = gold;
            CommonJump = commonJump;
            UncommonJump = uncommonJump;
            RareJump = rareJump;
            SuperRareJump = superRareJump;
            UltraRareJump = ultraRareJump;
            LegendaryJump = legendaryJump;
            EpicJump = epicJump;
        }

        public int GetJump(Rarity rarity)
        {
            switch (rarity)
            {
                case Rarity.Common:
                    return CommonJump;
                case Rarity.Uncommon:
                    return UncommonJump;
                case Rarity.Rare:
                    return RareJump;
                case Rarity.SuperRare:
                    return SuperRareJump;
                case Rarity.UltraRare:
                    return UltraRareJump;
                case Rarity.Legendary:
                    return LegendaryJump;
                case Rarity.Epic:
                    return EpicJump;
            }
            return 0;
        }
    }
}