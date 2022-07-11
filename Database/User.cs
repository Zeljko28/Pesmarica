using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pesmarica.Database
{
    public abstract class User
    {
        public abstract string GetUserType();
        public abstract string GetUsername();
        public abstract string GetPassword();

        public abstract void SetUserType(string userType);
        public abstract void SetUsername(string username);
        public abstract void SetPassword(string password);
    }
}
