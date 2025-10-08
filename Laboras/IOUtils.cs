using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Laboras
{
    class IOUtils
    {
        /// <summary>
        /// Prints all registers to a single text file
        /// </summary>
        /// <param name="registers"></param>
        /// <param name="fileName"></param>
        public static void PrintAllRegistersToFile(List<HeroesRegister> registers,
            string fileName)
        {
            List<string> lines = new List<string>();
            int i = 0;
            while (i < registers.Count)
            {
                HeroesRegister reg = registers[i];
                List<Heroes> heroes = reg.GetHeroesList();

                lines.Add(new string('-', 149));
                lines.Add($"Rasė: {reg.Race}, Pradžios miestas: {reg.City}");
                lines.Add(new string('-', 149));

                lines.Add(string.Format("|{0,-20}|{1,-15}|{2,20}|{3,10}|{4,15}" + 
                    "|{5,15}|{6,10}|{7,10}|{8,6}|{9,15}|",
                    "Vardas", "Klasė", "Gyvybės taškai", "Ištvermė",
                    "Atakos Taškai", "Gynybos taškai", "Galia", "Judėjimas",
                    "IQ", "Spec. Galia"));
                lines.Add(new string('-', 149));

                int j = 0;
                while (j < heroes.Count)
                {
                    lines.Add(heroes[j].ToString()); 
                    j++;
                }

                lines.Add(new string('-', 149));
                i++;
            }

            File.WriteAllLines(fileName, lines, Encoding.UTF8);
        }
        /// <summary>
        /// Prints all unique classes from all registers to a CSV file
        /// </summary>
        /// <param name="registers"></param>
        /// <param name="fileName"></param>
        public static void PrintAllClassesToCSV(
            List<HeroesRegister> registers, string fileName)
        {
            List<string> uniqueClasses = HeroesRegister.GetAllUniqueClasses(
                registers);

            List<string> lines = new List<string> { "Klasės" };
            int k = 0;
            while (k < uniqueClasses.Count)
            {
                lines.Add(uniqueClasses[k]);
                k++;
            }

            File.WriteAllLines(fileName, lines, Encoding.UTF8);
        }

        /// <summary>
        /// Prints all missing classes between two registers to a CSV file
        /// </summary>
        /// <param name="registers"></param>
        /// <param name="fileName"></param>
        public static void PrintAllMissingClassesToCSV(
            List<HeroesRegister> registers, string fileName)
        {
            if (registers.Count < 2)
            {
                Console.WriteLine(
                    "Reikia bent dviejų registrų trūkstamų klasių palyginimui");
                return;
            }

            List<List<string>> allMissing = HeroesRegister.GetAllMissingClasses(
                registers);
            List<string> lines = new List<string>();

            int row = 0;
            int maxRows = 0;

            // Find max row count
            int i = 0;
            while (i < allMissing.Count)
            {
                if (allMissing[i].Count > maxRows) maxRows = allMissing[i].Count;
                i++;
            }

            // First line = race names
            string header = "";
            int h = 0;
            while (h < registers.Count)
            {
                header += registers[h].Race + 
                    (h < registers.Count - 1 ? ";" : "");
                h++;
            }
            lines.Add(header);

            // Rows of missing classes
            row = 0;
            while (row < maxRows)
            {
                string line = "";
                int col = 0;
                while (col < allMissing.Count)
                {
                    string val = row < allMissing[col].Count ?
                        allMissing[col][row] : "";
                    line += val + (col < allMissing.Count - 1 ? ";" : "");
                    col++;
                }
                lines.Add(line);
                row++;
            }

            File.WriteAllLines(fileName, lines, Encoding.UTF8);
        }

        /// <summary>
        /// Reads heroes from a given file path
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static HeroesRegister ReadHeroes(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
            HeroesRegister register = new HeroesRegister();

            if (lines.Length < 3)
            {
                Console.WriteLine("Trūksta duomenų");
                return register;
            }

            register.Race = lines[0];
            register.City = lines[1];

            int i = 2;
            while (i < lines.Length)
            {
                string[] values = lines[i].Split(';');
                Heroes hero = new Heroes(
                    values[0], values[1],
                    int.Parse(values[2]), int.Parse(values[3]),
                    int.Parse(values[4]),
                    int.Parse(values[5]), int.Parse(values[6]),
                    int.Parse(values[7]),
                    int.Parse(values[8]), int.Parse(values[9])
                );
                register.AddHero(hero);
                i++;
            }

            return register;
        }
        /// <summary>
        /// Prints the strongest heroes across multiple files
        /// </summary>
        /// <param name="filePaths"></param>
        public static void PrintStrongestHeroesAcrossFiles(string[] filePaths)
        {
            List<Heroes> strongest = HeroesRegister.
                GetStrongestHeroesAcrossRegisters(filePaths);

            if (strongest.Count == 0)
            {
                Console.WriteLine("Herojų nėra");
                return;
            }

            Console.WriteLine("Stipriausias(i) herojus(-iai) tarp visų grupių:");
            Console.WriteLine(new string('-', 149));
            Console.WriteLine("|{0,-20}|{1,-15}|{2,20}|{3,10}|{4,15}" + 
                "|{5,15}|{6,10}|{7,10}|{8,6}|{9,15}|",
                "Vardas", "Klasė", "Gyvybės taškai", "Ištvermė", "Atakos Taškai",
                "Gynybos taškai", "Galia", "Judėjimas", "IQ", "Spec. Galia");
            Console.WriteLine(new string('-', 149));

            int k = 0;
            while (k < strongest.Count)
            {
                Console.WriteLine(strongest[k].ToString());
                k++;
            }

            Console.WriteLine(new string('-', 149));
        }
    }
}
