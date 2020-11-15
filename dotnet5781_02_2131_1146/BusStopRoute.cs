using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dotnet5781_02_2131_1146
{
    class BusStopRoute
    {
        public BusStopRoute(BusStop stop, TimeSpan time, double distance)
        {
            Stop = stop;
            TravelTime = time;
            TravelDistance = distance;

        }
        private BusStop Stop;
        public TimeSpan TravelTime { get; set; }
        public double TravelDistance { get; set; }

        public int GetNumber() 
        {
            return Stop.stopNumber;
        }

    }
}
