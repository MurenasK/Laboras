using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Laboras
{
    class HeroesRegister
    {
        private List<Heroes> heroes;

        public HeroesRegister()
        {
            heroes = new List<Heroes>();
        }

        public bool Contains(Heroes hero)
        {
            return heroes.Contains(hero);
        }

        public HeroesRegister(List<Heroes> Heroes)
        {
            heroes = new List<Heroes>();
            foreach (Heroes hero in Heroes)
            {
                if (!heroes.Contains(hero))
                {
                    heroes.Add(hero);
                }
            }
        }

        public void AddHero(Heroes hero)
        {
            if (!heroes.Contains(hero))
            {
                heroes.Add(hero);
            }
        }

        public int HeroCount()
        {
            return heroes.Count;
        }

        public Heroes GetHero(int index)
        {
            if (index < 0 || index >= heroes.Count)
            {
                Console.WriteLine("Herojaus nėra :("); ;
            }   
            return heroes[index];
        }

        public static HeroesRegister ReadHeroesFromFile(string filePath)
        {
            List<Heroes> heroesList = OutUtils.ReadHeroes(filePath); // Adjusted to match the return type of OutUtils.ReadHeroes
            HeroesRegister register = new HeroesRegister(heroesList); // Use the constructor that accepts a List<Heroes>
            return register;
        }

        public List<Heroes> GetHeroesList()
        {
            return new List<Heroes>(heroes);
        }

        public static List<string> GetUniqueClasses(List<Heroes> heroes)
        {
            HashSet<string> uniqueClasses = new HashSet<string>();
            foreach (var hero in heroes)
            {
                uniqueClasses.Add(hero.Class);
            }
            return uniqueClasses.ToList();
        }
        // Pataisyti kad nebutu 2 foreachaai
        public static List<Heroes> GetStrongestHero(HeroesRegister register)
        {
            List<Heroes> heroes = register.GetHeroesList();
            List<Heroes> strongestHeroes = new List<Heroes>();
            if (heroes.Count == 0)
            {
                return strongestHeroes;
            }
            int highestPower = heroes[0].LifePoints + heroes[0].DefPoints - heroes[0].DmgPoints;
            foreach (var hero in heroes)
            {
                int heroPower = hero.LifePoints + hero.DefPoints - hero.DmgPoints;
                if (heroPower > highestPower)
                {
                    highestPower = heroPower;
                }
            }
            foreach (var hero in heroes)
            {
                int heroPower = hero.LifePoints + hero.DefPoints - hero.DmgPoints;
                if (heroPower == highestPower)
                {
                    strongestHeroes.Add(hero);
                }
            }
            return strongestHeroes;
        }
    }
}