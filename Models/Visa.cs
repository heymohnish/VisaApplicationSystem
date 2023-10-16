using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VisaApplicationSystem.Models
{
    public class Visa
    {
        public int visaID { get; set; }
        public string visaName { get; set; }
        public string visaTitle { get; set; }
        public string visaDescription { get; set; }
        public int adminID { get; set; }

        public bool personalInformation { get; set; }
        public bool PAN { get; set; }
        public bool aadhar { get; set; }
        public bool governmentProof { get; set; }
        public bool photo { get; set; }
        public bool passport { get; set; }
        public bool employeeProof { get; set; }
        public bool educationProof { get; set; }
        public bool bankProof { get; set; }
        public bool toeflCertification { get; set; }
        public bool visitorProof { get; set; }
        public bool isPersonalInformation { get; set; }



        public BaseDetail baseDetail { get; set; }
    }

}