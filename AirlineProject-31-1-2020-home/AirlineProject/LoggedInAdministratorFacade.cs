﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject
{
    class LoggedInAdministratorFacade : AnonymousUserFacade, ILoggedInAdministratorFacade
    {
        public void CreateNewAirline(LoginToken<Administrator> token, AirlineCompany airline)
        {
            LoginHelper.CheckToken<Administrator>(token);
            POCOValidator.AirlineCompanyValidator(airline, false);
            if (_airlineDAO.GetAirlineByAirlineName(airline.AirlineName) != null)
                throw new AirlineNameAlreadyExistsException($"failed to create airline! there is already an airline with the name '{airline.AirlineName}'");
            if (_airlineDAO.GetAirlineByUsername(airline.UserName) != null || _customerDAO.GetCustomerByUsername(airline.UserName) != null || airline.UserName == "admin")
                throw new UsernameAlreadyExistsException($"failed to create airline! Username '{airline.UserName}' is already taken!");
            if (_countryDAO.Get(airline.CountryCode) == null)
                throw new CountryNotFoundException($"failed to create airline! there is no country with id [{airline.CountryCode}]");
            _airlineDAO.Add(airline);
        }

        public void CreateNewCustomer(LoginToken<Administrator> token, Customer customer)
        {
            LoginHelper.CheckToken<Administrator>(token);
            POCOValidator.CustomerValidator(customer, false);
            if (_airlineDAO.GetAirlineByUsername(customer.UserName) != null || _customerDAO.GetCustomerByUsername(customer.UserName) != null || customer.UserName == "admin")
                throw new UsernameAlreadyExistsException($"failed to create customer! Username '{customer.UserName}' is already taken!");
            _customerDAO.Add(customer);
        }

        public void RemoveAirline(LoginToken<Administrator> token, AirlineCompany airline)
        {
            LoginHelper.CheckToken<Administrator>(token);
            POCOValidator.AirlineCompanyValidator(airline, true);
            if (_airlineDAO.Get(airline.ID) == null) //doesn't mean the airline in the parameter has the same values as the airline in the database with the same id
                throw new UserNotFoundException($"failed to remove airline! airline with username [{airline.UserName}] was not found!");
            _airlineDAO.Remove(airline);
        }

        public void RemoveCustomer(LoginToken<Administrator> token, Customer customer)
        {
            LoginHelper.CheckToken<Administrator>(token);
            POCOValidator.CustomerValidator(customer, true);
            if (_customerDAO.Get(customer.ID) == null) //doesn't mean the customer in the parameter has the same values as the customer in the database with the same id
                throw new UserNotFoundException($"failed to remove customer! customer with username [{customer.UserName}] was not found!");
            IList<Flight> flights = _flightDAO.GetFlightsByCustomer(customer);
            flights.ToList().ForEach(f => f.RemainingTickets++); //is this how i should do it? 
            flights.ToList().ForEach(f => _flightDAO.Update(f));// feels like it's super inefficient
            _ticketDAO.RemoveTicketsByCustomer(customer);
            _customerDAO.Remove(customer);
        }

        public void UpdateAirlineDetails(LoginToken<Administrator> token, AirlineCompany airline)
        {
            LoginHelper.CheckToken<Administrator>(token);
            POCOValidator.AirlineCompanyValidator(airline, true);
            if (_airlineDAO.Get(airline.ID) == null) //doesn't mean the airline in the parameter has the same values as the airline in the database with the same id
                throw new UserNotFoundException($"failed to update airline! airline with username [{airline.UserName}] was not found!");
            if (_countryDAO.Get(airline.CountryCode) == null)
                throw new CountryNotFoundException($"failed to update airline! there is no country with id [{airline.CountryCode}]");
            _airlineDAO.Update(airline);
        }

        public void UpdateCustomerDetails(LoginToken<Administrator> token, Customer customer)
        {
            LoginHelper.CheckToken<Administrator>(token);
            POCOValidator.CustomerValidator(customer, true);
            if (_customerDAO.Get(customer.ID) == null) //doesn't mean the customer in the parameter has the same values as the customer in the database with the same id
                throw new UserNotFoundException($"failed to update customer! customer with username [{customer.UserName}] was not found!");
            _customerDAO.Update(customer);
        }
    }
}
