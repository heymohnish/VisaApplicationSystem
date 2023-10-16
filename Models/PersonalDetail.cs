using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VisaApplicationSystem.Models
{
    public class PersonalDetail
    {
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
    }


}