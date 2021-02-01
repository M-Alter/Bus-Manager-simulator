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
            //return from bus in DataSource.BusList
            //       group bus.Clone() by bus.FromDate.Year into fromYear
            //       orderby fromYear
            //       select fromYear;

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
            //foreach (var busItem in DataSource.BusList)
            //{
            //    if (busItem.LicenseNum == bus.LicenseNum)
            //    {
            //        //throw
            //    }
            //}

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

        public void RemoveBus(int licenseNum)
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
                throw new BusException(licenseNum, " bus wasn't found ");
        }


        public bool RemoveLine(int lineId, int lastStation)
        {
            Line line = new Line();
            foreach (var item in DataSource.LineList)
            {
                if (item.PersonalId == lineId && item.LastStation == lastStation)
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

        public void RemoveStation(int code)
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

        public bool UpdateAdjacentStations(AdjacentStations adjacentStations)
        {
            var temp = DataSource.AdjacentStationsList.Find(adj => adj.Station1 == adjacentStations.Station1 && adj.Station2 == adjacentStations.Station2);
            //Time is a struct type so no need to clone
            temp.Time = adjacentStations.Time;
            temp.Distance = adjacentStations.Distance;
            return true;
        }

        public int GenerateLinePersonalId()
        {
            return DataSource.linePersonalIdGenerator++;
        }

        public IEnumerable<LineTrip> GetLineSchedules()
        {
            return from ls in DataSource.LineTripsList
                   select ls.Clone();
        }

        public IEnumerable<TimeSpan> GetLineSchedules(int lineId)
        {
            return from ls in DataSource.LineTripsList
                   where ls.LineId == lineId
                   select ls.Clone().StartAt;
        }

        public bool UpdateLine(int lineId, int firstStation, int lastStation)
        {
            var cur = DataSource.LineList.Find(line => line.PersonalId == lineId);
            cur.FirstStation = firstStation;
            cur.LastStation = lastStation;
            return true;
        }

        /// <summary>
        /// add a linetrip to line at time time
        /// </summary>
        /// <param name="lineId"></param>
        /// <param name="startTime"></param>
        /// <returns></returns>
        public bool AddLineTrip(int lineId, TimeSpan startTime)
        {
            DataSource.LineTripsList.Add(new LineTrip { LineId = lineId, StartAt = startTime, Id = int.Parse($"{lineId}{startTime.Hours}{startTime.Minutes}{startTime.Seconds}") });
            return true;
        }

        /// <summary>
        /// remove a linetrip from line lineid
        /// </summary>
        /// <param name="lineId"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public bool RemoveLineTrip(int lineId, TimeSpan time)
        {
            return DataSource.LineTripsList.Remove(DataSource.LineTripsList.Find(lt => lt.StartAt == time));
        }
    }
}
