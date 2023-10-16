using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using VisaApplicationSystem.Models;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics.Contracts;
using System.Web.UI.WebControls;

namespace VisaApplicationSystem.Repository
{
    public class AdminRepository : BaseDatabaseConnection
    {
        public bool InsertVisaType(Visa visa)
        {
            InitializeConnection();
            bool ret = false;
            try
            {
                SqlCommand command = new SqlCommand("sp_InsertVisaType", connection);
                command.CommandType = CommandType.StoredProcedure;

                // Set the parameters for the stored procedure using the properties of the Visa object
                command.Parameters.AddWithValue("@VisaName", visa.visaName);
                command.Parameters.AddWithValue("@VisaTitle", visa.visaTitle);
                command.Parameters.AddWithValue("@VisaDescription", visa.visaDescription);
                command.Parameters.AddWithValue("@AdminID", visa.adminID);
                command.Parameters.AddWithValue("@PersonalInformation", visa.personalInformation ? 1 : 0);
                command.Parameters.AddWithValue("@PAN", visa.PAN ? 1 : 0);
                command.Parameters.AddWithValue("@Aadhar", visa.aadhar ? 1 : 0);
                command.Parameters.AddWithValue("@GovenmentProof", visa.governmentProof ? 1 : 0);
                command.Parameters.AddWithValue("@Photo", visa.photo ? 1 : 0);
                command.Parameters.AddWithValue("@Passport", visa.passport ? 1 : 0);
                command.Parameters.AddWithValue("@EmployeeProof", visa.employeeProof ? 1 : 0);
                command.Parameters.AddWithValue("@EducationProof", visa.educationProof ? 1 : 0);
                command.Parameters.AddWithValue("@BankProof", visa.bankProof ? 1 : 0);
                command.Parameters.AddWithValue("@ToeflCertification", visa.toeflCertification ? 1 : 0);
                command.Parameters.AddWithValue("@VisitorProof", visa.visitorProof ? 1 : 0);

                connection.Open();
                command.ExecuteNonQuery();
                ret = true; // Set ret to true if the stored procedure execution succeeds
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return ret;
        }

        public List<Visa> GetAllVisaTypes()
        {
            List<Visa> visaList = new List<Visa>();

            try
            {
                InitializeConnection();

                using (SqlCommand command = new SqlCommand("sp_GetAllVisaType", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Visa visa = new Visa
                            {
                                visaID = Convert.ToInt32(reader["VisaID"]),
                                visaName = reader["VisaName"].ToString(),
                                visaTitle = reader["VisaTitle"].ToString(),
                                visaDescription = reader["VisaDescription"].ToString(),
                                adminID = Convert.ToInt32(reader["AdminID"]),
                                personalInformation = Convert.ToBoolean(reader["PersonalInformation"]),
                                PAN = Convert.ToBoolean(reader["PAN"]),
                                aadhar = Convert.ToBoolean(reader["Aadhar"]),
                                governmentProof = Convert.ToBoolean(reader["GovenmentProof"]),
                                photo = Convert.ToBoolean(reader["Photo"]),
                                passport = Convert.ToBoolean(reader["Passport"]),
                                employeeProof = Convert.ToBoolean(reader["EmployeeProof"]),
                                educationProof = Convert.ToBoolean(reader["EducationProof"]),
                                bankProof = Convert.ToBoolean(reader["BankProof"]),
                                toeflCertification = Convert.ToBoolean(reader["ToeflCertification"]),
                                visitorProof = Convert.ToBoolean(reader["VisitorProof"]),
                            };

                            visaList.Add(visa);
                        }
                    }
                }
                return visaList;
            }
            finally
            {
                connection.Close();
            }

            
        }


