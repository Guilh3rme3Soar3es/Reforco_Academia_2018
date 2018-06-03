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

            Console.WriteLine(x.Hour);
            Console.WriteLine(x.Minute);
            Console.ReadKey();
        }
    }
}
