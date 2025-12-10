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

        /// <summary>
        /// Inicializuoja naują Heroes objektą su bazinėmis ir papildomomis
        /// herojaus savybėmis.
        /// </summary>
        public Heroes(string name, string playerClass, int life,
            int mana, int dmg, int armor, int power, int moveSpeed, int iq)
            : base(name, playerClass, life, mana, dmg, armor)
        {
            Power = power;
            MoveSpeed = moveSpeed;
            IQ = iq;
        }

        /// <summary>
        /// Grąžina žaidėjo tipą (Heroes).
        /// </summary>
        public override string GetPlayerType() => "Heroes";

        /// <summary>
        /// Sugeneruoja tekstinį objekto vaizdą, naudojamą lentelių atvaizdavimui.
        /// </summary>
        public override string ToString()
        {
            return string.Format(
                "{0} {1,10} | {2,10} | {3,10} | {4,10} |",
                ToBaseString(),
                Power,
                MoveSpeed,
                IQ,
                "-"
            );
        }

        /// <summary>
        /// Sukuria gilią objekto kopiją (Heroes tipo).
        /// </summary>
        public override Player Clone()
        {
            Heroes h = new Heroes(Name, Class, Life, Mana, Dmg, Armor,
                Power, MoveSpeed, IQ);
            h.Race = this.Race;
            h.City = this.City;
            return h;
        }

        /// <summary>
        /// Sugeneruoja objekto duomenis CSV formato eilutei.
        /// </summary>
        public override string ToCsvString()
        {
            return $"{GetPlayerType()};{Name};{Class};{Race};{Life};{Mana};" +
                $"{Dmg};{Armor};{Power};{MoveSpeed};{IQ};-";
        }
    }
}
