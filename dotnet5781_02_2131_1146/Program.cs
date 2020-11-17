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
                try
                {
                    stops.Add(new BusStop(r.Next(1, 1000000), (Areas)(i % 5) + 1));
                }
                catch (BusException e)
                {
                    Console.WriteLine(e.Message);
                }
            BusStop.PrintAll();
            List<BusLine> lines = new List<BusLine>();
            for (int i = 0; i < 10; i++)
                try
                {
                    lines.Add(new BusLine(i + 1, new BusStopRoute(stops[i + (2 * 5)], TimeSpan.FromMinutes((2 * i) + 5 % 3), 2 * i / 3), new BusStopRoute(stops[i + (3 * 5)], TimeSpan.FromMinutes((2 * i) + 6 % 5), 2 * i / 5)));
                }
                catch (BusException e)
                {
                    Console.WriteLine(e.Message);
                }
            foreach (var item in lines)
                Console.WriteLine(item);
            Options option;
            do
            {
                Console.WriteLine("Please choose from one of the following options");
                Console.WriteLine("1: ADD \n2: REMOVE \n3: SEARCH \n4: PRINT \n5: EXIT");
                Enum.TryParse(Console.ReadLine().ToUpper(),out option);
                switch (option)
                {
                    case Options.ADD:
                        break;
                    case Options.REMOVE:
                        break;
                    case Options.SEARCH:
                        break;
                    case Options.PRINT:
                        break;
                    case Options.EXIT:
                        break;
                    default:
                        break;
                }
            } while (option != Options.EXIT);
            Console.ReadKey();
        }
    }
}
