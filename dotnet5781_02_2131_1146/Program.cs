using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet5781_02_2131_1146
{
    class Program
    {
        static void Main(string[] args)
        {
            List<BusStop> stops = new List<BusStop>();
            Random r = new Random(DateTime.Today.Millisecond);
            for (int i = 0; i < 40; i++)
                stops.Add(new BusStop(r.Next(1,1000000),(Areas)(i%5)+1));
            BusStop.PrintAll();
            List<BusLine> lines = new List<BusLine>();
            for (int i = 0; i < 10; i++)
                lines.Add(new BusLine(r.Next(1, 999), (Areas)(2 * i + 1 % 5) + 1));
            foreach (var item in lines)
            {
                Console.WriteLine(item);
            }
            Console.ReadKey();
        }
    }
}
