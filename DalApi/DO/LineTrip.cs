using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class LineTrip
    {
        #region Fields
        private int id;
        private int lineId;
        private TimeSpan startAt;
        private TimeSpan frequency;
        private TimeSpan finishAt;
        #endregion

        #region Properties
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int LineId
        {
            get { return lineId; }
            set { lineId = value; }
        }

        public TimeSpan StartAt
        {
            get { return startAt; }
            set { startAt = value; }
        }

        public TimeSpan Frequency
        {
            get { return frequency; }
            set { frequency = value; }
        }

        public TimeSpan FinishAt
        {
            get { return finishAt; }
            set { finishAt = value; }
        }
        #endregion
    }
}
