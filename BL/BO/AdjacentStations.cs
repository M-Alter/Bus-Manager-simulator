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
        public int Station1
        {
            get { return station1; }
            set { station1 = value; }
        }

        public int Station2
        {
            get { return station2; }
            set { station2 = value; }
        }

        public double Distance
        {
            get { return distance; }
            set { distance = value; }
        }

        public TimeSpan Time
        {
            get { return time; }
            set { time = value; }
        }
        #endregion
    }
}
