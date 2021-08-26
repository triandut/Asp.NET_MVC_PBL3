namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProcessLog
    {
        [Key]
        public int ID_ProcessLog { get; set; }

        public int ID_Construction { get; set; }

        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        public int? Status { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedAt { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UpdatedAt { get; set; }

        public virtual Construction Construction { get; set; }
    }
}
