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
            int choice, line;
            bool success;
            Areas area;
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
            BusCollection lines = new BusCollection();
            for (int i = 0; i < 10; i++)
                try
                {
                    lines.Add(new BusLine(i + 100, new BusStopRoute(stops[i + (2 * 5)], TimeSpan.FromMinutes((2 * i) + 5 % 3), 2 * i / 3), new BusStopRoute(stops[i + (3 * 5)], TimeSpan.FromMinutes((2 * i) + 6 % 5), 2 * i / 5)));
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
                Enum.TryParse(Console.ReadLine().ToUpper(), out option);
                try
                {
                    switch (option)
                    {

                        case Options.ADD:
                            Console.WriteLine("1 - To add a new bus\n2 - To add a new bus stop to the existed line");
                            if (!int.TryParse(Console.ReadLine(), out choice) || choice > 2 || choice < 1)
                                throw new BusException("Wrong input");
                            switch (choice)
                            {
                                case 1:
                                    Console.WriteLine("Enter line number");
                                    success = int.TryParse(Console.ReadLine(), out line);
                                    if (!success || line > 999 || line < 1)
                                        throw new BusException("Wrong input");
                                    Console.WriteLine("Choose an Area: General, Jerusalem, North, South, Center");
                                    if (!Enum.TryParse(Console.ReadLine(), out area))
                                        throw new BusException("Wrong input");
                                    lines.Add(new BusLine(line, area));
                                    break;
                                default:
                                    break;
                            }
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
                }
                catch (BusException e)
                {
                    Console.WriteLine(e.Message);
                }
            } while (option != Options.EXIT);
            Console.ReadKey();
        }
    }
}
