using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library.Data
{
    public class LibraryContext : IdentityDbContext<Librarian, IdentityRole<int>, int>
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
            Database.EnsureCreated();
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Librarian>().ToTable("Librarians");
        }

        public DbSet<Book> Books { get; set; } = null!;

        public DbSet<Librarian> Librarians => Users;

        public DbSet<Rent> Rents { get; set; } = null!;

        public DbSet<Visitor> Visitors { get; set; } = null!;

        public DbSet<Volume> Volumes { get; set; } = null!;
    }
}
