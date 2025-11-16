using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Laboras4
{
    class IOUtils
    {
        public static void WriteStats(string fileName, Dictionary<string,
            int> stats)
        {
            using (StreamWriter writer = new StreamWriter(fileName, false,
                Encoding.UTF8))
            {
                var list = TaskUtils.ToList(stats);
                TaskUtils.SortStatistics(list);

                int limit = list.Count < 10 ? list.Count : 10;

                for(int i = 0; i < limit; i++)
                {
                    writer.WriteLine($"{list[i].Key} {list[i].Value}");
                }
            }
        }
    }
}
