using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plymouth_Pub.Models.OrderModel
{
    public class Table
    {
        [Required]
        [Display(Name = "Table number")]
        [StringLength(2, ErrorMessage = "Table number should be at least {2} digit"
            , MinimumLength = 2)] 
        public string TableNumber { get; set; }
    }
}