using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet5781_02_2131_1146
{
    /// <summary>
    /// Bus Exception ELIEZER
    /// </summary>
    public class BusException : Exception
    {
        public BusException(string info) : base(info) { }

        public override string Message => base.Message;
    }

    
}
