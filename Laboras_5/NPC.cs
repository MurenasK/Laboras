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

        /// <summary>
        /// Inicializuoja naują NPC objektą su bazinėmis Player savybėmis
        /// ir specialiu gebėjimu.
        /// </summary>
        public NPC(string name, string playerClass, int life, int mana,
            int dmg, int armor, int specialAbility)
            : base(name, playerClass, life, mana, dmg, armor)
        {
            SpecialAbility = specialAbility;
        }

        /// <summary>
        /// Grąžina žaidėjo tipą (NPC).
        /// </summary>
        public override string GetPlayerType() => "NPC";

        /// <summary>
        /// Sugeneruoja tekstinę objekto eilutę lentelės formatu.
        /// </summary>
        public override string ToString()
        {
            return string.Format(
                "{0} {1,10} | {2,10} | {3,10} | {4,10} |",
                ToBaseString(),
                "-",
                "-",
                "-",
                SpecialAbility
            );
        }

        /// <summary>
        /// Sukuria gilią NPC objekto kopiją.
        /// </summary>
        public override Player Clone()
        {
            NPC n = new NPC(Name, Class, Life, Mana, Dmg, Armor, SpecialAbility);
            n.Race = this.Race;
            n.City = this.City;
            return n;
        }

        /// <summary>
        /// Sugeneruoja NPC duomenis CSV formato eilutei.
        /// </summary>
        public override string ToCsvString()
        {
            return $"{GetPlayerType()};{Name};{Class};{Race};{Life};" +
                $"{Mana};{Dmg};{Armor};-;-;-;{SpecialAbility}";
        }
    }
}
