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