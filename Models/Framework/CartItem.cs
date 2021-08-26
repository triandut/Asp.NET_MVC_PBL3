using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Framework
{
    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public static explicit operator List<object>(CartItem v)
        {
            throw new NotImplementedException();
        }
    }
}
