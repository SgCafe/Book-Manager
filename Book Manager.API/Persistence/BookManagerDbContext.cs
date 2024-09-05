using Book_Manager.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Book_Manager.API.Persistence;

public class BookManagerDbContext : DbContext
{
    public BookManagerDbContext(DbContextOptions<BookManagerDbContext> options) 
        : base()
    {
        
    }

    public List<Book> Books { get; set; }
    public List<Loan> Loans { get; set; }
    public List<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder
            .Entity<User>(e =>
            {
                e.HasKey(u => u.Id);

                e.HasMany(u => u.Loans)
                    .WithOne(l => l.User)
                    .HasForeignKey(l => l.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

        builder
            .Entity<Loan>(e =>
            {
                e.HasKey(l => l.Id);

                e.HasOne(l => l.User)
                    .WithMany(u => u.Loans)
                    .HasForeignKey(l => l.UserId);

                e.HasOne(l => l.Book)
                    .WithMany(b => b.Loans)
                    .HasForeignKey(l => l.BookId);
            });
        
        builder
            .Entity<Book>(e =>
            {
                e.HasKey(b => b.Id);

                e.HasMany(b => b.Loans)
                    .WithOne(l => l.Book)
                    .HasForeignKey(l => l.BookId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        
        base.OnModelCreating(builder);
    }
}