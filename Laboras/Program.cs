using System;
using System.Collections.Generic;

namespace Laboras
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] heroFiles = { "heroes1.csv", "heroes2.csv" };

            List<HeroesRegister> allRegisters = new List<HeroesRegister>();
            allRegisters.Add(IOUtils.ReadHeroes(@"heroes1.csv"));
            allRegisters.Add(IOUtils.ReadHeroes(@"heroes2.csv"));

            IOUtils.PrintAllRegistersToFile(allRegisters, "PradDuomenys.txt");

            IOUtils.PrintAllClassesToCSV(allRegisters, "Klases.csv");

            IOUtils.PrintAllMissingClassesToCSV(allRegisters,
                "TrukstamosKlases.csv");
            IOUtils.PrintStrongestHeroesAcrossFiles(heroFiles);

            Console.WriteLine("Visi failai sugeneruoti sėkmingai.");
        }
    }
}
