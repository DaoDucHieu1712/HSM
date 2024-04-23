using HSM.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HSM.Persistance
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder) =>
            builder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);

        public DbSet<Department> Departments { get; set; }
    }
}
