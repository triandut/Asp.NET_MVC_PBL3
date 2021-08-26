using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class OrderDAO
    {
        FreeCyDB db = null;

        public OrderDAO()
        {
            db = new FreeCyDB();
        }

        public int Create(Order request)
        {
            try
            {
                db.Orders.Add(request);
                db.SaveChanges();
                return request.ID_Order;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public List<Order> GetSendOrders(int user_id)
        {
            return db.Orders.Where(o => o.ID_User == user_id).OrderByDescending(o => o.CreatedAt).ToList();
        }

        public List<Order> GetReceivedOrders(int user_id)
        {
            List<Order> list = new List<Order>();
            List<Product> products = new ProductDAO().ListProduct(-1, new UserDAO().ViewDetails(user_id));
            foreach (var p in products)
            {
                var tmp = db.Orders.Where(o => o.ID_Product == p.ID_Product && o.isFinish == false).OrderBy(o => o.CreatedAt).ToArray();
                if (tmp != null) { list.AddRange(tmp); }
            }
            return list;
        }

        public Order ViewDetail(int id)
        {
            return db.Orders.Find(id);
        }

        public bool SetCompleted(int order_id)
        {
            try
            {
                var item = db.Orders.Find(order_id);
                item.isFinish = true;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public int Insert(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
            return order.ID_Order;
        }
    }
}
