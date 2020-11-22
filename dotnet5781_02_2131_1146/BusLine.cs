using System;
using System.Collections.Generic;

namespace dotnet5781_02_2131_1146
{
    public class BusLine : IComparable<BusLine>
    {
        #region Fields

        //private List<BusStopRoute> Stations;
        private readonly int lineNumber;
        #endregion

        #region C'tors
        /// <summary>
        /// C'tor that requests the stops from the user
        /// </summary>
        /// <param name="line">line number</param>
        /// <param name="area">operational area</param>
        public BusLine(int line, Areas area)
        {
            Area = area;
            bool flagArea = (Area == Areas.GENERAL);
            lineNumber = line;
            Console.WriteLine("Here is all the bus stops:");
            BusStop.PrintAll();
            bool success;
            int indexBegin = -1, indexEnd;
            // Add the first stop
            Console.WriteLine("Choose your first stop");
            success = (int.TryParse(Console.ReadLine(), out indexBegin));
            if (!success)
                throw new BusException("please enter a line number");
            success = (indexBegin > 0 && indexBegin <= BusStop.BusStopsList.Count);
            if (!success)
                throw new BusException("index out of range");
            else if (!flagArea)
                if (!IsValidArea(BusStop.BusStopsList[indexBegin - 1]))
                    throw new BusException("this stop does not belong to your area");
            Begin = new BusStopRoute(BusStop.BusStopsList[indexBegin - 1], TimeSpan.Zero, 0);
            // Add the last stop
            Console.WriteLine("Choose your last stop");
            success = (int.TryParse(Console.ReadLine(), out indexEnd));
            if (!success)
                throw new BusException("please enter a number");
            success = (indexEnd > 0 && indexEnd <= BusStop.BusStopsList.Count);
            if (!success)
                throw new BusException("index out of range");
            else if (indexEnd == indexBegin)
                throw new BusException("This is the beggining stop. Try different");
            else if (!flagArea)
                if (!IsValidArea(BusStop.BusStopsList[indexEnd]))
                    throw new BusException("This stop is not from your area");
            // Get time
            Console.WriteLine("Enter time drive from the last stop");
            double usetTime = UserInput();
            // Get distance
            Console.WriteLine("Enter the distance from the last stop");
            double userDistance = UserInput();
            End = new BusStopRoute(BusStop.BusStopsList[indexEnd], TimeSpan.FromMinutes(usetTime), userDistance);
            Stations = new List<BusStopRoute>();
            Stations.Add(Begin);
            Stations.Add(End);
        }

        /// <summary>
        /// C'tor the gets all the information from the params and sets the first stop as the area
        /// </summary>
        /// <param name="line">line number</param>
        /// <param name="beginStop">Begin</param>
        /// <param name="endStop">End</param>
        public BusLine(int line, BusStopRoute beginStop, BusStopRoute endStop, Areas Area = Areas.GENERAL)
        {
            if (beginStop.GetNumber == endStop.GetNumber)
                throw new BusException("First stop can't be last stop");
            if (beginStop.GetArea != endStop.GetArea && beginStop.GetArea != Areas.GENERAL && endStop.GetArea != Areas.GENERAL && Area != Areas.GENERAL)
                throw new BusException("Areas dont match");
            lineNumber = line;
            this.Area = Area;
            Stations = new List<BusStopRoute>();
            AddStop(beginStop, 1);
            Begin = beginStop;
            AddStop(endStop, 2);
            End = endStop;
        }
        #endregion

        #region Properties
        public int LineNumber { get { return lineNumber; } }
        public BusStopRoute Begin { get; private set; }
        public BusStopRoute End { get; private set; }
        public Areas Area { get;  set; }
        public List<BusStopRoute> Stations { get; private set; }
        #endregion

        #region IComparable
        /// <summary>
        /// returns 0 if travel times are equal, 1 if this is greater and -1 if this is not greater
        /// </summary>
        /// <param name="obj">another BusLine to compare to</param>
        /// <returns>0,1,-1 see summary</returns>
        public int CompareTo(BusLine obj)
        {
            return obj.TotalTravelTime().CompareTo(this.TotalTravelTime());
        }
        #endregion

