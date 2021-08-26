using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Framework
{
    public class Login
    {
        [Key]
        [Required(ErrorMessage = "*Mời nhập user name")]
        public string UserName { set; get; }

        [Required(ErrorMessage = "*Mời nhập password")]
        public string Password { set; get; }
    }
}
