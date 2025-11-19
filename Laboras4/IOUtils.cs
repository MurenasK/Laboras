using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Laboras4
{
    class IOUtils
    {
        /// <summary>
        /// Regex skirtas žodžių paieškai
        /// (lietuviški simboliai ir skaičiai įtraukti).
        /// </summary>
        private static readonly Regex wordRegex =
            new Regex(@"[A-Za-zĄČĘĖĮŠŲŪŽąčęėįšųūž0-9]+", RegexOptions.Compiled);

        public static readonly string punctuation = " ,.;:!?()-";

        /// <summary>
        /// Informacinė klasė sakinių duomenims saugoti.
        /// </summary>
        private class SentenceInfo
        {
            public string Text;
            public int Words;
            public int Chars;
            public int StartLine;
        }

        /// <summary>
        /// Nuskaito failą ir suskaičiuoja,
        /// kiek kartų pasikartoja kiekvienas žodis.
        /// </summary>
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

        /// <summary>
        /// Perskaito failo eilutes ir grąžina suskaidytus
        /// bei apdorotus žodžių masyvus.
        /// </summary>
        public static List<List<string>> ReadWordLines(string filename)
        {
            List<List<string>> result = new List<List<string>>();

            using (StreamReader reader = new StreamReader(filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var tokens = TaskUtils.Tokenize(line);
                    var merged = TaskUtils.MergeTokens(tokens);

                    if (merged.Count > 0)
                        result.Add(merged);
                }
            }

            return result;
        }

        /// <summary>
        /// Išveda tekstą į failą taip, kad stulpeliai būtų
        /// sulygiuoti pagal colPos.
        /// </summary>
        public static void WriteAlignedLines(string output,
            List<List<string>> lines, int[] colPos)
        {
            using (StreamWriter writer = new StreamWriter(output))
            {
                foreach (var line in lines)
                {
                    StringBuilder sb = new StringBuilder();

                    for (int i = 0; i < line.Count; i++)
                    {
                        int target = colPos[i] - 1;

                        while (sb.Length < target - 1)
                            sb.Append(' ');

                        sb.Append(line[i]);
                    }

                    writer.WriteLine(sb.ToString().TrimEnd());
                }
            }
        }

        /// <summary>
        /// Grąžina 10 dažniausiai pasitaikančių žodžių iš failo.
        /// </summary>
        private static List<KeyValuePair<string, int>>
            GetTopWords(string inputFile)
        {
            var wordCounts = ReadWords(inputFile);
            var list = TaskUtils.ToList(wordCounts);
            TaskUtils.SortStatistics(list);

            int limit = Math.Min(10, list.Count);
            return list.GetRange(0, limit);
        }

        /// <summary>
        /// Suranda ilgiausius sakinius faile pagal žodžių ir simbolių skaičių.
        /// </summary>
        private static List<SentenceInfo> FindLongestSentences(string inputFile)
        {
            string[] lines = File.ReadAllLines(inputFile);
            string fullText = string.Join("\n", lines);

            Regex sentenceRegex = new Regex(@"[^\.!?]+[\.!?]",
                RegexOptions.Multiline);
            var matches = sentenceRegex.Matches(fullText);

            List<SentenceInfo> results = new List<SentenceInfo>();

            int maxWords = 0;
            int maxChars = 0;

            foreach (Match m in matches)
            {
                string s = m.Value.Trim();

                int wordCount = Regex.Matches(s,
                    @"[A-Za-zĄČĘĖĮŠŲŪŽąčęėįšųūž0-9]+").Count;

                int charCount = s.Length;

                int index = fullText.IndexOf(s);
                int startLine = fullText.Substring(0, index).Split('\n').Length;

                if (wordCount > maxWords || (wordCount == maxWords &&
                    charCount > maxChars))
                {
                    maxWords = wordCount;
                    maxChars = charCount;

                    results.Clear();
                    results.Add(new SentenceInfo
                    {
                        Text = s,
                        Words = wordCount,
                        Chars = charCount,
                        StartLine = startLine
                    });
                }
                else if (wordCount == maxWords && charCount == maxChars)
                {
                    results.Add(new SentenceInfo
                    {
                        Text = s,
                        Words = wordCount,
                        Chars = charCount,
                        StartLine = startLine
                    });
                }
            }

            return results;
        }

        /// <summary>
        /// Įrašo dažniausius žodžius į rezultatų failą.
        /// </summary>
        private static void WriteTopWords(StreamWriter w,
            List<KeyValuePair<string, int>> topWords)
        {
            if (topWords.Count == 0) return;

            w.WriteLine("DAŽNIAUSI ŽODŽIAI:");
            foreach (var p in topWords)
                w.WriteLine($"{p.Key} {p.Value}");
            w.WriteLine();
        }

        /// <summary>
        /// Įrašo ilgiausius sakinius į rezultatų failą.
        /// </summary>
        private static void WriteLongestSentences(StreamWriter w,
            List<SentenceInfo> sentences)
        {
            if (sentences.Count == 0)
            {
                w.WriteLine("Neaptikta žodžių");
                return;
            }

            w.WriteLine("ILGIAUSIAS SAKINYS/IAI:");
            foreach (var s in sentences)
            {
                w.WriteLine(s.Text);
                w.WriteLine($"Žodžių kiekis: {s.Words}");
                w.WriteLine($"Simbolių kiekis: {s.Chars}");
                w.WriteLine($"Sakinio pradžios eilutė: {s.StartLine}");
                w.WriteLine();
            }
        }

        /// <summary>
        /// Generuoja visus rodiklius: dažniausius žodžius ir ilgiausius sakinius.
        /// </summary>
        public static void WriteIndicators(string inputFile, string outputFile)
        {
            var topWords = GetTopWords(inputFile);
            var longestSentences = FindLongestSentences(inputFile);

            using (StreamWriter w = new StreamWriter(outputFile))
            {
                WriteTopWords(w, topWords);
                WriteLongestSentences(w, longestSentences);
            }
        }
    }
}
