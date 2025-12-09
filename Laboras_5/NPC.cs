using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboras_5
{
    public class NPC : Player
    {
        public int SpecialAbility { get; set; }

        public NPC(string name, string playerClass, int life, int mana,
            int dmg, int armor, int specialAbility)
            : base(name, playerClass, life, mana, dmg, armor)
        {
            SpecialAbility = specialAbility;
        }

        public override string GetPlayerType() => "NPC";

        public override string ToString()
        {
            return string.Format("{0} {1,10} | {2,10} | {3,10} | {4, 10} |",
                ToBaseString(), "-", "-", "-", SpecialAbility);
        }

        public override Player Clone()
        {
            return new NPC(Name, Class, Life, Mana, Dmg, Armor, SpecialAbility);
        }
    }
}
