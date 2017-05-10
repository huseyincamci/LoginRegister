using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace LoginRegisterFromScratch.Models
{
    public class ApplicationUser : IdentityUser
    {
        public short Age { get; set; }
        public string Address { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string Photo { get; set; }   
    }
}