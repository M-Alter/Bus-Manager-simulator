using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DO;

namespace DS
{
    public static class DataSource
    {
        public static List<Bus> BusList;
        public static List<Station> StationList;
        public static List<Line> LineList;
        public static List<Trip> TripList;

        /// <summary>
        /// C'tor that runs before an instance is created
        /// </summary>
        static DataSource()
        {
            InitAllLists();
        }

        
         static void InitAllLists()
        {
            BusList = new List<Bus>
            {

                new Bus()
                {
                    FromDate = DateTime.Today, FuelRemain = 1200, LicenseNum = 12345678, Status = Enums.BusStatus.READY, TotalTrip = 0
                }
            };
            for (int i = 0; i < 10; i++)
            {
                BusList.Add(new Bus()
                {
                    FromDate = DateTime.Today,
                    FuelRemain = 1200,
                    LicenseNum = 12345670+i,
                    Status = Enums.BusStatus.READY,
                    TotalTrip = 0
                });
            }
            StationList = new List<Station>();
            TripList = new List<Trip>();
            LineList = new List<Line>();
        }
    }
}
