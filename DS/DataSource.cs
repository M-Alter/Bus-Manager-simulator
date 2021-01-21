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
        public static int linePersonalIdGenerator = 20;

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
            for (int i = 0; i < 20; i++)
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
                new Station{Code = 38849, Lattitude = 31.963668, Longitude = 34.836363, Name = "מחנה צריפין\\מועדון"},
                new Station{Code =  38850   , Lattitude =   31.856115   , Longitude =   34.825249   , Name = "הרצל\\גולני	"},
                new Station{Code =  38851   , Lattitude =   31.874963   , Longitude =   34.81249    , Name = "הרותם\\הדגניות	"},
                new Station{Code =  38852   , Lattitude =   32.300035   , Longitude =   34.910842   , Name = "הערבה	"},
                new Station{Code =  38853   , Lattitude =   32.305234   , Longitude =   34.948647   , Name = "מבוא הגפן\\מורד התאנה	"},
                new Station{Code =  38854   , Lattitude =   32.304022   , Longitude =   34.943393   , Name = "מבוא הגפן\\ההרחבה	"},
                new Station{Code =  38855   , Lattitude =   32.302957   , Longitude =   34.940529   , Name = "ההרחבה א	"},
                new Station{Code =  38856   , Lattitude =   32.300264   , Longitude =   34.939512   , Name = "ההרחבה ב	"},
                new Station{Code =  38857   , Lattitude =   32.298171   , Longitude =   34.938705   , Name = "ההרחבה\\הותיקים	"},
                new Station{Code =  38858   , Lattitude =   31.990876   , Longitude =   34.8976 , Name = "רשות שדות התעופה\\העליה	"},
                new Station{Code =  38859   , Lattitude =   31.998767   , Longitude =   34.879725   , Name = "כנף\\ברוש	"},
                new Station{Code =  38860   , Lattitude =   31.883019   , Longitude =   34.818708   , Name = "החבורה\\דב הוז	"},
                new Station{Code =  38861   , Lattitude =   32.349776   , Longitude =   34.926837   , Name = "בית הלוי ה	"},
                new Station{Code =  38862   , Lattitude =   32.352953   , Longitude =   34.899465   , Name = "הראשונים\\כביש 5700	"},
                new Station{Code =  38863   , Lattitude =   31.897286   , Longitude =   34.775083   , Name = "הגאון בן איש חי\\צאלון	"},
                new Station{Code =  38864   , Lattitude =   31.883941   , Longitude =   34.807039   , Name = "עוקשי\\לוי אשכול	"},
                new Station{Code =  38865   , Lattitude =   31.896762   , Longitude =   34.816752   , Name = "מנוחה ונחלה\\יהודה גורודיסקי	"},
                new Station{Code =  38866   , Lattitude =   31.898463   , Longitude =   34.823461   , Name = "גורודסקי\\יחיאל פלדי	"},
                new Station{Code =  38867   , Lattitude =   32.076535   , Longitude =   34.904907   , Name = "דרך מנחם בגין\\יעקב חזן	"},
                new Station{Code =  38868   , Lattitude =   32.299994   , Longitude =   34.878765   , Name = "דרך הפארק\\הרב נריה	"},
                new Station{Code =  38869   , Lattitude =   31.865457   , Longitude =   34.859437   , Name = "התאנה\\הגפן	"},
                new Station{Code =  38870   , Lattitude =   31.866772   , Longitude =   34.864555   , Name = "התאנה\\האלון	"},
                new Station{Code =  38871   , Lattitude =   31.809325   , Longitude =   34.784347   , Name = "דרך הפרחים\\יסמין	"},
                new Station{Code =  38872   , Lattitude =   31.80037    , Longitude =   34.778239   , Name = "יצחק רבין\\פנחס ספיר	"},
                new Station{Code =  38873   , Lattitude =   31.799224   , Longitude =   34.782985   , Name = "מנחם בגין\\יצחק רבין	"},
                new Station{Code =  38874   , Lattitude =   31.800334   , Longitude =   34.785069   , Name = "חיים הרצוג\\דולב	"},
                new Station{Code =  38875   , Lattitude =   31.802319   , Longitude =   34.786735   , Name = "בית ספר גוונים\\ארז	"},
                new Station{Code =  38876   , Lattitude =   31.804595   , Longitude =   34.786623   , Name = "דרך האילנות\\אלון	"},
                new Station{Code =  38877   , Lattitude =   31.805041   , Longitude =   34.785098   , Name = "דרך האילנות\\מנחם בגין	"},
                new Station{Code =  38878   , Lattitude =   31.816751   , Longitude =   34.782252   , Name = "העצמאות\\וייצמן"},
                new Station{Code =  38879   , Lattitude =   31.816579   , Longitude =   34.779753   , Name = "וייצמן\\מרבד הקסמים"},
                new Station{Code =  38880   , Lattitude =   31.801182   , Longitude =   34.787199   , Name = "צאלה\\אלמוג"},
                new Station{Code =  38881   , Lattitude =   31.802279   , Longitude =   34.786055   , Name = "גן חצב\\צאלה"},
                new Station{Code =  38882   , Lattitude =   31.814676   , Longitude =   34.777574   , Name = "פינס\\לוינסון"},
                new Station{Code =  38883   , Lattitude =   31.813285   , Longitude =   34.775928   , Name = "פיינברג\\שכביץ"},
                new Station{Code =  38884   , Lattitude =   31.806959   , Longitude =   34.773504   , Name = "בן גוריון\\פוקס"},
                new Station{Code =  38885   , Lattitude =   31.884187   , Longitude =   34.805494   , Name = "לוי אשכול\\הרב דוד ישראל"},
                new Station{Code =  38886   , Lattitude =   31.910118   , Longitude =   34.805809   , Name = "שושן\\אופנהיימר"},
                new Station{Code =  38887   , Lattitude =   31.882474   , Longitude =   34.80506    , Name = "הרב דוד ישראל\\אריה דולצין"},
                new Station{Code =  38888   , Lattitude =   31.878667   , Longitude =   34.81138    , Name = "קרוננברג\\ארגמן"},
                new Station{Code =  38889   , Lattitude =   31.975479   , Longitude =   34.813355   , Name = "יעקב פריימן\\בנימין שמוטקין"}
            };

            LineList = new List<Line>
            {
                new Line    {   Area = Enums.Areas.CENTRAL, LineNumber = 1, PersonalId = 9, FirstStation = 38831, LastStation = 38836     },
                new Line    {   Area = Enums.Areas.CENTRAL, LineNumber = 51, PersonalId = 10, FirstStation = 38833, LastStation = 38837   },
                new Line    {   Area = Enums.Areas.CENTRAL, LineNumber = 23, PersonalId = 11, FirstStation = 38835, LastStation = 38845   },
                new Line    {   Area = Enums.Areas.CENTRAL, LineNumber = 100, PersonalId = 12, FirstStation = 38844, LastStation = 38848   },
                new Line    {   Area = Enums.Areas.CENTRAL, LineNumber = 770, PersonalId = 13, FirstStation = 38841, LastStation = 38849   },
                new Line    {   Area = Enums.Areas.CENTRAL, LineNumber = 999, PersonalId = 14, FirstStation = 38837, LastStation = 38846 },
                new Line    {   Area = Enums.Areas.CENTRAL, LineNumber = 111, PersonalId=15, FirstStation=38860, LastStation=38889  },
                new Line    {   Area = Enums.Areas.CENTRAL, LineNumber = 200, PersonalId=16, FirstStation=38870, LastStation=38880  },
                new Line    {   Area = Enums.Areas.CENTRAL, LineNumber = 288, PersonalId=17, FirstStation=38880, LastStation=38889  },
                new Line    {   Area = Enums.Areas.CENTRAL, LineNumber = 101, PersonalId=18, FirstStation=38865, LastStation=38885  },
                new Line    {   Area = Enums.Areas.CENTRAL, LineNumber = 110, PersonalId=19, FirstStation=38860, LastStation=38880  },
            };

            LineStationsList = new List<LineStation>
            {
                // Line 1
                new LineStation     {   StationCode = 38831, LineId = 9, LineStationIndex = 1, NextStation = 38832, PrevStation = 0     },
                new LineStation     {   StationCode = 38832, LineId = 9, LineStationIndex = 2, NextStation = 38833, PrevStation = 38831     },
                new LineStation     {   StationCode = 38833, LineId = 9, LineStationIndex = 3, NextStation = 38834, PrevStation = 38832     },
                new LineStation     {   StationCode = 38834, LineId = 9, LineStationIndex = 4, NextStation = 38835, PrevStation = 38833     },
                new LineStation     {   StationCode = 38835, LineId = 9, LineStationIndex = 5, NextStation = 38836, PrevStation = 38834     },
                new LineStation     {   StationCode = 38836, LineId = 9, LineStationIndex = 6, NextStation = 0, PrevStation = 38835     },

                // Line 51
                new LineStation     { StationCode = 38833, LineId = 10, LineStationIndex = 1, NextStation = 38834, PrevStation = 0  },
                new LineStation     { StationCode = 38834, LineId = 10, LineStationIndex = 2, NextStation = 38835, PrevStation = 38833},
                new LineStation     { StationCode = 38835, LineId = 10, LineStationIndex = 3, NextStation = 38836, PrevStation = 38834},
                new LineStation     { StationCode = 38836, LineId = 10, LineStationIndex = 4, NextStation = 38837, PrevStation = 38835},
                new LineStation     { StationCode = 38837, LineId = 10, LineStationIndex = 5, NextStation = 0, PrevStation = 38836  },

                // Line 770
                new LineStation     {   StationCode = 38841, LineId = 13, LineStationIndex = 1, NextStation = 38842, PrevStation = 0     },
                new LineStation     {   StationCode = 38842, LineId = 13, LineStationIndex = 2, NextStation = 38843, PrevStation = 38841     },
                new LineStation     {   StationCode = 38843, LineId = 13, LineStationIndex = 3, NextStation = 38849, PrevStation = 38842     },
                new LineStation     {   StationCode = 38849, LineId = 13, LineStationIndex = 4, NextStation = 0, PrevStation = 38843     },

                // Line 23
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
                new LineStation{    StationCode =   38845   , LineId = 11   , LineStationIndex =    11  , NextStation = 0   , PrevStation = 38844   },

                // Line 111
                new LineStation{    StationCode =   38860   , LineId = 15   , LineStationIndex =    1   , NextStation = 38861   , PrevStation = 0   },
                new LineStation{    StationCode =   38861   , LineId = 15   , LineStationIndex =    2   , NextStation = 38862   , PrevStation = 38860   },
                new LineStation{    StationCode =   38862   , LineId = 15   , LineStationIndex =    3   , NextStation = 38863   , PrevStation = 38861   },
                new LineStation{    StationCode =   38863   , LineId = 15   , LineStationIndex =    4   , NextStation = 38864   , PrevStation = 38862   },
                new LineStation{    StationCode =   38864   , LineId = 15   , LineStationIndex =    5   , NextStation = 38865   , PrevStation = 38863   },
                new LineStation{    StationCode =   38865   , LineId = 15   , LineStationIndex =    6   , NextStation = 38866   , PrevStation = 38864   },
                new LineStation{    StationCode =   38866   , LineId = 15   , LineStationIndex =    7   , NextStation = 38867   , PrevStation = 38865   },
                new LineStation{    StationCode =   38867   , LineId = 15   , LineStationIndex =    8   , NextStation = 38868   , PrevStation = 38866   },
                new LineStation{    StationCode =   38868   , LineId = 15   , LineStationIndex =    9   , NextStation = 38869   , PrevStation = 38867   },
                new LineStation{    StationCode =   38869   , LineId = 15   , LineStationIndex =    10  , NextStation = 38870   , PrevStation = 38868   },
                new LineStation{    StationCode =   38870   , LineId = 15   , LineStationIndex =    11  , NextStation = 38871   , PrevStation = 38869   },
                new LineStation{    StationCode =   38871   , LineId = 15   , LineStationIndex =    12  , NextStation = 38872   , PrevStation = 38870   },
                new LineStation{    StationCode =   38872   , LineId = 15   , LineStationIndex =    13  , NextStation = 38873   , PrevStation = 38871   },
                new LineStation{    StationCode =   38873   , LineId = 15   , LineStationIndex =    14  , NextStation = 38874   , PrevStation = 38872   },
                new LineStation{    StationCode =   38874   , LineId = 15   , LineStationIndex =    15  , NextStation = 38875   , PrevStation = 38873   },
                new LineStation{    StationCode =   38875   , LineId = 15   , LineStationIndex =    16  , NextStation = 38876   , PrevStation = 38874   },
                new LineStation{    StationCode =   38876   , LineId = 15   , LineStationIndex =    17  , NextStation = 38877   , PrevStation = 38875   },
                new LineStation{    StationCode =   38877   , LineId = 15   , LineStationIndex =    18  , NextStation = 38878   , PrevStation = 38876   },
                new LineStation{    StationCode =   38878   , LineId = 15   , LineStationIndex =    19  , NextStation = 38879   , PrevStation = 38877   },
                new LineStation{    StationCode =   38879   , LineId = 15   , LineStationIndex =    20  , NextStation = 38880   , PrevStation = 38878   },
                new LineStation{    StationCode =   38880   , LineId = 15   , LineStationIndex =    21  , NextStation = 38881   , PrevStation = 38879   },
                new LineStation{    StationCode =   38881   , LineId = 15   , LineStationIndex =    22  , NextStation = 38882   , PrevStation = 38880   },
                new LineStation{    StationCode =   38882   , LineId = 15   , LineStationIndex =    23  , NextStation = 38883   , PrevStation = 38881   },
                new LineStation{    StationCode =   38883   , LineId = 15   , LineStationIndex =    24  , NextStation = 38884   , PrevStation = 38882   },
                new LineStation{    StationCode =   38884   , LineId = 15   , LineStationIndex =    25  , NextStation = 38885   , PrevStation = 38883   },
                new LineStation{    StationCode =   38885   , LineId = 15   , LineStationIndex =    26  , NextStation = 38886   , PrevStation = 38884   },
                new LineStation{    StationCode =   38886   , LineId = 15   , LineStationIndex =    27  , NextStation = 38887   , PrevStation = 38885   },
                new LineStation{    StationCode =   38887   , LineId = 15   , LineStationIndex =    28  , NextStation = 38888   , PrevStation = 38886   },
                new LineStation{    StationCode =   38888   , LineId = 15   , LineStationIndex =    29  , NextStation = 38889   , PrevStation = 38887   },
                new LineStation{    StationCode =   38889   , LineId = 15   , LineStationIndex =    30  , NextStation = 0   , PrevStation = 38888   },

                // Line 200
                new LineStation{    StationCode =   38870   , LineId = 16   , LineStationIndex =    1  , NextStation = 38871   , PrevStation = 0   },
                new LineStation{    StationCode =   38871   , LineId = 16   , LineStationIndex =    2  , NextStation = 38872   , PrevStation = 38870   },
                new LineStation{    StationCode =   38872   , LineId = 16   , LineStationIndex =    3  , NextStation = 38873   , PrevStation = 38871   },
                new LineStation{    StationCode =   38873   , LineId = 16   , LineStationIndex =    4  , NextStation = 38874   , PrevStation = 38872   },
                new LineStation{    StationCode =   38874   , LineId = 16   , LineStationIndex =    5  , NextStation = 38875   , PrevStation = 38873   },
                new LineStation{    StationCode =   38875   , LineId = 16   , LineStationIndex =    6  , NextStation = 38876   , PrevStation = 38874   },
                new LineStation{    StationCode =   38876   , LineId = 16   , LineStationIndex =    7  , NextStation = 38877   , PrevStation = 38875   },
                new LineStation{    StationCode =   38877   , LineId = 16   , LineStationIndex =    8  , NextStation = 38878   , PrevStation = 38876   },
                new LineStation{    StationCode =   38878   , LineId = 16   , LineStationIndex =    9  , NextStation = 38879   , PrevStation = 38877   },
                new LineStation{    StationCode =   38879   , LineId = 16   , LineStationIndex =    10  , NextStation = 38880   , PrevStation = 38878   },
                new LineStation{    StationCode =   38880   , LineId = 16   , LineStationIndex =    11  , NextStation = 0   , PrevStation = 38879   },

                // Line 288
                new LineStation{    StationCode =   38880   , LineId = 17   , LineStationIndex =    1   , NextStation = 38881   , PrevStation = 0   },
                new LineStation{    StationCode =   38881   , LineId = 17   , LineStationIndex =    2   , NextStation = 38882   , PrevStation = 38880   },
                new LineStation{    StationCode =   38882   , LineId = 17   , LineStationIndex =    3   , NextStation = 38883   , PrevStation = 38881   },
                new LineStation{    StationCode =   38883   , LineId = 17   , LineStationIndex =    4   , NextStation = 38884   , PrevStation = 38882   },
                new LineStation{    StationCode =   38884   , LineId = 17   , LineStationIndex =    5   , NextStation = 38885   , PrevStation = 38883   },
                new LineStation{    StationCode =   38885   , LineId = 17   , LineStationIndex =    6   , NextStation = 38886   , PrevStation = 38884   },
                new LineStation{    StationCode =   38886   , LineId = 17   , LineStationIndex =    7   , NextStation = 38887   , PrevStation = 38885   },
                new LineStation{    StationCode =   38887   , LineId = 17   , LineStationIndex =    8   , NextStation = 38888   , PrevStation = 38886   },
                new LineStation{    StationCode =   38888   , LineId = 17   , LineStationIndex =    9   , NextStation = 38889   , PrevStation = 38887   },
                new LineStation{    StationCode =   38889   , LineId = 17   , LineStationIndex =    10  , NextStation = 0   , PrevStation = 38888   }

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
                new AdjacentStations{Station1 = 38835, Station2 = 38836, Distance = 1.5, Time = new TimeSpan(0,3,0) },
                new AdjacentStations{Station1 = 38836, Station2 = 38837, Distance = 1.5, Time = new TimeSpan(0,3,0) }
            };

            LineTripsList = new List<LineTrip>
            {
                new LineTrip{LineId = 9,StartAt = new TimeSpan(08,30,00),FinishAt = new TimeSpan(19,00,00), Frequency = new TimeSpan(00,15,00), Id=9083000 },
                new LineTrip{LineId = 9,StartAt = new TimeSpan(08,45,00),FinishAt = new TimeSpan(19,00,00), Frequency = new TimeSpan(00,15,00), Id=9083000 },
                new LineTrip{LineId = 9,StartAt = new TimeSpan(09,00,00),FinishAt = new TimeSpan(19,00,00), Frequency = new TimeSpan(00,15,00), Id=9083000 },
                new LineTrip{LineId = 9,StartAt = new TimeSpan(09,15,00),FinishAt = new TimeSpan(19,00,00), Frequency = new TimeSpan(00,15,00), Id=9083000 },
                new LineTrip{LineId = 9,StartAt = new TimeSpan(09,30,00),FinishAt = new TimeSpan(19,00,00), Frequency = new TimeSpan(00,15,00), Id=9083000 },
                new LineTrip{LineId = 9,StartAt = new TimeSpan(09,45,00),FinishAt = new TimeSpan(19,00,00), Frequency = new TimeSpan(00,15,00), Id=9083000 },
                new LineTrip{LineId = 10,StartAt = new TimeSpan(08,35,00),FinishAt = new TimeSpan(19,00,00), Frequency = new TimeSpan(00,15,00), Id=10083500 }

            };
        }
    }
}
