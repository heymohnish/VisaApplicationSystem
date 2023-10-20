using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace VisaApplicationSystem.Repository
{
    public class FormRepository
    {
        private SqlConnection connection;
        private void Connections()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBSC"].ToString();
            connection = new SqlConnection(connectionString);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool GetUser(string userName)
        {
            Connections();
            int studentId = 0;
            try
            {
                SqlCommand command = new SqlCommand("sp_DuplicateRegister", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserName", userName);
                connection.Open();
                studentId = (int)command.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            Console.WriteLine(studentId > 1);
            return studentId > 0;
        }
        /// <summary>
        /// This is a sample method that performs a specific action.
        /// </summary>
        /// <param name="parameter">Description of the parameter.</param>
        /// <returns>Description of the return value.</returns>
        public List<string> GetStates(string country)
        {
            List<string> states = new List<string>();
            Connections();
            try
            {
                SqlCommand command = new SqlCommand("sp_GetStatesByCountry", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CountryName", country);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string stateName = reader["StateName"].ToString();
                    states.Add(stateName);
                }
                reader.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {

                connection.Close();
            }

            return states;
        }
        /// <summary>
        /// Retrieves a list of country names from the database.
        /// </summary>
        /// <returns>A list of country names as strings.</returns>
        public List<string> GetCountry()
        {
            List<string> country = new List<string>();
            Connections();
            try
            {
                SqlCommand command = new SqlCommand("sp_GetCountry", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string countryName = reader["CountryName"].ToString();
                    country.Add(countryName);
                }
                reader.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {

                connection.Close();
            }

            return country;
        }
        /// <summary>
        /// Retrieves a list of city names within a specified state from the database.
        /// </summary>
        /// <param name="state">The name of the state for which city names are to be retrieved.</param>
        /// <returns>A list of city names as strings.</returns>
        public List<string> GetCity(string state)
        {
            List<string> city = new List<string>();
            Connections();
            try
            {
                SqlCommand command = new SqlCommand("sp_GetCitiesByState", connection);
                connection.Open();

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@StateName", state);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string cityName = reader["CityName"].ToString();
                    city.Add(cityName);
                }
                reader.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {

                connection.Close();
            }

            return city;
        }
        /// <summary>
        /// Checks if a user with the given email exists in the database.
        /// </summary>
        /// <param name="email">The email address to check for duplicates.</param>
        /// <returns>True if a user with the email exists; otherwise, false.</returns>

        public bool GetEmail(string userName)
        {
            Connections();
            int studentId = 0;
            try
            {
                SqlCommand command = new SqlCommand("sp_DuplicateEmail", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Email", userName);
                connection.Open();
                studentId = (int)command.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            Console.WriteLine(studentId > 1);
            return studentId > 0;
        }
    }
}