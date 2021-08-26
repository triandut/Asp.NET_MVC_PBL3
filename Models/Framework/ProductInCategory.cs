using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Framework
{
    public class ProductInCategory
    {
        public int ID_Product { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double? Price { get; set; }
        public string Image { get; set; }
        public string Experience { get; set; }
        public string Education { get; set; }
        public string Salary { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string WorkTime { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
    }
}
