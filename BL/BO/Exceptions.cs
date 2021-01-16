using System;
using System.Collections.Generic;

namespace BO
{
    /// <summary>
    /// exception for adding missing information in 2 adjacent stations
    /// </summary>
    public class AdjacentStationsExceptions : Exception
    {
        public AdjacentStations[] adjacentStationsArray;

        /// <summary>
        /// c'tor for the custom exception
        /// </summary>
        /// <param name="message">exception message</param>
        /// <param name="adjacentStations">array of all the adjacent station that are new in this line</param>
        public AdjacentStationsExceptions(string message, params AdjacentStations[] adjacentStations)
            : base(message)
        {
            adjacentStationsArray = new AdjacentStations[adjacentStations.Length];
            adjacentStationsArray = adjacentStations;
        }
    }

    /// <summary>
    /// exception for bus licence problems
    /// </summary>
    public class BusException: Exception
    {
        int licenseNum;
        public BusException(int licenseNum, string message) : base(message)
        {
            this.licenseNum = licenseNum;
        }
    }

    /// <summary>
    /// exception for exceptions of stations
    /// </summary>
    public class StationException:Exception
    {
        int stationId;
        public StationException(int stationId, string message) : base(message)
        {
            this.stationId = stationId;
        }
    }

    /// <summary>
    /// class to handle exceptions that relate to lines
    /// </summary>
    public class LineException : Exception
    {
        int lineNumber;
        /// <summary>
        /// accepts the lineNumber and the message
        /// </summary>
        /// <param name="lineNumber">line number of the bad line</param>
        /// <param name="message">message to the user</param>
        public LineException(int lineNumber, string message) : base(message)
        {
            this.lineNumber = lineNumber;
        }
    }
}
