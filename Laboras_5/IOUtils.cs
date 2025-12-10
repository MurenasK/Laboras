using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Laboras_5
{
    class IOUtils
    {
        /// <summary>
        /// Nuskaito žaidėjus iš kelių failų ir grąžina PlayerContainer masyvą.
        /// </summary>
        public static PlayerContainer[] ReadPlayersFromFile(string[] fileNames)
        {
            PlayerContainer[] containers = new PlayerContainer[fileNames.Length];

            for (int i = 0; i < fileNames.Length; i++)
                containers[i] = ReadPlayers(fileNames[i]);

            return containers;
        }

        /// <summary>
        /// Nuskaito žaidėjų duomenis iš vieno failo ir suformuoja
        /// PlayerContainer objektą.
        /// </summary>
        public static PlayerContainer ReadPlayers(string fileName)
        {
            PlayerContainer container = new PlayerContainer();

            using (StreamReader reader = new StreamReader(fileName,
                Encoding.UTF8))
            {
                string Rase = reader.ReadLine();
                string City = reader.ReadLine();

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

                        Heroes hero = new Heroes(name, cls, life, mana, dmg,
                            armor,
                            power, movement, iq);
                        hero.Race = Rase;
                        hero.City = City;
                        container.AddPlayer(hero);
                    }
                    else if (type == "NPC")
                    {
                        int special = int.Parse(parts[7]);

                        NPC npc = new NPC(name, cls, life, mana, dmg, armor,
                            special);
                        npc.Race = Rase;
                        npc.City = City;
                        container.AddPlayer(npc);
                    }
                }
            }
            return container;
        }

        /// <summary>
        /// Išveda visų žaidėjų informaciją į tekstinį failą lentelės formatu.
        /// </summary>
        public static void PrintPlayersToFile(PlayerContainer[] containers,
            string fileName)
        {
            List<string> output = new List<string>();

            for (int i = 0; i < containers.Length; i++)
            {
                PlayerContainer cont = containers[i].DeepCopy();

                var players = cont.GetPlayers();
                string race = players.Count > 0 ? players[0].Race : "Nežinoma";
                string city = players.Count > 0 ? players[0].City : "Nežinomas";

                output.Add(race);
                output.Add(city);
                output.Add(new string('=', 180));
                output.Add(string.Format(
    "| {0,-10} | {1,-20} | {2,-15} | {3,-15} | {4,10} | {5,10} | {6,10} |" +
    " {7,10} | {8,10} | {9,10} | {10,10} | {11,10} |",
    "Tipas", "Vardas", "Klasė", "Rasė", "Gyvybės", "Ištvermė",
    "Puolimas", "Gynyba",
    "Jėga", "Greitis", "IQ", "Special"
));
                output.Add(new string('=', 180));

                foreach (Player p in players)
                    output.Add(p.ToString());

                output.Add(new string('=', 180));
                output.Add("");
            }

            File.WriteAllLines(fileName, output, Encoding.UTF8);
        }

        /// <summary>
        /// Išveda į konsolę žaidėjus su didžiausiu gyvybių kiekiu
        /// kiekviename faile.
        /// </summary>
        public static void PrintConsoleBestLifePlayerInEachFile(
            PlayerContainer[] containers)
        {
            Console.WriteLine("\n=== Žaidėjai su didžiausiu gyvybių kiekiu ===\n");

            for (int i = 0; i < containers.Length; i++)
            {
                PlayerContainer cont = containers[i].DeepCopy();

                int maxLife = cont.GetMaxLife();
                List<Player> best = cont.GetPlayersWithLife(maxLife);

                var players = cont.GetPlayers();
                string race = players.Count > 0 ? players[0].Race : "Nežinoma";
                string city = players.Count > 0 ? players[0].City : "Nežinomas";

                Console.WriteLine($"Rasė: {race}, Miestas: {city}");
                Console.WriteLine("| {0,-10} | {1,-20} | {2,-15} | {3,-15}" +
                    " | {4,10} | {5,10} | {6,10} |" +
                    " {7,10} | {8,10} | {9,10} | {10,10} | {11,10} |",
                    "Tipas", "Vardas", "Klasė", "Rasė", "Gyvybės", "Ištvermė",
                    "Puolimas", "Gynyba",
                    "Jėga", "Greitis", "IQ", "Special");

                foreach (Player p in best)
                    Console.WriteLine(p);

                Console.WriteLine();
            }
        }

        /// <summary>
        /// Surenka visus žaidėjus, kurių gynyba viršija puolimą,
        /// ir išveda juos į CSV failą.
        /// </summary>
        public static void PrintPlayersWithMoreDefThanDmg(
            PlayerContainer[] containers)
        {
            PlayerContainer[] copy = DeepCopyArray(containers);

            PlayerContainer helper = new PlayerContainer();

            List<Player> selected = helper.CollectStrongPlayers(copy);

            if (selected.Count == 0)
            {
                File.WriteAllText("StipriausiVeikejai.csv",
                    "Nera stipriausiu veikeju");
                return;
            }

            copy[0].Sort(selected, new PlayerComparatorByArmorDamage());

            int median = helper.CalculateArmorMedian(selected);

            List<string> lines = new List<string>();
            lines.Add("Tipas,Vardas,Klase,Rase,Gyvybes,Istverme,Puolimas" +
                ",Gynyba,Jega,Greitis,IQ,Special");
            foreach (Player p in selected)
                lines.Add(p.ToCsvString());

            lines.Add("");
            lines.Add("Gynybos taškų mediana: " + ";" + median);

            File.WriteAllLines("StipriausiVeikejai.csv", lines, Encoding.UTF8);
        }

        /// <summary>
        /// Sukuria PlayerContainer masyvo gilią kopiją.
        /// </summary>
        private static PlayerContainer[] DeepCopyArray(PlayerContainer[] original)
        {
            PlayerContainer[] arr = new PlayerContainer[original.Length];

            for (int i = 0; i < original.Length; i++)
                arr[i] = original[i].DeepCopy();

            return arr;
        }

        /// <summary>
        /// Randa stipriausią herojų pagal jėgos atributą ir išveda jį į konsolę.
        /// </summary>
        public static void PrintStrongestHero(PlayerContainer[] containers)
        {
            PlayerContainer helper = new PlayerContainer();
            PlayerContainer[] copy = DeepCopyArray(containers);

            int maxStrength = helper.GetGlobalMaxHeroStrength(copy);
            var heroes = helper.GetHeroesWithStrength(copy, maxStrength);

            PrintHeroes(heroes, maxStrength);
        }

        /// <summary>
        /// Išveda sąrašą herojų su nurodyta jėga į konsolę.
        /// </summary>
        private static void PrintHeroes(List<Player> list, int strength)
        {
            Console.WriteLine("\n===== Stipriausi herojai =====");
            Console.WriteLine("Stiprumas: " + strength);

            foreach (var hero in list)
            {
                Console.WriteLine();
                Console.WriteLine("Rasė: " + hero.Race);
                Console.WriteLine("| {0,-10} | {1,-20} | {2,-15} | {3,-15}" +
                    " | {4,10} | {5,10} | {6,10} |" +
    " {7,10} | {8,10} | {9,10} | {10,10} | {11,10} |",
    "Tipas", "Vardas", "Klasė", "Rasė", "Gyvybės", "Ištvermė",
    "Puolimas", "Gynyba",
    "Jėga", "Greitis", "IQ", "Special");
                Console.WriteLine(hero.ToString());
            }
        }

        /// <summary>
        /// Aptinka, kurios HERO ir NPC klasės yra praleistos kiekviename faile,
        /// ir įrašo rezultatus į CSV.
        /// </summary>
        public static void FindMissingClasses(PlayerContainer[] containers)
        {
            PlayerContainer[] copy = DeepCopyArray(containers);

            PlayerContainer helper = new PlayerContainer();

            List<string> allHero = helper.CollectAllHeroClasses(copy);
            List<string> allNpc = helper.CollectAllNpcClasses(copy);

            List<string> output = new List<string>();

            for (int i = 0; i < copy.Length; i++)
            {
                PlayerContainer cont = copy[i];

                var missingHero = cont.GetMissingHeroClasses(allHero);
                var missingNpc = cont.GetMissingNpcClasses(allNpc);

                var players = cont.GetPlayers();
                string race = players.Count > 0 ? players[0].Race : "Nežinoma";

                output.Add("Rasė: " + race);
                output.Add("Trūkstamos HERO klasės: " + ";" +
                    (missingHero.Count == 0 ? "Nėra" : string.Join("; ",
                    missingHero)));

                output.Add("Trūkstamos NPC klasės: " + ";" +
                    (missingNpc.Count == 0 ? "Nėra" : string.Join("; ",
                    missingNpc)));

                output.Add("");
            }

            File.WriteAllLines("Trukstami.csv", output, Encoding.UTF8);
        }
    }
}
