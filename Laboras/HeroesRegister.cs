using System;
using System.Collections.Generic;

namespace Laboras
{
    class HeroesRegister
    {
        private List<Heroes> heroes;
        public string Race { get; set; }
        public string City { get; set; }

        public HeroesRegister()
        {
            heroes = new List<Heroes>();
        }

        public void AddHero(Heroes hero)
        {
            if (!Contains(hero))
            {
                heroes.Add(hero);
            }
        }

        public bool Contains(Heroes hero)
        {
            int i = 0;
            // ✅ Only one loop
            while (i < heroes.Count)
            {
                if (heroes[i].Name == hero.Name && heroes[i].Class == hero.Class)
                    return true;
                i++;
            }
            return false;
        }

        public List<Heroes> GetHeroesList()
        {
            return new List<Heroes>(heroes);
        }

        public int Count()
        {
            return heroes.Count;
        }

        public static Heroes GetStrongestHero(HeroesRegister register)
        {
            List<Heroes> heroes = register.GetHeroesList();
            if (heroes.Count == 0) return null;

            Heroes strongest = heroes[0];
            int i = 1;
            // ✅ Only one loop
            while (i < heroes.Count)
            {
                Heroes h = heroes[i];
                int total1 = strongest.LifePoints + strongest.DefPoints + strongest.Power;
                int total2 = h.LifePoints + h.DefPoints + h.Power;

                if (total2 > total1)
                {
                    strongest = h;
                }
                i++;
            }

            return strongest;
        }

        public static List<string> GetUniqueClasses(HeroesRegister register)
        {
            List<string> unique = new List<string>();
            List<Heroes> heroes = register.GetHeroesList();
            int i = 0;
            // ✅ Only one loop
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

        private static bool ContainsClass(List<string> list, string heroClass)
        {
            int i = 0;
            // ✅ Only one loop
            while (i < list.Count)
            {
                if (list[i] == heroClass) return true;
                i++;
            }
            return false;
        }

        public static List<string> GetMissingClasses(HeroesRegister baseRegister, HeroesRegister compareRegister)
        {
            List<string> baseClasses = GetUniqueClasses(baseRegister);
            List<string> compareClasses = GetUniqueClasses(compareRegister);
            List<string> missing = new List<string>();

            int i = 0;
            while (i < compareClasses.Count)
            {
                if (!ContainsClass(baseClasses, compareClasses[i]))
                {
                    missing.Add(compareClasses[i]);
                }
                i++;
            }

            return missing;
        }
        public static List<Heroes> GetStrongestHeroesAcrossRegisters(string[] filePaths)
        {
            List<Heroes> strongestOverall = new List<Heroes>();
            int i = 0;

            // ✅ Only one loop: process each file sequentially
            while (i < filePaths.Length)
            {
                HeroesRegister reg = IOUtils.ReadHeroes(filePaths[i]);
                List<Heroes> strongestInReg = reg.GetStrongestHeroes(); // single-loop method

                int j = 0;
                // ✅ Only one loop: merge into overall strongest
                while (j < strongestInReg.Count)
                {
                    Heroes hero = strongestInReg[j];
                    if (strongestOverall.Count == 0)
                    {
                        strongestOverall.Add(hero);
                    }
                    else
                    {
                        int power = hero.LifePoints + hero.DefPoints + hero.Power;
                        int highest = strongestOverall[0].LifePoints + strongestOverall[0].DefPoints + strongestOverall[0].Power;

                        if (power > highest)
                        {
                            strongestOverall.Clear();
                            strongestOverall.Add(hero);
                        }
                        else if (power == highest)
                        {
                            strongestOverall.Add(hero);
                        }
                    }

                    j++;
                }

                i++;
            }

            return strongestOverall;
        }

        public List<Heroes> GetStrongestHeroes()
        {
            List<Heroes> strongest = new List<Heroes>();

            // ✅ Only one loop for this register
            int i = 0;
            while (i < heroes.Count)
            {
                Heroes hero = heroes[i];
                int heroPower = hero.LifePoints + hero.DefPoints + hero.Power;

                if (strongest.Count == 0)
                {
                    strongest.Add(hero);
                }
                else
                {
                    int highestPower = strongest[0].LifePoints + strongest[0].DefPoints + strongest[0].Power;

                    if (heroPower > highestPower)
                    {
                        strongest.Clear();
                        strongest.Add(hero);
                    }
                    else if (heroPower == highestPower)
                    {
                        strongest.Add(hero);
                    }
                }

                i++;
            }

            return strongest;
        }

    }
}
