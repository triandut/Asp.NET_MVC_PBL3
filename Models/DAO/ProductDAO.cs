using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.DAO;
using Models.Framework;

namespace Models.DAO
{
    public class ProductDAO
    {
        FreeCyDB db = null;
        public ProductDAO()
        {
            db = new FreeCyDB();
        }
        public List<Product> ListProduct(int top, User user)
        {
            if (top == -1 && user == null)
            {
                return db.Products.Where(o => o.Status == 1).OrderBy(o => o.ID_Category).ToList();
            }
            else if (top == -1 && user != null)
            {
                return db.Products.Where(o => o.ID_User == user.ID_User && o.Status == 1).ToList();
            }
            else
            {
                return db.Products.Where(o => o.Status == 1).OrderByDescending(x => x.ID_Category).Take(top).ToList();
            }
        }
        public List<Product> ListProduct(int top, int soTrang, User user, string sort)
        {
            switch (sort)
            {
                case "recent":
                    return db.Products.Where(x=>x.Status == 1).OrderByDescending(x => x.CreatedAt).Skip((soTrang - 1) * top).Take(top).ToList();

                case "nameDesc":
                    return db.Products.Where(x => x.Status == 1).OrderByDescending(x => x.Name).Skip((soTrang - 1) * top).Take(top).ToList();
                case "nameAsc":
                    return db.Products.Where(x => x.Status == 1).OrderBy(x => x.Name).Skip((soTrang - 1) * top).Take(top).ToList();

                case "price":
                    return db.Products.Where(x => x.Status == 1).OrderBy(x => x.Price).Skip((soTrang - 1) * top).Take(top).ToList();

            }
            return db.Products.Where(x => x.Status == 1).OrderByDescending(x => x.ID_Category).Skip((soTrang - 1) * top).Take(top).ToList();
        }
        public List<Product> ListProduct2(int top, int soTrang, User user, string sort,int ID_Product)
        {
            switch (sort)
            {
                case "recent":
                    return db.Products.Where(x => x.Status == 1).OrderByDescending(x => x.CreatedAt).Skip((soTrang - 1) * top).Take(top).ToList();

                case "nameDesc":
                    return db.Products.Where(x => x.Status == 1).OrderByDescending(x => x.Name).Skip((soTrang - 1) * top).Take(top).ToList();
                case "nameAsc":
                    return db.Products.Where(x => x.Status == 1).OrderBy(x => x.Name).Skip((soTrang - 1) * top).Take(top).ToList();

                case "price":
                    return db.Products.Where(x => x.Status == 1).OrderBy(x => x.Price).Skip((soTrang - 1) * top).Take(top).ToList();

            }
            return db.Products.Where(x => x.Status == 1 && x.ID_Category == ID_Product ).OrderByDescending(x => x.ID_Category).Skip((soTrang - 1) * top).Take(top).ToList();
        }
        public List<ProductViewModel> Search(string keyword, ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        {
            totalRecord = db.Products.Where(x => x.Name == keyword).Count();
            var model = (from a in db.Products
                         
                         where a.Name.Contains(keyword)
                         select new
                         {
                            
                             ID_Product = a.ID_Product,
                             ID_User = a.ID_User,
                             Description = a.Description,
                            
                             Status  = a.Status,
                             CreatedAt = a.CreatedAt,
                             UpdatedAt = a.CreatedAt,
                             
                             Respons = a.Respons,
                             Experience = a.Respons,
                             Education = a.Education,
                             Salary = a.Salary,
                             TopHot = a.TopHot,

                             Image = a.Image,
                             Name = a.Name,
                             MetaTitle = a.MetaTitle,
                             Price = a.Price
                         }).AsEnumerable().Select(x => new ProductViewModel()
                         {

                             ID_Product = x.ID_Product,
                             ID_User = x.ID_User,
                             Description = x.Description,

                             Status = x.Status,
                             CreatedAt = x.CreatedAt,
                             UpdatedAt = x.CreatedAt,

                             Respons = x.Respons,
                             Experience = x.Respons,
                             Education = x.Education,
                             Salary = x.Salary,
                             TopHot = x.TopHot,
                             Image = x.Image,
                             Name = x.Name,
                             MetaTitle = x.MetaTitle,
                             Price = x.Price
                         });
            model.OrderByDescending(x => x.CreatedAt).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return model.ToList();
        }
        public List<Product> ListFeatureProduct(int top , User user)
        {
            return db.Products.Where(x => x.TopHot != null && x.TopHot > DateTime.Now).OrderByDescending(x => x.CreatedAt).Take(top).ToList();
        }

        public int GetTotalRecords()
        {
            return db.Products.OrderByDescending(x => x.ID_Category).Count();
        }
        public List<User> ListUser(int top)
        {
            return db.Users.OrderByDescending(x => x.CreateAt).Take(top).ToList();
        }
      
        public Product ViewDetail(int productID )
        {
            return db.Products.Find(productID);
        }
        //public IEnumerable<Product> ListAllPaging(int page, int pageSize)
        //{
        //    return db.Products.OrderByDescending(x => x.CreatedAt).ToPagedList(page, pageSize);
        //}

        public int Create(Product product, int id_user)
        {
            product.ID_User = id_user;
            product.Status = 1;
            product.CreatedAt = DateTime.Now;
            db.Products.Add(product);
            db.SaveChanges();
            return product.ID_Product;
        }

        public bool Update(Product product )
        {
            try
            {
                var item = db.Products.Find(product.ID_Product);
                item.ID_Category = product.ID_Category;
                item.Name = product.Name;
                item.Experience = product.Experience;
                item.Description = product.Description;
                item.Price = product.Price;
                item.Status = product.Status;
                //item.Website = product.Website;
                //item.Email = product.Email;
                //item.Phone = product.Phone;
                item.Education = product.Education;
                item.Salary = product.Salary;
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool Delete(Product product)
        {
            try
            {
                var item = db.Products.Find(product.ID_Product);
                item.Status = 0;
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public List<ProductViewModel> ListByCategoryId(User user,int categoryID, ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        {
            totalRecord = db.Products.Where(x => x.ID_Category == categoryID).Count();
            var model = (from a in db.Products
                         join b in db.Categories
                         on a.ID_Category equals b.ID_Category
                         where a.ID_Category == categoryID
                         select new
                         {
                             //CateMetaTitle = b.MetaTitle,
                             Name = a.Name,                        
                             Experience = a.Experience,
                             ID = a.ID_Product,
                             //ID_Product = a.ID_Product,
                             Images = a.Image,
                             Education = a.Education,
                             UserName = a.User.UserName,
                             Addres = a.User.Address,
                             WorkTime = a.User.WorkTime,
                             Status = a.Status,                        
                             MetaTitle = a.MetaTitle,
                             Price = a.Price,
                             Respons = a.Respons,
                             CreatedAt = a.CreatedAt
                         }).AsEnumerable().Select(x => new ProductViewModel()
                         {                        
                             Name = x.Name,
                             Experience = x.Experience,
                             ID_Product = x.ID,                         
                             Image = x.Images,
                             Education = x.Education,
                             UserName = x.UserName,
                             Address = x.Addres,
                             WorkTime = x.WorkTime,
                             Status = x.Status,                         
                             MetaTitle = x.MetaTitle,
                             Price = x.Price,                        
                             Respons = x.Respons,
                             CreatedAt = x.CreatedAt
                         });
            model.Where(x=>x.Status == 1).OrderByDescending(x => x.CreatedAt).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return model.ToList();
        }
    }
}