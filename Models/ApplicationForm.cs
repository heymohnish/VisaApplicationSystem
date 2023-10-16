using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VisaApplicationSystem.Models
{
    public class ApplicationForm
    {
        public int applicationID { get; set; }
        public string visaName { get; set; }
        public string visaTitle { get; set; }
        public string visaDiscription { get; set; }
        public int visaId { get; set; }

        public int registrationID { get; set; }
        /* public string visaName { get; set; }*/
        public byte[] photo { get; set; }
        public PersonalDetail personalInformation { get; set; }

        public byte[] PAN { get; set; }
        public byte[] aadhar { get; set; }
        public byte[] govenmentProof { get; set; }
        public byte[] passport { get; set; }
        public byte[] employeeProof { get; set; }
        public byte[] educationProof { get; set; }
        public byte[] bankProof { get; set; }
        public byte[] toeflCertification { get; set; }
        public byte[] visitorProof { get; set; }
        public bool isPersonalInformation { get; set; }
        public bool isPhoto { get; set; }
        public bool isPAN { get; set; }
        public bool isAadhar { get; set; }
        public bool isGovenmentProof { get; set; }
        public bool isPassport { get; set; }
        public bool isEmployeeProof { get; set; }
        public bool isEducationProof { get; set; }
        public bool isBankProof { get; set; }
        public bool isToeflCertification { get; set; }
        public bool isVisitorProof { get; set; }
        public Status status { get; set; }
        public string messageVCO { get; set; }
        public string messageUser { get; set; }
        public string applicantName { get; set; }
    }
}