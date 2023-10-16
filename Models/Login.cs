using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VisaApplicationSystem.Models
{
    public class Login
    {
        public int id { get; set; }   
        public string userName { get; set; }
        public string password { get; set; }
        public string message { get; set; }
    }
}