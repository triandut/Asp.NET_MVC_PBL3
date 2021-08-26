using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Framework
{
    public class UserUpdate
    {
        [Key]
        public int ID_User { get; set; }

        
        [StringLength(20)]
        public string UserName { get; set; }

        
        [StringLength(Int32.MaxValue)]

        public string Password { get; set; }

        
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(20)]
        public string GroupID { set; get; }
        public int? Phone { get; set; }

        public DateTime? Birthday { get; set; }

        public bool? Status { get; set; }

        public bool? isFreeLancer { get; set; }

        public bool? isEmployer { get; set; }

        [StringLength(500)]
        public string ResetPasswordCode { get; set; }
        [StringLength(50)]
        public string Skill { get; set; }
        [Column(TypeName = "ntext")]
        public string Introduce { get; set; }
        [Column(TypeName = "ntext")]
        public string Educate { get; set; }
        [StringLength(50)]
        public string ExpertTime { get; set; }
        [StringLength(50)]
        public string SalaryUser { get; set; }
        public bool? isEmailVerify { get; set; }

        [Column(TypeName = "text")]
        public string Address { get; set; }

        [Column(TypeName = "text")]
        public string WorkTime { get; set; }

        public DateTime? CreateAt { get; set; }

        public Guid? ActivationCode { get; set; }

        [StringLength(Int32.MaxValue)]

        public string ConfirmPassword { get; set; }
        public string Exper { get; set; }
    }
}
