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
        private int longitude;
        private int lattitude;
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

        public int Longitude
        {
            get { return longitude; }
            set { longitude = value; }
        }

        public int Lattitude
        {
            get { return lattitude; }
            set { lattitude = value; }
        }

        //public IEnumerable<Line> LinesAtStation { get; set; }

        #endregion
    }
}
