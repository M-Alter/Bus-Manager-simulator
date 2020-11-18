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
        //private const int MIN_LAT = -90;
        //private const int MAX_LAT = 90;
        //private const int MIN_LON = -180;
        //private const int MAX_LON = 180;

        #region Fields
        public readonly int stopNumber;
        public readonly double Latitude;
        public readonly double Longitude;
        private static List<int> AllStopsNumbers = new List<int>();
        public static List<BusStop> BusStopsList = new List<BusStop>();
        private static Random r = new Random(DateTime.Now.Millisecond);
        #endregion

        #region c'tor

        /// <summary>
        /// c'tor for BusStop 
        /// </summary>
        /// <param name="number">BusStop code</param>
        /// <param name="area">Operational area</param>
        public BusStop(int number, Areas area)
        {
            // Check the validation of the number
            if (number >= MAX_STOP_NUMBER)
                throw new ArgumentException("Number is longer than 6 digits");
            if (number == 0)
                throw new ArgumentException("Bus stop number can't be 0");
            // Check if this stop number alredy exist
            if (AllStopsNumbers.Contains(number))
                throw new ArgumentException(string.Format("Stop {0} already existed", number));
            stopNumber = number;
            Area = area;
            AllStopsNumbers.Add(number);
            Latitude = (r.NextDouble() * (33.3 - 31)) + 31;
            Longitude = r.NextDouble() * (35.5 - 34.3) + 34.3;
            Latitude = Math.Round(Latitude, 6);
            Longitude = Math.Round(Longitude, 6);
            BusStopsList.Add(this);
        }
        #endregion

        #region properties

        /// <summary>
        /// optional addition to each stop
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// area that this stop is located in
        /// </summary>
        public Areas Area { get; private set; }

        #endregion

        #region Methods
        /// <summary>
        /// Method to print all the Bustops available
        /// </summary>
        public static void PrintAll()
        {
            int i = 1;
            foreach (BusStop stop in BusStopsList)
                Console.WriteLine(String.Format("{0,3}: {1}", i++, stop));
        }

        /// <summary>
        /// Overridse ToString() with the BusStop code, Latitude, Longitude and area
        /// </summary>
        /// <returns>print info</returns>
        public override string ToString()
        {
            return String.Format("Bus Station Code: {0,-7}  {1,11}°N {2,11}°E, Operating area: {3}", stopNumber, Latitude, Longitude, Area);
        }

        #endregion

        

    }
}
