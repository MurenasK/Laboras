using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Laboras_5
{
    internal class Program
    {
        /// <summary>
        /// Programos įėjimo taškas. Vykdo duomenų nuskaitymą, analizę
        /// ir rezultatų išvedimą.
        /// </summary>
        static void Main(string[] args)
        {
            string[] players = { "players1.csv", "players2.csv", "players3.csv" };
            PlayerContainer[] groups = IOUtils.ReadPlayersFromFile(players);

            IOUtils.PrintPlayersToFile(groups, "PradDuomenys.txt");

            IOUtils.PrintConsoleBestLifePlayerInEachFile(groups);

            IOUtils.PrintPlayersWithMoreDefThanDmg(groups);

            IOUtils.PrintStrongestHero(groups);

            IOUtils.FindMissingClasses(groups);
        }
    }
}
