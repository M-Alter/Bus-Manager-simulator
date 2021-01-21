using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace BL
{
    //with a lot of modifications 
    internal sealed class ClockSimulator
    {
        #region singelton
        static readonly ClockSimulator instance = new ClockSimulator();
        static ClockSimulator() { }// static ctor to ensure instance init is done just before first usage
        ClockSimulator() { } // default => private
        internal static ClockSimulator Instance { get => instance; }// The public Instance property to use
        #endregion

        internal class Clock // this class is build to avoid inter-thread issues reading/writing the timespan value
        {
            internal TimeSpan Time;
            internal Clock(TimeSpan timespan) => Time = timespan;
        }

        internal volatile bool Cancel;
        private volatile Clock simulatorClock = null;
        internal Clock SimulatorClock => simulatorClock;
        private volatile int simulatorRate;
        internal int Rate => simulatorRate;

        private Stopwatch stopwatch = new Stopwatch();
        private Action<TimeSpan> clockObserver = null;
        internal event Action<TimeSpan> ClockObserver
        {
            add => clockObserver = value;
            remove => clockObserver = null;
        }

        private TimeSpan simulatorStartTime;

        /// <summary>
        /// start the simukator
        /// </summary>
        /// <param name="startTime">what the time should be at start</param>
        /// <param name="rate">at ehat rate should the simulator</param>
        internal void Start(TimeSpan startTime, int rate)
        {
            //start time 
            simulatorStartTime = startTime;
            //cloack thats start in start time and updates according to the rate
            simulatorClock = new Clock(startTime);
            //rate of the clock
            simulatorRate = rate;
            //boolean when to stop
            Cancel = false;
            //start the timer that progreeses the simulatorClock
            stopwatch.Restart();
            //run the clock thread
            new Thread(clockThread).Start();
            //run the simulation thread
            TripSimulator.Instance.Start();
        }


        //stop the clock and simulation
        internal void Stop()
        {
            Cancel = true;
        }

        /// <summary>
        /// while the clock isn't paused update the time every 10ms 
        /// </summary>
        void clockThread()
        {
            while (!Cancel)
            {
                //update the internal clock for the simulator 
                simulatorClock = new Clock(simulatorStartTime + new TimeSpan(stopwatch.ElapsedTicks * simulatorRate));
                //update the GUI clock
                clockObserver(new TimeSpan(simulatorClock.Time.Hours, simulatorClock.Time.Minutes, simulatorClock.Time.Seconds));
                Thread.Sleep(100);
            }
            clockObserver = null;
        }
    }
}
