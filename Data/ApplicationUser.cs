using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Book.Data
{
    public class ApplicationUser: IdentityUser
    {
        [Required]
        public string Name{set;get;}

        public string? StreetAddress{set;get;}

        public string? City{set;get;}

        public string? State{set;get;}

        public string? PostalCode{set;get;}
        
    }
}