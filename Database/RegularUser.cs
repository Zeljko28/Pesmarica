using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pesmarica.Database
{
    public class RegularUser : User
    {

        public int regularUserId { get; set; }
        public string name { get; set; }
        public string lastname { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string userType { get; set; }


        public override string GetUserType() { return userType; }
        public override string GetUsername() { return username; }
        public override string GetPassword() { return password; }

        public override void SetUserType(string userType) { this.userType = userType; }
        public override void SetUsername(string username) { this.username = username; }
        public override void SetPassword(string password) { this.password = password; }
    }
}
