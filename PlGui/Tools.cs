using PO;
using System.Collections.Generic;

namespace PlGui
{
    class Tools
    {
        /// <summary>
        /// method that gets a BO.Bus And Returns a PO.Bus
        /// </summary>
        /// <param name="boBus"></param>
        /// <returns></returns>
        public static Bus POBus(BO.Bus boBus)
        {
            Bus bus = new Bus
            {
                LicenseNum = boBus.LicenseNum,
                FromDate = boBus.FromDate,
                FuelRemain = boBus.FuelRemain,
                TotalTrip = boBus.TotalTrip,
                Status = boBus.Status
            };
            return bus;
        }

        /// <summary>
        /// method that gets a BO.Line and return a PO.Line
        /// </summary>
        /// <param name="boLine"></param>
        /// <returns></returns>
        public static Line POLine(BO.Line boLine)
        {
            Line line = new Line
            {
                PersonalId = boLine.PersonalId,
                LineNumber = boLine.LineNumber,
                FirstStation = boLine.FirstStation,
                FirstStationName = boLine.FirstStationName,
                LastStation = boLine.LastStation,
                LastStationName = boLine.LastStationName,
                Area = boLine.Area,
                IsActive = boLine.IsActive,
                Stations = boLine.Stations
            };
            return line;
        }

        /// <summary>
        /// method that gets a BO.Line and return a PO.Line
        /// </summary>
        /// <param name="poLine"></param>
        /// <returns></returns>
        public static BO.Line BOLine(PO.Line poLine)
        {
            BO.Line line = new BO.Line
            {
                PersonalId = poLine.PersonalId,
                LineNumber = poLine.LineNumber,
                FirstStation = poLine.FirstStation,
                FirstStationName = poLine.FirstStationName,
                LastStation = poLine.LastStation,
                LastStationName = poLine.LastStationName,
                Area = poLine.Area,
                IsActive = poLine.IsActive,
                Stations = poLine.Stations
            };
            return line;
        }

        public static List<PO.StationLine> POStationLine(IEnumerable<BO.StationLine> boStationLine)
        {
            List<StationLine> stationLine = new List<StationLine>();
            foreach (var item in boStationLine)
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
        /// <param name="boStation"></param>
        /// <returns></returns>
        public static Station POStation(BO.Station boStation)
        {
            Station station = new Station
            {
                Code = boStation.Code,
                Name = boStation.Name,
                Lattitude = boStation.Lattitude,
                Longitude = boStation.Longitude,
                LinesAtStation = POStationLine(boStation.LinesAtStation)
            };
            return station;
        }

        public static PO.AdjacentStations POAdjacentStations(BO.AdjacentStations boAdjacentStations)
        {
            AdjacentStations adjacentStations = new AdjacentStations
            {
                Station1 = boAdjacentStations.Station1,
                Station1Name = boAdjacentStations.Station1Name,
                Station2 = boAdjacentStations.Station2,
                Station2Name = boAdjacentStations.Station2Name,
                Distance = boAdjacentStations.Distance,
                Time = boAdjacentStations.Time
            };
            return adjacentStations;
        }


        public static BO.AdjacentStations BOAdjacentStations(BO.AdjacentStations poAdjacentStations)
        {
            BO.AdjacentStations adjacentStations = new BO.AdjacentStations
            {
                Station1 = poAdjacentStations.Station1,
                Station1Name = poAdjacentStations.Station1Name,
                Station2 = poAdjacentStations.Station2,
                Station2Name = poAdjacentStations.Station2Name,
                Distance = poAdjacentStations.Distance,
                Time = poAdjacentStations.Time
            };
            return adjacentStations;
        }
    }
}
