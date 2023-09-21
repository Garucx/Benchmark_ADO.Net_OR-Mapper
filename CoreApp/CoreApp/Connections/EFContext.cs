using CoreApp.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CoreApp.Connections
{
    public class EFContext : DbContext
    {
        public EFContext()
        {

        }
        public virtual DbSet<Personen> Personen { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server =.; Database = benchmark; Trusted_Connection = True;TrustServerCertificate=True;");
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Personen>(x =>
            {
                x.HasKey(x => x.ID);
                x.Property(xx => xx.ID).ValueGeneratedOnAdd();

                x.Property(x => x.Adresse).IsRequired().HasMaxLength(50);
                x.Property(x => x.Name).IsRequired().HasMaxLength(50);
                x.Property(x => x.Email).IsRequired().HasMaxLength(50);
            });
        }
    }
}
