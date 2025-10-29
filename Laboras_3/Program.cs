using Laboras_3;
using System;
using System.Collections.Generic;

namespace Laboras_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // masyvai registro tipo
            /// Define input files
            string[] heroFiles = { "heroes4.csv", "heroes3.csv" };
            // Read all registers from files
            HeroesContainer[] allRegisters = IOUtils.ReadMultipleHeroes(
                heroFiles);
            // Make a copy of original data
            HeroesContainer[] originalCopy = (HeroesContainer[])allRegisters.Clone();
            // Print all registers to a single file
            IOUtils.PrintRegistersToFile(allRegisters,
                "PradDuomenys.txt");
            // Print strongest heroes in each file
            IOUtils.PrintStrongestHeroesInEachFile(heroFiles);
            //  Print all classes to console and missing classes to CSV
            IOUtils.PrintAllClassesToConsole(originalCopy);
            //  Print missing classes to CSV
            IOUtils.PrintAllMissingClassesToCSV(originalCopy,
                "TrukstamosKlasės.csv");
            //  Print weird heroes to CSV
            HeroesRegister weirdHeroes = HeroesRegister.GetWeirdHeroes(
                allRegisters);
            // Print weird heroes to CSV
            IOUtils.PrintWeirdHeroesToCSV(weirdHeroes,
                "Herojai.csv");
            Console.WriteLine("Visi failai sugeneruoti sėkmingai.");
        }
    }
}
