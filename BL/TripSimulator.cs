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

        private int stationId = -1;
        internal int StationId { set => stationId = value; }

        private event Action<LineTiming> busObserver;
        internal event Action<LineTiming> BusObserver
        {
            add => busObserver = value;
            remove => busObserver = null;
        }

        internal void Initialize(IDL dl) { } // => this.dl = dl;

        internal void Start() => new Thread(runBusTripsThread).Start();

        void runBusTripsThread()
        {
            List<DO.LineTrip> scheds = (from sc in dl.GetLineSchedules()
                                        orderby sc.StartAt
                                        select sc).ToList();
            while (!ClockSimulator.Instance.Cancel)
            {
                foreach (var sc in scheds)
                {
                    if (ClockSimulator.Instance.Cancel) break;
                    TimeSpan clock = ClockSimulator.Instance.SimulatorClock.Time;
                    if (clock > sc.StartAt) continue;
                    Thread.Sleep((int)((sc.StartAt - clock).TotalMilliseconds / ClockSimulator.Instance.Rate));
                    if (ClockSimulator.Instance.Cancel) break;
                    new Thread(busTripThread).Start(sc);
                }
                Thread.Sleep(1000); // protect CPU
            }
        }

        void busTripThread(object lineSchedule)
        {
            DO.LineTrip sc = (DO.LineTrip)lineSchedule;
            int station = stationId;
            LineTiming lineTiming = new LineTiming
            {
                LineId = sc.LineId,
                LineNumber = dl.GetLine(sc.LineId).LineNumber,
                TripStart = sc.StartAt                
            };
            Thread.CurrentThread.Name = $"{lineTiming.ID}:{lineTiming.LineId}/{lineTiming.LineNumber}";
            var stationIDs = dl.GetLineStations(lineTiming.LineId).ToList();
            if (stationIDs.Count == 0) return;
            var route = (from st in stationIDs
                         select new LineStation { Station = st }).ToList();

            route.ForEach(ls => ls.StationName = dl.GetStation(ls.Station).Name);
            for (int i = 1; i < route.Count; ++i)
                route[i].TimeToNext = dl.GetAdjacentStations(route[i - 1].Station, route[i].Station).Time;
            lineTiming.LastStation = route[route.Count - 1].StationName;

            for (int i = 0; i < route.Count; ++i)
            {
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
                        TripStart = lineTiming.TripStart
                    };
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
                    if (stationId == route[j].Station)
                    {
                        lineTiming.Timing = total;
                        busObserver(lineTiming);
                        break;
                    }
                }
                if (i + 1 < route.Count)
                    //======================================================================================================================================================================
                    Thread.Sleep((int)(((TimeSpan)route[i + 1].TimeToNext).TotalMilliseconds * (0.9 + rand.NextDouble() / 2) / ClockSimulator.Instance.Rate));
                //===========================================================================================================================================================================
            }
        }

    }
}
