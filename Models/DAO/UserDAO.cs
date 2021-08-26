using Models.Common;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class UserDAO
    {
        static long _Id;
        FreeCyDB db = null;

        private static UserDAO _Instance;
        public static UserDAO Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new UserDAO();
                }
                return _Instance;
            }
            set
            {
                ;
            }
        }


        public UserDAO()
        {
            db = new FreeCyDB();
        }

        public int SaveChanges()
        {
            try
            {
                return db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + ": " + x.ErrorMessage));
                throw new DbEntityValidationException(errorMessages);
            }
        }
        public String GetNameById(int id)//fffffffffffffffff
        {
            return (db.Users.SingleOrDefault(x => x.ID_User == id)).UserName;
        }
        public bool CheckId(int id)
        {
            return db.Users.Count(x => x.ID_User == id) > 0;
        }
        public bool CheckUserName(string userName)
        {
            return db.Users.Count(x => x.UserName == userName) > 0;
        }
        public bool CheckUserEmail(string email)
        {
            return db.Users.Count(x => x.Email == email) > 0;
        }
        public int Insert(User entity)
        {

            db.Users.Add(entity);
            SaveChanges();
            return entity.ID_User;
        }
        public List<User> ListUsers(int top, int soTrang)
        {
            
            return db.Users.Where(x=>x.Status == true).OrderByDescending(x => x.ID_User).Skip((soTrang - 1) * top).Take(top).ToList();
        }
        public int GetTotalRecords()
        {
            return db.Users.OrderByDescending(x => x.ID_User).Count();
        }
        public string GetUserNameById(int id)
        {
            return (db.Users.SingleOrDefault(x => x.ID_User == id)).UserName;
        }
        public List<User> ListUser(int top)
        {
            return db.Users.Where(x=>x.Status == true).OrderByDescending(x => x.CreateAt).Take(top).ToList();
        }
        public long InsertForFacebook(User entity)
        {
            var user = db.Users.SingleOrDefault(x => x.UserName == entity.UserName);
            if (user == null)
            {
                db.Users.Add(entity);
                SaveChanges();
                return entity.ID_User;
            }
            else
            {
                return user.ID_User;
            }

        }
        public void ValidateOnSaveEnabled()
        {
            db.Configuration.ValidateOnSaveEnabled = false;
        }
        public User CheckUserByActivationCode(Guid code)
        {
            return db.Users.Where(x => x.ActivationCode == code).FirstOrDefault();
        }
        public int Login(string userName, string passWord, bool isLoginAdmin = false)
        {
            var result = db.Users.SingleOrDefault(x => x.UserName == userName);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (isLoginAdmin == true)
                {
                    if (result.GroupID == CommonConstant.ADMIN_GROUP || result.GroupID == CommonConstant.MOD_GROUP)
                    {
                        if (result.Status == false)
                        {
                            return -1;
                        }
                        else
                        {
                            if (result.Password == passWord)
                                return 1;
                            else
                                return -2;
                        }
                    }
                    else
                    {
                        return -3;
                    }
                }
                else
                {
                    if (result.Status == false)
                    {
                        return -1;
                    }
                    else
                    {
                        if (result.Password == passWord)
                            return 1;
                        else
                            return -2;
                    }
                }
            }
        }

        public List<string> GetListCredential(string userName)
        {
            var user = db.Users.Single(x => x.UserName == userName);
            var data = (from a in db.Credentials
                        join b in db.UserGroups on a.UserGroupID equals b.ID
                        join c in db.Roles on a.RoleID equals c.ID
                        where b.ID == user.GroupID
                        select new
                        {
                            RoleID = a.RoleID,
                            UserGroupID = a.UserGroupID
                        }).AsEnumerable().Select(x => new Credential()
                        {
                            RoleID = x.RoleID,
                            UserGroupID = x.UserGroupID
                        });
            return data.Select(x => x.RoleID).ToList();

        }
        public User GetById(string userName)
        {
            return db.Users.SingleOrDefault(x => x.UserName == userName);
        }
        public void SaveUserId(long id)
        {
            _Id = id;
        }
        public long GetUserId()
        {
            return _Id;
        }
        public List<User> getUser()
        {
            return db.Users.ToList();
        }
        public User ViewDetails(long id)
        {
            return db.Users.Find(id);
        }
        public User GetUserByEmail(string email)
        {
            return db.Users.Where(x => x.Email == email).FirstOrDefault();
        }
        public User ViewProfile(int ID_User)
        {
            return db.Users.Find(ID_User);
        }
        public User GetUserByResetCode(string code)
        {
            User user = new User();
            List<User> list = db.Users.ToList();
            foreach (User item in list)
            {
                if (item.ResetPasswordCode == code)
                {
                    user = item;
                }
            }
            return user;
        }




        public bool Update(User entity, int ID)
        {
            try
            {
                User entitys = new User();
                var user = db.Users.Find(ID);
                user.UserName = entity.UserName;
                if (!string.IsNullOrEmpty(entity.Password))
                {
                    user.Password = entity.Password;
                }
                user.Address = entity.Address;
                user.Email = entity.Email;
                user.Birthday = entity.Birthday;
                user.Phone = entity.Phone;
                user.Skill = entity.Skill;
                user.Exper = entity.Exper;
                user.ExpertTime = entity.ExpertTime;
                user.Introduce = entity.Introduce;
                user.WorkTime = entity.WorkTime;
                user.SalaryUser = entity.SalaryUser;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex);
                return false;
            }
        }
        public bool UpdateDAO(User entity)
        {
            try
            {
                var user = db.Users.Find(entity.ID_User);
                user.UserName = entity.UserName;
                user.Address = entity.Address;
                user.Phone = entity.Phone;
                //user.FavoriteCategory = entity.FavoriteCategory;
                SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public User EditProfileDAO(int id)
        {
            return db.Users.Where(x => x.ID_User == id).FirstOrDefault();
        }
        public void AddDAO(User entity)
        {
            try
            {
                db.Users.Add(entity);
                db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                
            }

        }
        public void DeleteDAO(User entity, long id)
        {
            try
            {
                entity = db.Users.Find(id);
                db.Users.Remove(entity);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error" + e);
            }
        }
        public string GetUserFullName(long id)
        {
            return ViewDetails(id).UserName.ToString();
        }
        public User FindUserDAO(int id)
        {
            return db.Users.Find(id);
        }
        public void EditUserDAO(User user)
        {
            db.Entry(user).State = EntityState.Modified;
            SaveChanges();
        }
        public bool DeleteUser(User user)
        {
            try
            {
                var item = db.Users.Find(user.ID_User);
                item.Status = false;
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}