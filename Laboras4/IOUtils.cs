using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Laboras4
{
    class IOUtils
    {

        private static readonly Regex wordRegex =
        new Regex(@"[A-Za-zĄČĘĖĮŠŲŪŽąčęėįšųūž]+", RegexOptions.Compiled);
        public static void WriteStats(string fileName, Dictionary<string,
            int> stats)
        {
            using (StreamWriter writer = new StreamWriter(fileName, false,
                Encoding.UTF8))
            {
                var list = TaskUtils.ToList(stats);
                TaskUtils.SortStatistics(list);

                int limit = list.Count < 10 ? list.Count : 10;

                for (int i = 0; i < limit; i++)
                {
                    writer.WriteLine($"{list[i].Key} {list[i].Value}");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static Dictionary<string, int> ReadWords(string filename)
        {
            Dictionary<string, int> counts = new Dictionary<string, int>();

            using (StreamReader reader = new StreamReader(filename))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    foreach (Match m in wordRegex.Matches(line))
                    {
                        string w = m.Value.ToLower();

                        if (counts.ContainsKey(w))
                            counts[w]++;
                        else
                            counts[w] = 1;
                    }
                }
            }

            return counts;
        }

        public static void WriteAlignedLines(string output, List<List<string>>
            allLines, int[] pos)
        {
            using (StreamWriter writer = new StreamWriter(output))
            {
                foreach(var line in allLines)
                {
                    StringBuilder sb = new StringBuilder();
                    for(int c = 0; c < line.Count; c++)
                    {
                        int target = pos[c] - 1;

                        while (sb.Length < target)
                            sb.Append(' ');

                        sb.Append(line[c]);
                    }
                    writer.WriteLine(sb.ToString().TrimEnd());
                }
            }
        }

        public static List<List<string>> ReadWordLines(string filename)
        {
            List<List<string>> result = new List<List<string>>();
            using (StreamReader reader = new StreamReader(filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    List<string> words = new List<string>();
                    foreach(Match m in wordRegex.Matches(line))
                        words.Add(m.Value);

                    if (words.Count > 0)
                    {
                        result.Add(words);
                    }
                }
            }
            return result;
        }
    }
}
