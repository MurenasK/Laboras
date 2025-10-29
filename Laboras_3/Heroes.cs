using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Laboras_3
{
    /// <summary>
    /// Takes and appends hero charasteristics
    /// </summary>
    class Heroes
    {
        public string Name { get; set; }
        public string Class { get; set; }
        public int LifePoints { get; set; }
        public int Mana { get; set; }
        public int DmgPoints { get; set; }
        public int DefPoints { get; set; }
        public int Power { get; set; }
        public int Movement { get; set; }
        public int IQ { get; set; }
        public int SpecPower { get; set; }
        public int Strength { get; set; }

        /// <summary>
        /// Appends charasteristic to a hero
        /// </summary>
        public Heroes(string name,
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
        /// <summary>
        /// To string override
        /// </summary>
        /// <returns></returns>
        
        public int CompareTo(Heroes other)
        {
            if (other == null)
            {
                return 1;
            }

            int lifePointsComparison = this.LifePoints.CompareTo(other.LifePoints);
            if (lifePointsComparison != 0)
            {
                return lifePointsComparison;
            }
            int defPointsComparison = this.DefPoints.CompareTo(other.DefPoints);
            if (defPointsComparison != 0)
            {
                return defPointsComparison;
            }
            return string.Compare(other.Name, this.Name, StringComparison.OrdinalIgnoreCase);
        }

        public Heroes Clone()
        {
            return new Heroes(Name, Class, LifePoints, Mana, DmgPoints,
                DefPoints, Power, Movement, IQ, SpecPower);
        }

        public override string ToString()
        {
            return string.Format(
                "|{0,-20}|{1,-15}|{2,20}|{3,10}|{4,15}" +
                "|{5,15}|{6,10}|{7,10}|{8,6}|{9,15}|",
                Name, Class, LifePoints, Mana, DmgPoints, DefPoints,
                Power, Movement, IQ, SpecPower
            );
        }
        /// <summary>
        /// Calculates power score
        /// </summary>
        /// <returns></returns>
        public int PowerScore()
        {
            return LifePoints + DefPoints + Power;
        }
        /// <summary>
        /// Checks if one hero is stronger than another
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        /// 

        public int IsWeird()
        {
            if (LifePoints > DefPoints)
            {
                return 1;
            }
            return 0;
        }

        public bool IsStrongerThan(Heroes other)
        {
            return this.PowerScore() > other.PowerScore();
        }
        /// <summary>
        /// Checks if two heroes are of equal strength
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool IsEqualStrength(Heroes other)
        {
            return this.PowerScore() == other.PowerScore();
        }
    }
}
