namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Payment
    {
        [Key]
        public int ID_Payment { get; set; }

        public int ID_Construction { get; set; }

        public double? Price { get; set; }

        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        public int? Status { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedAt { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UpdatedAt { get; set; }
        [StringLength(50)]
        public string ShipAddress { get; set; }
        [StringLength(50)]
        public string ShipMobile { get; set; }
        [StringLength(50)]
        public string ShipName { get; set; }
        [StringLength(50)]
        public string ShipEmail { get; set; }
        public virtual Construction Construction { get; set; }
    }
}
