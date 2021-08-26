using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class UploadFileDAO
    {
        FreeCyDB db = null;
        public UploadFileDAO()
        {
            db = new FreeCyDB();
        }

        public void Create(UploadFile file)
        {
            db.UploadFiles.Add(file);
            db.SaveChanges();
        }

        public List<UploadFile> GetUploadFiles(int order_id)
        {
            return db.UploadFiles.Where(o => o.ID_Order == order_id).ToList();
        }

        public string GetFileName(int id)
        {
            return db.UploadFiles.Find(id).Filename;
        }

        public string GetPath(int id)
        {
            return db.UploadFiles.Find(id).Path;
        }
    }
}
