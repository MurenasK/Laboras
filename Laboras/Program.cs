using System;
using System.Collections.Generic;

namespace Laboras
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] heroFiles = { "heroes1.csv", "heroes2.csv" };

            // Step 1: Read all hero registers
            List<HeroesRegister> allRegisters = new List<HeroesRegister>();
            allRegisters.Add(IOUtils.ReadHeroes(@"heroes1.csv"));
            allRegisters.Add(IOUtils.ReadHeroes(@"heroes2.csv"));
            // Add more registers here if needed

            // Step 2: Print all registers into one PradDuomenys.txt file
            IOUtils.PrintAllRegistersToFile(allRegisters, "PradDuomenys.txt");

            // Step 3: Print all unique classes together into Klases.csv
            IOUtils.PrintAllClassesToCSV(allRegisters, "Klases.csv");

            // Step 4: Print missing classes between all registers
            IOUtils.PrintAllMissingClassesToCSV(allRegisters, "TrukstamosKlases.csv");

            // Step 5: Print strongest hero among all registers
            IOUtils.PrintStrongestHeroesAcrossFiles(heroFiles);

            Console.WriteLine("Visi failai sugeneruoti sėkmingai.");
        }
    }
}
