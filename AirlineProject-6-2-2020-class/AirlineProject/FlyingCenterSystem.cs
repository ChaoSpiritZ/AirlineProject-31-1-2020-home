using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineProject
{
    public class FlyingCenterSystem
    {
        private static FlyingCenterSystem INSTANCE;

        private FlyingCenterSystem()
        {

        }

        public static FlyingCenterSystem GetInstance()
        {
            if (INSTANCE == null)
            {
                INSTANCE = new FlyingCenterSystem();
            }
            return INSTANCE;
        }

        public FacadeBase Login(string username, string pwd, out ILoginToken loginToken)
        {
            LoginService lS = new LoginService();
            if (lS.TryAdminLogin(username, pwd, out LoginToken<Administrator> adminToken))
            {
                loginToken = adminToken;
                return new LoggedInAdministratorFacade();
            }
            if(lS.TryAirlineLogin(username, pwd, out LoginToken<AirlineCompany> airlineToken))
            {
                loginToken = airlineToken;
                return new LoggedInAirlineFacade();
            }
            if(lS.TryCustomerLogin(username, pwd, out LoginToken<Customer> customerToken))
            {
                loginToken = customerToken;
                return new LoggedInCustomerFacade();
            }
            //if username null => return anonymous facade
            loginToken = null;
            return null;
        }
        
    }
}
