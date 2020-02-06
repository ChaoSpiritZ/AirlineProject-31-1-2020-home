using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject
{
    class LoginService : ILoginService
    {
        private IAirlineDAO _airlineDAO;
        private ICustomerDAO _customerDAO;

        public bool TryAdminLogin(string userName, string password, out LoginToken<Administrator> token)
        {
            if(userName == "admin" && password == "9999")
            {
                token = new LoginToken<Administrator>();
                return true;
            }
            token = null;
            return false;
        }

        public bool TryAirlineLogin(string userName, string password, out LoginToken<AirlineCompany> token)
        {
            AirlineCompany airlineCompany = _airlineDAO.GetAirlineByUsername(userName);
            if(airlineCompany != null)
            {
                if(airlineCompany.Password == password)
                {
                    token = new LoginToken<AirlineCompany>();
                    return true;
                }
                else
                {
                    throw new WrongPasswordException();
                }
            }
            token = null;
            return false;
        }

        public bool TryCustomerLogin(string userName, string password, out LoginToken<Customer> token)
        {
            Customer customer = _customerDAO.GetCustomerByUsername(userName);
            if(customer != null)
            {
                if(customer.Password == password)
                {
                    token = new LoginToken<Customer>();
                    return true;
                }
                else
                {
                    throw new WrongPasswordException();
                }
            }
            token = null;
            return false;
        }
    }
}
