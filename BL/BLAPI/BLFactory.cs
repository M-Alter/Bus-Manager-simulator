using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLAPI
{
    /// <summary>
    /// static factory class that returns an instance of IBL
    /// </summary>
    public static class BLFactory
    {
        public static IBL GetIBL()
        {
            return BLImp.Instance;
        }
    }
}
