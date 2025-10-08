using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboras
{
    class HeroesGroup
    {
        public string Race { get; set; }
        public string StartingCity { get; set; }
        public List<Heroes> HeroesList { get; set; }

        public HeroesGroup(string race, string startingCity)
        {
            Race = race;
            StartingCity = startingCity;
            HeroesList = new List<Heroes>();
        }

        public void AddHero(Heroes hero)
        {
            HeroesList.Add(hero);
        }
    }
}
