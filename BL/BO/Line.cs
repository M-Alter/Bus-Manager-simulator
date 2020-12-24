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
        private int id;
        private int code;
        private Enums.Areas areas;
        private int firstStation;
        private int lastStation;
        #endregion

        #region Properties
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
      
        public int Code
        {
            get { return code; }
            set { code = value; }
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
