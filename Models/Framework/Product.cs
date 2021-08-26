namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            Bids = new HashSet<Bid>();
        }

        [Key]
        public int ID_Product { get; set; }

        public int ID_Category { get; set; }

        public int ID_User { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        public double? Price { get; set; }

        public int? Status { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdatedAt { get; set; }

        [StringLength(300)]
        public string Image { get; set; }

        [StringLength(100)]
        public string MetaTitle { get; set; }

        [Column(TypeName = "ntext")]
        public string Respons { get; set; }

        [Column(TypeName = "ntext")]
        public string Experience { get; set; }

        [Column(TypeName = "ntext")]
        public string Benifit { get; set; }

        [Column(TypeName = "ntext")]
        public string Education { get; set; }

        [StringLength(50)]
        public string Salary { get; set; }
        public DateTime? TopHot { get; set; }
        public int PartnerID { get; set; }
        [StringLength(200)]
        public string CateName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bid> Bids { get; set; }

        public virtual Category Category { get; set; }


        public virtual User User { get; set; }
    }
}
