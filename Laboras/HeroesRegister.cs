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

    }
}
