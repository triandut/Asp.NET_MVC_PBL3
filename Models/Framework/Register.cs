using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Framework
{
    public class Register
    {
        [Key]
        public int ID_User { get; set; }

        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "*Yêu cầu nhập tên đăng nhập !")]
        public string UserName { get; set; }
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "*Yêu cầu nhập mật khẩu !")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "*Độ dài mật khẩu ít nhất 6 ký tự !")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Xác nhận mật khẩu không khớp")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        public bool? isEmailVerify { get; set; }
        public Guid? ActivationCode { get; set; }
    }
}
