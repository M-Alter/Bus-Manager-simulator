using System;
using System.Collections.Generic;

namespace BO
{
    public class AdjacentStationsExceptions : Exception
    {
        public AdjacentStations[] adjacentStationsArray;
        public AdjacentStationsExceptions(string message, params AdjacentStations[] adjacentStations)
            : base(message)
        {
            adjacentStationsArray = new AdjacentStations[adjacentStations.Length];
            adjacentStationsArray = adjacentStations;
        }
    }
}
