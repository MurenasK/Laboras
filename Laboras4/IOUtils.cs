using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Laboras4
{
    class IOUtils
    {
        private static readonly Regex wordRegex =
            new Regex(@"[A-Za-zĄČĘĖĮŠŲŪŽąčęėįšųūž0-9]+", RegexOptions.Compiled);

        private static readonly string punctuation = " ,.;:!?()-";


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
                        if (counts.ContainsKey(w)) counts[w]++;
                        else counts[w] = 1;
                    }
                }
            }

            return counts;
        }

        public static void WriteStats(string fileName, Dictionary<string, int> stats)
        {
            using (StreamWriter writer = new StreamWriter(fileName, false, Encoding.UTF8))
            {
                var list = TaskUtils.ToList(stats);
                TaskUtils.SortStatistics(list);

                int limit = Math.Min(list.Count, 10);
                for (int i = 0; i < limit; i++)
                    writer.WriteLine($"{list[i].Key} {list[i].Value}");
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
                    var tokens = Tokenize(line);
                    var merged = MergeTokens(tokens);

                    if (merged.Count > 0)
                        result.Add(merged);
                }
            }

            return result;
        }
        // pataisymui kablelis ir ilgiausias sakinys ir kai kableliai pranesimas kad nera

        private static List<string> Tokenize(string line)
        {
            line = Regex.Replace(
                line,
                @"([" + Regex.Escape(punctuation) + @"])\1+",
                "$1"
            );

            var matches = Regex.Matches(
                line,
                @"[A-Za-zĄČĘĖĮŠŲŪŽąčęėįšųūž0-9]+|[" + Regex.Escape(punctuation) + "]+"
            );

            List<string> tokens = new List<string>();
            foreach (Match m in matches)
                tokens.Add(m.Value);

            return tokens;
        }

        private static List<string> MergeTokens(List<string> tokens)
        {
            List<string> merged = new List<string>();

            foreach (var tok in tokens)
            {
                if (char.IsLetterOrDigit(tok[0]))
                {
                    merged.Add(tok);
                }
                else
                {
                    if (merged.Count > 0)
                        merged[merged.Count - 1] += tok;
                    else
                        merged.Add(tok);
                }
            }

            return merged;
        }

        public static void WriteAlignedLines(string output, List<List<string>> lines, int[] colPos)
        {
            using (StreamWriter writer = new StreamWriter(output))
            {
                foreach (var line in lines)
                {
                    StringBuilder sb = new StringBuilder();

                    for (int i = 0; i < line.Count; i++)
                    {
                        int target = colPos[i] - 1;

                        while (sb.Length < target)
                            sb.Append(' ');

                        sb.Append(line[i]);
                    }

                    writer.WriteLine(sb.ToString().TrimEnd());
                }
            }
        }
    }
}
