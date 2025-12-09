using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace Laboras_5
{
    public abstract class Player
    {
        public string Name { get; set; }
        public string Class { get; set; }
        public int Life { get; set; }
        public int Mana { get; set; }
        public int Dmg { get; set; }
        public int Armor { get; set; }

        public Player(string name, string playerClass, int life, int mana,
            int dmg, int armor)
        {
            Name = name;
            Class = playerClass;
            Life = life;
            Mana = mana;
            Dmg = dmg;
            Armor = armor;
        }

        public virtual string ToBaseString()
        {
            return string.Format("| {0, -10} | {1,-20} | {2,-15} | {3,20} | {4,10} | {5,15} " +
                "| {6,15} |",
                GetPlayerType(),
                Name, Class, Life, Mana, Dmg, Armor);
        }

        public override string ToString()
        {
            return ToBaseString();
        }

        public abstract string GetPlayerType();

        public abstract Player Clone();

    }
}
