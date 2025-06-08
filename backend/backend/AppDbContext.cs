using backend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

//https://www.npgsql.org/efcore/?tabs=aspnet
namespace backend
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<ContactDetails> ContactDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            // Map C# enum Category to PostgreSQL enum "category"
            builder.HasPostgresEnum<ContactCategory>();
            builder.HasPostgresEnum<ContactSubcategory>();

            builder.Entity<ContactDetails>()
                .Property(cd => cd.Category)
                .HasConversion<string>();

            builder.Entity<ContactDetails>()
                .Property(cd => cd.Subcategory)
                .HasConversion<string>()
                .IsRequired(false);

            builder.Entity<Contact>()
                .HasIndex(c => c.Email)
                .IsUnique();

            // Relation configuration 1:1
            builder.Entity<Contact>()
                .HasOne(c => c.ContactDetails)
                .WithOne(cd => cd.Contact)
                .HasForeignKey<ContactDetails>(cd => cd.ContactId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(builder);
        }
    }
}
