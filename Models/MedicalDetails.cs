using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VisaApplicationSystem.Models
{
    public class MedicalDetails
    {
       
        public string bloodType { get; set; }
        public string medicalCondition { get; set; }
        public string allergies { get; set; }
        public string medications { get; set; }
        public string emergencyContactName { get; set; }
        public string emergencyContactNumber { get; set; }
        public bool isOrganDonor { get; set; }
    }

}