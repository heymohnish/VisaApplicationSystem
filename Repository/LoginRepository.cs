using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using VisaApplicationSystem.Models;
using System.Security.Cryptography;
using System.Text;
using System.Web.Routing;

namespace VisaApplicationSystem.Repository
{
    public class LoginRepository :BaseDatabaseConnection
    {
        /// <summary>
        /// Retrieves registration information based on a user's login credentials.
        /// </summary>
        /// <param name="login">The login credentials (username and password).</param>
        /// <returns>A Registration object containing user information if login is successful; otherwise, an empty Registration object.</returns>

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
                        roleBase = (RoleBase)reader["RoleBase"],
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
                        salt = reader.GetString(reader.GetOrdinal("Salt")),
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
                HttpContext.Current.Session["Role"] = registration.role;
                if (isPasswordCorrect)
                {
                    registration.isVerified = true;
                    
                }
                else
                {
                    registration.isVerified = false;
                }
                reader.Close();
                return registration;
            }
            finally
            {
                connection.Close();
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
        /// <summary>
        /// Resets a user's password by updating the hashed password and salt in the database.
        /// </summary>
        /// <param name="forgotPassword">A data structure containing user email, username, and the new password.</param>
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
        /// <summary>
        /// Verifies if the entered password matches a stored hashed password using the provided salt.
        /// </summary>
        /// <param name="enteredPassword">The password entered by the user for verification.</param>
        /// <param name="storedSalt">The salt used to hash the stored password.</param>
        /// <param name="storedHashedPassword">The stored hashed password to compare against.</param>
        /// <returns>True if the entered password matches the stored password; otherwise, false.</returns>

        public static bool VerifyPassword(string enteredPassword, string storedSalt, string storedHashedPassword)
        {
            string hashedPasswordToCompare = HashPassword(enteredPassword, storedSalt);
            return hashedPasswordToCompare == storedHashedPassword;
        }
        /// <summary>
        /// Hashes a password with a provided salt using the SHA-256 algorithm.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <param name="salt">The salt to use in the hashing process.</param>
        /// <returns>The base64-encoded hashed password.</returns>
        public static string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] saltedPasswordBytes = Encoding.UTF8.GetBytes(password + salt);
                byte[] hashedBytes = sha256.ComputeHash(saltedPasswordBytes);
                return Convert.ToBase64String(hashedBytes);
            }
        }
        /// <summary>
        /// Uploads an image associated with a user's registration to the database.
        /// </summary>
        /// <param name="registrationID">The ID of the user's registration.</param>
        /// <param name="imageData">The binary image data to upload.</param>

    }
}