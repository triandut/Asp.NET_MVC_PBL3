using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreeCy.Common
{

    [Serializable]
    public class UserLogin
    {
        public long ID_User { set; get; }
        public string UserName { set; get; }
    }

}