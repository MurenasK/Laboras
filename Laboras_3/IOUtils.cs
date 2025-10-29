using Laboras_3;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Laboras_3
{
    class IOUtils
    {
        /// <summary>
        /// Prints all registers to a single text file
        /// </summary>
        /// <param name="registers"></param>
        /// <param name="fileName"></param>
        public static void PrintRegistersToFile(HeroesContainer[] registers, string fileName)
        {
            List<string> lines = new List<string>();

            int r = 0;
            while (r < registers.Length)
            {
                HeroesContainer register = registers[r];

                // Print first two lines: race and city
                lines.Add(register.Race);
                lines.Add(register.City);

                lines.Add(new string('-', 149));
                lines.Add(string.Format("|{0,-20}|{1,-15}|{2,20}|{3,10}|{4,15}" +
                    "|{5,15}|{6,10}|{7,10}|{8,6}|{9,15}|",
                    "Vardas", "Klasė", "Gyvybės taškai", "Ištvermė", "Atakos Taškai",
                    "Gynybos taškai", "Galia", "Judėjimas", "IQ", "Spec. Galia"));
                lines.Add(new string('-', 149));

                int i = 0;
                while (i < register.Count)
                {
                    lines.Add(register.Get(i).ToString());
                    i++;
                }

                lines.Add(new string('-', 149));
                lines.Add(""); // empty line between registers
                r++;
            }

            File.WriteAllLines(fileName, lines, Encoding.UTF8);
            Console.WriteLine("Failas paruoštas: " + fileName);
        }

        public static void PrintAllClassesToConsole(HeroesContainer[] registers)
        {
            HashSet<string> uniqueClasses = HeroesRegister.GetAllUniqueClassesFromContainers(registers);

            Console.WriteLine("Visos unikalios klasės:");
            foreach (var heroClass in uniqueClasses)
            {
                Console.WriteLine(heroClass);
            }
        }

        /// <summary>
        /// Prints all missing classes between two registers to a CSV file
        /// </summary>
        /// <param name="registers"></param>
        /// <param name="fileName"></param>
        public static void PrintAllMissingClassesToCSV(
    HeroesContainer[] registers, string fileName)
        {
            if (registers.Count() < 2)
            {
                Console.WriteLine(
                    "Reikia bent dviejų registrų trūkstamų klasių palyginimui");
                return;
            }
            // Convert HeroesContainer[] to HeroesRegister[]
            HeroesRegister[] heroRegisters = new HeroesRegister[registers.Length];
            for (int i = 0; i < registers.Length; i++)
            {
                HeroesRegister reg = new HeroesRegister(registers[i]);
                reg.Race = registers[i].Race;
                reg.City = registers[i].City;
                heroRegisters[i] = reg;
            }
            var allMissing = HeroesRegister.GetAllMissingClasses(heroRegisters);

            // Determine max row count in one pass
            int maxRows = allMissing.Max(list => list.Count);

            var lines = new List<string>();

            // Header (race names)
            lines.Add(string.Join(";", registers.Select(r => r.Race)));

            // Data rows
            for (int row = 0; row < maxRows; row++)
            {
                var rowValues = new List<string>();
                for (int col = 0; col < allMissing.Count; col++)
                {
                    rowValues.Add(
                        row < allMissing[col].Count ? allMissing[col][row] : "");
                }
                lines.Add(string.Join(";", rowValues));
            }

            File.WriteAllLines(fileName, lines, Encoding.UTF8);
        }


        /// <summary>
        /// Reads heroes from a given file path
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static HeroesContainer ReadHeroes(string filePath)
        {
            HeroesContainer allHeroes = new HeroesContainer();
            string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);

            if (lines.Length < 3)
            {
                Console.WriteLine("Trūksta duomenų");
                return allHeroes;
            }

            allHeroes.Race = lines[0]; // first line
            allHeroes.City = lines[1]; // second line

            int i = 2;
            while (i < lines.Length)
            {
                string[] values = lines[i].Split(';');
                Heroes hero = new Heroes(
                    values[0], values[1],
                    int.Parse(values[2]), int.Parse(values[3]),
                    int.Parse(values[4]), int.Parse(values[5]),
                    int.Parse(values[6]), int.Parse(values[7]),
                    int.Parse(values[8]), int.Parse(values[9])
                );
                allHeroes.Add(hero);
                i++;
            }

            return allHeroes;
        }

        public static HeroesContainer[] ReadMultipleHeroes(string[] files)
        {
            HeroesContainer[] allRegisters = new HeroesContainer[files.Length];

            for (int i = 0; i < files.Length; i++)
            {
                allRegisters[i] = ReadHeroes(files[i]);
            }

            return allRegisters;
        }

        /// <summary>
        /// Prints the strongest heroes across multiple files
        /// </summary>
        /// <param name="filePaths"></param>
        public static void PrintStrongestHeroesInEachFile(string[] filePaths)
        {
            for (int f = 0; f < filePaths.Length; f++)
            {
                HeroesContainer register = ReadHeroes(filePaths[f]);
                Heroes[] strongestHeroes = HeroesRegister.GetStrongestHeroesInRegister(register);
                if (strongestHeroes.Length == 0)
                {
                    Console.WriteLine($"Herojų nėra faile: {filePaths[f]}");
                    continue;
                }
                Console.WriteLine($"Stipriausias(i) herojus(-iai) rasėje: {register.Race}");
                Console.WriteLine(new string('-', 149));
                Console.WriteLine(string.Format("|{0,-20}|{1,-15}|{2,20}|{3,10}|{4,15}" +
                    "|{5,15}|{6,10}|{7,10}|{8,6}|{9,15}|",
                    "Vardas", "Klasė", "Gyvybės taškai", "Ištvermė", "Atakos Taškai",
                    "Gynybos taškai", "Galia", "Judėjimas", "IQ", "Spec. Galia"));
                Console.WriteLine(new string('-', 149));
                for (int k = 0; k < strongestHeroes.Length; k++)
                {
                    Console.WriteLine(strongestHeroes[k].ToString());
                }
                Console.WriteLine(new string('-', 149));
            }
        }

        public static void PrintWeirdHeroesToCSV(HeroesRegister weirdHeroes, string fileName)
        {
            if (weirdHeroes == null || weirdHeroes.Count == 0)
            {
                Console.WriteLine(weirdHeroes.Count);
                Console.WriteLine("Nėra herojų, ką spausdinti į CSV.");
                return;
            }

            List<string> lines = new List<string>();

            // Header line
            lines.Add("Vardas;Klasė;Gyvybės taškai;Ištvermė;Atakos Taškai;Gynybos taškai;Galia;Judėjimas;IQ;Spec. Galia");

            int i = 0;
            while (i < weirdHeroes.Count)
            {
                Heroes hero = weirdHeroes.GetHero(i);
                string line =
                    hero.Name + ";" +
                    hero.Class + ";" +
                    hero.LifePoints + ";" +
                    hero.Mana + ";" +
                    hero.DmgPoints + ";" +
                    hero.DefPoints + ";" +
                    hero.Power + ";" +
                    hero.Movement + ";" +
                    hero.IQ + ";" +
                    hero.SpecPower;

                lines.Add(line);
                i++;
            }

            File.WriteAllLines(fileName, lines, Encoding.UTF8);
            Console.WriteLine("CSV failas sėkmingai sukurtas: " + fileName);
        }

    }
}
