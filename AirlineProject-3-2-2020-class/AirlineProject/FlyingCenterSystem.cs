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
            LoginService LS = new LoginService();
            LS.TryAdminLogin(username, pwd, out LoginToken<Administrator> AdminToken);
            LS.TryAirlineLogin(username, pwd, out LoginToken<AirlineCompany> AirlineToken);
            LS.TryCustomerLogin(username, pwd, out LoginToken<Customer> CustomerToken);
            loginToken = null;
            return null;
            //still not done here... will probably need help here
            //oh yeah btw unrelated, create ticket history and flight history
        }
        
    }
}
