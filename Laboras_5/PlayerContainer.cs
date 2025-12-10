using System;
using System.Collections.Generic;

namespace Laboras_5
{
    public class PlayerContainer
    {
        /// <summary>
        /// Žaidėjų sąrašas (saugomas vidinis sąrašas).
        /// </summary>
        private List<Player> players;

        /// <summary>
        /// Inicializuoja tuščią PlayerContainer objektą.
        /// </summary>
        public PlayerContainer()
        {
            players = new List<Player>();
        }

        /// <summary>
        /// Prideda žaidėją į konteinerį, naudojant jo kopiją.
        /// </summary>
        public void AddPlayer(Player player)
        {
            players.Add(player.Clone());
        }

        /// <summary>
        /// Grąžina gilią žaidėjų sąrašo kopiją.
        /// </summary>
        public List<Player> GetPlayers()
        {
            List<Player> copy = new List<Player>();

            for (int i = 0; i < players.Count; i++)
                copy.Add(players[i].Clone());

            return copy;
        }

        /// <summary>
        /// Grąžina didžiausią gyvybių (Life) reikšmę tarp visų žaidėjų.
        /// </summary>
        public int GetMaxLife()
        {
            int max = 0;
            List<Player> list = GetPlayers();

            for (int i = 0; i < list.Count; i++)
                if (list[i].Life > max)
                    max = list[i].Life;

            return max;
        }

        /// <summary>
        /// Grąžina visus žaidėjus, kurių Life atitinka nurodytą reikšmę.
        /// </summary>
        public List<Player> GetPlayersWithLife(int life)
        {
            List<Player> result = new List<Player>();
            List<Player> list = GetPlayers();

            for (int i = 0; i < list.Count; i++)
                if (list[i].Life == life)
                    result.Add(list[i]);

            return result;
        }

        /// <summary>
        /// Surūšiuoja žaidėjų sąrašą pagal pateiktą PlayerComparator.
        /// </summary>
        public void Sort(List<Player> list, PlayerComparator comparator)
        {
            int i = 1;

            while (i < list.Count)
            {
                Player current = list[i];
                int j = i - 1;
                while (j >= 0 && comparator.Compare(list[j], current) > 0)
                {
                    list[j + 1] = list[j];
                    j--;
                }

                list[j + 1] = current;
                i++;
            }
        }

        /// <summary>
        /// Grąžina unikalių HERO klasės pavadinimų sąrašą.
        /// </summary>
        public List<string> GetHeroClasses()
        {
            List<string> result = new List<string>();
            List<Player> list = GetPlayers();

            for (int i = 0; i < list.Count; i++)
                if (list[i] is Heroes h && !result.Contains(h.Class))
                    result.Add(h.Class);

            return result;
        }

        /// <summary>
        /// Grąžina unikalių NPC klasės pavadinimų sąrašą.
        /// </summary>
        public List<string> GetNpcClasses()
        {
            List<string> result = new List<string>();
            List<Player> list = GetPlayers();

            for (int i = 0; i < list.Count; i++)
                if (list[i] is NPC n && !result.Contains(n.Class))
                    result.Add(n.Class);

            return result;
        }

        /// <summary>
        /// Grąžina HERO klases, kurių trūksta šiame konteineryje.
        /// </summary>
        public List<string> GetMissingHeroClasses(List<string> allClasses)
        {
            List<string> existing = GetHeroClasses();
            List<string> missing = new List<string>();

            for (int i = 0; i < allClasses.Count; i++)
                if (!existing.Contains(allClasses[i]))
                    missing.Add(allClasses[i]);

            return missing;
        }

        /// <summary>
        /// Grąžina NPC klases, kurių trūksta šiame konteineryje.
        /// </summary>
        public List<string> GetMissingNpcClasses(List<string> allClasses)
        {
            List<string> existing = GetNpcClasses();
            List<string> missing = new List<string>();

            for (int i = 0; i < allClasses.Count; i++)
                if (!existing.Contains(allClasses[i]))
                    missing.Add(allClasses[i]);

            return missing;
        }

