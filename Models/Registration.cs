using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VisaApplicationSystem.Models
{
    public class Registration
    {
        public int registrationID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string gender { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public DateTime dob { get; set; }
        public int age { get; set; }
        public long phone { get; set; }

        public string address { get; set; }
        public string role { get; set; }
        public string userName { get; set; }
        public string passwordHash { get; set; }
        public string salt { get; set; }
        public bool isVerified { get; set; }
        public byte[] photo { get; set; }  
        public int adminID { get; set; }
        public RoleBase roleBase { get; set; }  



    }
}