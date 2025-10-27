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
            string[] heroFiles = { "heroes1.csv", "heroes2.csv" };
            /// Read all registers from input files
            //List<HeroesRegister> allRegisters = new List<HeroesRegister>();
            HeroesContainer[] allRegisters = IOUtils.ReadMultipleHeroes(heroFiles);

            IOUtils.PrintRegistersToConsole(allRegisters);

            IOUtils.PrintRegistersToFile(allRegisters,
                "PradDuomenys.txt");
            //allRegisters.Add(IOUtils.ReadHeroes(@"heroes3.csv"));
            //allRegisters.Add(IOUtils.ReadHeroes(@"heroes4.csv"));
            /// Print all registers to a single output file
            //IOUtils.PrintAllRegistersToFile(allHeroes, "PradDuomenys.txt");
            /// Print all unique classes to a CSV file
            //IOUtils.PrintAllClassesToCSV(allRegisters, "Klases.csv");
            /// Print all missing classes to a CSV file
            //IOUtils.PrintAllMissingClassesToCSV(allRegisters,
            //    "TrukstamosKlasės.csv");
            /// Print the strongest heroes across all files to a CSV file
            IOUtils.PrintStrongestHeroesAcrossFiles(heroFiles);
            /// Notify user of successful file generation
            Console.WriteLine("Visi failai sugeneruoti sėkmingai.");
        }
    }
}
