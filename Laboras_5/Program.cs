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
        static void Main(string[] args)
        {
            string[] players = { "players4.csv", "players5.csv", "players6.csv"};
            PlayerContainer[] groups = IOUtils.ReadPlayersFromFile(players);

            IOUtils.PrintPlayersToFile(groups, "AllPlayers.txt");

            IOUtils.PrintConsoleBestLifePlayerInEachFile(groups);

            IOUtils.PrintPlayersWithMoreDefThanDmg(groups);

            IOUtils.PrintStrongestHero(groups);

            IOUtils.FindMissingClasses(groups);
        }
    }
}
