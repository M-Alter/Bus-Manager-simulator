using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class User
    {
        #region Fields
        private string userName;
        private string password;
        #endregion

        #region Properties
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public bool Admin { get; set; }
        #endregion

    }
}
