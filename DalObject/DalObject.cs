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

        public void AddBus(Bus bus)
        {
            foreach (var busItem in DataSource.BusList)
            {
                if (busItem.LicenseNum == bus.LicenseNum)
                {
                    //throw
                }
            }

            DataSource.BusList.Add(bus);
        }

        public Bus GetBus(int licenseNum)
        {
            Bus bus = DataSource.BusList.Find(b => b.LicenseNum == licenseNum);

            //if (bus != null)
                return bus;
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
            throw new NotImplementedException();
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
    }
}
