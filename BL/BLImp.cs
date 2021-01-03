using BLAPI;
using BO;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL
{
    internal class BLImp : IBL
    {
        IDL dl = DLFactory.GetDL();
        static Random r = new Random(DateTime.Now.Millisecond);
        public IEnumerable<Bus> GetAllBuses()
        {
            return from item in dl.GetAllBuses()
                   let bus = GetBus(item.LicenseNum)
                   select bus;
        }

        public Bus GetBus(int license)
        {
            Bus bus = new Bus();
            var tempBus = dl.GetBus(license);
            tempBus.CopyPropertiesTo(bus);
            return bus;
        }

        public IEnumerable<Bus> GetAllBusesThat(Predicate<Bus> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Line> GetAllLines()
        {
            return from item in dl.GetAllLines()
                   let line = GetLine(item.Id)
                   select line;
        }

        public IEnumerable<Station> GetAllStations()
        {
            return from item in dl.GetAllStations()
                   let station = GetStation(item.Code)
                   select station;
        }

        public Station GetStation(int code)
        {
            Station station = new Station();
            var tempStation = dl.GetStation(code);
            tempStation.CopyPropertiesTo(station);
            station.LinesAtStation = from lines in dl.GetStationLines(code)
                                     orderby lines
                                     select lines;
            return station;
        }

        public Line GetLine(int id)
        {
            Line line = new Line();
            var tempLine = dl.GetLine(id);
            tempLine.CopyPropertiesTo(line);
            var stationIDs = from numbers in dl.GetLineStations(id)
                             select numbers;
            int index = 1;
            line.Stations = from numbers in stationIDs
                            let name = dl.GetStation(numbers).Name
                            select new LineStation() { Station = numbers, StationName = name, Index = index++ };


            ;
            return line;
        }

        public bool AddBus(Bus bus)
        {
            const int MIN_SEVEN = 1000000;
            const int MAX_SEVEN = 9999999;
            const int MIN_EIGHT = 10000000;
            const int MAX_EIGHT = 99999999;
            var busBO = dl.GetBus(bus.LicenseNum);
            if (busBO != null)
            {
                throw new Exception("This bus number already exist");
            }
            if (!(bus.FromDate.Year >= 2018 && bus.LicenseNum >= MIN_EIGHT && bus.LicenseNum <= MAX_EIGHT)
                || !(bus.FromDate.Year < 2018 && bus.LicenseNum <= MAX_SEVEN && bus.LicenseNum >= MIN_SEVEN))
                throw new Exception("License num length doesn't match begin date!");
            if(bus.FuelRemain < 0 || bus.FuelRemain > 1200)
                throw new Exception("Gas should be between 0 to 1200!");            
            DO.Bus busDO = new DO.Bus();
            bus.CopyPropertiesTo(busDO);
            dl.AddBus(busDO);
            return true;
        }

        public bool AddStation(Station station)
        {
            var stationBO = dl.GetStation(station.Code);
            if (stationBO != null)
            {
                throw new Exception("This station already exist");
            }
            DO.Station stationDO = new DO.Station();
            station.CopyPropertiesTo(stationDO);
            dl.AddStation(stationDO);
            return true;
        }

        public bool AddLine(Line line)
        {
            var lineBO = dl.GetLine(line.Code);
            if (lineBO != null)
            {
                throw new Exception("This line already exist");
            }
            DO.Line lineDO = new DO.Line();
            line.CopyPropertiesTo(lineDO);
            dl.AddLine(lineDO);
            int index = 0;
            int[] stationArray = new int[line.Stations.Count()];

            foreach (var item in line.Stations)
            {
                stationArray[index++] = item.Station;
            };
            for (int i = 0; i < stationArray.Length; i++)
            {
                if (i == 0)
                    dl.AddLineStation(new DO.LineStation { LineId = line.Id, LineStationIndex = 1, StationCode = stationArray[0], NextStation = stationArray[1], PrevStation = 0 });
                else if (i == stationArray.Length - 1)
                    dl.AddLineStation(new DO.LineStation { LineId = line.Id, LineStationIndex = i + 1, StationCode = stationArray[i], NextStation = 0, PrevStation = stationArray[i - 1] });
                else
                    dl.AddLineStation(new DO.LineStation { LineId = line.Id, LineStationIndex = i + 1, StationCode = stationArray[i], NextStation = stationArray[i + 1], PrevStation = stationArray[i - 1] });
            }

            double distance;
            double distanceTime;
            for (int i = 0; i < stationArray.Length - 1; i++)
            {
                if (dl.GetAdjacentStations(stationArray[i], stationArray[i + 1]) == null)
                {
                    distance = r.NextDouble() * 10;
                    distanceTime = distance * r.Next(20, 50) / 60 / 60;
                    dl.AddAdjacentStations(new DO.AdjacentStations { Station1 = stationArray[i], Station2 = stationArray[i + 1], Distance = distance, Time = new TimeSpan((int)distanceTime % 60 % 60, (int)distanceTime % 60 / 60, (int)distanceTime / 60 / 60) });
                }
            }
            return true;
        }

        public IEnumerable<string> GetAllUserNames()
        {
            return from item in dl.GetAllUsers()
                   select item.UserName;
        }

        public bool ValidateUser(string userName, string password)
        {
            return dl.GetAllUsers().Where(u => u.UserName.ToLower() == userName.ToLower() && u.Password == password && u.Admin == true).Select(u => u.Admin).FirstOrDefault();
        }

        public bool RemoveStationFromLine(Line line, int stationToRemove)
        {
            foreach (var item in line.Stations)
            {
                dl.RemoveLineStation(item.Station);
            }
            //if station is first
            //if station is last
            int index = 0;
            int[] stationArray = new int[line.Stations.Count()];
            foreach (var item in line.Stations)
            {
                if(item.Station != stationToRemove)
                    stationArray[index++] = item.Station;
            };
            foreach (var item in line.Stations)
            {
                stationArray[index++] = item.Station;
            };
            for (int i = 0; i < stationArray.Length; i++)
            {
                if (i == 0)
                    dl.AddLineStation(new DO.LineStation { LineId = line.Id, LineStationIndex = 1, StationCode = stationArray[0], NextStation = stationArray[1], PrevStation = 0 });
                else if (i == stationArray.Length - 1)
                    dl.AddLineStation(new DO.LineStation { LineId = line.Id, LineStationIndex = i + 1, StationCode = stationArray[i], NextStation = 0, PrevStation = stationArray[i - 1] });
                else
                    dl.AddLineStation(new DO.LineStation { LineId = line.Id, LineStationIndex = i + 1, StationCode = stationArray[i], NextStation = stationArray[i + 1], PrevStation = stationArray[i - 1] });
            }
            return true;
        }
    }
}