        public Visa GetVisaType(int id)
        {
            Visa visa = null;
            try {
                InitializeConnection();
                connection.Open();

                using (SqlCommand command = new SqlCommand("sp_GetVisaTypeById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@VisaID", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            visa = new Visa
                            {
                                visaID = (int)reader["VisaID"],
                                visaName = reader["VisaName"].ToString(),
                                visaTitle = reader["VisaTitle"].ToString(),
                                visaDescription = reader["VisaDescription"].ToString(),
                                adminID = (int)reader["AdminID"],
                                personalInformation = (bool)reader["PersonalInformation"],
                                PAN = (bool)reader["PAN"],
                                aadhar = (bool)reader["Aadhar"],
                                governmentProof = (bool)reader["GovenmentProof"],
                                photo = (bool)reader["Photo"],
                                passport = (bool)reader["Passport"],
                                employeeProof = (bool)reader["EmployeeProof"],
                                educationProof = (bool)reader["EducationProof"],
                                bankProof = (bool)reader["BankProof"],
                                toeflCertification = (bool)reader["ToeflCertification"],
                                visitorProof = (bool)reader["VisitorProof"]
                            };
                        }
                    }
                }
                return visa;
            }
            finally
            {
                connection.Close();
            }

            
        }

        public void UpdateVisaType(Visa visa)
        {
            try
            {
                InitializeConnection();
                connection.Open();

                using (SqlCommand command = new SqlCommand("sp_UpdateVisaTypeById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@VisaID", visa.visaID);
                    command.Parameters.AddWithValue("@VisaName", visa.visaName);
                    command.Parameters.AddWithValue("@VisaTitle", visa.visaTitle);
                    command.Parameters.AddWithValue("@VisaDescription", visa.visaDescription);
                    command.Parameters.AddWithValue("@AdminID", visa.adminID);
                    command.Parameters.AddWithValue("@PersonalInformation", visa.personalInformation);
                    command.Parameters.AddWithValue("@PAN", visa.PAN);
                    command.Parameters.AddWithValue("@Aadhar", visa.aadhar);
                    command.Parameters.AddWithValue("@GovenmentProof", visa.governmentProof);
                    command.Parameters.AddWithValue("@Photo", visa.photo);
                    command.Parameters.AddWithValue("@Passport", visa.passport);
                    command.Parameters.AddWithValue("@EmployeeProof", visa.employeeProof);
                    command.Parameters.AddWithValue("@EducationProof", visa.educationProof);
                    command.Parameters.AddWithValue("@BankProof", visa.bankProof);
                    command.Parameters.AddWithValue("@ToeflCertification", visa.toeflCertification);
                    command.Parameters.AddWithValue("@VisitorProof", visa.visitorProof);

                    command.ExecuteNonQuery();
                }
            }
            finally
            {
                connection.Close();
            }
        }

        public void DeleteVisaType(int visaID)
        {
            try
            {
                InitializeConnection();
                connection.Open();

                using (SqlCommand command = new SqlCommand("sp_DeleteVisaTypeById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@VisaID", visaID);

                    command.ExecuteNonQuery();
                }
            }
            finally
            {
                connection.Close();
            }
        }

