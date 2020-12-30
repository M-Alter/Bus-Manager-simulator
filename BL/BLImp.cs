using BLAPI;
using BO;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    internal class BLImp : IBL
    {
        IDL dl = DLFactory.GetDL();

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
                            select new LineStation() { Station = numbers, StationName = name, Index = index++};


                            ;
            return line;
        }
    }
}
