using Laboras_3;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;

namespace Laboras_3
{
    class HeroesRegister
    {
        /// <summary>
        /// Creates a list of heroes
        /// </summary>
        public string Race { get; set; }
        public string City { get; set; }

        public HeroesContainer Weirdos = new HeroesContainer();

        private HeroesContainer heroes = new HeroesContainer();
        private HeroesRegister heroesRegister;

        /// <summary>
        /// Clears the register
        /// </summary>
        public void Clear()
        {
            heroes.Clear();
        }
        /// <summary>
        /// Register constructor
        /// </summary>
        /// <param name="container"></param>
        public HeroesRegister(HeroesContainer container)
        {
            this.heroes = container; // your internal container field
        }
        /// <summary>
        /// Register copy constructor
        /// </summary>
        /// <param name="heroesRegister"></param>
        public HeroesRegister(HeroesRegister heroesRegister)
        {
            this.heroesRegister = heroesRegister;
        }
        /// <summary>
        /// Register default constructor
        /// </summary>
        public HeroesRegister()
        {
        }
        /// <summary>
        /// Count of heroes in the register
        /// </summary>
        public int Count => heroes.Count;
        /// <summary>
        /// Gets all heroes from the register
        /// </summary>
        /// <returns></returns>
        public Heroes[] GetAllHeroes()
        {
            Heroes[] arr = new Heroes[heroes.Count];
            for (int i = 0; i < heroes.Count; i++)
                arr[i] = heroes.Get(i);
            return arr;
        }
        /// <summary>
        /// Adds a hero to the register
        /// </summary>
        /// <param name="hero"></param>
        public void AddHero(Heroes hero)
        {
            if (!Contains(hero))
                heroes.Add(hero);
        }
        /// <summary>
        /// Contains check for a hero in the register
        /// </summary>
        /// <param name="hero"></param>
        /// <returns></returns>
        public bool Contains(Heroes hero)
        {
            for (int i = 0; i < heroes.Count; i++)
            {
                Heroes h = heroes.Get(i);
                if (h.Name == hero.Name && h.Class == hero.Class)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Gets a hero by index from the register
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Heroes GetHero(int index)
        {
            return heroes.Get(index);
        }

        /// <summary>
        /// Brings out the strongest hero from the register
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        /// 
        // forai
        public Heroes[] GetStrongestHeroes()
        {
            if (heroes.Count == 0) return new Heroes[0];

            HeroesContainer strongest = FindStrongestContainer();
            return ConvertContainerToArray(strongest);
        }

        /// <summary>
        /// Finds the strongest hero container
        /// </summary>
        /// <returns></returns>
        private HeroesContainer FindStrongestContainer()
        {
            HeroesContainer strongest = new HeroesContainer();
            if (heroes.Count == 0) return strongest;

            strongest.Add(heroes.Get(0));

            for (int i = 1; i < heroes.Count; i++)
            {
                Heroes hero = heroes.Get(i);

                if (hero.IsStrongerThan(strongest.Get(0)))
                {
                    strongest = new HeroesContainer();
                    strongest.Add(hero);
                }
                else if (hero.IsEqualStrength(strongest.Get(0)))
                {
                    strongest.Add(hero);
                }
            }

            return strongest;
        }

        /// <summary>
        /// Converts a container to an array
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        private Heroes[] ConvertContainerToArray(HeroesContainer container)
        {
            Heroes[] result = new Heroes[container.Count];
            for (int i = 0; i < container.Count; i++)
            {
                result[i] = container.Get(i);
            }
            return result;
        }


        /// <summary>
        /// Takjes out unique classes from the register
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public List<string> GetAllUniqueClasses(HeroesRegister[] registers)
        {
            List<string> uniqueClasses = new List<string>();
            int i = 0;
            while (i < registers.Length)
            {
                HeroesRegister reg = registers[i];
                if (reg != null)  // <-- null check
                {
                    List<string> regClasses = GetUniqueClasses(reg);

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
                }

                i++;
            }
            return uniqueClasses;
        }
        /// <summary>
        /// Gets unique classes from a single register
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>

        public List<string> GetUniqueClasses(HeroesRegister register)
        {
            List<string> unique = new List<string>();

            if (register == null) return unique; // early return if null

            for (int i = 0; i < register.Count; i++)
            {
                Heroes hero = register.GetHero(i);
                if (hero == null) continue; // skip null heroes

                string heroClass = hero.Class;
                if (!ContainsClass(unique, heroClass))
                {
                    unique.Add(heroClass);
                }
            }

            return unique;
        }




        /// <summary>
        /// Checks if a list contains a specific class
        /// </summary>
        /// <param name="list"></param>
        /// <param name="heroClass"></param>
        /// <returns></returns>
        private bool ContainsClass(List<string> list, string heroClass)
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
        public List<List<string>> GetAllMissingClasses(
            HeroesRegister[] registers)
        {
            List<List<string>> allMissing = new List<List<string>>();
            int i = 0;
            while (i < registers.Length)
            {
                List<string> baseClasses = GetUniqueClasses(registers[i]);
                List<string> missing = new List<string>();

                int j = 0;
                while (j < registers.Length)
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
        /// Gets the strongest heroes in a register
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public Heroes[] GetStrongestHeroesInRegister(
            HeroesRegister register)
        {
            return register.GetStrongestHeroes();
        }
        /// <summary>
        /// Gets weird heroes from multiple registers
        /// </summary>
        /// <param name="registers"></param>
        /// <returns></returns>
        public HeroesRegister GetWeirdHeroes(
            HeroesRegister[] registers)
        {
            HeroesContainer weirdHeroes = new HeroesContainer();
            int temp = 0;
            for (int i = 0; i < registers.Length; i++)
            {
                HeroesRegister container = registers[i];
                if (container == null) continue; // skip null containers
                for (int j = 0; j < container.Count; j++)
                {
                    Heroes hero = container.GetHero(j);
                    if (hero == null) continue;
                    // Check for weird characteristics
                    if (hero.IsWeird() == 1)
                    {
                        weirdHeroes.Insert(temp, hero);
                        temp++;
                    }
                }
            }
            weirdHeroes.Sort();
            return new HeroesRegister(weirdHeroes);
        }
        /// <summary>
        /// Gets all unique classes from multiple containers
        /// </summary>
        /// <param name="registers"></param>
        /// <returns></returns>
        public HashSet<string> GetAllUniqueClassesFromContainers(HeroesRegister[]
            registers)
        {
            HashSet<string> uniqueClasses = new HashSet<string>();

            // Iterate through all containers and collect unique classes
            for (int i = 0; i < registers.Length; i++)
            {
                HeroesRegister container = registers[i];
                if (container == null) continue; // skip null containers

                for (int j = 0; j < container.Count; j++)
                {
                    Heroes hero = container.GetHero(j).Clone();
                    if (hero == null) continue;

                    uniqueClasses.Add(hero.Class);
                }
            }

            return uniqueClasses;
        }

        /// <summary>
        /// Checks for the strongest heroes across multiple registers
        /// </summary>
        /// <param name="filePaths"></param>
        /// <returns></returns>
        public Heroes[] GetStrongestHeroesAcrossRegisters(string[] filePaths)
        {
            HeroesContainer allStrongest = new HeroesContainer();

            for (int i = 0; i < filePaths.Length; i++)
            {
                HeroesRegister reg = IOUtils.ReadHeroes(filePaths[i]);
                Heroes[] strongestInReg = reg.GetStrongestHeroes();

                UpdateStrongestOverall(allStrongest, strongestInReg);
            }

            return ConvertContainerToArray(allStrongest);
        }
        /// <summary>
        /// Updates the overall strongest heroes container
        /// </summary>
        /// <param name="strongestOverall"></param>
        /// <param name="strongestInReg"></param>
        private void UpdateStrongestOverall(HeroesContainer strongestOverall,
            Heroes[] strongestInReg)
        {
            for (int j = 0; j < strongestInReg.Length; j++)
            {
                Heroes hero = strongestInReg[j];

                if (strongestOverall.Count == 0)
                {
                    strongestOverall.Add(hero);
                }
                else if (hero.IsStrongerThan(strongestOverall.Get(0)))
                {
                    strongestOverall.Clear();
                    strongestOverall.Add(hero);
                }
                else if (hero.IsEqualStrength(strongestOverall.Get(0)))
                {
                    strongestOverall.Add(hero);
                }
            }
        }

        /// <summary>
        /// Palieka TRUE, jei pirmasis registras turi mažiau herojų nei antrasis.
        /// Lyginama pagal herojų skaičių (Count()).
        /// </summary>
        public static bool operator <(HeroesRegister reg1, HeroesRegister reg2)
        {
            if (reg1 is null || reg2 is null)
            {
                // Null registras visada "mažesnis" už ne-null
                return reg1 is null && !(reg2 is null);
            }
            return reg1.Count < reg2.Count;
        }

        /// <summary>
        /// Palieka TRUE, jei pirmasis registras turi daugiau herojų nei antrasis.
        /// Lyginama pagal herojų skaičių (Count()).
        /// </summary>
        public static bool operator >(HeroesRegister reg1, HeroesRegister reg2)
        {
            if (reg1 is null || reg2 is null)
            {
                // Ne-null registras visada "didesnis" už null
                return !(reg1 is null) && reg2 is null;
            }
            return reg1.Count > reg2.Count;
        }

        /// <summary>
        /// Palieka TRUE, jei pirmasis registras turi mažiau arba
        /// lygiai tiek pat herojų kaip antrasis.
        /// Lyginama pagal herojų skaičių (Count()).
        /// </summary>
        public static bool operator <=(HeroesRegister reg1, HeroesRegister reg2)
        {
            if (reg1 is null && reg2 is null) return true;
            if (reg1 is null || reg2 is null)
            {
                // Null <= ne-null yra true, ne-null <= null yra false
                return reg1 is null;
            }
            return reg1.Count <= reg2.Count;
        }

        /// <summary>
        /// Palieka TRUE, jei pirmasis registras turi daugiau arba lygiai tiek
        /// pat herojų kaip antrasis.
        /// Lyginama pagal herojų skaičių (Count()).
        /// </summary>
        public static bool operator >=(HeroesRegister reg1, HeroesRegister reg2)
        {
            if (reg1 is null && reg2 is null) return true;
            if (reg1 is null || reg2 is null)
            {
                // Ne-null >= null yra true, null >= ne-null yra false
                return reg2 is null;
            }
            return reg1.Count >= reg2.Count;
        }

        // --- LYGUMO (EQUALITY) OPERATORIAI IR METODAI ---

        /// <summary>
        /// Palieka TRUE, jei du registrai yra lygūs (pagal herojų skaičių).
        /// </summary>
        public static bool operator ==(HeroesRegister reg1, HeroesRegister reg2)
        {
            if (reg1 is null && reg2 is null) return true;
            if (reg1 is null || reg2 is null) return false;
            // Galima pridėti ir kitus laukus lyginimui, pvz.:
            // return reg1.Count() == reg2.Count() && reg1.Race == reg2.Race;
            return reg1.Count == reg2.Count;
        }

        /// <summary>
        /// Palieka TRUE, jei du registrai nėra lygūs (pagal herojų skaičių).
        /// </summary>
        public static bool operator !=(HeroesRegister reg1, HeroesRegister reg2)
        {
            return !(reg1 == reg2);
        }

        /// <summary>
        /// Perkrautas Equals metodas lyginimui su objektu.
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is HeroesRegister other)
            {
                return this == other; // Naudojame perkrautą == operatorių
            }
            return false;
        }

        /// <summary>
        /// Perkrautas GetHashCode metodas. Būtinas,
        /// kai perkraunami ==/!= ir Equals.
        /// </summary>
        public override int GetHashCode()
        {
            // Naudojame herojų skaičių (Count()) hash kodo generavimui.
            return this.Count.GetHashCode();
        }
    }
}