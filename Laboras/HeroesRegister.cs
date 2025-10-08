using System;
using System.Collections.Generic;

namespace Laboras
{
    class HeroesRegister
    {
        /// <summary>
        /// Creates a list of heroes
        /// </summary>
        private List<Heroes> heroes;
        public string Race { get; set; }
        public string City { get; set; }
        /// <summary>
        /// Creates a register for heroes
        /// </summary>
        public HeroesRegister()
        {
            heroes = new List<Heroes>();
        }
        /// <summary>
        /// Method to add hero to the register
        /// </summary>
        /// <param name="hero"></param>
        public void AddHero(Heroes hero)
        {
            if (!Contains(hero))
            {
                heroes.Add(hero);
            }
        }
        /// <summary>
        /// Checks if the register contains a specific hero
        /// </summary>
        /// <param name="hero"></param>
        /// <returns></returns>
        public bool Contains(Heroes hero)
        {
            int i = 0;
            while (i < heroes.Count)
            {
                if (heroes[i].Name == hero.Name && heroes[i].Class == hero.Class)
                    return true;
                i++;
            }
            return false;
        }
        /// <summary>
        /// Returns a list of heroes
        /// </summary>
        /// <returns></returns>
        public List<Heroes> GetHeroesList()
        {
            return new List<Heroes>(heroes);
        }
        /// <summary>
        /// Returns the count of heroes in the register
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return heroes.Count;
        }
        /// <summary>
        /// Brings out the strongest hero from the register
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public static Heroes GetStrongestHero(HeroesRegister register)
        {
            List<Heroes> heroes = register.GetHeroesList();
            if (heroes.Count == 0) return null;

            Heroes strongest = heroes[0];
            int i = 1;
            while (i < heroes.Count)
            {
                Heroes h = heroes[i];
                if (h.IsStrongerThan(strongest))
                {
                    strongest = h;  // now encapsulated
                }
                i++;
            }

            return strongest;
        }
        /// <summary>
        /// Takjes out unique classes from the register
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        /// 

        public static List<string> GetAllUniqueClasses(List<HeroesRegister> registers)
        {
            List<string> uniqueClasses = new List<string>();
            int i = 0;
            while (i < registers.Count)
            {
                List<string> regClasses = GetUniqueClasses(registers[i]);

                int j = 0;
                while (j < regClasses.Count)
                {
                    string heroClass = regClasses[j];
                    if (!ContainsClass(uniqueClasses, heroClass))
                    {
                        uniqueClasses.Add(heroClass);
                    }
                    j++;
                }

                i++;
            }
            return uniqueClasses;
        }

        public static List<string> GetUniqueClasses(HeroesRegister register)
        {
            List<string> unique = new List<string>();
            List<Heroes> heroes = register.GetHeroesList();
            int i = 0;
            while (i < heroes.Count)
            {
                string heroClass = heroes[i].Class;
                if (!ContainsClass(unique, heroClass))
                {
                    unique.Add(heroClass);
                }
                i++;
            }
            return unique;
        }
        /// <summary>
        /// Checks if a list contains a specific class
        /// </summary>
        /// <param name="list"></param>
        /// <param name="heroClass"></param>
        /// <returns></returns>
        private static bool ContainsClass(List<string> list, string heroClass)
        {
            int i = 0;
            while (i < list.Count)
            {
                if (list[i] == heroClass) return true;
                i++;
            }
            return false;
        }
        /// <summary>
        /// Searches for missing classes in one register compared to another
        /// </summary>
        /// <param name="baseRegister"></param>
        /// <param name="compareRegister"></param>
        /// <returns></returns>
        public static List<List<string>> GetAllMissingClasses(
            List<HeroesRegister> registers)
        {
            List<List<string>> allMissing = new List<List<string>>();
            int i = 0;
            while (i < registers.Count)
            {
                List<string> baseClasses = GetUniqueClasses(registers[i]);
                List<string> missing = new List<string>();

                int j = 0;
                while (j < registers.Count)
                {
                    if (i != j)
                    {
                        List<string> compareClasses = GetUniqueClasses(
                            registers[j]);
                        int k = 0;
                        while (k < compareClasses.Count)
                        {
                            if (!ContainsClass(baseClasses, compareClasses[k]))
                            {
                                if (!ContainsClass(missing, compareClasses[k]))
                                    missing.Add(compareClasses[k]);
                            }
                            k++;
                        }
                    }
                    j++;
                }

                // If no missing classes, add "VISI"
                if (missing.Count == 0)
                {
                    missing.Add("VISI");
                }

                allMissing.Add(missing);
                i++;
            }

            return allMissing;
        }


        /// <summary>
        /// Checks for the strongest heroes across multiple registers
        /// </summary>
        /// <param name="filePaths"></param>
        /// <returns></returns>
        public static List<Heroes> GetStrongestHeroesAcrossRegisters(
            string[] filePaths)
        {
            List<Heroes> strongestOverall = new List<Heroes>();
            int i = 0;

            while (i < filePaths.Length)
            {
                HeroesRegister reg = IOUtils.ReadHeroes(filePaths[i]);
                List<Heroes> strongestInReg = reg.GetStrongestHeroes();

                int j = 0;
                while (j < strongestInReg.Count)
                {
                    Heroes hero = strongestInReg[j];

                    if (strongestOverall.Count == 0)
                    {
                        strongestOverall.Add(hero);
                    }
                    else if (hero.IsStrongerThan(strongestOverall[0]))
                    {
                        strongestOverall.Clear();
                        strongestOverall.Add(hero);
                    }
                    else if (hero.IsEqualStrength(strongestOverall[0]))
                    {
                        strongestOverall.Add(hero);
                    }

                    j++;
                }

                i++;
            }

            return strongestOverall;
        }
        /// <summary>
        /// Brings out the strongest heroes from the register
        /// </summary>
        /// <returns></returns>
        public List<Heroes> GetStrongestHeroes()
        {
            List<Heroes> strongest = new List<Heroes>();
            int i = 0;
            while (i < heroes.Count)
            {
                Heroes hero = heroes[i];

                if (strongest.Count == 0)
                {
                    strongest.Add(hero);
                }
                else if (hero.IsStrongerThan(strongest[0]))
                {
                    strongest.Clear();
                    strongest.Add(hero);
                }
                else if (hero.IsEqualStrength(strongest[0]))
                {
                    strongest.Add(hero);
                }
                i++;
            }
            return strongest;
        }
    }
}
