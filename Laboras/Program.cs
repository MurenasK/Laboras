using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboras
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HeroesRegister register = HeroesRegister.ReadHeroesFromFile(@"heroes.txt");
        }
    }
}
