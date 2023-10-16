using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VisaApplicationSystem.Models
{
    public class EmployeeDetails
    {
        public decimal salary { get; set; }
        public string department { get; set; }
        public string position { get; set; }
        public string supervisor { get; set; }
        public bool isActive { get; set; }
    }

}