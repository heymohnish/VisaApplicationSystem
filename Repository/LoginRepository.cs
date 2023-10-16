using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using VisaApplicationSystem.Models;
using System.Security.Cryptography;
using System.Text;

namespace VisaApplicationSystem.Repository
{
    public class LoginRepository :BaseDatabaseConnection
    {
        public Registration GetLogin(Login login)
        {
            InitializeConnection();
            Registration registration = new Registration();
            try
            {
                SqlCommand command = new SqlCommand("sp_GetUserByUsername", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserName", login.userName);
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
                        role = reader.GetString(reader.GetOrdinal("Role")),
                        address = reader.GetString(reader.GetOrdinal("Address")),
                        city = reader.GetString(reader.GetOrdinal("City")),
                        state = reader.GetString(reader.GetOrdinal("State")),
                        country = reader.GetString(reader.GetOrdinal("Country")),
                        userName = reader.GetString(reader.GetOrdinal("UserName")),
                        passwordHash = reader.GetString(reader.GetOrdinal("PasswordHash")),
                        salt = reader.GetString(reader.GetOrdinal("Salt"))
                    };
                        
                    if (!reader.IsDBNull(reader.GetOrdinal("Photo")))
                    {
                        registration.photo = (byte[])reader["Photo"];
                    }
                    else
                    {
                        registration.photo = null; // Assign null if the column is NULL in the database
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("AdminID")))
                    {
                        // Assuming that registration.adminID is of type int?
                        registration.adminID = reader.GetInt32(reader.GetOrdinal("AdminID"));
                    }

                }
                bool isPasswordCorrect = VerifyPassword(login.password, registration.salt, registration.passwordHash);
                HttpContext.Current.Session["LoginId"] = registration.registrationID;
                HttpContext.Current.Session["UserName"] = registration.userName;
                if (isPasswordCorrect)
                {
                    registration.isVerified = true;
                    
                }
                else
                {
                    registration.isVerified = false;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return registration;
        }
        public static bool VerifyPassword(string enteredPassword, string storedSalt, string storedHashedPassword)
        {
            string hashedPasswordToCompare = HashPassword(enteredPassword, storedSalt);
            return hashedPasswordToCompare == storedHashedPassword;
        }
        public static string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] saltedPasswordBytes = Encoding.UTF8.GetBytes(password + salt);
                byte[] hashedBytes = sha256.ComputeHash(saltedPasswordBytes);
                return Convert.ToBase64String(hashedBytes);
            }
        }

        public void UploadImage(int registrationID, byte[] imageData)
        {
            InitializeConnection();
            try
            {
                SqlCommand command = new SqlCommand("sp_UploadImage", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@RegistrationID", registrationID);
                command.Parameters.AddWithValue("@Photo", imageData);
                connection.Open();
                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }

        public void ForgetPassword(ForgotPassword forgotPassword)
        {
            InitializeConnection();
            AdminRepository adminRepository = new AdminRepository();
            string salt = adminRepository.GenerateRandomSalt();
            string hashPassword = adminRepository.HashPassword(forgotPassword.password, salt);
            try
            {
                SqlCommand command = new SqlCommand("sp_ForgetPassword", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Email", forgotPassword.email);
                command.Parameters.AddWithValue("@UserName", forgotPassword.userName);
                command.Parameters.AddWithValue("@PasswordHash", hashPassword);
                command.Parameters.AddWithValue("@Salt", salt);
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