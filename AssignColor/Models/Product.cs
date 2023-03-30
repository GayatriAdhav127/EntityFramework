using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssignColor.Models
{
    public class Product
    {
        [Key]
        public Int64 ProductID { get; set; }
        public string ProductName { get; set; }
        public string MfgName { get; set; }
        public decimal Price { get; set; }
        public virtual List<ProductColor>ProductColors { get; set; }
    }
}