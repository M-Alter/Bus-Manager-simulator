using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Line
    {
        #region Fields
        private int personalId;
        private int lineNumber;
        private Enums.Areas areas;
        private int firstStation;
        private int lastStation;
        private bool isActive = true;       

        #endregion

        #region Properties
        /// <summary>
        /// personal id of the line
        /// </summary>
        public int PersonalId
        {
            get { return personalId; }
            set { personalId = value; }
        }
      
        /// <summary>
        /// display line number of the line
        /// </summary>
        public int LineNumber
        {
            get { return lineNumber; }
            set { lineNumber = value; }
        }

        /// <summary>
        /// area that the bus operates
        /// </summary>
        public Enums.Areas Area
        {
            get { return areas; }
            set { areas = value; }
        }

        /// <summary>
        /// first station number of the line
        /// </summary>
        public int FirstStation
        {
            get { return firstStation; }
            set { firstStation = value; }
        }

        /// <summary>
        /// name of the first station
        /// </summary>
        public string FirstStationName { get; set; }

        /// <summary>
        /// last station number
        /// </summary>
        public int LastStation
        {
            get { return lastStation; }
            set { lastStation = value; }
        }

        /// <summary>
        /// name of the last station
        /// </summary>
        public string LastStationName { get; set; }

        /// <summary>
        /// collection of the stations in this line
        /// </summary>
        public IEnumerable<LineStation> Stations { get; set; }

        /// <summary>
        /// collection of all the timing of the line
        /// </summary>
        public IEnumerable<TimeSpan> Timing { get; set; }

        /// <summary>
        /// false if inactive 
        /// </summary>
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }
        #endregion

        public override string ToString()
        {
            return string.Format($"{this.LineNumber}   {this.LastStation}  {this.LastStationName}");
        }
    }
}
