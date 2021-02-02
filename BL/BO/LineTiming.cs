using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class LineTiming
    {       
        /// <summary>
        /// internal counter for identity reasons
        /// </summary>
        private static int counter = 0;

        /// <summary>
        /// id of the lineTiming
        /// </summary>
        public int ID;

        /// <summary>
        /// c'tor that gives the ID a value
        /// </summary>
        public LineTiming() => ID = ++counter;

        
        //public TimeSpan TripStart { get; set; }

        /// <summary>
        /// id of the line
        /// </summary>
        public int LineId { get; set; }       
        
        /// <summary>
        /// display number of the line
        /// </summary>
        public int LineNumber { get; set; }     
        
        /// <summary>
        /// final station of the line
        /// </summary>
        public string LastStation { get; set; }

        /// <summary>
        /// time to start the trip
        /// </summary>
        public TimeSpan Timing { get; set; }
    }
}
