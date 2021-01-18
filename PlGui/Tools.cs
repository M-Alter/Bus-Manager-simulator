using PO;
using System.Collections.Generic;

namespace PlGui
{
    class Tools
    {
        /// <summary>
        /// method that gets a BO.Bus And Returns a PO.Bus
        /// </summary>
        /// <param name="BOBus"></param>
        /// <returns></returns>
        public static Bus POBus(BO.Bus BOBus)
        {
            Bus bus = new Bus
            {
                LicenseNum = BOBus.LicenseNum,
                FromDate = BOBus.FromDate,
                FuelRemain = BOBus.FuelRemain,
                TotalTrip = BOBus.TotalTrip,
                Status = BOBus.Status
            };
            return bus;
        }

        /// <summary>
        /// method that gets a BO.Line and return a PO.Line
        /// </summary>
        /// <param name="BOLine"></param>
        /// <returns></returns>
        public static Line POLine(BO.Line BOLine)
        {
            Line line = new Line
            {
                PersonalId = BOLine.PersonalId,
                LineNumber = BOLine.LineNumber,
                FirstStation = BOLine.FirstStation,
                FirstStationName = BOLine.FirstStationName,
                LastStation = BOLine.LastStation,
                LastStationName = BOLine.LastStationName,
                Area = BOLine.Area,
                IsActive = BOLine.IsActive,
                Stations = BOLine.Stations
            };
            return line;
        }

        /// <summary>
        /// method that gets a BO.Line and return a PO.Line
        /// </summary>
        /// <param name="POLine"></param>
        /// <returns></returns>
        public static BO.Line BOLine(PO.Line POLine)
        {
            BO.Line line = new BO.Line
            {
                PersonalId = POLine.PersonalId,
                LineNumber = POLine.LineNumber,
                FirstStation = POLine.FirstStation,
                FirstStationName = POLine.FirstStationName,
                LastStation = POLine.LastStation,
                LastStationName = POLine.LastStationName,
                Area = POLine.Area,
                IsActive = POLine.IsActive,
                Stations = POLine.Stations
            };
            return line;
        }

        public static List<PO.StationLine> POStationLine(IEnumerable<BO.StationLine> BOStationLine)
        {
            List<StationLine> stationLine = new List<StationLine>();
            foreach (var item in BOStationLine)
            {
                stationLine.Add(new StationLine
                {
                    LineNumber = item.LineNumber,
                    LastStation = item.LastStation
                });
            };
            return stationLine;
        }

        /// <summary>
        /// method that get a BO.Station and returns a PO.Station
        /// </summary>
        /// <param name="BOStation"></param>
        /// <returns></returns>
        public static Station POStation(BO.Station BOStation)
        {
            Station station = new Station
            {
                Code = BOStation.Code,
                Name = BOStation.Name,
                Lattitude = BOStation.Lattitude,
                Longitude = BOStation.Longitude,
                LinesAtStation = POStationLine(BOStation.LinesAtStation)
            };
            return station;
        }
    }
}
