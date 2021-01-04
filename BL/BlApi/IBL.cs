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

        #endregion
        Station GetStation(int id);
        IEnumerable<Station> GetAllStations();
        bool AddStation(Station station);
        Line GetLine(int id);
        IEnumerable<Line> GetAllLines();
        bool AddLine(Line line);
        bool RemoveStationFromLine(Line line, int stationToRemove);
        IEnumerable<string> GetAllUserNames(bool admin);
        bool ValidatePassword(string userName, string password);
        void ResendPassword(string userName, string emailAddress);

    }
}
