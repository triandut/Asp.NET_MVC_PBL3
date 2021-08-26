using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Framework
{
    public class ProductViewModel
    {
        public int ID_Product { get; set; }
        public int ID_User { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double? Price { get; set; }
        public int? Status { get; set; }

        public User User { get; set; }
        public DateTime? CreatedAt { get; set; }

  
        public DateTime? UpdatedAt { get; set; }

  
        public string Image { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public string WorkTime { get; set; }

        public string MetaTitle { get; set; }

   
        public string Respons { get; set; }

   
        public string Experience { get; set; }

        public string Benifit { get; set; }

     
        public string Education { get; set; }

  
        public string Salary { get; set; }
        public DateTime? TopHot { get; set; }
    }
}
