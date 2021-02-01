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
        /// <summary>
        /// returns all the buses
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Bus> GetAllBuses()
        {
            List<Bus> buses = new List<Bus>();
            foreach (var bus in DataSource.BusList)
                buses.Add(bus.Clone());
            return buses.AsEnumerable();
        }

        /// <summary>
        /// get all the buses that comply to the pridicate condition
        /// </summary>
        /// <param name="predicate">condition</param>
        /// <returns></returns>
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

        /// <summary>
        /// add a bus
        /// </summary>
        /// <param name="bus"></param>
        /// <returns></returns>
        public bool AddBus(Bus bus)
        {
            DataSource.BusList.Add(bus.Clone());
            return true;
        }

        /// <summary>
        /// get the bus with the lincence number
        /// </summary>
        /// <param name="licenseNum"></param>
        /// <returns></returns>
        public Bus GetBus(int licenseNum)
        {
            Bus bus = DataSource.BusList.Find(b => b.LicenseNum == licenseNum);

            if (bus != null)
                return bus.Clone();
            else
                return default(Bus);
        }

        /// <summary>
        /// obsolete method
        /// </summary>
        public void UpdateBus()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// remove a bus
        /// </summary>
        /// <param name="licenseNum"></param>
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

        /// <summary>
        /// remove a line
        /// </summary>
        /// <param name="lineId"></param>
        /// <param name="lastStation"></param>
        /// <returns></returns>
        public bool RemoveLine(int lineId, int lastStation)
        {
            Line line = new Line();
            foreach (var item in DataSource.LineList)
            {
                if (item.PersonalId == lineId /*&& item.LastStation == lastStation*/)
                {
                    line = item;
                    break;
                }
            }
            if (line != default(Line))
                DataSource.LineList.Remove(line);
            else
                throw new LineException(lineId, "This bus wasn't found");
            return true;
        }

        #endregion

        #region Station
        /// <summary>
        /// get all the stations
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Station> GetAllStations()
        {
            return from station in DataSource.StationList
                   select station.Clone();
        }

        /// <summary>
        /// get all station that comply to the predicate condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Station> GetAllStationsThat(Predicate<Station> predicate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// add a new station
        /// </summary>
        /// <param name="station"></param>
        /// <returns></returns>
        public bool AddStation(Station station)
        {// can be removed due to check in the bl
            foreach (var stationItem in DataSource.StationList)
            {
                if (stationItem.Code == station.Code) 
                {
                    throw new StationException(station.Code, "this station exists already");
                }
            }
            DataSource.StationList.Add(station);
            return true;
        }

        /// <summary>
        /// get a certain station
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Station GetStation(int code)
        {
            Station result = DataSource.StationList.Find(s => s.Code == code);
            return result;
        }

        /// <summary>
        /// remove a station
        /// </summary>
        /// <param name="code"></param>
        public void RemoveStation(int code)
        {
            Station result = DataSource.StationList.Find(s => s.Code == code);

            if (result != null)
            {
                DataSource.StationList.Remove(result);
            }
            else
                throw new StationException(code, "this station wasn't found");
        }
        #endregion

        #region Lines

        /// <summary>
        /// get a line
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Line GetLine(int id)
        {
            Line result = DataSource.LineList.Find(s => s.PersonalId == id);
            return result;
        }

        /// <summary>
        /// get all the lines 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Line> GetAllLines()
        {
            return from lines in DataSource.LineList
                   select lines.Clone();
        }

        /// <summary>
        /// get all the stations numbers of a line
        /// </summary>
        /// <param name="lineId"></param>
        /// <returns></returns>
        public IEnumerable<int> GetLineStations(int lineId)
        {
            //get the stations of this line
            var stations = from station in DataSource.LineStationsList
                           where station.LineId == lineId
                           select station;
            //make sure that the stations exist
            return from item in stations
                   from station in DataSource.StationList
                   where item.StationCode == station.Code
                   orderby item.LineStationIndex
                   select station.Code;
        }

        /// <summary>
        /// get all the lines that pass in the station
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public IEnumerable<int> GetStationLines(int code)
        {
            return from station in DataSource.LineStationsList
                   where station.StationCode == code
                   orderby station.LineId
                   select station.LineId;
        }

        /// <summary>
        /// add a line 
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public bool AddLine(Line line)
        {
            DataSource.LineList.Add(line.Clone());
            return true;
        }

        /// <summary>
        /// add a line station
        /// </summary>
        /// <param name="lineStation"></param>
        /// <returns></returns>
        public bool AddLineStation(LineStation lineStation)
        {
            DataSource.LineStationsList.Add(lineStation.Clone());
            return true;
        }
        #endregion


        /// <summary>
        /// get all the users credentials
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> GetAllUsers()
        {
            return from item in DataSource.UserList
                   select item.Clone();
        }

        /// <summary>
        /// get the details of 2 adjacent stations
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public AdjacentStations GetAdjacentStations(int first, int second)
        {
            return (from item in DataSource.AdjacentStationsList
                    where item.Station1 == first && item.Station2 == second
                    select item).FirstOrDefault();
        }

        /// <summary>
        /// add the details between 2 stations
        /// </summary>
        /// <param name="adjacentStations"></param>
        /// <returns></returns>
        public bool AddAdjacentStations(AdjacentStations adjacentStations)
        {
            DataSource.AdjacentStationsList.Add(adjacentStations.Clone());
            return true;
        }

        /// <summary>
        /// removes all the stations from a line
        /// </summary>
        /// <param name="lineID"></param>
        /// <returns></returns>
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
            return true;
        }

        /// <summary>
        /// validate the password
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool ValidatePassword(string userName, string password)
        {
            return DataSource.UserList.Exists(u => u.UserName.ToLower() == userName.ToLower() && u.Password == password);
        }

        /// <summary>
        /// get all the details of adjacent stations
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AdjacentStations> GetAllAdjacentStations()
        {
            return from item in DataSource.AdjacentStationsList
                   select item;
        }

        /// <summary>
        /// get the station that comes after stationCode in lineId 
        /// </summary>
        /// <param name="lineId"></param>
        /// <param name="stationCode"></param>
        /// <returns></returns>
        public int GetNextStation(int lineId, int stationCode)
        {
            var result = from item in DataSource.LineStationsList
                         where item.LineId == lineId && item.StationCode == stationCode
                         select item.NextStation;
            return result.FirstOrDefault();
        }

        /// <summary>
        /// check if a line exists
        /// </summary>
        /// <param name="lineId"></param>
        /// <returns></returns>
        public bool LineExists(int lineId)
        {
            return DataSource.LineList.Exists(line => line.PersonalId == lineId);
        }

        /// <summary>
        /// update time and distance between 2 stations
        /// </summary>
        /// <param name="adjacentStations"></param>
        /// <returns></returns>
        public bool UpdateAdjacentStations(AdjacentStations adjacentStations)
        {
            var temp = DataSource.AdjacentStationsList.Find(adj => adj.Station1 == adjacentStations.Station1 && adj.Station2 == adjacentStations.Station2);
            //Time is a struct type so no need to clone
            temp.Time = adjacentStations.Time;
            temp.Distance = adjacentStations.Distance;
            return true;
        }

        /// <summary>
        /// get the next line ID and add 1
        /// </summary>
        /// <returns></returns>
        public int GenerateLinePersonalId()
        {
            return DataSource.linePersonalIdGenerator++;
        }

        /// <summary>
        /// get all the timings of all lines
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LineTrip> GetLineSchedules()
        {
            return from ls in DataSource.LineTripsList
                   select ls.Clone();
        }

        /// <summary>
        /// get the line schedules of a certain line
        /// </summary>
        /// <param name="lineId"></param>
        /// <returns></returns>
        public IEnumerable<TimeSpan> GetLineSchedules(int lineId)
        {
            return from ls in DataSource.LineTripsList
                   where ls.LineId == lineId
                   select ls/*.Clone()*/.StartAt;
        }

        /// <summary>
        /// update first or last station in the line
        /// </summary>
        /// <param name="lineId"></param>
        /// <param name="firstStation"></param>
        /// <param name="lastStation"></param>
        /// <returns></returns>
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
