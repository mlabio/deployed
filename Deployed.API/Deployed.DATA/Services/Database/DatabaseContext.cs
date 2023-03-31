using Deployed.Data.Services.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deployed.Data.Services.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) 
            : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerDetail> CustomerDetails { get; set; }
        public DbSet<Dependent> Dependents { get; set; }
        public DbSet<DependentDetail> DependentDetails { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder) { }
    }
}
