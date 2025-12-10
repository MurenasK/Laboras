using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboras_5
{
    public abstract class PlayerComparator
    {
        /// <summary>
        /// Palygina du žaidėjus pagal pasirinktą kriterijų. 
        /// Grąžina neigiamą, teigiamą arba nulį, priklausomai nuo
        /// palyginimo rezultato.
        /// </summary>
        public abstract int Compare(Player player1, Player player2);
    }
}
