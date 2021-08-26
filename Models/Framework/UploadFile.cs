using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Models.Framework
{
    [Table("UploadFile")]
    public partial class UploadFile
    {
        public int Id { get; set; }



        public int ID_Order { get; set; }



        [StringLength(50)]
        public string Filename { get; set; }



        [StringLength(500)]
        public string Path { get; set; }



        public virtual Order Order { get; set; }
    }
}
