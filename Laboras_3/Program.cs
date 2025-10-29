using Laboras_3;
using System;
using System.Collections.Generic;

namespace Laboras_3
{
    internal class Program
    {
        static void Main(string[] args)
        {

            /// Define input files
            string[] heroFiles = { "heroes3.csv", "heroes4.csv" };
            // Read all registers from files
            HeroesContainer[] allRegisters = IOUtils.ReadMultipleHeroes(
                heroFiles);
            HeroesContainer[] originalCopy = (HeroesContainer[])allRegisters.Clone();
            IOUtils.PrintRegistersToFile(allRegisters,
                "PradDuomenys.txt");

            IOUtils.PrintStrongestHeroesInEachFile(heroFiles);
            IOUtils.PrintAllClassesToConsole(originalCopy);
            IOUtils.PrintAllMissingClassesToCSV(originalCopy,
                "TrukstamosKlasės.csv");
            HeroesRegister weirdHeroes = HeroesRegister.GetWeirdHeroes(
                allRegisters);
            IOUtils.PrintWeirdHeroesToCSV(weirdHeroes,
                "Herojai.csv");
            Console.WriteLine("Visi failai sugeneruoti sėkmingai.");
        }
    }
}
