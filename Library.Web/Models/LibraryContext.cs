using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Library.Data;

namespace Library.Web.Models
{
    public class LibraryContext : IdentityDbContext<Visitor, IdentityRole<int>, int>
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Visitor>().ToTable("Visitors");
        }

        public DbSet<Book> Books { get; set; } = null!;

        public DbSet<Librarian> Librarians { get; set; } = null!;

        public DbSet<Rent> Rents { get; set; } = null!;

        public DbSet<Visitor> Visitors => Users;

        public DbSet<Volume> Volumes { get; set; } = null!;
    }
}
