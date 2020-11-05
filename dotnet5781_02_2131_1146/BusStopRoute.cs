using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet5781_02_2131_1146
{
    public class BusStopRoute : BusStop
    {
        public BusStopRoute(int number, TimeSpan time, double distance) : base(number) 
        {
            TravelTime = time;
            TravelDistance = distance;
        }
        //public BusStopRoute(BusStop busstop) : base(busstop.stopNumber) { }
             
        public TimeSpan TravelTime { get; set; }
        public double TravelDistance { get; set; }
        
       

    }
}
