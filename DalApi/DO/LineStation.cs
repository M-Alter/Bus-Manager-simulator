using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class LineStation
    {
        #region Fields
        private int lineID;
        private int stationCode;
        private int lineStationIndex;
        private int prevStation;
        private int nextStation;
        #endregion

        #region Properties
        public int LineId
        {
            get { return lineID; }
            set { lineID = value; }
        }

        public int StationCode
        {
            get { return stationCode; }
            set { stationCode = value; }
        }

        public int LineStationIndex
        {
            get { return lineStationIndex; }
            set { lineStationIndex = value; }
        }

        public int PrevStation
        {
            get { return prevStation; }
            set { prevStation = value; }
        }

        public int NextStation
        {
            get { return nextStation; }
            set { nextStation = value; }
        }
        #endregion

    }
}
