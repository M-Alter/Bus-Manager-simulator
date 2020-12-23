using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    class Trip
    {
        #region Fields
        private int id;
        private string userName;
        private int lineId;
        private int inStation;
        private TimeSpan inAt;
        private int outStation;
        private TimeSpan outAt;
        #endregion

        #region Properties
        public int Id
        {
            get { return id; }
            set { id = value; }
        }


        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }


        public int LineId
        {
            get { return lineId; }
            set { lineId = value; }
        }


        public int InStation
        {
            get { return inStation; }
            set { inStation = value; }
        }


        public TimeSpan InAt
        {
            get { return inAt; }
            set { inAt = value; }
        }


        public int OutStation
        {
            get { return outStation; }
            set { outStation = value; }
        }


        public TimeSpan OutAt
        {
            get { return outAt; }
            set { outAt = value; }
        }
        #endregion

    }
}
