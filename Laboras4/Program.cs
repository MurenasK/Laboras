using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            IOUtils.WriteStats(statsFile, counts);
            TaskUtils.RebuildText(inputFile, outputFile);
        }
    }
}
