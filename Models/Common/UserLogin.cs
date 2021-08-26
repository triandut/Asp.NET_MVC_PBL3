using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Common
{
    [Serializable]
    public class UserLogin
    {
        public long ID_User { set; get; }
        public string UserName { set; get; }
        public string GroupID { set; get; }
    }
}
