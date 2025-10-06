using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboras
{
    /// <summary>
    /// Takes and appends hero charasteristics
    /// </summary>
    class Heroes
    {
        public string Name { get; set; }
        public string Race { get; set; }
        public string Class { get; set; }
        public int LifePoints { get; set; }
        public int Mana { get; set; }
        public int DmgPoints { get; set; }
        public int DefPoints { get; set; }
        public int Power { get; set; }
        public int Movement { get; set; }
        public int IQ { get; set; }
        public int SpecPower { get; set; }

        /// <summary>
        /// Appends charasteristic to a hero
        /// </summary>
        public Heroes(string name,
                      string race,
                      string @class,
                      int lifePoints,
                      int mana, int dmgPoints,
                      int defPoints,
                      int power,
                      int movement,
                      int iQ,
                      int specPower)
        {
            Name = name;
            Race = race;
            Class = @class;
            LifePoints = lifePoints;
            Mana = mana;
            DmgPoints = dmgPoints;
            DefPoints = defPoints;
            Power = power;
            Movement = movement;
            IQ = iQ;
            SpecPower = specPower;
        }
    }
}
