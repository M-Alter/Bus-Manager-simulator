using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class AdjacentStations
    {
        #region Fields
        private int station1;
        private int station2;
        private double distance;
        private TimeSpan time;
        #endregion

        #region Properties
        /// <summary>
        /// first station in the pair
        /// </summary>
        public int Station1
        {
            get { return station1; }
            set { station1 = value; }
        }

        /// <summary>
        /// name of the first station
        /// </summary>
        public string Station1Name { get; set; }

        /// <summary>
        /// name of the second station
        /// </summary>
        public string Station2Name { get; set; }

        /// <summary>
        /// second station
        /// </summary>
        public int Station2
        {
            get { return station2; }
            set { station2 = value; }
        }

        /// <summary>
        /// distance to the next station
        /// </summary>
        public double Distance
        {
            get { return distance; }
            set { distance = value; }
        }

        /// <summary>
        /// time to the next station
        /// </summary>
        public TimeSpan Time
        {
            get { return time; }
            set { time = value; }
        }
        #endregion
    }
}
