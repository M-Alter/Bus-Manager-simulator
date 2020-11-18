using System;

namespace dotnet5781_02_2131_1146
{
    public class BusStopRoute
    {
        #region C'tor
        /// <summary>
        /// c'tor for BusStopRoute
        /// </summary>
        /// <param name="stop">the stop to represent</param>
        /// <param name="time">time from previous stop</param>
        /// <param name="distance">distance from previous stop</param>
        public BusStopRoute(BusStop stop, TimeSpan time, double distance)
        {
            Stop = stop;
            TravelTime = time;
            TravelDistance = distance;
        }
        #endregion

        #region Fields
        private BusStop Stop;
        #endregion

        #region Properties
        /// <summary>
        /// get the stop details of the BusStop
        /// </summary>
        public BusStop GetBusStop { get { return Stop; } }

        /// <summary>
        /// travel time since the previous stop
        /// </summary>
        public TimeSpan TravelTime { get; private set; }

        /// <summary>
        /// travel distance since the previous stop
        /// </summary>
        public double TravelDistance { get; private set; }

        /// <summary>
        /// get the BusStop number 
        /// </summary>
        public int GetNumber { get { return Stop.stopNumber; } }

        /// <summary>
        /// get the area of the BusStop
        /// </summary>
        public Areas GetArea { get { return Stop.Area; } }

        #endregion
    }
}
