using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
namespace Models.Framework
{
    [Table("Order")]
    public partial class Order
    {

        public Order()
        {
            UploadFiles = new HashSet<UploadFile>();
        }
        [Key]
        public int ID_Order { get; set; }



        public int ID_Product { get; set; }



        public int ID_User { get; set; }



        public int? Quantity { get; set; }



        public double? Price { get; set; }



        [Column(TypeName = "ntext")]
        public string Description { get; set; }



        public bool? isFinish { get; set; }



        public bool? isCancel { get; set; }



        [Column(TypeName = "date")]
        public DateTime? CreatedAt { get; set; }



        [Column(TypeName = "date")]
        public DateTime? UpdatedAt { get; set; }



        [StringLength(500)]
        public string CompletedFile { get; set; }
        public string ShipAddress { get; set; }
        [StringLength(50)]
        public string ShipMobile { get; set; }
        [StringLength(50)]
        public string ShipName { get; set; }
        [StringLength(50)]
        public string ShipEmail { get; set; }


        public virtual Product Product { get; set; }



        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UploadFile> UploadFiles { get; set; }
    }
}
