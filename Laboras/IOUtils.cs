using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Laboras
{
    class IOUtils
    {
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
        public static void PrintAllClassesToCSV(List<HeroesRegister> registers,
                                                                string fileName)
        {
            List<string> uniqueClasses = new List<string>();
            int i = 0;
            while (i < registers.Count)
            {
                List<string> classes = HeroesRegister.GetUniqueClasses(
                    registers[i]
                    );
                int j = 0;
                while (j < classes.Count)
                {
                    if (!ContainsClass(uniqueClasses, classes[j]))
                    {
                        uniqueClasses.Add(classes[j]);
                    }
                    j++;
                }
                i++;
            }
                                                                                  
            List<string> lines = new List<string> { "Klasės" };
            int k = 0;
            while (k < uniqueClasses.Count)
            {
                lines.Add(uniqueClasses[k]);
                k++;
            }

            File.WriteAllLines(fileName, lines, Encoding.UTF8);
        }

        public static void PrintAllMissingClassesToCSV(
            List<HeroesRegister> registers,
            string fileName
            )
        {
            if (registers.Count < 2)
            {
                Console.WriteLine("Reikia bent dviejų registrų trūkstamų klasių");
                return;
            }

            HeroesRegister reg1 = registers[0];
            HeroesRegister reg2 = registers[1];

            List<string> missing1 = HeroesRegister.GetMissingClasses(reg1, reg2);
            List<string> missing2 = HeroesRegister.GetMissingClasses(reg2, reg1);

            List<string> lines = new List<string>
            {
                reg1.Race + ";" + reg2.Race
            };

            int i = 0;
            int max = missing1.Count > missing2.Count ? 
                missing1.Count : 
                missing2.Count;
            while (i < max)
            {
                string val1 = i < missing1.Count ? missing1[i] : "";
                string val2 = i < missing2.Count ? missing2[i] : "";
                lines.Add(val1 + ";" + val2);
                i++;
            }

            File.WriteAllLines(fileName, lines, Encoding.UTF8);
        }
        private static bool ContainsClass(List<string> list, string value)
        {
            int i = 0;
            while (i < list.Count)
            {
                if (list[i] == value) return true;
                i++;
            }
            return false;
        }
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
