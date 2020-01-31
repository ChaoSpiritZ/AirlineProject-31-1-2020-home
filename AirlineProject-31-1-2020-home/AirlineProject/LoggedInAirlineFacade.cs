using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject
{
    class LoggedInAirlineFacade : AnonymousUserFacade, ILoggedInAirlineFacade
    {
        public void CancelFlight(LoginToken<AirlineCompany> token, Flight flight)
        {
            //what if it's in the air? how do i know the current date?
            LoginHelper.CheckToken<AirlineCompany>(token);
            POCOValidator.FlightValidator(flight, true);
            if (_flightDAO.Get(flight.ID) == null)
                throw new FlightNotFoundException($"failed to cancel flight! there is no flight with id [{flight.ID}]");
            if (flight.AirlineCompanyId != token.User.ID)
                throw new InaccessibleFlightException($"failed to cancel flight! you do not own flight [{flight}]");
            _ticketDAO.RemoveTicketsByFlight(flight);
            _flightDAO.Remove(flight);


            //need to add functions to to remove Poco lists
            //if customer is deleted then flight's vacancy is increased

            // to delete a poco ==> you need to delete those
            //airlineCompany ==> flights ==> what if it's flying?
            //country ==> airlineCompanies, flights ==> umm... bermuda triangle stuff right here?
            //customer ==> tickets
            //flight ==> tickets
            //ticket ==> none
        }

        public void ChangeMyPassword(LoginToken<AirlineCompany> token, string oldPassword, string newPassword)
        {
            LoginHelper.CheckToken<AirlineCompany>(token);
            if (newPassword.Trim() == "")
                throw new EmptyPasswordException($"failed to change password! new password is empty!");
            if (token.User.Password != oldPassword)
                throw new WrongPasswordException($"failed to change password! old password doesn't match!");
            token.User.Password = newPassword;
            _airlineDAO.Update(token.User);
        }

        public void CreateFlight(LoginToken<AirlineCompany> token, Flight flight)
        {
            LoginHelper.CheckToken<AirlineCompany>(token);
            POCOValidator.FlightValidator(flight, false);
            if (flight.AirlineCompanyId != token.User.ID)
                throw new InaccessibleFlightException($"failed to create flight [{flight}], you do not own this flight!"); //probably won't happen unless something goes wrong
            if (DateTime.Compare(flight.DepartureTime, flight.LandingTime) > 0)
                throw new InvalidFlightDateException($"failed to create flight [{flight}], cannot fly back in time from [{flight.DepartureTime}] to [{flight.LandingTime}]");
            if (DateTime.Compare(flight.DepartureTime, flight.LandingTime) == 0)
                throw new InvalidFlightDateException($"failed to create flight [{flight}], departure time and landing time are the same [{flight.DepartureTime}], and as you know, teleportation isn't invented yet");
            if (_countryDAO.Get(flight.OriginCountryCode) == null)
                throw new CountryNotFoundException($"failed to create flight [{flight}], origin country with id [{flight.OriginCountryCode}] was not found!");
            if (_countryDAO.Get(flight.DestinationCountryCode) == null)
                throw new CountryNotFoundException($"failed to create flight [{flight}], destination country with id [{flight.DestinationCountryCode}] was not found!");
            _flightDAO.Add(flight);
        }

        public IList<Flight> GetAllFlights(LoginToken<AirlineCompany> token)
        {
            //all flights or my flights?
            //what if there are no flights? exception or empty list?
            throw new NotImplementedException();
        }

        public IList<Ticket> GetAllTickets(LoginToken<AirlineCompany> token)
        {
            //does that mean all tickets of all of my flights?
            throw new NotImplementedException();
        }

        public void ModifyAirlineDetails(LoginToken<AirlineCompany> token, AirlineCompany airline)
        {
            //leave password change alone?
            throw new NotImplementedException();
        }

        public void UpdateFlight(LoginToken<AirlineCompany> token, Flight flight)
        {
            LoginHelper.CheckToken<AirlineCompany>(token);
            POCOValidator.FlightValidator(flight, true);
            if (_flightDAO.Get(flight.ID) == null)
                throw new FlightNotFoundException($"failed to update flight! flight with id of [{flight.ID}] was not found!");
            if (flight.AirlineCompanyId != token.User.ID)
                throw new InaccessibleFlightException($"failed to update flight! you do not own flight [{flight}]");
            if (DateTime.Compare(flight.DepartureTime, flight.LandingTime) > 0)
                throw new InvalidFlightDateException($"failed to update flight [{flight}], cannot fly back in time from [{flight.DepartureTime}] to [{flight.LandingTime}]");
            if (DateTime.Compare(flight.DepartureTime, flight.LandingTime) == 0)
                throw new InvalidFlightDateException($"failed to update flight [{flight}], departure time and landing time are the same [{flight.DepartureTime}], and as you know, teleportation isn't invented yet");
            if (_countryDAO.Get(flight.OriginCountryCode) == null)
                throw new CountryNotFoundException($"failed to update flight [{flight}], origin country with id [{flight.OriginCountryCode}] was not found!");
            if (_countryDAO.Get(flight.DestinationCountryCode) == null)
                throw new CountryNotFoundException($"failed to update flight [{flight}], destination country with id [{flight.DestinationCountryCode}] was not found!");
            _flightDAO.Update(flight);
        }
    }
}
