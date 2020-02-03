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

        public AnonymousUserFacade GetFacade<T>()
        {
            //umm what???
        }
    }
}
