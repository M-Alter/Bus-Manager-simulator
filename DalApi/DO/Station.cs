using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class Station
    {
        #region Fields
        private int code;
        private string name;
        private double longitude;
        private double lattitude;
        #endregion

        #region Properties
        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public double Longitude
        {
            get { return longitude; }
            set { longitude = value; }
        }

        public double Lattitude
        {
            get { return lattitude; }
            set { lattitude = value; }
        }

        #endregion

    }
}
