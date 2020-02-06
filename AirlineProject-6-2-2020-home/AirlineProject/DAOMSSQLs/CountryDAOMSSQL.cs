using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AirlineProject
{
    public class CountryDAOMSSQL : ICountryDAO //supposed to be internal
    {
        public void Add(Country t)
        {
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("ADD_COUNTRY", con))
                {
                    cmd.Parameters.AddWithValue("@countryName", t.CountryName);
                    cmd.CommandType = CommandType.StoredProcedure;

                    t.ID = (long)(decimal)cmd.ExecuteScalar();
                }
            }
        }

        public Country Get(long id)
        {
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("GET_COUNTRY", con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Country country = new Country()
                            {
                                ID = (long)reader["ID"],
                                CountryName = (string)reader["COUNTRY_NAME"]
                            };
                            return country;
                        }
                        return null;
                    }
                }
            }
        }

        public IList<Country> GetAll()
        {
            List<Country> countries = new List<Country>();
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("GET_ALL_COUNTRIES", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Country country = new Country()
                            {
                                ID = (long)reader["ID"],
                                CountryName = (string)reader["COUNTRY_NAME"]
                            };
                            countries.Add(country);
                        }
                    }
                }
            }
            return countries;
        }

        public void Remove(Country t)
        {
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("REMOVE_COUNTRY", con))
                {
                    cmd.Parameters.AddWithValue("@id", t.ID);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                    }
                }
            }
        }

        public void Update(Country t)
        {
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("UPDATE_COUNTRY", con))
                {
                    cmd.Parameters.AddWithValue("@countryName", t.CountryName);
                    cmd.Parameters.AddWithValue("@id", t.ID);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                    }
                }
            }
        }
    }
}
