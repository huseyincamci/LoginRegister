using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace LoginRegisterFromScratch.Models
{
    public class LoginRegisterDbContex : IdentityDbContext<ApplicationUser>
    {
        public LoginRegisterDbContex() : base("DefaultConnection")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();  
            base.OnModelCreating(modelBuilder);
        }
    }
}