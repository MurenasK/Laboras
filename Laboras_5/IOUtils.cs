using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Laboras_5
{
    class IOUtils
    {
        // ============================================================
        // 1. FILE READING
        // ============================================================

        public static PlayerContainer[] ReadPlayersFromFile(string[] fileNames)
        {
            PlayerContainer[] containers = new PlayerContainer[fileNames.Length];

            for (int i = 0; i < fileNames.Length; i++)
                containers[i] = ReadPlayers(fileNames[i]);

            return containers;
        }

        public static PlayerContainer ReadPlayers(string fileName)
        {
            PlayerContainer container = new PlayerContainer();

            using (StreamReader reader = new StreamReader(fileName, Encoding.UTF8))
            {
                container.Race = reader.ReadLine();
                container.City = reader.ReadLine();

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(';');
                    string type = parts[0];
                    string name = parts[1];
                    string cls = parts[2];

                    int life = int.Parse(parts[3]);
                    int mana = int.Parse(parts[4]);
                    int dmg = int.Parse(parts[5]);
                    int armor = int.Parse(parts[6]);

                    if (type == "Heroes")
                    {
                        int power = int.Parse(parts[7]);
                        int movement = int.Parse(parts[8]);
                        int iq = int.Parse(parts[9]);
                        int spec = int.Parse(parts[10]);

                        Heroes hero = new Heroes(name, cls, life, mana, dmg, armor,
                                                 power, movement, iq);
                        container.AddPlayer(hero);
                    }
                    else if (type == "NPC")
                    {
                        int special = int.Parse(parts[7]);

                        NPC npc = new NPC(name, cls, life, mana, dmg, armor, special);
                        container.AddPlayer(npc);
                    }
                }
            }

            return container;
        }


        // ============================================================
        // 2. PRINT PLAYERS TO FILE (NO DATA MODIFICATION)
        // ============================================================

        public static void PrintPlayersToFile(PlayerContainer[] containers, string fileName)
        {
            List<string> output = new List<string>();

            for (int i = 0; i < containers.Length; i++)
            {
                PlayerContainer cont = containers[i].DeepCopy(); // SAFE COPY

                output.Add(cont.Race);
                output.Add(cont.City);
                output.Add(new string('=', 180));

                foreach (Player p in cont.GetPlayers())
                    output.Add(p.ToString());

                output.Add(new string('=', 180));
                output.Add("");
            }

            File.WriteAllLines(fileName, output, Encoding.UTF8);
        }


        // ============================================================
        // 3. BEST LIFE PLAYERS PER FILE
        // ============================================================

        public static void PrintConsoleBestLifePlayerInEachFile(PlayerContainer[] containers)
        {
            Console.WriteLine("\n=== Žaidėjai su didžiausiu gyvybių kiekiu ===\n");

            for (int i = 0; i < containers.Length; i++)
            {
                PlayerContainer cont = containers[i].DeepCopy();

                int maxLife = cont.GetMaxLife();
                List<Player> best = cont.GetPlayersWithLife(maxLife);

                Console.WriteLine($"Rasė: {cont.Race}, Miestas: {cont.City}");

                foreach (Player p in best)
                    Console.WriteLine(p);
                Console.WriteLine();
            }
        }


        // ============================================================
        // 4. STIPRIAUSI VEIKĖJAI (ARMOR > DAMAGE + MEDIANA)
        // ============================================================

        public static void PrintPlayersWithMoreDefThanDmg(PlayerContainer[] containers)
        {
            PlayerContainer[] copy = DeepCopyArray(containers);

            List<Player> selected = CollectStrongPlayers(copy);

            if (selected.Count == 0)
            {
                File.WriteAllText("StipriausiVeikejai.csv", "Nėra stipriausių veikėjų");
                return;
            }

            // rikiavimas naudojant comparator
            copy[0].Sort(selected, new PlayerComparatorByArmorDamage());

            int median = CalculateArmorMedian(selected);

            List<string> lines = new List<string>();
            foreach (Player p in selected)
                lines.Add(p.ToString());

            lines.Add("");
            lines.Add("Gynybos taškų mediana: " + median);

            File.WriteAllLines("StipriausiVeikejai.csv", lines, Encoding.UTF8);
        }


        private static PlayerContainer[] DeepCopyArray(PlayerContainer[] original)
        {
            PlayerContainer[] arr = new PlayerContainer[original.Length];

            for (int i = 0; i < original.Length; i++)
                arr[i] = original[i].DeepCopy();

            return arr;
        }

        private static List<Player> CollectStrongPlayers(PlayerContainer[] containers)
        {
            List<Player> result = new List<Player>();

            for (int i = 0; i < containers.Length; i++)
            {
                List<Player> list = containers[i].GetPlayers();

                for (int j = 0; j < list.Count; j++)
                    if (list[j].Armor > list[j].Dmg)
                        result.Add(list[j]);
            }

            return result;
        }

        private static int CalculateArmorMedian(List<Player> list)
        {
            int count = list.Count;

            if (count % 2 == 1)
                return list[count / 2].Armor;

            int a = list[count / 2 - 1].Armor;
            int b = list[count / 2].Armor;

            return (a + b) / 2;
        }


        // ============================================================
        // 5. STRONGEST HERO ACROSS ALL RACES
        // ============================================================

        public static void PrintStrongestHero(PlayerContainer[] containers)
        {
            PlayerContainer[] copy = DeepCopyArray(containers);

            Player strongest = null;
            string race = "";
            int best = int.MinValue;

            for (int i = 0; i < copy.Length; i++)
            {
                Player h = copy[i].GetStrongestHero();
                if (h == null) continue;

                int power = copy[i].GetHeroStrength(h);

                if (power > best)
                {
                    best = power;
                    strongest = h;
                    race = copy[i].Race;
                }
            }

            Console.WriteLine("\n===== Stipriausias herojus =====");
            Console.WriteLine("Rasė: " + race);
            Console.WriteLine("Stiprumas: " + best);
            Console.WriteLine(strongest.ToString());
        }


        // ============================================================
        // 6. FIND MISSING HERO / NPC CLASSES
        // ============================================================

        public static void FindMissingClasses(PlayerContainer[] containers)
        {
            PlayerContainer[] copy = DeepCopyArray(containers);

            List<string> allHero = CollectAllHeroClasses(copy);
            List<string> allNpc = CollectAllNpcClasses(copy);

            List<string> output = new List<string>();

            for (int i = 0; i < copy.Length; i++)
            {
                PlayerContainer cont = copy[i];

                var missingHero = cont.GetMissingHeroClasses(allHero);
                var missingNpc = cont.GetMissingNpcClasses(allNpc);

                output.Add("Rasė: " + cont.Race);
                output.Add("Trūkstamos HERO klasės: " +
                    (missingHero.Count == 0 ? "Nėra" : string.Join(", ", missingHero)));

                output.Add("Trūkstamos NPC klasės: " +
                    (missingNpc.Count == 0 ? "Nėra" : string.Join(", ", missingNpc)));

                output.Add("");
            }

            File.WriteAllLines("Trukstami.csv", output, Encoding.UTF8);
        }


        private static List<string> CollectAllHeroClasses(PlayerContainer[] arr)
        {
            List<string> list = new List<string>();

            for (int i = 0; i < arr.Length; i++)
                foreach (string cls in arr[i].GetHeroClasses())
                    if (!list.Contains(cls)) list.Add(cls);

            return list;
        }

        private static List<string> CollectAllNpcClasses(PlayerContainer[] arr)
        {
            List<string> list = new List<string>();

            for (int i = 0; i < arr.Length; i++)
                foreach (string cls in arr[i].GetNpcClasses())
                    if (!list.Contains(cls)) list.Add(cls);

            return list;
        }
    }
}
