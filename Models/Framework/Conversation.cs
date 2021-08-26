namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Conversation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID_Conves { get; set; }

        public int ID_User { get; set; }

        [Required]
        [StringLength(36)]
        public string ID_MessRoom { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedAt { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UpdatedAt { get; set; }

        public virtual MessRoom MessRoom { get; set; }

        public virtual User User { get; set; }
    }
}
