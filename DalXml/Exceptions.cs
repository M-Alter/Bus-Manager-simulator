using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class FileLoadingException : Exception
    {
        string filePath;
        string additionalMessage;

        /// <summary>
        /// exception for file loading problems
        /// </summary>
        /// <param name="message">message recieved from previous exception</param>
        /// <param name="filePath">path of file error</param>
        /// <param name="additionalMessage">optional string for additional info</param>
        FileLoadingException(string message, string filePath, string additionalMessage = "") : base(message)
        {
            this.additionalMessage = additionalMessage;
            this.filePath = filePath;
        }


    }


    //public class BusException:Exception
    //{
    //    int licenseNum;
    //    public BusException(int licenseNum, string message): base (message)
    //    {
    //        this.licenseNum = licenseNum;
    //    }

    //    public override string ToString()
    //    {
    //        return licenseNum + ": " + Message;
    //    }
    //}


    
}
