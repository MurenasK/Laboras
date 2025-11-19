using System;
using System.Collections.Generic;

namespace Laboras4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string inputFile = "Knyga.txt";
            string statsFile = "Rodikliai.txt";
            string outputFile = "ManoKnyga.txt";

            var counts = IOUtils.ReadWords(inputFile);
            TaskUtils.RebuildText(inputFile, outputFile);
            IOUtils.WriteIndicators(inputFile, statsFile);

        }
    }
}
