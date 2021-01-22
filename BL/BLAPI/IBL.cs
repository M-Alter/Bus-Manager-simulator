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
        /// <summary>
        /// get a bus 
        /// </summary>
        /// <param name="license">license of the bus</param>
        /// <returns>a bus instance</returns>
        Bus GetBus(int license);
        /// <summary>
        /// get all the buses
        /// </summary>
        /// <returns>a collection of busses</returns>
        IEnumerable<Bus> GetAllBuses();
        IEnumerable<Bus> GetAllBusesThat(Predicate<Bus> predicate);
        bool AddBus(Bus bus);
        void DeleteBus(int licenseNum);

        #endregion
        Station GetStation(int id);

        /// <summary>
        /// get all the available stations
        /// </summary>
        /// <returns></returns>
        IEnumerable<Station> GetAllStations();

        /// <summary>
        /// gets all stations that conform the predicate function given
        /// </summary>
        /// <param name="predicate">condition to conform</param>
        /// <returns>a collection of stations that conform to predicate</returns>
        IEnumerable<Station> GetAllStations(Predicate<Station> predicate);


        bool AddStation(Station station);

        Line GetLine(int id);
        /// <summary>
        /// gets all the lines available
        /// </summary>
        /// <returns>a collection of all the lines</returns>
        IEnumerable<Line> GetAllLines();

        bool AddLine(Line line);
        bool AddLineTrip(int lineId, TimeSpan time);
        bool RemoveLine(int lineId, int lastStation);
        bool RemoveStationFromLine(int lineId, int stationToRemove);
        bool AddAdjacentStations(AdjacentStations adj);

        /// <summary>
        /// get the details of the 2 adjacent stations
        /// </summary>
        /// <param name="first">first station</param>
        /// <param name="second">second station</param>
        /// <returns>all the details between these two stations</returns>
        AdjacentStations GetAdjacentStations(int first, int second);
        IEnumerable<AdjacentStations> GetAllAdjacentStations();
        bool UpdateAdjacentStatoins(AdjacentStations adjacentStations);
        IEnumerable<string> GetAllUserNames(bool admin);
        bool ValidatePassword(string userName, string password);
        void ResendPassword(string userName, string emailAddress);

        void StartSimulator(TimeSpan startTime, int Rate, Action<TimeSpan> updateTime);
        void StopSimulator();
        
        void SetStationPanel(int station, Action<LineTiming> updateBus);
        bool RemoveLineTrip(int personalId, TimeSpan time);
    }
}
