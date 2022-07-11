using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pesmarica.Database
{
    public class UserFactory
    {
        public User CreateUser(string userType)
        {
            userType = userType.ToLower();
            if (userType == "regular") { return new RegularUser(); }
            else if (userType == "administrator") { return new Administrator(); }
            else { return null; }
        }
    }
}
