using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AssignColor.Models
{
    public class ProductColor
    {
        [Key]
        public Int64 ProductColorID{ get; set; }
        [ForeignKey("Product")]
        public Int64 ProductID { get; set; }
        [ForeignKey("Color")]
        public Int64 ColorID { get; set; }
        public virtual Product Product { get; set; }
        public virtual Color Color { get; set; }

    }
}