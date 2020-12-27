using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BLAPI
{
    public interface IBL
    {
        Bus GetBus(int license);
        IEnumerable<Bus> GetAllBuses();
        IEnumerable<Station> GetAllStations();
        IEnumerable<Line> GetAllLines();
    }
}
