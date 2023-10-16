using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VisaApplicationSystem.Models
{
    public class ApplicationDB
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
       

        public ApplicationDB(ApplicationForm model)
        {
            this.applicationID = applicationID;
            this.visaName = model.visaName;
            this.visaTitle = model.visaTitle;
            this.visaDiscription = model.visaDiscription;
            this.visaId = model.visaId;
            this.registrationID = model.registrationID;
            if (model.personalInformation != null)
            {
                if (model.personalInformation.fullName != null)
                {
                    this.fullName = model.personalInformation.fullName;
                }

                if (model.personalInformation.dateOfBirth != null)
                {
                    this.dateOfBirth = model.personalInformation.dateOfBirth;
                }

                if (model.personalInformation.nationality != null)
                {
                    this.nationality = model.personalInformation.nationality;
                }

                if (model.personalInformation.gender != null)
                {
                    this.gender = model.personalInformation.gender;
                }

                if (model.personalInformation.passportNumber != null)
                {
                    this.passportNumber = model.personalInformation.passportNumber;
                }

                if (model.personalInformation.passportExpiryDate != null)
                {
                    this.passportExpiryDate = model.personalInformation.passportExpiryDate;
                }

                if (model.personalInformation.phoneNumber != null)
                {
                    this.phoneNumber = model.personalInformation.phoneNumber;
                }

                if (model.personalInformation.email != null)
                {
                    this.email = model.personalInformation.email;
                }

                if (model.personalInformation.residentialAddress != null)
                {
                    this.residentialAddress = model.personalInformation.residentialAddress;
                }

                if (model.personalInformation.purposeOfTravel != null)
                {
                    this.purposeOfTravel = model.personalInformation.purposeOfTravel;
                }

                if (model.personalInformation.departureDate != null)
                {
                    this.departureDate = model.personalInformation.departureDate;
                }

                if (model.personalInformation.returnDate != null)
                {
                    this.returnDate = model.personalInformation.returnDate;
                }
            }

            if (model.photo != null)
            {
                this.photo = model.photo;
            }

            if (model.PAN != null)
            {
                this.PAN = model.PAN;
            }


            if (model.aadhar != null)
            {
                this.aadhar = model.aadhar;
            }

            if (model.govenmentProof != null)
            {
                this.govenmentProof = model.govenmentProof;
            }

            if (model.passport != null)
            {
                this.passport = model.passport;
            }

            if (model.employeeProof != null)
            {
                this.employeeProof = model.employeeProof;
            }

            if (model.educationProof != null)
            {
                this.educationProof = model.educationProof;
            }

            if (model.bankProof != null)
            {
                this.bankProof = model.bankProof;
            }

            if (model.toeflCertification != null)
            {
                this.toeflCertification = model.toeflCertification;
            }

            if (model.visitorProof != null)
            {
                this.visitorProof = model.visitorProof;
            }

            if (model.isPersonalInformation != null)
            {
                this.isPersonalInformation = model.isPersonalInformation;
            }

            if (model.isPhoto != null)
            {
                this.isPhoto = model.isPhoto;
            }

            if (model.isPAN != null)
            {
                this.isPAN = model.isPAN;
            }

            if (model.isAadhar != null)
            {
                this.isAadhar = model.isAadhar;
            }

            if (model.isGovenmentProof != null)
            {
                this.isGovenmentProof = model.isGovenmentProof;
            }

            if (model.isPassport != null)
            {
                this.isPassport = model.isPassport;
            }

            // Continue with the rest of the properties in a similar manner

            if (model.isEmployeeProof != null)
            {
                this.isEmployeeProof = model.isEmployeeProof;
            }

            if (model.isEducationProof != null)
            {
                this.isEducationProof = model.isEducationProof;
            }

            if (model.isBankProof != null)
            {
                this.isBankProof = model.isBankProof;
            }

            if (model.isToeflCertification != null)
            {
                this.isToeflCertification = model.isToeflCertification;
            }

            if (model.isVisitorProof != null)
            {
                this.isVisitorProof = model.isVisitorProof;
            }

            if (model.status != null)
            {
                this.status = model.status;
            }

            if (model.messageVCO != null)
            {
                this.messageVCO = model.messageVCO;
            }

            if (model.messageUser != null)
            {
                this.messageUser = model.messageUser;
            }

            // Continue with any other properties that need to be assigned

        }
    }
}