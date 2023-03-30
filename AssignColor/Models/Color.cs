using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssignColor.Models
{
    public class Color
    {
        [Key]
        public Int64 ColorID{ get; set; }
        public string ColorName { get; set; }
        public   virtual List<ProductColor>ProductColors { get; set; }
    }
}