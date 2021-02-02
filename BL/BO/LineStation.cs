using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class LineStation
    {
        /// <summary>
        /// index in the line
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// station number
        /// </summary>
        public int Station { get; set; }
        /// <summary>
        /// display name of the station
        /// </summary>
        public string StationName { get; set; }
        /// <summary>
        /// time to the next station 
        /// </summary>
        public TimeSpan? TimeToNext { get; set; }
        /// <summary>
        /// distance to the next station
        /// </summary>
        public double? Distance { get; set; }
    }
}
