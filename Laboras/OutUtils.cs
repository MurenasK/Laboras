using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;

namespace Laboras
{
    class OutUtils
    {
        public static void PrintCombinedRegister(HeroesRegister combinedRegister, string fileName)
        {
            List<Heroes> heroes = combinedRegister.GetHeroesList();
            string[] lines = new string[heroes.Count + 2];
            lines[0] = new string('-', 149);
            lines[1] = string.Format("|{0,-20}|{1,-12}|{2,-15}|{3,-15}|{4,-10}|{5,-14}|"
                + "{6,-15}|{7,-8}|{8,-10}|{9,-6}|{10,-12}|", "Vardas", "Rasė",
                                        "Klasė", "Gyvybės taškai", "Ištvermė",
                                        "Atakos Taškai", "Gynybos taškai", "Galia",
                                        "Judėjimas", "IQ", "Spec. Galia");
            lines[2] = new string('-', 149);
            for (int i = 2; i < heroes.Count; i++)
            {
                Heroes hero = heroes[i];
                lines[i + 1] = string.Format("|{0,-20}|{1,-12}|{2,-15}|{3,15}|{4,10}|{5,14}|"
                + "{6,15}|{7,8}|{8,10}|{9,6}|{10,12}|", hero.Name, hero.Race,
                    hero.Class, hero.LifePoints,
                    hero.Mana, hero.DmgPoints, hero.DefPoints, hero.Power, hero.Movement,
                    hero.IQ, hero.SpecPower);
            }
            lines[lines.Length - 1] = new string('-', 149);
            File.WriteAllLines(fileName, lines, Encoding.UTF8);
        }

        public static List<Heroes> ReadHeroes(string filePath)
        {
            List<Heroes> heroes = new List<Heroes>();
            HeroesRegister register = new HeroesRegister();
            string[] lines = File.ReadAllLines(filePath);
            if (lines.Length < 3)
            {
                Console.WriteLine("Trūksta duomenų");
            }
            else
            {
                string race = lines[0];
                string startingCity = lines[1];
                for (int i = 2; i < lines.Length; i++)
                {
                    string[] Values = lines[i].Split(';');
                    string name = Values[0];
                    string @class = Values[1];
                    int lifePoints = int.Parse(Values[2]);
                    int mana = int.Parse(Values[3]);
                    int dmgPoints = int.Parse(Values[4]);
                    int defPoints = int.Parse(Values[5]);
                    int power = int.Parse(Values[6]);
                    int movement = int.Parse(Values[7]);
                    int iQ = int.Parse(Values[8]);
                    int specPower = int.Parse(Values[9]);
                    Heroes hero = new Heroes(name, @class, lifePoints, mana, dmgPoints, defPoints, power, movement, iQ, specPower);
                    if (!register.Contains(hero))
                    {
                        register.AddHero(hero);
                    }
                }
            }
            return register.GetHeroesList();
        }

        public static void PrintHeroesClassesToCSV(HeroesRegister combinedRegister, string fileName)
        {
            List<Heroes> heroes = combinedRegister.GetHeroesList();
            var uniqueClasses = HeroesRegister.GetUniqueClasses(heroes);
            List<string> lines = new List<string> { "Klasės" };
            foreach (var heroClass in uniqueClasses)
            {
                lines.Add(heroClass);
            }
            File.WriteAllLines(fileName, lines, Encoding.UTF8);
        }

        public static void PrintMissingClassesToCSV(HeroesRegister reg1, HeroesRegister reg2, HeroesRegister combinedRegister, string fileName)
        {
            List<Heroes> heroes1 = reg1.GetHeroesList();
            List<Heroes> heroes2 = reg2.GetHeroesList();

            string race1 = heroes1.Count > 0 ? heroes1[0].Race : "Nežinoma";
            string race2 = heroes2.Count > 0 ? heroes2[0].Race : "Nežinoma";

            var uniqueClasses1 = HeroesRegister.GetUniqueClasses(heroes1);
            var uniqueClasses2 = HeroesRegister.GetUniqueClasses(heroes2);

            var missingForRace1 = uniqueClasses2.Except(uniqueClasses1).ToList();
            var missingForRace2 = uniqueClasses1.Except(uniqueClasses2).ToList();

            if (missingForRace1.Count == 0) missingForRace1.Add("VISI");
            if (missingForRace2.Count == 0) missingForRace2.Add("VISI");

            int maxRows = Math.Max(missingForRace1.Count, missingForRace2.Count);
            List<string> lines = new List<string>
            {
                $"{race1};{race2}"
            };
            for (int i = 0; i < maxRows; i++)
            {
                string col1 = i < missingForRace1.Count ? missingForRace1[i] : "";
                string col2 = i < missingForRace2.Count ? missingForRace2[i] : "";
                lines.Add($"{col1};{col2}");
            }
            File.WriteAllLines(fileName, lines, Encoding.UTF8);
        }

        public static void PrintStrongestHeroToConsole(HeroesRegister combinedRegister)
        {
            List<Heroes> strongestHeroes = HeroesRegister.GetStrongestHero(combinedRegister);

            if (strongestHeroes.Count == 0)
            {
                Console.WriteLine("Herojų nėra");
                return;
            }

            Console.WriteLine("Stipriausias(i) Herojai");
            Console.WriteLine(new string('-', 149));
            Console.WriteLine("|{0,-20}|{1,-12}|{2,-15}|{3,-15}|{4,-10}|{5,-14}|"
                + "{6,-15}|{7,-8}|{8,-10}|{9,-6}|{10,-12}|", "Vardas", "Rasė",
                                        "Klasė", "Gyvybės taškai", "Ištvermė",
                                        "Atakos Taškai", "Gynybos taškai", "Galia",
                                        "Judėjimas", "IQ", "Spec. Galia");
            Console.WriteLine(new string('-', 149));
            foreach (var hero in strongestHeroes)
            {
                Console.WriteLine("|{0,-20}|{1,-12}|{2,-15}|{3,15}|{4,10}|{5,14}|"
                + "{6,15}|{7,8}|{8,10}|{9,6}|{10,12}|", hero.Name, hero.Race,
                    hero.Class, hero.LifePoints,
                    hero.Mana, hero.DmgPoints, hero.DefPoints, hero.Power, hero.Movement,
                    hero.IQ, hero.SpecPower);
            }
            Console.WriteLine(new string('-', 149));
        }
    }
}
