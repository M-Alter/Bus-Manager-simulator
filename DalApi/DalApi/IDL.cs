using System;
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
        bool AddBus(Bus bus);
        void RemoveBus(int licenseNum);
        void UpdateBus();
        #endregion

        #region Station
        IEnumerable<Station> GetAllStations();
        IEnumerable<Station> GetAllStationsThat(Predicate<Station> predicate);
        bool AddStation(Station station);
        Station GetStation(int code);
        void RemoveStation(int code);
        #endregion

        #region Lines
        Line GetLine(int id);
        IEnumerable<Line> GetAllLines();
        bool AddLine(Line line);
        bool LineExists(int lineId);
        bool RemoveLine(int lineId, int lastStation);
        int GenerateLinePersonalId();
        #endregion

        #region LineStation

        IEnumerable<int> GetLineStations(int lineId);
        int GetNextStation(int lineId, int stationCode);
        IEnumerable<int> GetStationLines(int code);
        bool AddLineStation(LineStation lineStation);
        bool RemoveAllLineStation(int lineID);
        #endregion

        #region AdjacentStations
        AdjacentStations GetAdjacentStations(int first, int second);
        bool AddAdjacentStations(AdjacentStations adjacentStations);
        bool UpdateAdjacentStations(AdjacentStations adjacentStations);
        IEnumerable<AdjacentStations> GetAllAdjacentStations();

        #endregion

        #region Users
        IEnumerable<User> GetAllUsers();
        bool ValidatePassword(string userName, string password);

        #endregion
    }
}