using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Restaurant.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name ="Full Name"),MaxLength(20),Required]
        public string FirstName { get; set; }
        [MaxLength(20), Required]
        public string LastName { get; set; }
        [NotMapped]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
}
