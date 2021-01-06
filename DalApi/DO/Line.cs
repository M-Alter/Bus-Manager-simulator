using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class Line
    {
        #region Fields
        private int personalId;
        private int lineNumber;
        private Enums.Areas areas;
        private int firstStation;
        private int lastStation;
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

        public int LastStation
        {
            get { return lastStation; }
            set { lastStation = value; }
        }
        #endregion
    }
}
