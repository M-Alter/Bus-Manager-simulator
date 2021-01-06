using System;
using System.Collections.Generic;

namespace BO
{
    public class AdjacentStationsExceptions : Exception
    {
        public List<AdjacentStations> adjacentStations = new List<AdjacentStations>();
        public AdjacentStationsExceptions(string message, params AdjacentStations[] adjacentStations)
            : base(message)
        {
            foreach (var item in adjacentStations)
            {
                this.adjacentStations.Add(item);
            }
        }
    }
}
