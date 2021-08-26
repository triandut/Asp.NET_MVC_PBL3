using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class CategoryDAO
    {
        FreeCyDB db = null;
        private static CategoryDAO _Instance;
        public static CategoryDAO Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new CategoryDAO();
                }
                return _Instance;
            }
            set
            {
                ;
            }
        }
        public CategoryDAO()
        {
            db = new FreeCyDB();
        }
        public List<Category> ListAllCategoty()
        {
            return db.Categories.ToList();
        }
        public Category ViewDetail(int cateId)
        {
            return db.Categories.Find(cateId);
        }
    }
}