        /// <summary>
        /// Apskaičiuoja herojaus stiprumą pagal (Life + Armor - Dmg).
        /// </summary>
        public int GetHeroStrength(Player p)
        {
            return p.Life + p.Armor - p.Dmg;
        }

        /// <summary>
        /// Grąžina stipriausią herojų šiame konteineryje.
        /// </summary>
        public Player GetStrongestHero()
        {
            Player bestHero = null;
            int best = int.MinValue;

            List<Player> list = GetPlayers();

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] is Heroes h)
                {
                    int s = GetHeroStrength(h);
                    if (s > best)
                    {
                        best = s;
                        bestHero = h;
                    }
                }
            }

            return bestHero;
        }

        /// <summary>
        /// Randa didžiausią herojaus stiprumą visų konteinerių masyve.
        /// </summary>
        public int GetGlobalMaxHeroStrength(PlayerContainer[] containers)
        {
            int best = int.MinValue;

            for (int i = 0; i < containers.Length; i++)
            {
                Player h = containers[i].GetStrongestHero();
                if (h == null) continue;

                int s = containers[i].GetHeroStrength(h);
                if (s > best)
                    best = s;
            }

            return best;
        }

        /// <summary>
        /// Grąžina visus herojus, kurių stiprumas lygus nurodytai reikšmei.
        /// </summary>
        public List<Player> GetHeroesWithStrength(PlayerContainer[] containers,
            int strength)
        {
            List<Player> result = new List<Player>();

            for (int i = 0; i < containers.Length; i++)
            {
                Player h = containers[i].GetStrongestHero();
                if (h == null) continue;

                int s = containers[i].GetHeroStrength(h);
                if (s == strength)
                    result.Add(h);
            }

            return result;
        }

        /// <summary>
        /// Sukuria gilią PlayerContainer kopiją.
        /// </summary>
        public PlayerContainer DeepCopy()
        {
            PlayerContainer copy = new PlayerContainer();
            List<Player> list = this.GetPlayers();

            for (int i = 0; i < list.Count; i++)
                copy.AddPlayer(list[i]);

            return copy;
        }

        /// <summary>
        /// Surenka visas HERO klases iš pateiktų konteinerių masyvo.
        /// </summary>
        public List<string> CollectAllHeroClasses(PlayerContainer[] containers)
        {
            List<string> list = new List<string>();

            for (int i = 0; i < containers.Length; i++)
            {
                foreach (string cls in containers[i].GetHeroClasses())
                    if (!list.Contains(cls))
                        list.Add(cls);
            }

            return list;
        }

        /// <summary>
        /// Surenka visas NPC klases iš pateiktų konteinerių masyvo.
        /// </summary>
        public List<string> CollectAllNpcClasses(PlayerContainer[] containers)
        {
            List<string> list = new List<string>();

            for (int i = 0; i < containers.Length; i++)
            {
                foreach (string cls in containers[i].GetNpcClasses())
                    if (!list.Contains(cls))
                        list.Add(cls);
            }

            return list;
        }

        /// <summary>
        /// Surenka visus žaidėjus, kurių Armor didesnis už Dmg.
        /// </summary>
        public List<Player> CollectStrongPlayers(PlayerContainer[] containers)
        {
            List<Player> result = new List<Player>();

            for (int i = 0; i < containers.Length; i++)
            {
                List<Player> list = containers[i].GetPlayers();

                for (int j = 0; j < list.Count; j++)
                {
                    if (list[j].Armor > list[j].Dmg)
                        result.Add(list[j]);
                }
            }

            return result;
        }

        /// <summary>
        /// Apskaičiuoja gynybos (Armor) medianą žaidėjų sąraše.
        /// </summary>
        public int CalculateArmorMedian(List<Player> list)
        {
            if (list == null || list.Count == 0)
                return 0;

            list.Sort((p1, p2) => p1.Armor.CompareTo(p2.Armor));

            int count = list.Count;

            if (count % 2 == 1)
                return list[count / 2].Armor;

            int a = list[count / 2 - 1].Armor;
            int b = list[count / 2].Armor;

            return (a + b) / 2;
        }
    }
}
