using DalApi;
using DO;
using DS;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DL
{
    sealed class DalObject : IDL
    {
        #region singelton
        static readonly DalObject instance = new DalObject();
        static DalObject() { }// static ctor to ensure instance init is done just before first usage
        DalObject() { } // default => private
        public static DalObject Instance { get => instance; }// The public Instance property to use
        #endregion

        #region Bus

        public IEnumerable<Bus> GetAllBuses()
        {
            List<Bus> buses = new List<Bus>();
            foreach (var bus in DataSource.BusList)
                buses.Add(bus.Clone());
            return buses.AsEnumerable();
        }

        public IEnumerable<Bus> GetAllBusesThat(Predicate<Bus> predicate)
        {
            List<Bus> buses = new List<Bus>();
            foreach (var bus in DataSource.BusList)
            {
                if (predicate(bus))
                    buses.Add(bus.Clone());
            }
            return buses.AsEnumerable();
        }

        public void AddBus(Bus bus)
        {
            foreach (var busItem in DataSource.BusList)
            {
                if (busItem.LicenseNum == bus.LicenseNum)
                {
                    //throw
                }
            }

            DataSource.BusList.Add(bus.Clone());
        }

        public Bus GetBus(int licenseNum)
        {
            Bus bus = DataSource.BusList.Find(b => b.LicenseNum == licenseNum);

            //if (bus != null)
                return bus.Clone();
            //else
            //    throw new Exception("exception");

        }

        public void UpdateBus()
        {
            throw new NotImplementedException();
        }

        public void DeleteBus(int licenseNum)
        {
            Bus bus = DataSource.BusList.Find(b => b.LicenseNum == licenseNum);

            if (bus != null)
            {
                DataSource.BusList.Remove(bus);
            }
            //else
            //    throw
        }

        #endregion

        #region Station
        public IEnumerable<Station> GetAllStations()
        {
            return from station in DataSource.StationList
                   select station.Clone();
        }

        public IEnumerable<Station> GetAllStationsThat(Predicate<Station> predicate)
        {
            throw new NotImplementedException();
        }

        public void AddStation(Station station)
        {
            foreach (var stationItem in DataSource.StationList)
            {
                if (stationItem.Code == station.Code)
                {
                    //throw
                }
            }

            DataSource.StationList.Add(station);
        }

        public Station GetStation(int code)
        {
            Station result = DataSource.StationList.Find(s => s.Code == code);

          //  if (result != null)
            {
                return result;
            }
           // throw
        }

        public void DeleteStation(int code)
        {
            Station result = DataSource.StationList.Find(s => s.Code == code);

              if (result != null)
            {
                DataSource.StationList.Remove(result);
            }
            // throw
        }
        #endregion

        #region Lines

        public Line GetLine(int id)
        {
            Line result = DataSource.LineList.Find(s => s.Id == id);

            //  if (result != null)
            {
                return result;
            }
            // throw
        }

        public IEnumerable<Line> GetAllLines()
        {
            return from lines in DataSource.LineList
                   select lines.Clone();
        }
        #endregion


        public IEnumerable<int> GetLineStations(int lineId)
        {
            var stations = from station in DataSource.LineStationsList
                           where station.LineId == lineId
                           select station/*.Clone()*/;
            return from item in stations
                   from station in DataSource.StationList
                   where item.StationCode == station.Code
                   orderby item.LineStationIndex
                   select station.Code;
        }

        public IEnumerable<int> GetStationLines(int code)
        {
            return from station in DataSource.LineStationsList
                   where station.StationCode == code
                   orderby station.LineId
                   select station.LineId;
        }

        bool IDL.AddBus(Bus bus)
        {
            throw new NotImplementedException();
        }

        bool IDL.AddStation(Station station)
        {
            throw new NotImplementedException();
        }

        public bool AddLine(Line line)
        {
            throw new NotImplementedException();
        }

        public bool AddLineStation(LineStation lineStation)
        {
            throw new NotImplementedException();
        }

        public AdjacentStations GetAdjacentStations(int first, int second)
        {
            return (from item in DataSource.AdjacentStationsList
                    where item.Station1 == first && item.Station2 == second
                    select item).FirstOrDefault();
        }

        public bool AddAdjacentStations(AdjacentStations adjacentStations)
        {
            DataSource.AdjacentStationsList.Add(adjacentStations.Clone());
            return true;
        }
    }
}
