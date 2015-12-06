using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns2
{
    public class DBAuthentication
    {
        protected Boolean CheckUser(String login,
            String password)
        {
            //Запрос в БД, есть ли там пользователь
            //c таким логином

            return true;
        }
    }

    public class AuthenticationProxy: DBAuthentication
    {
        public Boolean Register(String login, 
            String password, String password2)
        {

            if (password==password2)
            {
                return CheckUser(login, password);
            }

            return false;
        }


    }

    
}
