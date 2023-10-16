using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VisaApplicationSystem.Models
{
    public class ApplicationPayload
    {
        public int applicationID { get; set; }
        public string visaName { get; set; }
        public string visaTitle { get; set; }
        public string visaDiscription { get; set; }
        public int visaId { get; set; }

        public int registrationID { get; set; }
        //personal
        public string fullName { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string nationality { get; set; }
        public string gender { get; set; }
        public string passportNumber { get; set; }
        public DateTime passportExpiryDate { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string residentialAddress { get; set; }
        public string purposeOfTravel { get; set; }
        public DateTime departureDate { get; set; }
        public DateTime returnDate { get; set; }
        //supportdocument
        public byte[] photo { get; set; }
        public byte[] PAN { get; set; }
        public byte[] aadhar { get; set; }
        public byte[] govenmentProof { get; set; }
        public byte[] passport { get; set; }
        public byte[] employeeProof { get; set; }
        public byte[] educationProof { get; set; }
        public byte[] bankProof { get; set; }
        public byte[] toeflCertification { get; set; }
        public byte[] visitorProof { get; set; }
        //sdBool
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
        //other
        public Status status { get; set; }
        public string messageVCO { get; set; }
        public string messageUser { get; set; }
        public int statusCast { get; set; }

        public string returnd { get; set; }
        public string departure { get; set; }
        public string passExpire { get; set; }
        public string dobstring { get; set; }

    }
}