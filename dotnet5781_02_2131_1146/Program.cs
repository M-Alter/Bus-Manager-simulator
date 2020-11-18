﻿using System;
using System.Collections.Generic;

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

                    stops.Add(new BusStop(i + 10000, (Areas)(i / 8) + 1));
                }
                catch (BusException e)
                {
                    Console.WriteLine(e.Message);
                }
            BusStop.PrintAll();
            BusCollection Company = new BusCollection();
            for (int i = 0; i < 18; i++)
                try
                {
                    Company.Add(new BusLine(i + 100, new BusStopRoute(stops[((i + 1) * 2)], TimeSpan.FromMinutes((2 * i) + 5 % 3), 2 * i / 3), new BusStopRoute(stops[((i + 1) * 2) + 1], TimeSpan.FromMinutes((2 * i) + 6 % 5), 2 * i / 5)));
                }
                catch (BusException e)
                {
                    Console.WriteLine(e.Message);
                }
            try
            {
                Company.Add(new BusLine(150, new BusStopRoute(stops[0], TimeSpan.Zero, 00), new BusStopRoute(stops[39], TimeSpan.FromMinutes(4), 3.3)));
                for (int i = 2; i < 41; i++)
                    try
                    {
                        Company[150].AddStops(new BusStopRoute(stops[i - 1], TimeSpan.FromMinutes((i % 10) + 1), i * 1.333), i);
                    }
                    catch { }
            }
            catch (BusException e)
            {
                Console.WriteLine(e.Message);
            }
            foreach (var item in Company)
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
                            Console.WriteLine("1 - To add a new bus\n2 - To add a new bus stop to the existing line");
                            if (!int.TryParse(Console.ReadLine(), out choice) || choice > 2 || choice < 1)
                                throw new BusException("Wrong input");
                            switch (choice)
                            {
                                case 1:
                                    AddLine(ref Company);
                                    break;
                                case 2:
                                    AddStop(ref Company);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case Options.REMOVE:
                            Console.WriteLine("1 - To remove a bus\n2 - To remove a stop from an existing line");
                            if (!int.TryParse(Console.ReadLine(), out choice) || choice > 2 || choice < 1)
                                throw new BusException("Wrong input");
                            switch (choice)
                            {
                                case 1:
                                    RemoveLine(ref Company);
                                    break;
                                case 2:
                                    RemoveStop(ref Company);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case Options.SEARCH:
                            Console.WriteLine("1 - Find lines in a stop\n2 - Find route between 2 stops");
                            if (!int.TryParse(Console.ReadLine(), out choice) || choice > 2 || choice < 1)
                                throw new BusException("Wrong input");
                            switch (choice)
                            {
                                case 1:
                                    FindLines(ref Company);
                                    break;
                                case 2:
                                    FindRoute(ref Company);
                                    break;
                                default:
                                    break;
                            }
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

        private static void FindRoute(ref BusCollection Company)
        {
            Console.WriteLine("Here is all the stops:");
            BusStop.PrintAll();
            int inputBegin, inputEnd;
            BusStop Begin, End;
            bool success = int.TryParse(Console.ReadLine(), out inputBegin);
            if (!success || inputBegin > BusStop.BusStopsList.Count || inputBegin < 1)
                throw new BusException("This station doesn't exist");
            success = int.TryParse(Console.ReadLine(), out inputEnd);
            if (!success || inputEnd > BusStop.BusStopsList.Count || inputEnd < 1)
                throw new BusException("This station doesn't exist");
            Begin = BusStop.BusStopsList[inputBegin - 1];
            End = BusStop.BusStopsList[inputEnd - 1];
            Company.GetRoutes(Begin, End);
        }

        //private static void FindRoute(BusStopRoute begin, BusStopRoute end)
        //{
        //    foreach (var item in collection)
        //    {

        //    }
        //}

        private static void FindLines(ref BusCollection Company)
        {
            int stopNumber;
            bool success;
            Console.WriteLine("Enter stop number");
            success = int.TryParse(Console.ReadLine(), out stopNumber);
            if (!success)
                throw new BusException("Wrong input");
            Company.BusStopLines(stopNumber);
        }

        private static void RemoveStop(ref BusCollection Company)
        {
            int line;
            bool success;
            Console.WriteLine("Enter line number");
            success = int.TryParse(Console.ReadLine(), out line);
            if (!success)
                throw new BusException("Wrong input");
            Company.RemoveStop(line);
        }


        private static void RemoveLine(ref BusCollection Company)
        {
            Company.RemoveLine();
        }

        private static void AddStop(ref BusCollection Company)
        {
            int line;
            bool success;
            Console.WriteLine("Enter line number");
            success = int.TryParse(Console.ReadLine(), out line);
            if (!success)
                throw new BusException("Wrong input");
            Company.AddStop(line);
        }

        private static void AddLine(ref BusCollection Company)
        {
            int line;
            bool success;
            Areas area;
            Console.WriteLine("Enter line number");
            success = int.TryParse(Console.ReadLine(), out line);
            if (!success || line > 999 || line < 1)
                throw new BusException("Wrong input");
            Console.WriteLine("Choose an Area: General, Jerusalem, North, South, Center");
            if (!Enum.TryParse(Console.ReadLine(), out area))
                throw new BusException("Wrong input");
            Company.Add(new BusLine(line, area));
        }
    }
}
