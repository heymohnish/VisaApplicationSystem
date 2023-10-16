using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VisaApplicationSystem.Models
{
    public class Password
    {
        public int registerID { get; set; }
        public string newPassword { get; set; }
        public string conformPassword { get; set; }
    }
}