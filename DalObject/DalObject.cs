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
        private static readonly DalObject instance = new DalObject();
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

            if (bus != null)
                return bus.Clone();
            else
                return default(Bus);

        }

        public void UpdateBus()
        {
            throw new NotImplementedException();
        }

        public void DeleteBus(int licenseNum)
        {
            //Bus bus = DataSource.BusList.Find(b => b.LicenseNum == licenseNum);
            Bus bus = new Bus();
            foreach (var item in DataSource.BusList)
            {
                if (item.LicenseNum == licenseNum)
                    bus = item;
            }
            if (bus != null)
            {
                DataSource.BusList.Remove(bus);
            }
            else
                throw new Exception("This bus wasn't found");
        }


        public bool RemoveLine(int lineId, int lastStation)
        {
            Line line = new Line();
            foreach (var item in DataSource.LineList)
            {
                if (item.LineNumber == lineId && item.LastStation == lastStation)
                {
                    line = item;
                    break;
                }  
            }
            if (line != default(Line))
                DataSource.LineList.Remove(line);
            else
                throw new Exception("This bus wasn't found");
            return true;
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
                return result;
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
            Line result = DataSource.LineList.Find(s => s.PersonalId == id);
                return result;
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

        public IEnumerable<User> GetAllUsers()
        {
            return from item in DataSource.UserList
                   select item.Clone();
        }

        bool IDL.AddBus(Bus bus)
        {
            DataSource.BusList.Add(bus.Clone());
            return true;
        }

        bool IDL.AddStation(Station station)
        {
            DataSource.StationList.Add(station.Clone());
            return true;
        }

        public bool AddLine(Line line)
        {
            DataSource.LineList.Add(line.Clone());
            return true;
        }

        public bool AddLineStation(LineStation lineStation)
        {
            DataSource.LineStationsList.Add(lineStation.Clone());
            return true;
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

        public bool RemoveAllLineStation(int lineID)
        {
            List<LineStation> stations = new List<LineStation>();

            foreach (var item in DataSource.LineStationsList)
                if (item.LineId == lineID)
                    stations.Add(item);

            foreach (var item in stations)
            {
                DataSource.LineStationsList.Remove(item);
            }
            //IEnumerable<LineStation> lineStations = ;
            //var stam = from item in DataSource.LineStationsList
            //where item.LineId == lineID
            //let flag = DataSource.LineStationsList.Remove(item)
            //select flag;

            //foreach (var item in lineStations)
            //{
            //    if (!DataSource.LineStationsList.Remove(item))
            //        return false;
            //}
            return true;
        }

        public bool ValidatePassword(string userName, string password)
        {
            return DataSource.UserList.Exists(u => u.UserName.ToLower() == userName.ToLower() && u.Password == password);
        }

        public IEnumerable<AdjacentStations> GetAllAdjacentStations()
        {
            return from item in DataSource.AdjacentStationsList
                   select item;
        }

        public int GetNextStation(int lineId, int stationCode)
        {
            var result = from item in DataSource.LineStationsList
                         where item.LineId == lineId && item.StationCode == stationCode
                         select item.NextStation;
            return result.FirstOrDefault();
        }

        public bool LineExists(int lineId)
        {
            return DataSource.LineList.Exists(line => line.PersonalId == lineId);
        }

    }
}
