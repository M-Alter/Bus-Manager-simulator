﻿using BLAPI;
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

        public Station GetStation(int id)
        {
            Station station = new Station();
            var tempStation = dl.GetStation(id);
            tempStation.CopyPropertiesTo(station);
            return station;
        }

        public Line GetLine(int id)
        {
            Line line = new     Line();
            var tempLine = dl.GetLine(id);
            tempLine.CopyPropertiesTo(line);
            return line;
        }
    }
}
