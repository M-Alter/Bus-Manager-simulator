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
        public static List<LineStation> LineStationsList;

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
                    FromDate = DateTime.Today, FuelRemain = 1200, LicenseNum = 87654321, Status = Enums.BusStatus.READY, TotalTrip = 0
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
            StationList = new List<Station>
            {
                new Station{Code = 38831, Lattitude = 32.183921, Longitude = 34.917806, Name = "בי\"ס בר לב\\בן יהודה" },
                new Station{Code = 38832, Lattitude = 31.870034, Longitude = 34.819541, Name = "הרצל\\צומת בילו" }
            };
            TripList = new List<Trip>();
            LineList = new List<Line>
            {
                new Line
                {
                    Area = Enums.Areas.CENTRAL, Code = 1, Id = 1, FirstStation = 38831, LastStation = 38832
                }
            };
            LineStationsList = new List<LineStation>
            {
                new LineStation
                {
                    StationCode = 38831, LineId = 1, LineStationIndex = 1, NextStation = 38832, PrevStation = 0
                },
                new LineStation
                {
                    StationCode = 38832, LineId = 1, LineStationIndex = 1, NextStation = 0, PrevStation = 38831
                }
            };
        }
    }
}
