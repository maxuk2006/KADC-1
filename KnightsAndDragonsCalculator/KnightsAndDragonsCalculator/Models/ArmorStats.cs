using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace KnightsAndDragonsCalculator.Models
{
    [DataContract]
    public class ArmorStats
    {
        [DataMember]
        public int AttackStart { get; protected set; }
        [DataMember]
        public int AttackUp { get; protected set; }
        [DataMember]
        public int DefenseStart { get; protected set; }
        [DataMember]
        public int DefenseUp { get; protected set; }

        public ArmorStats(int attackStart, int attackUp, int defenseStart, int defenseUp)
        {
            AttackStart = attackStart;
            AttackUp = attackUp;
            DefenseStart = defenseStart;
            DefenseUp = defenseUp;
        }
    }
}