using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    
    public class PayDAO
    {
        FreeCyDB db = null;
        public PayDAO()
        {
            db = new FreeCyDB();
        }
        public int Insert(Order pay)
        {
            db.Orders.Add(pay);
            db.SaveChanges();
            return pay.ID_Order;
        }
    }
}
