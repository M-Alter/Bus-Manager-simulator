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
        /// <summary>
        /// get all the buses
        /// </summary>
        /// <returns></returns>
        IEnumerable<Bus> GetAllBuses();

        /// <summary>
        /// get all the stations that comply to the condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<Bus> GetAllBusesThat(Predicate<Bus> predicate);

        /// <summary>
        /// get a bus
        /// </summary>
        /// <param name="licenseNum"></param>
        /// <returns></returns>
        Bus GetBus(int licenseNum);

        /// <summary>
        /// add a bus
        /// </summary>
        /// <param name="bus"></param>
        /// <returns></returns>
        bool AddBus(Bus bus);

        /// <summary>
        /// remove a bus
        /// </summary>
        /// <param name="licenseNum"></param>
        void RemoveBus(int licenseNum);

        /// <summary>
        /// unavailable (not in use)
        /// </summary>
        [Obsolete("this method is not available", true)]
        void UpdateBus();
        #endregion

        #region Station
        /// <summary>
        /// get all the stations
        /// </summary>
        /// <returns></returns>
        IEnumerable<Station> GetAllStations();

        /// <summary>
        /// get all the stations that comply to the given condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<Station> GetAllStationsThat(Predicate<Station> predicate);

        /// <summary>
        /// add a new station
        /// </summary>
        /// <param name="station"></param>
        /// <returns></returns>
        bool AddStation(Station station);

        /// <summary>
        /// get a certain station 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Station GetStation(int code);

        /// <summary>
        /// remove a certion station
        /// </summary>
        /// <param name="code"></param>
        void RemoveStation(int code);
        #endregion

        #region Lines
        /// <summary>
        /// get a certain line
        /// </summary>
        /// <param name="personalId"></param>
        /// <returns></returns>
        Line GetLine(int personalId);

        /// <summary>
        /// get all the lines
        /// </summary>
        /// <returns></returns>
        IEnumerable<Line> GetAllLines();

        /// <summary>
        /// add a new line
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        bool AddLine(Line line);

        /// <summary>
        /// check if a line exists
        /// </summary>
        /// <param name="lineId"></param>
        /// <returns></returns>
        bool LineExists(int lineId);

        /// <summary>
        /// remove a line 
        /// </summary>
        /// <param name="lineId"></param>
        /// <returns></returns>
        bool RemoveLine(int lineId);

        /// <summary>
        /// generate a new id 
        /// </summary>
        /// <returns></returns>
        int GenerateLinePersonalId();

        /// <summary>
        /// update first and last station of a line
        /// </summary>
        /// <param name="lineId"></param>
        /// <param name="firstStation"></param>
        /// <param name="lastStation"></param>
        /// <returns></returns>
        bool UpdateLine(int lineId, int firstStation, int lastStation);

        /// <summary>
        /// add a linetrip to a line
        /// </summary>
        /// <param name="lineId"></param>
        /// <param name="startTime"></param>
        /// <returns></returns>
        bool AddLineTrip(int lineId, TimeSpan startTime);
        #endregion

        #region LineStation
        /// <summary>
        /// get all the station ids of a line
        /// </summary>
        /// <param name="lineId"></param>
        /// <returns></returns>
        IEnumerable<int> GetLineStations(int lineId);

        /// <summary>
        /// get the station that comes after stationCode in lineId
        /// </summary>
        /// <param name="lineId"></param>
        /// <param name="stationCode"></param>
        /// <returns></returns>
        int GetNextStation(int lineId, int stationCode);

        /// <summary>
        /// get all the line that pss station code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        IEnumerable<int> GetStationLines(int code);

        /// <summary>
        /// add a new linestation
        /// </summary>
        /// <param name="lineStation"></param>
        /// <returns></returns>
        bool AddLineStation(LineStation lineStation);

        /// <summary>
        /// reomve all stations from a line
        /// </summary>
        /// <param name="lineID"></param>
        /// <returns></returns>
        bool RemoveAllLineStation(int lineID);

        /// <summary>
        /// get the line schedule of all lines
        /// </summary>
        /// <returns></returns>
        IEnumerable<LineTrip> GetLineSchedules();

        /// <summary>
        /// get the times that lineId starts
        /// </summary>
        /// <param name="lineId"></param>
        /// <returns></returns>
        IEnumerable<TimeSpan> GetLineSchedules(int lineId);
        #endregion

        #region AdjacentStations
        /// <summary>
        /// get the time and distance beween the 2 station
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        AdjacentStations GetAdjacentStations(int first, int second);

        /// <summary>
        /// add adjacentStations
        /// </summary>
        /// <param name="adjacentStations"></param>
        /// <returns></returns>
        bool AddAdjacentStations(AdjacentStations adjacentStations);

        /// <summary>
        /// update time and distance between 2 stations
        /// </summary>
        /// <param name="adjacentStations"></param>
        /// <returns></returns>
        bool UpdateAdjacentStations(AdjacentStations adjacentStations);

        /// <summary>
        /// get all adjacent stations
        /// </summary>
        /// <returns></returns>
        IEnumerable<AdjacentStations> GetAllAdjacentStations();

        #endregion

        #region Users
        IEnumerable<User> GetAllUsers();
        bool ValidatePassword(string userName, string password);
        bool RemoveLineTrip(int lineId, TimeSpan time);

        #endregion
    }
}