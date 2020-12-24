using DalApi;
using DO;
using DS;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DL
{
    class DalObject : IDL
    {

        #region Bus

        public IEnumerable<Bus> GetAllBuses()
        {
            List<Bus> buses = new List<Bus>();
            foreach (var bus in DataSource.BusList)
                buses.Add(bus);
            return buses.AsEnumerable();
        }

        public IEnumerable<Bus> GetAllBusesThat(Predicate<Bus> predicate)
        {
            List<Bus> buses = new List<Bus>();
            foreach (var bus in DataSource.BusList)
            {
                if (predicate(bus))
                    buses.Add(bus);
            }
            return buses.AsEnumerable();
        }

        public Bus GetBus(int licenseNum)
        {
            Bus bus = DataSource.BusList.Find(b => b.LicenseNum == licenseNum);

            // if (bus != null)
            return bus;
            //else
            //    throw;

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
            throw new NotImplementedException();
        }

        public IEnumerable<Station> GetAllStationsThat(Predicate<Station> predicate)
        {
            throw new NotImplementedException();
        }

        public Station GetStation(int code)
        {
            throw new NotImplementedException();
        }

        public void DeleteStation(int code)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
