using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.DAO;
using Models.Framework;
namespace Models.DAO
{
    public class ProductCategoryDAO
    {
        FreeCyDB db = null;
        public ProductCategoryDAO()
        {
            db = new FreeCyDB();
        }
        public List<Category> ListAll()
        {
            return db.Categories.Where(x => x.Status == true).OrderBy(x => x.Name).ToList();
        }
        public Category ViewDetail(int productID)
        {
            return db.Categories.Find(productID);
        }
        public List<ProductInCategory> ListProductInCategory()
        {
            var result = (from p in db.Products
                         join c in db.Categories
                         on p.ID_Category equals c.ID_Category
                         where p.PartnerID == c.PartnerID
                         select new
                         {
                             Name = p.Name,
                             Experience = p.Respons,
                             Education = p.Education,
                             Salary = p.Salary,
                             Image = p.Image,
                             UserName = p.User.UserName,
                             Address = p.User.Address,
                             CreatedAt = p.User.CreateAt,
                             Price = p.Price,
                             WorkTime = p.User.WorkTime,
                             ProductId = p.ID_Product,
                             Description = p.Description
                         }).AsEnumerable().Select(x => new ProductInCategory()
                         {
                             Name = x.Name,
                             Experience = x.Experience,
                             Education = x.Education,
                             Salary = x.Salary,
                             Image = x.Image,
                             UserName = x.UserName,
                             Address = x.Address,
                             CreatedAt = x.CreatedAt,
                             Price = x.Price,
                             WorkTime = x.WorkTime,
                             ID_Product = x.ProductId,
                             Description = x.Description
                         }).ToList();

            return result;
        }

    }
}
