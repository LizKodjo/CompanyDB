using CompanyDB.Data.Entity;
using CompanyDB.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyDB.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<CompanyModel> Companies { get; set; }
        public DbSet<EmployeeModel> Employees { get; set; }
    }
}
