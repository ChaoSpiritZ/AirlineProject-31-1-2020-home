using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject
{
    public class FlightDAOMSSQL : IFlightDAO //supposed to be internal
    {
        public void Add(Flight t)
        {
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("ADD_FLIGHT", con))
                {
                    cmd.Parameters.AddWithValue("@airlineCompanyId", t.AirlineCompanyId);
                    cmd.Parameters.AddWithValue("@originCountryCode", t.OriginCountryCode);
                    cmd.Parameters.AddWithValue("@destinationCountryCode", t.DestinationCountryCode);
                    cmd.Parameters.AddWithValue("@departureTime", t.DepartureTime);
                    cmd.Parameters.AddWithValue("@landingTime", t.LandingTime);
                    cmd.Parameters.AddWithValue("@remainingTickets", t.RemainingTickets);
                    cmd.CommandType = CommandType.StoredProcedure;

                    t.ID = (long)(decimal)cmd.ExecuteScalar();
                }
            }
        }

        public Flight Get(long id)
        {
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("GET_FLIGHT", con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Flight flight = new Flight()
                            {
                                ID = (long)reader["ID"],
                                AirlineCompanyId = (long)reader["AIRLINECOMPANY_ID"],
                                OriginCountryCode = (long)reader["ORIGIN_COUNTRY_CODE"],
                                DestinationCountryCode = (long)reader["DESTINATION_COUNTRY_CODE"],
                                DepartureTime = (DateTime)reader["DEPARTURE_TIME"],
                                LandingTime = (DateTime)reader["LANDING_TIME"],
                                RemainingTickets = (int)reader["REMAINING_TICKETS"],
                            };
                            return flight;
                        }
                        return null;
                    }
                }
            }
        }

        public IList<Flight> GetAll()
        {
            List<Flight> flights = new List<Flight>();
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("GET_ALL_FLIGHTS", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Flight flight = new Flight()
                            {
                                ID = (long)reader["ID"],
                                AirlineCompanyId = (long)reader["AIRLINECOMPANY_ID"],
                                OriginCountryCode = (long)reader["ORIGIN_COUNTRY_CODE"],
                                DestinationCountryCode = (long)reader["DESTINATION_COUNTRY_CODE"],
                                DepartureTime = (DateTime)reader["DEPARTURE_TIME"],
                                LandingTime = (DateTime)reader["LANDING_TIME"],
                                RemainingTickets = (int)reader["REMAINING_TICKETS"],
                            };
                            flights.Add(flight);
                        }
                    }
                }
            }
            return flights;
        }

        public Dictionary<Flight, int> GetAllFlightsVacancy()
        {
            Dictionary<Flight, int> flights = new Dictionary<Flight, int>();
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("GET_ALL_FLIGHTS", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Flight flight = new Flight()
                            {
                                ID = (long)reader["ID"],
                                AirlineCompanyId = (long)reader["AIRLINECOMPANY_ID"],
                                OriginCountryCode = (long)reader["ORIGIN_COUNTRY_CODE"],
                                DestinationCountryCode = (long)reader["DESTINATION_COUNTRY_CODE"],
                                DepartureTime = (DateTime)reader["DEPARTURE_TIME"],
                                LandingTime = (DateTime)reader["LANDING_TIME"],
                                RemainingTickets = (int)reader["REMAINING_TICKETS"],
                            };
                            flights.Add(flight, flight.RemainingTickets);
                        }
                    }
                }
            }
            return flights;
        }

        //public Flight GetFlightById(int id)
        //{
        //    using (SqlConnection con = new SqlConnection(AirlineProjectConfig.path))
        //    {
        //        con.Open();
        //        using (SqlCommand cmd = new SqlCommand($"select * from Flights where ID = {id}", con))
        //        {
        //            using (SqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    Flight flight = new Flight()
        //                    {
        //                        ID = (long)reader["ID"],
        //                        AirlineCompanyId = (long)reader["AIRLINECOMPANY_ID"],
        //                        OriginCountryCode = (long)reader["ORIGIN_COUNTRY_CODE"],
        //                        DestinationCountryCode = (long)reader["DESTINATION_COUNTRY_CODE"],
        //                        DepartureTime = (DateTime)reader["DEPARTURE_TIME"],
        //                        LandingTime = (DateTime)reader["LANDING_TIME"],
        //                        RemainingTickets = (int)reader["REMAINING_TICKETS"],
        //                    };
        //                    return flight;
        //                }
        //                return null;
        //            }
        //        }
        //    }
        //}

        public IList<Flight> GetFlightsByCustomer(Customer customer)
        {
            List<Flight> flights = new List<Flight>();
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("GET_FLIGHTS_BY_CUSTOMER", con))
                {
                    cmd.Parameters.AddWithValue("@customerId", customer.ID);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Flight flight = new Flight()
                            {
                                ID = (long)reader["ID"],
                                AirlineCompanyId = (long)reader["AIRLINECOMPANY_ID"],
                                OriginCountryCode = (long)reader["ORIGIN_COUNTRY_CODE"],
                                DestinationCountryCode = (long)reader["DESTINATION_COUNTRY_CODE"],
                                DepartureTime = (DateTime)reader["DEPARTURE_TIME"],
                                LandingTime = (DateTime)reader["LANDING_TIME"],
                                RemainingTickets = (int)reader["REMAINING_TICKETS"],
                            };
                            flights.Add(flight);
                        }
                    }
                }
            }
            return flights;
        }

        public IList<Flight> GetFlightsByDepartureDate(DateTime departureDate)
        {
            List<Flight> flights = new List<Flight>();
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("GET_FLIGHTS_BY_DEPARTURE_DATE", con))
                {
                    cmd.Parameters.AddWithValue("@departureDate", departureDate);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Flight flight = new Flight()
                            {
                                ID = (long)reader["ID"],
                                AirlineCompanyId = (long)reader["AIRLINECOMPANY_ID"],
                                OriginCountryCode = (long)reader["ORIGIN_COUNTRY_CODE"],
                                DestinationCountryCode = (long)reader["DESTINATION_COUNTRY_CODE"],
                                DepartureTime = (DateTime)reader["DEPARTURE_TIME"],
                                LandingTime = (DateTime)reader["LANDING_TIME"],
                                RemainingTickets = (int)reader["REMAINING_TICKETS"],
                            };
                            flights.Add(flight);
                        }
                    }
                }
            }
            return flights;
        }

        public IList<Flight> GetFlightsByDestinationCountry(long countryCode)
        {
            List<Flight> flights = new List<Flight>();
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("GET_FLIGHTS_BY_DESTINATION_COUNTRY", con))
                {
                    cmd.Parameters.AddWithValue("@countryCode", countryCode);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Flight flight = new Flight()
                            {
                                ID = (long)reader["ID"],
                                AirlineCompanyId = (long)reader["AIRLINECOMPANY_ID"],
                                OriginCountryCode = (long)reader["ORIGIN_COUNTRY_CODE"],
                                DestinationCountryCode = (long)reader["DESTINATION_COUNTRY_CODE"],
                                DepartureTime = (DateTime)reader["DEPARTURE_TIME"],
                                LandingTime = (DateTime)reader["LANDING_TIME"],
                                RemainingTickets = (int)reader["REMAINING_TICKETS"],
                            };
                            flights.Add(flight);
                        }
                    }
                }
            }
            return flights;
        }

        public IList<Flight> GetFlightsByLandingDate(DateTime landingDate)
        {
            List<Flight> flights = new List<Flight>();
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("GET_FLIGHTS_BY_LANDING_DATE", con))
                {
                    cmd.Parameters.AddWithValue("@landingDate", landingDate);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Flight flight = new Flight()
                            {
                                ID = (long)reader["ID"],
                                AirlineCompanyId = (long)reader["AIRLINECOMPANY_ID"],
                                OriginCountryCode = (long)reader["ORIGIN_COUNTRY_CODE"],
                                DestinationCountryCode = (long)reader["DESTINATION_COUNTRY_CODE"],
                                DepartureTime = (DateTime)reader["DEPARTURE_TIME"],
                                LandingTime = (DateTime)reader["LANDING_TIME"],
                                RemainingTickets = (int)reader["REMAINING_TICKETS"],
                            };
                            flights.Add(flight);
                        }
                    }
                }
            }
            return flights;
        }

        public IList<Flight> GetFlightsByOriginCountry(long countryCode)
        {
            List<Flight> flights = new List<Flight>();
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("GET_FLIGHTS_BY_ORIGIN_COUNTRY", con))
                {
                    cmd.Parameters.AddWithValue("@countryCode", countryCode);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Flight flight = new Flight()
                            {
                                ID = (long)reader["ID"],
                                AirlineCompanyId = (long)reader["AIRLINECOMPANY_ID"],
                                OriginCountryCode = (long)reader["ORIGIN_COUNTRY_CODE"],
                                DestinationCountryCode = (long)reader["DESTINATION_COUNTRY_CODE"],
                                DepartureTime = (DateTime)reader["DEPARTURE_TIME"],
                                LandingTime = (DateTime)reader["LANDING_TIME"],
                                RemainingTickets = (int)reader["REMAINING_TICKETS"],
                            };
                            flights.Add(flight);
                        }
                    }
                }
            }
            return flights;
        }

        public void Remove(Flight t)
        {
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("REMOVE_FLIGHT", con))
                {
                    cmd.Parameters.AddWithValue("@id", t.ID);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                    }
                }
            }
        }

        public void Update(Flight t)
        {
            using (SqlConnection con = new SqlConnection(AirlineProjectConfig.CONNECTION_STRING))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("UPDATE_FLIGHT", con))
                {
                    cmd.Parameters.AddWithValue("@airlineCompanyId", t.AirlineCompanyId);
                    cmd.Parameters.AddWithValue("@originCountryCode", t.OriginCountryCode);
                    cmd.Parameters.AddWithValue("@destinationCountryCode", t.DestinationCountryCode);
                    cmd.Parameters.AddWithValue("@departureTime", t.DepartureTime);
                    cmd.Parameters.AddWithValue("@landingTime", t.LandingTime);
                    cmd.Parameters.AddWithValue("@remainingTickets", t.RemainingTickets);
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
