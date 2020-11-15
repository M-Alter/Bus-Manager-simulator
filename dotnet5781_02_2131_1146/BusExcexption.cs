using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet5781_02_2131_1146
{
    class BusExcexption : Exception
    {
        public BusExcexption(string info) : base(info) { };

        public override string Message => base.Message;
    }

    
}
