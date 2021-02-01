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
        /// get all the buses grouped by years
        /// </summary>
        /// <returns>a collection of buses</returns>
        IEnumerable<IGrouping<int, Bus>> GetAllBuses();

        /// <summary>
        /// get all the buses that conform to the predicate function
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<Bus> GetAllBusesThat(Predicate<Bus> predicate);

        /// <summary>
        /// add a new bus 
        /// </summary>
        /// <param name="bus">bus to add</param>
        /// <returns>true if the bus was added successfully</returns>
        bool AddBus(Bus bus);

        /// <summary>
        /// delete a bus
        /// </summary>
        /// <param name="licenseNum">lisence num of bus to remove</param>
        void DeleteBus(int licenseNum);

        #endregion
        /// <summary>
        /// gets a single station
        /// </summary>
        /// <param name="code">code number of the station</param>
        /// <returns>return the station that has the requested code</returns>
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

        /// <summary>
        /// get a single line
        /// </summary>
        /// <param name="id">lines personal id</param>
        /// <returns>a line with the personal id == id</returns>
        Line GetLine(int id);

        /// <summary>
        /// gets all the lines available
        /// </summary>
        /// <returns>a collection of all the lines</returns>
        IEnumerable<Line> GetAllLines();

        /// <summary>
        /// adds a new line 
        /// </summary>
        /// <param name="line">line to add</param>
        /// <returns>true if added successfully</returns>
        bool AddLine(Line line);

        /// <summary>
        /// add a new start time to a line
        /// </summary>
        /// <param name="lineId"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        bool AddLineTrip(int lineId, TimeSpan time);

        /// <summary>
        /// remove a line
        /// </summary>
        /// <param name="lineId"></param>
        /// <param name="lastStation"></param>
        /// <returns>true if removed successfully</returns>
        bool RemoveLine(int lineId, int lastStation);

        /// <summary>
        /// removes a station fron the line 
        /// </summary>
        /// <param name="lineId">line to be removed from</param>
        /// <param name="stationToRemove">station to remove</param>
        /// <returns>true if removed successfully</returns>
        bool RemoveStationFromLine(int lineId, int stationToRemove);

        /// <summary>
        /// adds a new adjacent stations
        /// </summary>
        /// <param name="adj">instatnce to add</param>
        /// <returns>true if added successfully</returns>
        bool AddAdjacentStations(AdjacentStations adj);

        /// <summary>
        /// get the details of the 2 adjacent stations
        /// </summary>
        /// <param name="first">first station</param>
        /// <param name="second">second station</param>
        /// <returns>all the details between these two stations</returns>
        AdjacentStations GetAdjacentStations(int first, int second);

        /// <summary>
        /// gets all the adjacent stations
        /// </summary>
        /// <returns>all the adjacent stations</returns>
        IEnumerable<AdjacentStations> GetAllAdjacentStations();

        /// <summary>
        /// updates the adjacent stations details
        /// </summary>
        /// <param name="adjacentStations"></param>
        /// <returns>true if updates successfully</returns>
        bool UpdateAdjacentStations(AdjacentStations adjacentStations);

        /// <summary>
        /// gets user names that are or aren't admins 
        /// </summary>
        /// <param name="admin">true if you want admin usernames of false for non admin usernames</param>
        /// <returns>a collection of usernames</returns>
        IEnumerable<string> GetAllUserNames(bool admin);

        /// <summary>
        /// check if the username and password match 
        /// </summary>
        /// <param name="userName">username to check</param>
        /// <param name="password">password to match</param>
        /// <returns>true if match was succsessfull</returns>
        bool ValidatePassword(string userName, string password);

        /// <summary>
        /// sends an email to emailAddress with a reminder of the password of the username account
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="emailAddress">email address to send the password to</param>
        void ResendPassword(string userName, string emailAddress);

        /// <summary>
        /// method to start the simulator
        /// </summary>
        /// <param name="startTime">time to start the simulator at</param>
        /// <param name="Rate">speed rate of the similator</param>
        /// <param name="updateTime">action to update the time in the GUI</param>
        void StartSimulator(TimeSpan startTime, int Rate, Action<TimeSpan> updateTime);
        
        /// <summary>
        /// method to stop the simulator
        /// </summary>
        void StopSimulator();
        
        /// <summary>
        /// a method to update the panel window
        /// </summary>
        /// <param name="station">what station the panel is reflecting</param>
        /// <param name="updateBus">what bus to update</param>
        void SetStationPanel(int station, Action<LineTiming> updateBus);

        /// <summary>
        /// to remove a line trip from a line
        /// </summary>
        /// <param name="personalId"></param>
        /// <param name="time"></param>
        bool RemoveLineTrip(int personalId, TimeSpan time);
    }
}
