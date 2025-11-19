using System;
using System.Collections.Generic;
using Laboras4;

public static class TaskUtils
{
    public static List<KeyValuePair<string, int>> ToList(Dictionary<string, int> dict)
    {
        List<KeyValuePair<string, int>> list = new List<KeyValuePair<string, int>>();
        foreach (var p in dict) list.Add(p);
        return list;
    }

    public static void SortStatistics(List<KeyValuePair<string, int>> list)
    {
        for (int i = 0; i < list.Count - 1; i++)
        {
            for (int j = 0; j < list.Count - i - 1; j++)
            {
                bool swap = false;

                if (list[j].Value < list[j + 1].Value) swap = true;
                else if (list[j].Value == list[j + 1].Value &&
                         string.Compare(list[j].Key, list[j + 1].Key, StringComparison.Ordinal) > 0)
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

    public static void RebuildText(string input, string output)
    {
        var lines = IOUtils.ReadWordLines(input);
        int maxCols = GetMaxColumns(lines);
        int[] colPos = CalculatePositions(lines, maxCols);
        IOUtils.WriteAlignedLines(output, lines, colPos);
    }

    private static int GetMaxColumns(List<List<string>> lines)
    {
        int max = 0;
        foreach (var l in lines)
            if (l.Count > max) max = l.Count;

        return max;
    }

    private static int[] CalculatePositions(List<List<string>> lines, int maxCols)
    {
        int[] pos = new int[maxCols];
        pos[0] = 1;

        for (int i = 1; i < maxCols; i++)
        {
            int maxLen = GetColumnMax(lines, i - 1);
            pos[i] = pos[i - 1] + maxLen + 1;
        }

        return pos;
    }

    private static int GetColumnMax(List<List<string>> lines, int col)
    {
        int max = 0;

        foreach (var line in lines)
            if (line.Count > col && line[col].Length > max)
                max = line[col].Length;

        return max;
    }
}
