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

        public IEnumerable<Line> GetAllLines()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Station> GetAllStations()
        {
            throw new NotImplementedException();
        }

        public Bus GetBus(int license)
        {
            Bus bus = new Bus();
            var tempBus = dl.GetBus(license);
            tempBus.CopyPropertiesTo(bus);
            return bus;
        }
    }
}
