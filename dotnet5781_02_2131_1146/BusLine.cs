using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace dotnet5781_02_2131_1146
{
    public class BusLine : IComparable<BusLine>
    {
       // private static Dictionary<int, int> lines = new Dictionary<int, int>();
        private List<BusStopRoute> Stops;
        private int lineNumber;
        private BusStopRoute Begin;
        private BusStopRoute End;
        private Areas Area;
        //private De
        private bool IsValidArea(BusStop stop)
        {
            return (stop.Area == Area);
        }

        public BusLine(int line, Areas area)
        {
            Area = area;
            bool flagArea = (Area == Areas.General);
            lineNumber = line;
            Console.WriteLine("Here is all existed bus stops:");
            BusStop.PrintAll();
            bool success;
            int input1 = -1, input2, i = -1;
            // Add the first stop
            while (i != 0)
            {
                Console.WriteLine("Choose your first stop");
                success = (int.TryParse(Console.ReadLine(), out input1));
                success = (input1 > 0 && input1 <= BusStop.BusStopsList.Count);
                if (!success)
                {
                    Console.WriteLine("Wrong input");
                    continue;
                }
                else if (!flagArea)
                {
                    if (!IsValidArea(BusStop.BusStopsList[input1]))
                    {
                        Console.WriteLine("This stop is not from your area");
                        continue;
                    }
                }
                Begin = new BusStopRoute(BusStop.BusStopsList[input1], TimeSpan.Zero, 0);
                i = 0;
            }
            i = -1;
            // Add the last stop
            while (i != 0)
            {
                Console.WriteLine("Choose your last stop");
                success = (int.TryParse(Console.ReadLine(), out input2));
                success = (input2 > 0 && input2 <= BusStop.BusStopsList.Count);
                if (!success)
                {
                    Console.WriteLine("Wrong input");
                    continue;
                }
                else if (input2 == input1)
                {
                    Console.WriteLine("This is the beggining stop. Try different");
                    continue;
                }
                else if (!flagArea)
                {
                    if (!IsValidArea(BusStop.BusStopsList[input2]))
                    {
                        Console.WriteLine("This stop is not from your area");
                        continue;
                    }
                }
                // Get time
                Console.WriteLine("Enter time drive from the last stop");
                double usetTime = UserInput();
                // Get distance
                Console.WriteLine("Enter the distance from the last stop");
                double userDistance = UserInput();
                End = new BusStopRoute(BusStop.BusStopsList[input2], TimeSpan.FromMinutes(usetTime), userDistance);
            }
            Stops = new List<BusStopRoute>();
            Stops.Add(Begin);
            Stops.Add(End);
            i = -1;
            // Enable to add more stops
            while (i != 0)
            {
                Console.WriteLine("Add more stops. To finish press 0");
                success = (int.TryParse(Console.ReadLine(), out input2));
                success = (input2 > 0 && input2 <= BusStop.BusStopsList.Count);
                if (!success && input2 != 0)
                {
                    Console.WriteLine("Wrong input");
                    continue;
                }
                else if (input2 == 0)
                {
                    i = 0;
                    continue;
                }
                foreach (BusStopRoute stop in Stops)
                {
                    if (stop.GetNumber() == BusStop.BusStopsList[input2].stopNumber)
                    {
                        Console.WriteLine("This stop in the line already");
                        continue;
                    }
                }
                if (!flagArea)
                {
                    if (!IsValidArea(BusStop.BusStopsList[input2]))
                    {
                        Console.WriteLine("This stop is not from your area");
                        continue;
                    }
                }
                // Get time
                Console.WriteLine("Enter time drive from the last stop");
                double usetTime = UserInput();
                // Get distance
                Console.WriteLine("Enter the distance from the last stop");
                double userDistance = UserInput();
                Stops.Insert(Stops.Count - 2, new BusStopRoute(BusStop.BusStopsList[input2], TimeSpan.FromMinutes(usetTime), userDistance));
            }
        }

        public int LineNumber{get; }
        private double UserInput()
        {
            int i = -1;
            bool success;
            double input = 0;
            while (i != 0)
            {
                //Console.WriteLine("Enter distance from the last stop");
                success = (double.TryParse(Console.ReadLine(), out input));
                success = (input > 0);
                if (!success)
                {
                    Console.WriteLine("Wrong input");
                    continue;
                }
                i = 0;
            }
            return input;
        }
        public void AddStop()
        {
            int i = 1, index;
            Console.WriteLine("This is the route of this line");
            foreach (BusStopRoute stop in Stops)
            {
                Console.WriteLine(String.Format("{0}: {1}", i++, stop));
            }
            Console.WriteLine("Choose a position for the new stop");
            bool success = (int.TryParse(Console.ReadLine(), out index));
            success = (index > 0 && index <= BusStop.BusStopsList.Count);
            if (!success)
            {
                throw new BusException("Wrong input");
            }
            AddStop(index - 1);
        }

        private void AddStop(int index)
        {
            Console.WriteLine("Here is all existed bus stops:");
            BusStop.PrintAll();
            int input;
            bool success = (int.TryParse(Console.ReadLine(), out input));
            success = (input > 0 && input <= BusStop.BusStopsList.Count);
            if (!success)
            {
                throw new BusException("Wrong input");
            }
            foreach (BusStopRoute stop in Stops)
            {
                if (stop.GetNumber() == BusStop.BusStopsList[input].stopNumber)
                {
                    Console.WriteLine("This stop in the line already");
                    continue;
                }
            }
            if (Area != Areas.General)
            {
                if (!IsValidArea(BusStop.BusStopsList[input]))
                {
                    throw new BusException("This stop is not from your area");
                }
            }

            // Get time
            Console.WriteLine("Enter time drive from the last stop");
            double usetTime = UserInput();
            // Get distance
            Console.WriteLine("Enter the distance from the last stop");
            double userDistance = UserInput();
            Stops.Insert(index, new BusStopRoute(BusStop.BusStopsList[input], TimeSpan.FromMinutes(usetTime), userDistance));
        }

        public void RemoveStop()
        {
            Console.WriteLine("Here are all the bus stops in this route:");
            ToString();
            int input;
            bool success = (int.TryParse(Console.ReadLine(), out input));
            success = (input > 0 && input <= Stops.Count);
            if (!success)
            {
                throw new BusException("Wrong input");
            }
            // after consulting with the teacher removing a benig or end stop is allowed without considering the with the reversed route
            Stops.Remove(Stops[input-1]);
            if (input == 1)
            {
                Begin = Stops[0];
            }
            if (input == Stops.Count)
            {
                End = Stops[input - 2];
            }
        }

        private BusStopRoute this[int i]
        {
            get { return Stops[i]; }
            set { Stops[i] = value; }
        }


        public bool Exists(BusStopRoute stop)
        {
            return Stops.Contains(stop);
        }

        private double Distance(BusStopRoute begin, BusStopRoute end)
        {
            int beginIndex = 0, endIndex = 0;
            double result = 0;
            for (int i = 0; i < Stops.Count; i++)
            {
                if (Stops[i] == begin)
                {
                    beginIndex = i;
                    for (i = beginIndex; i < Stops.Count; i++)
                    {
                        if (Stops[i] == end)
                            endIndex = i;
                    }
                    break;
                }
            }
            if (beginIndex != 0 && endIndex != 0)
            {
                for (int i = beginIndex; i < endIndex; i++)
                {
                    result += Stops[i].TravelDistance;
                }
                return result;
            }
            throw new BusException("stops are not part of this line");
        }

        public TimeSpan TravelTime(BusStopRoute begin, BusStopRoute end)
        {
            int beginIndex = 0, endIndex = 0;
            TimeSpan result = new TimeSpan();
            for (int i = 0; i < Stops.Count; i++)
            {
                if (Stops[i] == begin)
                {
                    beginIndex = i;
                    for (i = beginIndex; i < Stops.Count; i++)
                    {
                        if (Stops[i] == end)
                            endIndex = i;
                    }
                    break;
                }
            }
            if (beginIndex != 0 && endIndex != 0)
            {
                for (int i = beginIndex; i < endIndex; i++)
                {
                    result.Add(Stops[i].TravelTime);
                }
                return result;
            }
            throw new BusException("stops are not part of this line");
        }
        public TimeSpan TotalTravelTime()
        {
            TimeSpan result = new TimeSpan();
            foreach (BusStopRoute stop in Stops)
                result += stop.TravelTime;
            return result;
        }
        //TO add a line number to the subline
        public BusLine SubLine(BusStopRoute begin, BusStopRoute end)
        {
            int beginIndex = 0, endIndex = 0;

            BusLine result = new BusLine(99, this.Area);
            for (int i = 0; i < Stops.Count; i++)
            {
                if (Stops[i] == begin)
                {
                    beginIndex = i;
                    for (i = beginIndex; i < Stops.Count; i++)
                    {
                        if (Stops[i] == end)
                        {
                            endIndex = i;
                            break;
                        }
                    }
                    break;
                }
            }

            if (endIndex != 0)
            {
                for (int i = beginIndex; i < endIndex; i++)
                {
                    result.Stops.Add(Stops[i]);
                }
                return result;
            }
            throw new BusException("these two stops dont create a subline of this line");
        }

        public int CompareTo(BusLine obj)
        {
            return obj.TotalTravelTime().CompareTo(this.TotalTravelTime());
        }

        public bool Contains(int StopNumber)
        {
            foreach (var item in Stops)
                if (item.GetNumber() == StopNumber)
                    return true;
            return false;
        }

        public override string ToString()
        {
            string result = string.Format("Line: {0}\nArea: {1}\n", lineNumber, Area);
            foreach (BusStopRoute stop in Stops)
                result.Concat(stop.GetNumber() + ", ");
            return result;
        }
    }
}
