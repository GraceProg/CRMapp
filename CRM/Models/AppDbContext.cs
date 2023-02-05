using System.Reflection.Emit;

using CRM.Models.DBClasses;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CRM.Models
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        //public AppDbContext()
        //{

        //}
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerCall> CustomerCalls { get; set; }

    }
}
