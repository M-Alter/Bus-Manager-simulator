using BO;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace BL
{
    internal sealed class TripSimulator
    {
        #region singelton
        static readonly TripSimulator instance = new TripSimulator();
        static TripSimulator() { }// static ctor to ensure instance init is done just before first usage
        TripSimulator() { } // default => private
        internal static TripSimulator Instance { get => instance; }// The public Instance property to use
        #endregion

        private static Random rand = new Random();
        private IDL dl = DLFactory.GetDL();

        /// <summary>
        /// station to monitor
        /// </summary>
        private int stationId = -1;
        internal int StationId { set => stationId = value; }

        private event Action<LineTiming> busObserver;
        internal event Action<LineTiming> BusObserver
        {
            add => busObserver = value;
            remove => busObserver = null;
        }

        internal void Initialize(IDL dl) { } // => this.dl = dl;

        /// <summary>
        /// 
        /// </summary>
        internal void Start() => new Thread(runBusTripsThread).Start();

        /// <summary>
        /// get the schudule for all the bus trips that will take place sort them and simulate them
        /// </summary>
        void runBusTripsThread()
        {
            //get all the trip that will take place
            List<DO.LineTrip> scheds = (from sc in dl.GetLineSchedules()
                                        orderby sc.StartAt
                                        select sc).ToList();
            //run while the simulation isn't cancelled
            while (!ClockSimulator.Instance.Cancel)
            {
                foreach (var sc in scheds)
                {
                    //check if cancelled
                    if (ClockSimulator.Instance.Cancel) break;
                    //get he current time
                    TimeSpan clock = ClockSimulator.Instance.SimulatorClock.Time;
                    //check if the bus has left already
                    if (clock > sc.StartAt) continue;
                    //wait until the next bus leaves
                    Thread.Sleep((int)((sc.StartAt - clock).TotalMilliseconds / ClockSimulator.Instance.Rate));
                    //check if cancelled
                    if (ClockSimulator.Instance.Cancel) break;
                    //run the trip in a new thread 
                    new Thread(busTripThread).Start(sc);
                }
                //wait a second between each scan
                Thread.Sleep(1000);
            }
        }


        void busTripThread(object lineSchedule)
        {
            DO.LineTrip sc = (DO.LineTrip)lineSchedule;
            //set the station to the current station
            int station = stationId;
            //get the details of the trip
            LineTiming lineTiming = new LineTiming
            {
                LineId = sc.LineId,
                LineNumber = dl.GetLine(sc.LineId).LineNumber,
                Timing = sc.StartAt
            };
            //name the thread so we can track it
            Thread.CurrentThread.Name = $"{lineTiming.ID}:{lineTiming.LineId}/{lineTiming.LineNumber}";
            //get the list of station the line passes through
            var stationIDs = dl.GetLineStations(lineTiming.LineId).ToList();
            //if there isnt any stations return
            if (stationIDs.Count == 0) return;
            //put all the station IDs in a list a lineStation
            var route = (from st in stationIDs
                         select new LineStation { Station = st }).ToList();
            //add the name to each station
            route.ForEach(ls => ls.StationName = dl.GetStation(ls.Station).Name);
            //get the time between the statoins 
            for (int i = 1; i < route.Count; ++i)
                route[i].TimeToNext = dl.GetAdjacentStations(route[i - 1].Station, route[i].Station).Time;
            //get the name of the destination
            lineTiming.LastStation = route[route.Count - 1].StationName;


            //run through the route
            for (int i = 0; i < route.Count; ++i)
            {
                //if the observing station was changed
                if (station != stationId)
                {
                    lineTiming.Timing = TimeSpan.Zero;
                    busObserver(lineTiming); // Reached the station...

                    lineTiming = new LineTiming
                    {
                        ID = lineTiming.ID,
                        LineId = lineTiming.LineId,
                        LineNumber = lineTiming.LineNumber,
                        LastStation = lineTiming.LastStation,
                        Timing = lineTiming.Timing
                    };
                    //set the station to the new station
                    station = stationId;
                }
                if (ClockSimulator.Instance.Cancel) break;
                if (stationId == route[i].Station)
                { // changed the monitored station
                    lineTiming.Timing = TimeSpan.Zero;
                    busObserver(lineTiming); // Reached the station...
                }
                TimeSpan total = TimeSpan.Zero;
                for (int j = i + 1; j < route.Count; ++j)
                {
                    total += (TimeSpan)route[j].TimeToNext;
                    //if a new station that is along the route is chosen 
                    if (stationId == route[j].Station)
                    {
                        lineTiming.Timing = total;
                        busObserver(lineTiming);
                        break;
                    }
                }
                if (i + 1 < route.Count)
                    //sleep until hte bus reaches the next stop (between 0.90% to 1.40% of the journey time)
                    //======================================================================================================================================================================
                    Thread.Sleep((int)(((TimeSpan)route[i + 1].TimeToNext).TotalMilliseconds * (0.9 + rand.NextDouble() / 2) / ClockSimulator.Instance.Rate));
                //===========================================================================================================================================================================
            }
        }

    }
}
