using Models.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class UserModel
    {
        public FreeCyDB context = null;
        public UserModel()
        {
            context = new FreeCyDB();
        }

        public bool Login(string username, string password)
        {
            Object[] sqlparams = 
            {
                new SqlParameter("@username", username),
                new SqlParameter("@password", password)
            };
            var res = context.Database.SqlQuery<bool>("Account_login @username,@password",sqlparams).SingleOrDefault();
            return res;
        }
        public bool SignUp(string username, string password, string email)
        {
            Object[] sqlparams =
            {
                new SqlParameter("@username", username),
                new SqlParameter("@password", password),
                new SqlParameter("@email", email)
            };
            var res = context.Database.SqlQuery<bool>("Account_register @username,@password,@email", sqlparams).SingleOrDefault();
            return res;
        }


    }
}
