using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject
{
    class LoggedInCustomerFacade : AnonymousUserFacade, ILoggedInCustomerFacade
    {
        public void CancelTicket(LoginToken<Customer> token, Ticket ticket)
        {
            LoginHelper.CheckToken<Customer>(token);
            POCOValidator.TicketValidator(ticket, true);
            if (_ticketDAO.Get(ticket.ID) == null)
                throw new TicketNotFoundException($"failed to cancel ticket [{ticket}], ticket with id of [{ticket.ID}] was not found!");
            if (ticket.CustomerId != token.User.ID)
                throw new InaccessibleTicketException($"failed to cancel ticket , you do not own ticket [{ticket}]");
            Flight updatedFlight = _flightDAO.Get(ticket.FlightId);
            updatedFlight.RemainingTickets++;
            _flightDAO.Update(updatedFlight);
            _ticketDAO.Remove(ticket);
        }

        public IList<Flight> GetAllMyFlights(LoginToken<Customer> token)
        {
            LoginHelper.CheckToken<Customer>(token);
            //if no flight, return empty list
            IList<Flight> myFlights = _flightDAO.GetFlightsByCustomer(token.User);
            return myFlights;
        }

        public Ticket PurchaseTicket(LoginToken<Customer> token, Flight flight)
        {
            LoginHelper.CheckToken<Customer>(token);
            POCOValidator.FlightValidator(flight, false);
            if (_flightDAO.Get(flight.ID) == null)
                throw new FlightNotFoundException($"failed to purchase ticket, there is no flight with id of [{flight.ID}]");
            IList<Ticket> tickets = _ticketDAO.GetTicketsByCustomerId(token.User);
            if (tickets.Any(item => item.FlightId == flight.ID)) //boolean
                throw new TicketAlreadyExistsException($"failed to purchase ticket, you already purchased a ticket to flight [{flight}]");
            if (_flightDAO.Get(flight.ID).RemainingTickets == 0)
                throw new NoMoreTicketsException($"failed to purchase ticket to flight [{flight}], there are no more tickets left!");
            //do i even need to check if the flight in the parameter is legit? --- no
            Ticket newTicket = new Ticket(0, flight.ID, token.User.ID);
            _ticketDAO.Add(newTicket);
            flight.RemainingTickets--;
            _flightDAO.Update(flight);
            return newTicket;
        }
    }
}
