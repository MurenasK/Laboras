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
            string[] heroFiles = { "heroes1.csv", "heroes2.csv" };
            // Read all registers from files
            HeroesRegister[] allRegisters = IOUtils.ReadMultipleHeroes(
                heroFiles);
            // Make a copy of original data for later use
            HeroesRegister[] originalCopy = (HeroesRegister[])allRegisters.Clone();
            // Print all registers to a single file
            // Print all registers to a single file
            IOUtils.PrintRegistersToFile(allRegisters,
                "PradDuomenys.txt");
            // Print strongest heroes in each file
            IOUtils.PrintStrongestHeroesInEachFile(heroFiles, allRegisters);
            //// Print all classes to console
            IOUtils.PrintAllClassesToConsole(originalCopy);
            // Print all missing classes to CSV
            IOUtils.PrintAllMissingClassesToCSV(originalCopy,
                "TrukstamosKlasės.csv");
            HeroesRegister tempRegister = new HeroesRegister();
            // Print weird heroes to CSV
            HeroesRegister weirdHeroes = tempRegister.GetWeirdHeroes(
                allRegisters);
            // Output weird heroes to file
            IOUtils.PrintWeirdHeroesToCSV(weirdHeroes,
                "Herojai.csv");
            // Indicate successful generation
            Console.WriteLine("Visi failai sugeneruoti sėkmingai.");
        }
    }
}
