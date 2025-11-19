using Laboras4;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;

public static class TaskUtils
{
    /// <summary>
    /// Konvertuoja žodžių dažnių žodyną į sąrašą,
    /// kad būtų galima lengviau rikiuoti ar apdoroti.
    /// </summary>
    public static List<KeyValuePair<string, int>>
        ToList(Dictionary<string, int> dict)
    {
        List<KeyValuePair<string, int>> list =
            new List<KeyValuePair<string, int>>();
        foreach (var p in dict) list.Add(p);
        return list;
    }

    /// <summary>
    /// Rikiuoja žodžių-statistikų sąrašą mažėjančia tvarka pagal reikšmę,
    /// o esant vienodoms reikšmėms — pagal abėcėlę.
    /// </summary>
    public static void SortStatistics(List<KeyValuePair<string, int>> list)
    {
        for (int i = 0; i < list.Count - 1; i++)
        {
            for (int j = 0; j < list.Count - i - 1; j++)
            {
                bool swap = false;

                if (list[j].Value < list[j + 1].Value) swap = true;
                else if (list[j].Value == list[j + 1].Value &&
                         string.Compare(list[j].Key, list[j + 1].Key,
                         StringComparison.Ordinal) > 0)
                    swap = true;

                if (swap)
                {
                    var tmp = list[j];
                    list[j] = list[j + 1];
                    list[j + 1] = tmp;
                }
            }
        }
    }

    /// <summary>
    /// Iš naujo sudėlioja tekstą iš failo taip,
    /// kad stulpeliai būtų sulygiuoti pagal maksimalų žodžių plotį.
    /// </summary>
    public static void RebuildText(string input, string output)
    {
        var lines = IOUtils.ReadWordLines(input);
        int maxCols = GetMaxColumns(lines);
        int[] colPos = CalculatePositions(lines, maxCols);
        IOUtils.WriteAlignedLines(output, lines, colPos);
    }

    /// <summary>
    /// Randa didžiausią stulpelių kiekį tarp visų eilučių.
    /// </summary>
    private static int GetMaxColumns(List<List<string>> lines)
    {
        int max = 0;
        foreach (var l in lines)
            if (l.Count > max) max = l.Count;

        return max;
    }

    /// <summary>
    /// Apskaičiuoja kiekvieno stulpelio pradžios poziciją tekste.
    /// </summary>
    private static int[] CalculatePositions(List<List<string>> lines, int maxCols)
    {
        if (maxCols == 0)
            return new int[0];

        int[] pos = new int[maxCols];
        pos[0] = 1;

        for (int i = 1; i < maxCols; i++)
        {
            int maxLen = GetColumnMax(lines, i - 1);
            pos[i] = pos[i - 1] + maxLen + 1;
        }

        return pos;
    }

    /// <summary>
    /// Randa didžiausią žodžio ilgį konkrečiame stulpelyje.
    /// </summary>
    private static int GetColumnMax(List<List<string>> lines, int col)
    {
        int max = 0;

        foreach (var line in lines)
            if (line.Count > col && line[col].Length > max)
                max = line[col].Length;

        return max;
    }

    /// <summary>
    /// Suskaido eilutę į žodžius ir skyrybos ženklus,
    /// taip pat sutvarko pasikartojančius skyrybos simbolius.
    /// </summary>
    public static List<string> Tokenize(string line)
    {
        line = Regex.Replace(
            line,
            @"([" + Regex.Escape(IOUtils.punctuation) + @"])\1+",
            "$1"
        );

        var matches = Regex.Matches(
            line,
            @"[A-Za-zĄČĘĖĮŠŲŪŽąčęėįšųūž0-9]+|[" +
            Regex.Escape(IOUtils.punctuation) + "]+"
        );

        List<string> tokens = new List<string>();
        foreach (Match m in matches)
            tokens.Add(m.Value);

        return tokens;
    }

    /// <summary>
    /// Sujungia žodžius ir šalia jų esančius skyrybos ženklus
    /// į vientisus tekstinius elementus.
    /// </summary>
    public static List<string> MergeTokens(List<string> tokens)
    {
        List<string> merged = new List<string>();
        bool containsWord = false;

        foreach (var tok in tokens)
        {
            if (char.IsLetterOrDigit(tok[0]))
            {
                containsWord = true;
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

        if (!containsWord)
        {
            return new List<string>();
        }

        return merged;
    }
}
