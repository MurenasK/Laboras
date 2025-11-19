using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Laboras4;
using System.Xml.Schema;

public static class TaskUtils
{
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
        var allLines = IOUtils.ReadWordLines(input);
        int maxCols = GetMaxColumnCount(allLines);
        int[] pos = CalculateColumnPositions(allLines, maxCols);
        IOUtils.WriteAlignedLines(output, allLines, pos);
    }

    private static int GetMaxColumnCount(List<List<string>> allLines)
    {
        int max = 0;
        foreach (var line in allLines)
        {
            if (line.Count > max)
                max = line.Count;
        }
        return max;
    }

    private static int[] CalculateColumnPositions(List<List<string>> 
        allLines, int maxCols)
    {
        int[] pos = new int[maxCols];
        pos[0] = 1;
        for(int i = 1; i < maxCols; i++)
        {
            int maxLen = 0;
            for(int j = 0; j < allLines.Count; j++)
            {
                if(allLines[j].Count > i - 1)
                {
                    int len = allLines[j][i - 1].Length;
                    if (len > maxLen)
                    {
                        maxLen = len;
                    }
                }
            }
            pos[i] = pos[i - 1] + maxLen + 1;
        }
        return pos;
    }
}