        public List<Registration> GetAllByRole(string role)
        {
            List<Registration> adminList = new List<Registration>();

            try
            {
                InitializeConnection();
                connection.Open();

                using (SqlCommand command = new SqlCommand("sp_GetUsersByRole", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Role", role);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Registration admin = new Registration
                            {
                                registrationID = (int)reader["RegistrationID"],
                                firstName = reader["FirstName"].ToString(),
                                lastName = reader["LastName"].ToString(),
                                dob = (DateTime)reader["DateOfBirth"],
                                age = (int)reader["Age"],
                                gender = reader["Gender"].ToString(),
                                email = reader["Email"].ToString(),
                                phone = (long)reader["Phone"],
                                role = reader["Role"].ToString(),
                                address = reader["Address"].ToString(),
                                city = reader["City"].ToString(),
                                state = reader["State"].ToString(),
                                country = reader["Country"].ToString(),
                                userName = reader["UserName"].ToString(),
                                passwordHash = reader["PasswordHash"].ToString(),
                                salt = reader["Salt"].ToString(),
                                adminID = reader["AdminID"] != DBNull.Value ? (int)reader["AdminID"] : 0,
                                photo = reader["Photo"] != DBNull.Value ? (byte[])reader["Photo"] : null
                                // You can handle the Photo field as needed based on your application.
                            };

                            adminList.Add(admin);
                        }
                    }
                }
                return adminList;
            }
            finally
            {
                connection.Close();
            }

           
        }
        public void DeleteByID(int id)
        {
            try
            {
                InitializeConnection();
                connection.Open();

                using (SqlCommand command = new SqlCommand("sp_DeleteRegistrationById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RegistrationID", id);
                    command.ExecuteNonQuery();
                }
            }
            finally
            {
                connection.Close();
            }
        }
        public bool InsertRegistration(Registration registration)
        {
            try
            {
                InitializeConnection();
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("sp_IRegistration", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Set the parameters for the stored procedure
                        command.Parameters.AddWithValue("@FirstName", registration.firstName);
                        command.Parameters.AddWithValue("@LastName", registration.lastName);
                        command.Parameters.AddWithValue("@DateOfBirth", registration.dob);
                        command.Parameters.AddWithValue("@Age", registration.age);
                        command.Parameters.AddWithValue("@Gender", registration.gender);
                        command.Parameters.AddWithValue("@Email", registration.email);
                        command.Parameters.AddWithValue("@Phone", registration.phone);
                        command.Parameters.AddWithValue("@Role", registration.role);
                        command.Parameters.AddWithValue("@Address", registration.address);
                        command.Parameters.AddWithValue("@City", registration.city);
                        command.Parameters.AddWithValue("@State", registration.state);
                        command.Parameters.AddWithValue("@Country", registration.country);
                        command.Parameters.AddWithValue("@UserName", registration.userName);
                        command.Parameters.AddWithValue("@PasswordHash", registration.passwordHash);
                        command.Parameters.AddWithValue("@Salt", registration.salt);

                        // Handle Photo (may be NULL)
                        /*if (registration.photo != null)
                        {
                            command.Parameters.AddWithValue("@Photo", registration.photo);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@Photo", DBNull.Value);
                        }*/

                        // Handle AdminID (may be NULL)
                        if (registration.adminID != null)
                        {
                            command.Parameters.AddWithValue("@AdminID", registration.adminID);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@AdminID", DBNull.Value);
                        }

                        command.ExecuteNonQuery();
                    }
                }

                return true; // Indicates successful insertion
            }
            finally
            {
                connection.Close();
            }
        }
        //admin application
        public List<ApplicationPayload> GetAllApplicationAdmin(int status)
        {
            List<ApplicationPayload> applications = new List<ApplicationPayload>();

            try
            {
                InitializeConnection();
                connection.Open();

                // Define your SQL query

                using (SqlCommand command = new SqlCommand("sp_ApplicationFilter", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (status ==6)
                    {
                        command.Parameters.AddWithValue("@Status", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@Status", status);
                    }
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ApplicationPayload application = new ApplicationPayload
                            {
                                applicationID = Convert.ToInt32(reader["ApplicationID"]),
                                visaName = reader["VisaName"].ToString(),
                                visaTitle = reader["VisaTitle"].ToString(),
                                fullName = reader["FullName"].ToString(),
                                status=(Status)reader["Status"],
                            };
                            applications.Add(application);
                        }
                    }
                }
                return applications;
            }
            finally
            {
                connection.Close();
            }

            
        }
        public List<Contact> GetAllContact()
        {
            List<Contact> contacts = new List<Contact>();

            try
            {
                InitializeConnection();
                connection.Open();

                // Define your SQL query

                using (SqlCommand command = new SqlCommand("sp_SelectContactUs", connection))
                {
                   
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Contact contact = new Contact
                            {
                                contactID = Convert.ToInt32(reader["ContactID"]),
                                name = reader["Name"].ToString(),
                                email = reader["Email"].ToString(),
                                message = reader["Message"].ToString(),
                            };
                            contacts.Add(contact);
                        }
                    }
                }
                return contacts;
            }
            finally
            {
                connection.Close();
            }
            
        }
        public void DeleteContactUs(int id)
        {
            try
            {
                InitializeConnection();
                connection.Open();

                // Define your SQL query

                using (SqlCommand command = new SqlCommand("sp_DeleteContact", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ContactID", id);
                    command.ExecuteReader();
                }
            }
            finally
            {
                connection.Close();
            }
        }
        public void CreateContactMessage(Contact contact)
        {

            try
            {
                InitializeConnection();
                connection.Open();

                using (SqlCommand command = new SqlCommand("sp_InsertContactUs", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", contact.name);
                    command.Parameters.AddWithValue("@Email", contact.email);
                    command.Parameters.AddWithValue("@Message", contact.message);
                    command.ExecuteReader();
                }
            }
            finally
            {
                connection.Close();
            }
        }
        public string GenerateRandomSalt()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] saltBytes = new byte[16];
                rng.GetBytes(saltBytes);
                return Convert.ToBase64String(saltBytes);
            }
        }
        public string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                // Combine the password and salt as bytes
                byte[] saltedPasswordBytes = Encoding.UTF8.GetBytes(password + salt);

                // Compute the hash
                byte[] hashedBytes = sha256.ComputeHash(saltedPasswordBytes);

                // Convert the byte array to a Base64 string
                return Convert.ToBase64String(hashedBytes);
            }
        }

        public Registration GetProfileById(int userId)
        {
            InitializeConnection();
            Registration registration = new Registration();
            try
            {
                SqlCommand command = new SqlCommand("sp_SelectById", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@RegistrationID", userId);
                connection.Open();
                command.ExecuteNonQuery();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    registration = new Registration
                    {
                        registrationID = (int)reader["RegistrationID"],
                        firstName = reader["FirstName"].ToString(),
                        lastName = reader.GetString(reader.GetOrdinal("LastName")),
                        dob = reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                        age = reader.GetInt32(reader.GetOrdinal("Age")),
                        gender = reader.GetString(reader.GetOrdinal("Gender")),
                        email = reader.GetString(reader.GetOrdinal("Email")),
                        phone = reader.GetInt64(reader.GetOrdinal("Phone")),
                        address = reader.GetString(reader.GetOrdinal("Address")),
                        city = reader.GetString(reader.GetOrdinal("City")),
                        state = reader.GetString(reader.GetOrdinal("State")),
                        country = reader.GetString(reader.GetOrdinal("Country")),
                    };

                    if (!reader.IsDBNull(reader.GetOrdinal("Photo")))
                    {
                        registration.photo = (byte[])reader["Photo"];
                    }
                    else
                    {
                        registration.photo = null; // Assign null if the column is NULL in the database
                    }

                }
                reader.Close();
                return registration;
            }
            finally
            {
                connection.Close();
            }
            
        }

        public void GetUpdateProfile(Registration registration)
        {
            InitializeConnection();
            try
            {
                SqlCommand command = new SqlCommand("sp_UpdateProfile", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@RegistrationID", registration.registrationID);
                command.Parameters.AddWithValue("@FirstName", registration.firstName);
                command.Parameters.AddWithValue("@LastName", registration.lastName);
                command.Parameters.AddWithValue("@Age", registration.age);
                command.Parameters.AddWithValue("@Gender", registration.gender);
                command.Parameters.AddWithValue("@Phone", registration.phone);
                connection.Open();
                command.ExecuteNonQuery();
                
            }
            finally
            {
                connection.Close();
            }
        }
    }
}