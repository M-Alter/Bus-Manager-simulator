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
        bool AddLineTrip(int lineId, TimeSpan time);
        bool RemoveLine(int lineId, int lastStation);
        bool RemoveStationFromLine(int lineId, int stationToRemove);
        bool AddAdjacentStations(AdjacentStations adj);
        AdjacentStations GetAdjacentStations(int first, int second);
        IEnumerable<AdjacentStations> GetAllAdjacentStations();
        bool UpdateAdjacentStatoins(AdjacentStations adjacentStations);
        IEnumerable<string> GetAllUserNames(bool admin);
        bool ValidatePassword(string userName, string password);
        void ResendPassword(string userName, string emailAddress);

        void StartSimulator(TimeSpan startTime, int Rate, Action<TimeSpan> updateTime);
        void StopSimulator();
        bool AddLineTrip(int personalId, TimeSpan reuslt);
        void SetStationPanel(int station, Action<LineTiming> updateBus);
        bool RemoveLineTrip(int personalId, TimeSpan time);
    }
}
