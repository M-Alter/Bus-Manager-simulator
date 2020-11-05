using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet5781_02_2131_1146
{
    public class BusStop
    {
        private const int MAX_STOP_NUMBER = 1000000;
        private const int MIN_LAT = -90;
        private const int MAX_LAT = 90;
        private const int MIN_LON = -180;
        private const int MAX_LON = 180;

        public readonly int stopNumber;
        public readonly double Latitude;
        public readonly double Longitude;

        public BusStop(int number)
        {
            if (number>=MAX_STOP_NUMBER)
            {
                throw new ArgumentException("Number is longer than 6 digits");
            }
            // Check if this stop number alredy exist
            if (AllStopsNumbers.Contains(number))
                throw new ArgumentException(string.Format("Stop {0} already existed", number));
            stopNumber = number;
            AllStopsNumbers.Add(number);
            Random r = new Random(DateTime.Now.Millisecond);
            Latitude = r.NextDouble() * (33.3 - 31) + 31;
            Longitude = r.NextDouble() * (35.5 - 34.3) + 34.3;
        }

        public string Address   { get; set; }

        public override string ToString()
        {
            return String.Format("Bus Station Code: {0}, {1}°N {2}°E", stopNumber, Latitude, Longitude);
        }
        private static List<int> AllStopsNumbers = new List<int>();
    }
}
