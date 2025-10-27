using Laboras_3;
using System;
using System.Collections.Generic;
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

        private HeroesContainer heroes = new HeroesContainer();

        public HeroesRegister(HeroesContainer container)
        {
            this.heroes = container; // your internal container field
        }

        public int Count => heroes.Count;

        public Heroes[] GetAllHeroes()
        {
            Heroes[] arr = new Heroes[heroes.Count];
            for (int i = 0; i < heroes.Count; i++)
                arr[i] = heroes.Get(i);
            return arr;
        }

        public void AddHero(Heroes hero)
        {
            if (!Contains(hero))
                heroes.Add(hero);
        }

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

        public Heroes GetHero(int index)
        {
            return heroes.Get(index);
        }

        /// <summary>
        /// Brings out the strongest hero from the register
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public Heroes[] GetStrongestHeroes()
        {
            if (heroes.Count == 0) return new Heroes[0];

            HeroesContainer strongest = new HeroesContainer();
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

            // Convert HeroesContainer to array manually
            Heroes[] result = new Heroes[strongest.Count];
            for (int i = 0; i < strongest.Count; i++)
            {
                result[i] = strongest.Get(i);
            }

            return result;
        }


        /// <summary>
        /// Takjes out unique classes from the register
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public static List<string> GetAllUniqueClasses(HeroesRegister[] registers)
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


        public static List<string> GetUniqueClasses(HeroesRegister register)
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
        /// Checks for the strongest heroes across multiple registers
        /// </summary>
        /// <param name="filePaths"></param>
        /// <returns></returns>
        public static Heroes[] GetStrongestHeroesAcrossRegisters(string[] filePaths)
        {
            HeroesContainer strongestOverall = new HeroesContainer();

            for (int i = 0; i < filePaths.Length; i++)
            {
                HeroesContainer reg = IOUtils.ReadHeroes(filePaths[i]);
                Heroes[] strongestInReg = reg.GetStrongestHeroes(); // container method returning array

                for (int j = 0; j < strongestInReg.Length; j++)
                {
                    Heroes hero = strongestInReg[j];

                    if (strongestOverall.Count == 0)
                    {
                        strongestOverall.Add(hero);
                    }
                    else if (hero.IsStrongerThan(strongestOverall.Get(0)))
                    {
                        strongestOverall = new HeroesContainer();
                        strongestOverall.Add(hero);
                    }
                    else if (hero.IsEqualStrength(strongestOverall.Get(0)))
                    {
                        strongestOverall.Add(hero);
                    }
                }
            }

            // Convert container to array to return
            Heroes[] result = new Heroes[strongestOverall.Count];
            for (int k = 0; k < strongestOverall.Count; k++)
                result[k] = strongestOverall.Get(k);

            return result;
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