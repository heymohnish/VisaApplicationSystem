﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using VisaApplicationSystem.Models;
using static System.Net.Mime.MediaTypeNames;

namespace VisaApplicationSystem.Repository
{
    public class VCORepository : BaseDatabaseConnection
    {
        public List<ApplicationForm> GetSubmitedApplication()
        {
            List<ApplicationForm> applications = new List<ApplicationForm>();

            try
            {
                InitializeConnection();
                connection.Open();


                using (SqlCommand command = new SqlCommand("sp_GetSubmitedApplication", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ApplicationForm application = new ApplicationForm
                            {
                                applicationID = Convert.ToInt32(reader["ApplicationID"]),
                                visaName = reader["VisaName"].ToString(),
                                visaTitle = reader["VisaTitle"].ToString(),
                                visaId = (int)reader["VisaID"],
                                registrationID = (int)reader["RegistrationID"],
                                applicantName= reader["FullName"].ToString(),
                                //download
                            };
                            applications.Add(application);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions here
            }
            finally
            {
                connection.Close();
            }

            return applications;
        }
        public List<ApplicationForm> GetAllApplication()
        {
            List<ApplicationForm> applications = new List<ApplicationForm>();

            try
            {
                InitializeConnection();
                connection.Open();

                // Define your SQL query

                using (SqlCommand command = new SqlCommand("sp_GetSubmitedApplication", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ApplicationForm application = new ApplicationForm
                            {
                                applicationID = Convert.ToInt32(reader["ApplicationID"]),
                                visaName = reader["VisaName"].ToString(),
                                visaTitle = reader["VisaTitle"].ToString(),
                                visaId = (int)reader["VisaID"],
                                visaDiscription =reader["VisaDescription"].ToString(),
                                registrationID = (int)reader["RegistrationID"],
                            };
                            applications.Add(application);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions here
            }
            finally
            {
                connection.Close();
            }

            return applications;
        }
        public ApplicationPayload GetApplicationForm(int id)
        {
            ApplicationPayload application = null;
            try
            {
                InitializeConnection();
                connection.Open();

                using (SqlCommand command = new SqlCommand("sp_GetAllApplicationById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ApplicationID", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            application = new ApplicationPayload
                            {
                                applicationID = reader.GetInt32(reader.GetOrdinal("ApplicationID")),
                                visaName = reader.GetString(reader.GetOrdinal("VisaName")),
                                visaTitle = reader.GetString(reader.GetOrdinal("VisaTitle")),
                                visaDiscription = reader.GetString(reader.GetOrdinal("VisaDescription")),
                                visaId = reader.GetInt32(reader.GetOrdinal("VisaID")),
                                registrationID = reader.GetInt32(reader.GetOrdinal("RegistrationID")),
                                fullName = reader.GetString(reader.GetOrdinal("FullName")),
                                dateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                                nationality = reader.GetString(reader.GetOrdinal("Nationality")),
                                gender = reader.GetString(reader.GetOrdinal("Gender")),
                                passportNumber = reader.GetString(reader.GetOrdinal("PassportNumber")),
                                passportExpiryDate = reader.GetDateTime(reader.GetOrdinal("PassportExpiryDate")),
                                phoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber")),
                                email = reader.GetString(reader.GetOrdinal("Email")),
                                residentialAddress = reader.GetString(reader.GetOrdinal("ResidentialAddress")),
                                purposeOfTravel = reader.GetString(reader.GetOrdinal("PurposeOfTravel")),
                                departureDate = reader.GetDateTime(reader.GetOrdinal("DepartureDate")),
                                returnDate = reader.GetDateTime(reader.GetOrdinal("ReturnDate")),
                                photo = reader["Photo"] != DBNull.Value ? (byte[])reader["Photo"] : null,
                                PAN = reader["PAN"] != DBNull.Value ? (byte[])reader["PAN"] : null,
                                aadhar = reader["Aadhar"] != DBNull.Value ? (byte[])reader["Aadhar"] : null,
                                govenmentProof = reader["GovernmentProof"] != DBNull.Value ? (byte[])reader["GovernmentProof"] : null,
                                passport = reader["Passport"] != DBNull.Value ? (byte[])reader["Passport"] : null,
                                employeeProof = reader["EmployeeProof"] != DBNull.Value ? (byte[])reader["EmployeeProof"] : null,
                                educationProof = reader["EducationProof"] != DBNull.Value ? (byte[])reader["EducationProof"] : null,
                                bankProof = reader["BankProof"] != DBNull.Value ? (byte[])reader["BankProof"] : null,
                                toeflCertification = reader["ToeflCertification"] != DBNull.Value ? (byte[])reader["ToeflCertification"] : null,
                                visitorProof = reader["VisitorProof"] != DBNull.Value ? (byte[])reader["VisitorProof"] : null,

                                isPersonalInformation = reader.GetBoolean(reader.GetOrdinal("IsPersonalInformation")),
                                isPhoto = reader.GetBoolean(reader.GetOrdinal("IsPhoto")),
                                isPAN = reader.GetBoolean(reader.GetOrdinal("IsPAN")),
                                isAadhar = reader.GetBoolean(reader.GetOrdinal("IsAadhar")),
                                isGovenmentProof = reader.GetBoolean(reader.GetOrdinal("IsGovernmentProof")),
                                isPassport = reader.GetBoolean(reader.GetOrdinal("IsPassport")),
                                isEmployeeProof = reader.GetBoolean(reader.GetOrdinal("IsEmployeeProof")),
                                isEducationProof = reader.GetBoolean(reader.GetOrdinal("IsEducationProof")),
                                isBankProof = reader.GetBoolean(reader.GetOrdinal("IsBankProof")),
                                isToeflCertification = reader.GetBoolean(reader.GetOrdinal("IsToeflCertification")),
                                isVisitorProof = reader.GetBoolean(reader.GetOrdinal("IsVisitorProof")),
                                statusCast = reader.GetInt32(reader.GetOrdinal("Status")),

                                /*messageVCO = reader.GetString(reader.GetOrdinal("MessageVCO")),
                                messageUser = reader.GetString(reader.GetOrdinal("MessageUser"))*/

                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions here
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                    connection.Close();
            }
            application.status = (Status)application.statusCast;
            Console.WriteLine(application.statusCast);

            return application;
        }

        public void UpdateStatus(ApplicationPayload application)
        {
            try
            {
                InitializeConnection();
                connection.Open();

                using (SqlCommand command = new SqlCommand("sp_MessageUserAnsStatus", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ApplicationID", application.applicationID);
                    command.Parameters.AddWithValue("@Status", (int)application.status);
                    command.Parameters.AddWithValue("@MessageUser", application.messageUser);
                    command.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }

    }
}