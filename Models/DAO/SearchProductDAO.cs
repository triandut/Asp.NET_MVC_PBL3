using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class SearchProductDAO
    {
        FreeCyDB db = null;
        public SearchProductDAO()
        {
            db = new FreeCyDB();
        }
        public List<Product> ResultSearchingDAO(string keyproduct)
        {
            return db.Products.Where(x => x.Name.Contains(keyproduct) && x.Status == 1 ).ToList();
        }
        public List<User> ResultSearchingUserDAO(string keyuser)
        {
            return db.Users.Where(x => x.UserName.Contains(keyuser) && x.Status == true).ToList();
        }
        public List<Product> ResultSearchingInLayoutSearchingDAO(string keyword)
        {
            return db.Products.Where(x => x.Name.Contains(keyword)).ToList();
        }

    }
}
