using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class BusOnTrip
    {
        #region Fields
        private int id;
        private int licenseNum;
        private int lineId;
        private TimeSpan plannedTakeOff;
        private TimeSpan actualTakeOff;
        private int prevStation;
        private TimeSpan prevStationAt;
        private TimeSpan nextStationAt;
        #endregion

        #region Properties
        public int Id
        {
            get { return id; }
            set { id = value; }
        }


        public int LicenseNum
        {
            get { return licenseNum; }
            set { licenseNum = value; }
        }


        public int LineId
        {
            get { return lineId; }
            set { lineId = value; }
        }


        public TimeSpan PlannedTakeOff
        {
            get { return plannedTakeOff; }
            set { plannedTakeOff = value; }
        }


        public TimeSpan ActualTakeOff
        {
            get { return actualTakeOff; }
            set { actualTakeOff = value; }
        }


        public int PrevStation
        {
            get { return prevStation; }
            set { prevStation = value; }
        }


        public TimeSpan PrevStationAt
        {
            get { return prevStationAt; }
            set { prevStationAt = value; }
        }


        public TimeSpan NextStationAt
        {
            get { return nextStationAt; }
            set { nextStationAt = value; }
        }
        #endregion

    }
}
