using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Library.Models
{
  public class LibraryContext : IdentityDbContext<ApplicationUser>
  {
    public DbSet<Book> Books { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<BookGenre> BookGenres { get; set; }
    public DbSet<BookSubject> BookSubjects { get; set; }
    public LibraryContext(DbContextOptions options) : base(options) { }
  }
}