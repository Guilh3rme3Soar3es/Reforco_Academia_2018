using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaReuniao
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime x = DateTime.Now;
            DateTime y = DateTime.Now.AddDays(-1);
            Console.WriteLine(x.DayOfWeek);
            Console.WriteLine(y.DayOfYear);
            Console.ReadKey();
        }
    }
}
