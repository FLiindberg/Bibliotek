using Bibliotek.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bibliotek.Data
{
    public class BibliotekContext : DbContext
    {

        public DbSet<Book> Books { get; set; }
        public DbSet<Bookloan> Bookloans { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }

        public BibliotekContext(DbContextOptions<BibliotekContext> options) : base(options)
        {

        }

        public DbSet<Bibliotek.Models.Author> Authour { get; set; }

        public DbSet<Bibliotek.Models.Customer> Customer { get; set; }

        public DbSet<Bibliotek.Models.Bookloan> Bookloan { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) // Måste säga åt entity Framework hur vi vill att denna many-tomany-ralation ska vara. 
        {
            modelBuilder.Entity<BookAuthor>()
                .HasKey(ba => new { ba.BookId, ba.AuthorId });

            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Book)
                .WithMany(b => b.BookAuthors)
                .HasForeignKey(ba => ba.BookId);

            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Author)
                .WithMany(a => a.BookAuthors)
                .HasForeignKey(ba => ba.AuthorId);


        }

        public DbSet<Bibliotek.Models.Inventory> Inventory { get; set; }
    }
}
