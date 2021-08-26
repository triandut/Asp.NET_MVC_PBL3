namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Message
    {
        [Key]
        public int ID_Mess { get; set; }
        [StringLength(36)]
        public string ID_MessRoom { get; set; }

        public int ID_User { get; set; }

        public int ID_MessType { get; set; }

        [Column(TypeName = "ntext")]
        public string Contents { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedAt { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UpdatedAt { get; set; }

        //public virtual MessageType MessageType { get; set; }

        public virtual MessRoom MessRoom { get; set; }

        public virtual User User { get; set; }
    }
}
