﻿using System;
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
        public int PersonalId
        {
            get { return personalId; }
            set { personalId = value; }
        }
      
        public int LineNumber
        {
            get { return lineNumber; }
            set { lineNumber = value; }
        }

        public Enums.Areas Area
        {
            get { return areas; }
            set { areas = value; }
        }

        public int FirstStation
        {
            get { return firstStation; }
            set { firstStation = value; }
        }

        public string FirstStationName { get; set; }

        public int LastStation
        {
            get { return lastStation; }
            set { lastStation = value; }
        }

        public string LastStationName { get; set; }
        public IEnumerable<LineStation> Stations { get; set; }
        public IEnumerable<TimeSpan> Timing { get; set; }

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
