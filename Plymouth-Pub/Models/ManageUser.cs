using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plymouth_Pub.Models
{
    public class ManageUser
    {
        [Required] 
        public string Id { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "{0} should at least {2} digit.", MinimumLength = 1)] 
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}