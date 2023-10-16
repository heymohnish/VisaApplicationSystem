using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VisaApplicationSystem.Models
{
    public class Contact
    {
        public int contactID { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string message { get; set; }
    }

}