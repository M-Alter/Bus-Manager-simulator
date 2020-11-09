using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace dotnet5781_02_2131_1146
{
    class BusLine
    {
        private static Dictionary<int, int> lines = new Dictionary<int, int>();
        private List<BusStopRoute> Stops;
        private int lineNumber;
        private BusStopRoute Begin;
        private BusStopRoute End;
        private Areas Area;

        public BusLine(int line)
        {
            //if (lines.ContainsKey(line) && lines[line] == 2)
            //    throw new ArgumentException("This line already exists twice");
            //if (lines.ContainsKey(line))
            //{
            //    //check if the stations suit each other
            //    lines[line]++;

            //}
            //else
            //    lines.Add(line, 1);
            //Stops = new List<BusStopRoute>();
            //bool success;
            //int i = 0, input;
            ////foreach (BusStopRoute stop in Stops)
            ////    Console.WriteLine(string.Format("{0}: {1}",i++,stop));
            //Console.WriteLine("Choose your first stop");
            //success = (int.TryParse(Console.ReadLine(), out input));
            //if (success)
            //{
            //    Stops.Add();
            //}
            //Console.WriteLine("Choose your second stop");

            ////print all busStops and add first and last stops to line
        }

        public BusStopRoute this[int i]
        {
            get { return Stops[i]; }
            set { Stops[i] = value; }
        }

        public bool Exists(BusStopRoute stop)
        {
            return Stops.Contains(stop);
        }

        public double Distance(BusStopRoute begin, BusStopRoute end)
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
            throw new ArgumentException("stops are not part of this line");
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
                    result.Add( Stops[i].TravelTime);
                }
                return result;
            }
            throw new ArgumentException("stops are not part of this line");
        }


    }
}
