using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BLAPI
{
    public interface IBL
    {
        #region Bus
        Bus GetBus(int license);
        IEnumerable<Bus> GetAllBuses();
        IEnumerable<Bus> GetAllBusesThat(Predicate<Bus> predicate);
        bool AddBus(Bus bus);
        void DeleteBus(int licenseNum);

        #endregion
        Station GetStation(int id);
        IEnumerable<Station> GetAllStations();
        IEnumerable<Station> GetAllStations(Predicate<Station> predicate);
        bool AddStation(Station station);
        Line GetLine(int id);
        IEnumerable<Line> GetAllLines();
        bool AddLine(Line line);
        void RemoveLine(int lineId, int lastStation);
        bool RemoveStationFromLine(int lineId, int stationToRemove);
        bool AddAdjacentStations(AdjacentStations adj);
        AdjacentStations GetAdjacentStations(int first, int second);
        IEnumerable<AdjacentStations> GetAllAdjacentStations();
        IEnumerable<string> GetAllUserNames(bool admin);
        bool ValidatePassword(string userName, string password);
        void ResendPassword(string userName, string emailAddress);


    }
}
