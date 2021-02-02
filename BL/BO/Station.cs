using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
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
        /// <summary>
        /// code of the station
        /// </summary>
        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        /// <summary>
        /// name of the station
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// longitude of the station
        /// </summary>
        public double Longitude
        {
            get { return longitude; }
            set { longitude = value; }
        }

        /// <summary>
        /// lattitude of the station
        /// </summary>
        public double Lattitude
        {
            get { return lattitude; }
            set { lattitude = value; }
        }

        public override string ToString()
        {
            return string.Format($"{Name} {Code}");
        }
        public IEnumerable<StationLine> LinesAtStation { get; set; }

        #endregion
    }
}
