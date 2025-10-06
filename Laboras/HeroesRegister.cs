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
            HeroesRegister register = new HeroesRegister();
            string[] lines = File.ReadAllLines(filePath);
            if(lines.Length < 3)
            {
                Console.WriteLine("Trūksta duomenų");
            }
            else
            {
                string race = lines[0];
                string startingCity = lines[1];

                for (int i = 2; i < lines.Length; i++)
                {
                    string[] Values = lines[i].Split(',');
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
                    Heroes hero = new Heroes(name, race, startingCity, @class, lifePoints, mana, dmgPoints, defPoints, power, movement, iQ, specPower);
                    if (!register.Contains(hero))
                    {
                        register.AddHero(hero);
                    }
                }
            }
            return register;
        }
    }
}