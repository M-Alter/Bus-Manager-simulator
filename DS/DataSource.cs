using DO;
using System;
using System.Collections.Generic;

namespace DS
{
    public static class DataSource
    {
        public static List<Bus> BusList;
        public static List<Station> StationList;
        public static List<Line> LineList;
        public static List<Trip> TripList;
        public static List<LineStation> LineStationsList;
        public static List<User> UserList;
        public static List<AdjacentStations> AdjacentStationsList;
        public static List<LineTrip> LineTripsList;
        public static int linePersonalIdGenerator = 15;

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
                    LicenseNum = 12345670 + i,
                    Status = Enums.BusStatus.READY,
                    TotalTrip = 0
                });
            }
            StationList = new List<Station>
            {
                new Station{Code = 38831, Lattitude = 32.183921, Longitude = 34.917806, Name = "בי\"ס בר לב\\בן יהודה" },
                new Station{Code = 38832, Lattitude = 31.870034, Longitude = 34.819541, Name = "הרצל\\צומת בילו" },
                new Station{Code = 38833, Lattitude = 32.183921, Longitude = 34.917806, Name = "הנחשול\\דייגים"},
                new Station{Code = 38834, Lattitude = 31.870034, Longitude = 34.819541, Name = "פריד\\ששת הימים" },
                new Station{Code = 38835, Lattitude = 32.183921, Longitude = 34.917806, Name = "ת. מרכזית לוד\\הורדה"},
                new Station{Code = 38836, Lattitude = 31.870034, Longitude = 34.819541, Name = "חנה אברך\\וולקני"},
                new Station{Code = 38837, Lattitude = 31.857565, Longitude = 34.824106, Name = "הרצל\\משה שרת" },
                new Station{Code = 38838, Lattitude = 31.862305, Longitude = 34.822237, Name = "הבנים\\אלי כהן" },
                new Station{Code = 38839, Lattitude = 31.865085, Longitude = 34.818957, Name = "ויצמן\\הבנים" },
                new Station{Code = 38840, Lattitude = 31.865222, Longitude = 34.818392, Name = "האירוס\\הכלנית" },
                new Station{Code = 38841, Lattitude = 31.867597, Longitude = 34.828702, Name = "הכלנית\\הנרקיס" },
                new Station{Code = 38842, Lattitude = 31.862449, Longitude = 34.763896, Name = "אלי כהן\\לוחמי הגטאות"},
                new Station{Code = 38843, Lattitude = 31.863501, Longitude = 34.912602, Name = "שבזי\\שבת אחים" },
                new Station{Code = 38844, Lattitude = 31.865348, Longitude = 34.828702, Name = "שבזי\\ויצמן" },
                new Station{Code = 38845, Lattitude = 31.977409, Longitude = 34.763896, Name = "חיים בר לב\\שדרות יצחק רבין" },
                new Station{Code = 38846, Lattitude = 32.300345, Longitude = 34.912708, Name = "מרכז לבריאות הנפש לב השרון" },
                new Station{Code = 38847, Lattitude = 32.301347, Longitude = 34.912708, Name = "מרכז לבריאות הנפש "},
                new Station{Code = 38848, Lattitude = 31.914255, Longitude = 34.807944, Name = "הולצמן\\המדע" },
                new Station{Code = 38849, Lattitude = 31.963668, Longitude = 34.836363, Name = "מחנה צריפין\\מועדון"}

            };

            LineList = new List<Line>
            {
                new Line    {   Area = Enums.Areas.CENTRAL, LineNumber = 1, PersonalId = 9, FirstStation = 38831, LastStation = 38836     },
                new Line    {   Area = Enums.Areas.CENTRAL, LineNumber = 51, PersonalId = 10, FirstStation = 38833, LastStation = 38837   },
                new Line    {   Area = Enums.Areas.CENTRAL, LineNumber = 23, PersonalId = 11, FirstStation = 38835, LastStation = 38845   },
                new Line    {   Area = Enums.Areas.CENTRAL, LineNumber = 100, PersonalId = 12, FirstStation = 38844, LastStation = 38848   },
                new Line    {   Area = Enums.Areas.CENTRAL, LineNumber = 770, PersonalId = 13, FirstStation = 38841, LastStation = 38849   },
                new Line    {   Area = Enums.Areas.JERUSALEM, LineNumber = 999, PersonalId = 14, FirstStation = 38837, LastStation = 38846 }
            };

            LineStationsList = new List<LineStation>
            {
                new LineStation     {   StationCode = 38831, LineId = 9, LineStationIndex = 1, NextStation = 38832, PrevStation = 0     },
                new LineStation     {   StationCode = 38832, LineId = 9, LineStationIndex = 2, NextStation = 38833, PrevStation = 38831     },
                new LineStation     {   StationCode = 38833, LineId = 9, LineStationIndex = 3, NextStation = 38834, PrevStation = 38832     },
                new LineStation     {   StationCode = 38834, LineId = 9, LineStationIndex = 4, NextStation = 38835, PrevStation = 38833     },
                new LineStation     {   StationCode = 38835, LineId = 9, LineStationIndex = 5, NextStation = 38836, PrevStation = 38834     },
                new LineStation     {   StationCode = 38836, LineId = 9, LineStationIndex = 6, NextStation = 0, PrevStation = 38835     },


                new LineStation     {   StationCode = 38833, LineId = 10, LineStationIndex = 1, NextStation = 38834, PrevStation = 0   },
                new LineStation     {    StationCode =   38834   , LineId = 10   , LineStationIndex =    2   , NextStation = 38835   , PrevStation = 38833   },
                new LineStation     {    StationCode =   38835   , LineId = 10   , LineStationIndex =    3   , NextStation = 38836   , PrevStation = 38834   },
                new LineStation     {    StationCode =   38836   , LineId = 10   , LineStationIndex =    4   , NextStation = 38837   , PrevStation = 38835   },
                new LineStation     {    StationCode =   38837   , LineId = 10   , LineStationIndex =    5   , NextStation = 0   , PrevStation = 38836   },

                new LineStation     {   StationCode = 38841, LineId = 13, LineStationIndex = 1, NextStation = 38842, PrevStation = 0     },
                new LineStation     {   StationCode = 38842, LineId = 13, LineStationIndex = 2, NextStation = 38843, PrevStation = 38841     },
                new LineStation     {   StationCode = 38843, LineId = 13, LineStationIndex = 3, NextStation = 38849, PrevStation = 38842     },
                new LineStation     {   StationCode = 38849, LineId = 13, LineStationIndex = 4, NextStation = 0, PrevStation = 38843     },

                new LineStation{    StationCode =   38835   , LineId = 11   , LineStationIndex =    1   , NextStation = 38836   , PrevStation = 0   },
                new LineStation{    StationCode =   38836   , LineId = 11   , LineStationIndex =    2   , NextStation = 38837   , PrevStation = 38835   },
                new LineStation{    StationCode =   38837   , LineId = 11   , LineStationIndex =    3   , NextStation = 38838   , PrevStation = 38836   },
                new LineStation{    StationCode =   38838   , LineId = 11   , LineStationIndex =    4   , NextStation = 38839   , PrevStation = 38837   },
                new LineStation{    StationCode =   38839   , LineId = 11   , LineStationIndex =    5   , NextStation = 38840   , PrevStation = 38838   },
                new LineStation{    StationCode =   38840   , LineId = 11   , LineStationIndex =    6   , NextStation = 38841   , PrevStation = 38839   },
                new LineStation{    StationCode =   38841   , LineId = 11   , LineStationIndex =    7   , NextStation = 38842   , PrevStation = 38840   },
                new LineStation{    StationCode =   38842   , LineId = 11   , LineStationIndex =    8   , NextStation = 38843   , PrevStation = 38841   },
                new LineStation{    StationCode =   38843   , LineId = 11   , LineStationIndex =    9   , NextStation = 38844   , PrevStation = 38842   },
                new LineStation{    StationCode =   38844   , LineId = 11   , LineStationIndex =    10  , NextStation = 38845   , PrevStation = 38843   },
                new LineStation{    StationCode =   38845   , LineId = 11   , LineStationIndex =    11  , NextStation = 0   , PrevStation = 38844   }

            };

            UserList = new List<User>
            {
                new User    {   UserName = "QA", Password = "", Admin = true    },
                new User    {   UserName = "Admin", Password = "Admin", Admin = true    },
                new User    {   UserName = "Menachem", Password = "339832131", Admin = false    }
            };

            TripList = new List<Trip>();
            AdjacentStationsList = new List<AdjacentStations>
            {
                new AdjacentStations{Station1 = 38831, Station2 = 38832, Distance = 1.5, Time = new TimeSpan(0,3,0) },
                new AdjacentStations{Station1 = 38832, Station2 = 38833, Distance = 1.5, Time = new TimeSpan(0,3,0) },
                new AdjacentStations{Station1 = 38833, Station2 = 38834, Distance = 1.5, Time = new TimeSpan(0,3,0) },
                new AdjacentStations{Station1 = 38834, Station2 = 38835, Distance = 1.5, Time = new TimeSpan(0,3,0) },
                new AdjacentStations{Station1 = 38835, Station2 = 38836, Distance = 1.5, Time = new TimeSpan(0,3,0) }
            };

            LineTripsList = new List<LineTrip>
            {
                new LineTrip{LineId = 9,StartAt = new TimeSpan(08,30,00),FinishAt = new TimeSpan(19,00,00), Frequency = new TimeSpan(00,15,00), Id=9083000 }

            };
        }
    }
}
