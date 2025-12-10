using System;
using System.Collections.Generic;

namespace Laboras_5
{
    public class PlayerContainer
    {
        private List<Player> players;

        public string Race { get; set; } // Naikinti 
        public string City { get; set; }

        public PlayerContainer()
        {
            players = new List<Player>();
        }

        public void AddPlayer(Player player)
        {
            players.Add(player); // clonus dadet
        }

        // =====================================================
        // DEEP COPY RETURN
        // =====================================================
        public List<Player> GetPlayers()
        {
            List<Player> copy = new List<Player>();

            for (int i = 0; i < players.Count; i++)
                copy.Add(players[i].Clone());

            return copy;
        }

        // =====================================================
        // MAX LIFE
        // =====================================================
        public int GetMaxLife()
        {
            int max = 0;
            List<Player> list = GetPlayers(); // deep copy

            for (int i = 0; i < list.Count; i++)
                if (list[i].Life > max)
                    max = list[i].Life;

            return max;
        }

        public List<Player> GetPlayersWithLife(int life)
        {
            List<Player> result = new List<Player>();
            List<Player> list = GetPlayers(); // deep copy

            for (int i = 0; i < list.Count; i++)
                if (list[i].Life == life)
                    result.Add(list[i]);

            return result;
        }

        // =====================================================
        // SORTING — works on deep copy list
        // =====================================================
        // Burbaulas while reikia
        public void Sort(List<Player> list, PlayerComparator comparator)
        {
            for (int i = 0; i < list.Count - 1; i++) // cia
            {
                for (int j = 0; j < list.Count - 1 - i; j++)
                {
                    if (comparator.Compare(list[j], list[j + 1]) > 0)
                    {
                        Player tmp = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = tmp;
                    }
                }
            }
        }

        // =====================================================
        // CLASS COLLECTION USING DEEP COPY
        // =====================================================
        public List<string> GetHeroClasses()
        {
            List<string> result = new List<string>();
            List<Player> list = GetPlayers();

            for (int i = 0; i < list.Count; i++)
                if (list[i] is Heroes h && !result.Contains(h.Class))
                    result.Add(h.Class);

            return result;
        }

        public List<string> GetNpcClasses()
        {
            List<string> result = new List<string>();
            List<Player> list = GetPlayers();

            for (int i = 0; i < list.Count; i++)
                if (list[i] is NPC n && !result.Contains(n.Class))
                    result.Add(n.Class);

            return result;
        }

        // =====================================================
        // MISSING CLASS SEARCH — uses deep copy lists
        // =====================================================
        public List<string> GetMissingHeroClasses(List<string> allClasses)
        {
            List<string> existing = GetHeroClasses();
            List<string> missing = new List<string>();

            for (int i = 0; i < allClasses.Count; i++)
                if (!existing.Contains(allClasses[i]))
                    missing.Add(allClasses[i]);

            return missing;
        }

        public List<string> GetMissingNpcClasses(List<string> allClasses)
        {
            List<string> existing = GetNpcClasses();
            List<string> missing = new List<string>();

            for (int i = 0; i < allClasses.Count; i++)
                if (!existing.Contains(allClasses[i]))
                    missing.Add(allClasses[i]);

            return missing;
        }

        // =====================================================
        // HERO STRENGTH
        // =====================================================
        public int GetHeroStrength(Player p)
        {
            return p.Life + p.Armor - p.Dmg;
        }

        public Player GetStrongestHero()
        {
            Player bestHero = null;
            int best = int.MinValue;

            List<Player> list = GetPlayers(); // deep copy

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

        public static List<(Player hero, string race)>
            GetHeroesWithStrength(PlayerContainer[] containers, int strength)
        {
            List<(Player hero, string race)> result = new List<(Player hero,
                string race)>();

            for (int i = 0; i < containers.Length; i++)
            {
                Player h = containers[i].GetStrongestHero();
                if (h == null) continue;

                int s = containers[i].GetHeroStrength(h);
                if (s == strength)
                    result.Add((h, containers[i].Race));
            }

            return result;
        }


        // =====================================================
        // FULL CONTAINER DEEP COPY
        // =====================================================
        public PlayerContainer DeepCopy()
        {
            PlayerContainer copy = new PlayerContainer();

            copy.Race = this.Race;
            copy.City = this.City;

            List<Player> list = this.GetPlayers(); // deep copy

            for (int i = 0; i < list.Count; i++)
                copy.AddPlayer(list[i]); // already cloned

            return copy;
        }
    }
}
