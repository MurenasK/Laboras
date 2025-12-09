using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboras_5
{
    public class Heroes : Player
    {
        public int Power { get; set; }
        public int MoveSpeed { get; set; }
        public int IQ { get; set; }
        
        public Heroes(string name, string playerClass, int life,
            int mana, int dmg, int armor, int power, int moveSpeed, int iq)
            : base(name, playerClass, life, mana, dmg, armor)
        {
            Power = power;
            MoveSpeed = moveSpeed;
            IQ = iq;
        }

        public override string GetPlayerType() => "Heroes";

        public override string ToString()
        {
            return string.Format("{0} {1,10} | {2,10} | {3,10} | {4, 10} |",
                ToBaseString(), Power, MoveSpeed, IQ, "-");
        }

        public override Player Clone()
        {
            return new Heroes(Name, Class, Life, Mana, Dmg, Armor,
                              Power, MoveSpeed, IQ);
        }
    }
}