        #region Methods

        /// <summary>
        /// method that checks if the BusStop's area is valid for this line 
        /// </summary>
        /// <param name="stop">stop to check</param>
        /// <returns>bool</returns>
        private bool IsValidArea(BusStop stop)
        {
            if (Area == Areas.GENERAL || stop.Area == Areas.GENERAL)
                return true;
            return (stop.Area == Area);
        }

        /// <summary>
        /// to add a stop after stop has been checked for compatability
        /// </summary>
        /// <param name="stop">Bustop to add</param>
        /// <param name="index">on what part of the route to add it</param>
        private void AddStop(BusStopRoute stop, int index)
        {
            if (index == 1)
            {
                //Area = stop.GetArea;
                stop.TravelDistance = 0;
                stop.TravelTime = TimeSpan.Zero;
            }
            Stations.Insert(index - 1, stop);
        }
        /// <summary>
        /// method to get a doubble from the user
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// add stop that requests all the information from the user
        /// </summary>
        public void AddStop()
        {
            int i = 1, index;
            Console.WriteLine("This is the route of this line");
            foreach (BusStopRoute stop in Stations)
            {
                Console.WriteLine(String.Format("{0}: {1}", i++, stop.GetBusStop));
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

        /// <summary>
        /// this addStop works if you have the desired position you would like the new stop to be
        /// </summary>
        /// <param name="index">position in the route</param>
        private void AddStop(int index)
        {
            if (index > Stations.Count)
                throw new IndexOutOfRangeException();
            Console.WriteLine("Here are all the bus stops:");
            BusStop.PrintAll();
            int input;
            bool success = (int.TryParse(Console.ReadLine(), out input));
            success = (input > 0 && input <= BusStop.BusStopsList.Count);
            if (!success)
            {
                throw new BusException("Wrong input");
            }
            foreach (BusStopRoute stop in Stations)
            {
                if (stop.GetNumber == BusStop.BusStopsList[input].stopNumber)
                {
                    Console.WriteLine("This stop in the line already");
                    continue;
                }
            }
            if (Area != Areas.GENERAL)
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
            Stations.Insert(index, new BusStopRoute(BusStop.BusStopsList[input - 1], TimeSpan.FromMinutes(usetTime), userDistance));
        }

        /// <summary>
        /// mmethod to get the distance between two stops 
        /// </summary>
        /// <param name="begin">from what stop</param>
        /// <param name="end">until what stop</param>
        /// <returns>double distance</returns>
        private double Distance(BusStopRoute begin, BusStopRoute end)
        {
            int beginIndex = 0, endIndex = 0;
            double result = 0;
            for (int i = 0; i < Stations.Count; i++)
            {
                if (Stations[i] == begin)
                {
                    beginIndex = i;
                    for (i = beginIndex; i < Stations.Count; i++)
                    {
                        if (Stations[i] == end)
                            endIndex = i;
                    }
                    break;
                }
            }
            if (beginIndex != 0 && endIndex != 0)
            {
                for (int i = beginIndex; i < endIndex; i++)
                {
                    result += Stations[i].TravelDistance;
                }
                return result;
            }
            throw new BusException("stops are not part of this line");
        }

        /// <summary>
        /// when given two stops in a route fins the TimeSpan between them
        /// </summary>
        /// <param name="begin">the stop from where we should count</param>
        /// <param name="end"></param>
        /// <returns></returns>
        public TimeSpan TravelTime(BusStopRoute begin, BusStopRoute end)
        {
            int beginIndex = 0, endIndex = 0;
            TimeSpan result = new TimeSpan();
            for (int i = 0; i < Stations.Count; i++)
            {
                if (Stations[i] == begin)
                {
                    beginIndex = i;
                    for (i = beginIndex; i < Stations.Count; i++)
                    {
                        if (Stations[i] == end)
                            endIndex = i;
                    }
                    break;
                }
            }
            if (beginIndex != 0 && endIndex != 0)
            {
                for (int i = beginIndex; i < endIndex; i++)
                {
                    result.Add(Stations[i].TravelTime);
                }
                return result;
            }
            throw new BusException("stops are not part of this line");
        }

        /// <summary>
        /// as requested to create a methos that receives a stop 
        /// </summary>
        /// <param name="stop"></param>
        /// <returns>returns true if the stop is in this route</returns>
        public bool Exists(BusStopRoute stop)
        {
            return Stations.Contains(stop);
        }

        /// <summary>
        /// method to add a stop without asking the user for more information
        /// </summary>
        /// <param name="stop">stop to be added</param>
        /// <param name="index">position in the route to be inserted</param>
        public void AddStops(BusStopRoute stop, int index)
        {
            if (IsValidArea(stop.GetBusStop))
            {
                if (index > 0 && index < BusStop.BusStopsList.Count)
                {
                    if (!Contains(stop.GetNumber))
                    {
                        AddStop(stop, index);
                        return;
                    }
                    throw new BusException("stop exists already");
                }
                throw new BusException("index out of range");
            }
            throw new BusException("Areas don't match");
        }

        /// <summary>
        /// method that sums up thte total travel time of a route
        /// </summary>
        /// <returns>TimeSpan of the full route</returns>
        public TimeSpan TotalTravelTime()
        {
            TimeSpan result = new TimeSpan();
            foreach (BusStopRoute stop in Stations)
                result += stop.TravelTime;
            return result;
        }

        /// <summary>
        /// method that removes a stop from the line
        /// </summary>
        public void RemoveStop()
        {
            Console.WriteLine("Here are all the bus stops in this route:");
            ToString();
            int input;
            bool success = (int.TryParse(Console.ReadLine(), out input));
            success = (input > 0 && input <= Stations.Count);
            if (!success)
            {
                throw new BusException("Wrong input");
            }
            // after consulting with the teacher removing a benig or end stop is allowed without considering the with the reversed route
            Stations.Remove(Stations[input - 1]);
            if (input == 1)
            {
                Begin = Stations[0];
            }
            if (input == Stations.Count)
            {
                End = Stations[input - 2];
            }
        }
        /// <summary>
        /// method that creates a subline of the route and returns it
        /// </summary>
        /// <param name="begin">from where the subline should begin</param>
        /// <param name="end">from where the subline should end</param>
        /// <returns>the subline</returns>
        public BusLine SubLine(BusStop begin, BusStop end)
        {
            int beginIndex = 0, endIndex = 0;

            for (int i = 0; i < Stations.Count; i++)
                if (Stations[i].GetNumber == begin.stopNumber)
                {
                    beginIndex = i;
                    for (i = beginIndex; i < Stations.Count; i++)
                        if (Stations[i].GetNumber == end.stopNumber)
                        {
                            endIndex = i;
                            break;
                        }
                    break;
                }

            if (endIndex != 0)
            {
                BusLine result;
                result = new BusLine(lineNumber, Stations[beginIndex], Stations[endIndex], Areas.GENERAL);
                //try 
                //{
                //}
                //catch 
                //{
                //    result.Area = Areas.GENERAL;
                //    result.Begin = Stations[beginIndex];
                //    result.End = Stations[endIndex];
                //}
                for (int i = beginIndex; i < endIndex; i++)
                {
                    result.Stations.Insert(i - beginIndex, Stations[i]);
                }
                return result;
            }
            throw new BusException("these two stops don't create a subline of this line");
        }

        /// <summary>
        /// method to see if the BusLine contains a stop with the given stop number
        /// </summary>
        /// <param name="StopNumber">number to search for</param>
        /// <returns>true if it exists of false if not</returns>
        public bool Contains(int StopNumber)
        {
            foreach (var item in Stations)
                if (item.GetNumber == StopNumber)
                    return true;
            return false;
        }

        /// <summary>
        /// ovverrides ToString() and prints the line number, operational area, and all the stops with their travel time
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string result = string.Format("Line: {0,-4} Area: {1,-10}  \n", lineNumber, Area);
            foreach (BusStopRoute stop in Stations)
                result += (string.Format("{0,-77} {1}\n", stop.GetBusStop, stop.TravelTime));
            return result;
        }

        #endregion

    }
}
