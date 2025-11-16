using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

public static class TaskUtils
{
    /// <summary>
    //
    /// </summary>
    private static readonly Regex wordRegex =
        new Regex(@"[A-Za-zĄČĘĖĮŠŲŪŽąčęėįšųūž]+", RegexOptions.Compiled);

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

    /// <summary>
    /// Puts everything To list
    /// </summary>
    /// <param name="dict"></param>
    /// <returns></returns>
    public static List<KeyValuePair<string, int>> ToList(Dictionary<string,
        int> dict)
    {
        var list = new List<KeyValuePair<string, int>>();

        foreach (var p in dict)
            list.Add(p);

        return list;
    }
    /// <summary>
    /// Sorting out the Statistics
    /// </summary>
    /// <param name="list"></param>
    public static void SortStatistics(List<KeyValuePair<string, int>> list)
    {
        for (int i = 0; i < list.Count - 1; i++)
        {
            for (int j = 0; j < list.Count - i - 1; j++)
            {
                bool swap = false;

                if (list[j].Value < list[j + 1].Value)
                    swap = true;

                else if (list[j].Value == list[j + 1].Value &&
                         string.Compare(list[j].Key, list[j + 1].Key,
                         StringComparison.Ordinal) > 0)
                    swap = true;

                if (swap)
                {
                    var temp = list[j];
                    list[j] = list[j + 1];
                    list[j + 1] = temp;
                }
            }
        }
    }
    /// <summary>
    /// Rebuilds text the correct way
    /// </summary>
    /// <param name="input"></param>
    /// <param name="output"></param>
    public static void RebuildText(string input, string output)
    {
        List<List<string>> allLines = new List<List<string>>();

        using (StreamReader reader = new StreamReader(input))
        {
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                List<string> words = new List<string>();

                foreach (Match m in wordRegex.Matches(line))
                    words.Add(m.Value);

                if (words.Count > 0)
                    allLines.Add(words);
            }
        }

        int maxCols = 0;
        for (int i = 0; i < allLines.Count; i++)
            if (allLines[i].Count > maxCols)
                maxCols = allLines[i].Count;

        int[] pos = new int[maxCols];
        pos[0] = 1;

        for (int c = 1; c < maxCols; c++)
        {
            int maxLen = 0;

            for (int r = 0; r < allLines.Count; r++)
            {
                if (allLines[r].Count > c - 1)
                {
                    int len = allLines[r][c - 1].Length;
                    if (len > maxLen) maxLen = len;
                }
            }

            pos[c] = pos[c - 1] + maxLen + 1;
        }

        using (StreamWriter writer = new StreamWriter(output))
        {
            for (int i = 0; i < allLines.Count; i++)
            {
                List<string> line = allLines[i];
                StringBuilder sb = new StringBuilder();

                for (int c = 0; c < line.Count; c++)
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
}
