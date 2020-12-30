﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DalApi
{
    public interface IDL
    {
        #region Bus
        IEnumerable<Bus> GetAllBuses();
        IEnumerable<Bus> GetAllBusesThat(Predicate<Bus> predicate);
        Bus GetBus(int licenseNum);
        void AddBus(Bus bus);
        void DeleteBus(int licenseNum);
        void UpdateBus();
        #endregion

        #region Station
        IEnumerable<Station> GetAllStations();
        IEnumerable<Station> GetAllStationsThat(Predicate<Station> predicate);
        void AddStation(Station station);
        Station GetStation(int code);
        void DeleteStation(int code);
        #endregion

        #region Lines
        Line GetLine(int id);
        IEnumerable<Line> GetAllLines();
        #endregion

        #region LineStation

        IEnumerable<int> GetLineStations(int lineId);

        IEnumerable<int> GetStationLines(int code);

        #endregion
    }
}