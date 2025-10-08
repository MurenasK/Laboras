using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboras
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HeroesRegister reg1 = HeroesRegister.ReadHeroesFromFile(@"heroes1.csv");
            HeroesRegister reg2 = HeroesRegister.ReadHeroesFromFile(@"heroes2.csv");
            HeroesRegister reg2 = HeroesRegister.ReadHeroesFromFile(@"heroes2.csv");
            HeroesRegister reg2 = HeroesRegister.ReadHeroesFromFile(@"heroes2.csv");
            HeroesRegister reg2 = HeroesRegister.ReadHeroesFromFile(@"heroes2.csv");
            HeroesRegister reg2 = HeroesRegister.ReadHeroesFromFile(@"heroes2.csv");
            HeroesRegister reg2 = HeroesRegister.ReadHeroesFromFile(@"heroes2.csv");
            HeroesRegister reg2 = HeroesRegister.ReadHeroesFromFile(@"heroes2.csv");
            HeroesRegister reg2 = HeroesRegister.ReadHeroesFromFile(@"heroes2.csv");
            OutUtils.PrintCombinedRegister(combinedRegister, "PradDuomenys.txt");
            OutUtils.PrintHeroesClassesToCSV(combinedRegister, "Klases.csv");
            OutUtils.PrintMissingClassesToCSV(reg1, reg2, combinedRegister, "TrukstamosKlases.csv");
            OutUtils.PrintStrongestHeroToConsole(combinedRegister);
        }
    }
}
