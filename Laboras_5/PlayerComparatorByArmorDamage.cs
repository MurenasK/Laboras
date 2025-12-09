using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboras_5
{
    class PlayerComparatorByArmorDamage : PlayerComparator
    {
        public override int Compare(Player player1, Player player2)
        {
            int result = player1.Armor.CompareTo(player2.Armor);
            if (result != 0)
            {
                return result;
            }

            return player1.Dmg.CompareTo(player2.Dmg);
        }
    }
}
