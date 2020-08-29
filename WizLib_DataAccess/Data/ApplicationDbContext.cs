using System;
using Microsoft.EntityFrameworkCore;
using WizLib_DataAccess.FluentConfig;
using WizLib_Model.Models;

namespace WizLib_DataAccess.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {   
        }

        public DbSet<BookDetailsFromView> BookDetailsFromView { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookDetail> BookDetails { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<SampleAnnotation> SampleAnnotations { get; set; }  // Note! Map to tb_SampleAnnotations

        // Fluent API Models
        public DbSet<Fluent_BookDetail> Fluent_BookDetails { get; set; }
        public DbSet<Fluent_Book> Fluent_Books { get; set; }
        public DbSet<Fluent_Author> Fluent_Authors { get; set; }
        public DbSet<Fluent_Publisher> Fluent_Publishers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Setup Many-to-Many relationship with Annotation between Book and Author using BookAuthor link table with Fluent API
            // 1.) Define composite keys
            modelBuilder.Entity<BookAuthor>().HasKey(ba => new {ba.AuthorId, ba.BookId});   

            // Table Name and Column Name
            modelBuilder.Entity<SampleAnnotation>().ToTable("tbl_SampleAnnotations");
            modelBuilder.Entity<SampleAnnotation>().Property(s => s.SampleName).HasColumnName("Name");



            // Fluent API Exercise
            // Note! Moved to FluentBookDetailConfig.cs
            // Fluent_BookDetail
            /*
            modelBuilder.Entity<Fluent_BookDetail>().HasKey(b => b.BookDetailId);
            modelBuilder.Entity<Fluent_BookDetail>().Property(b => b.NumberOfChapters).IsRequired();
            */

            // Note! Moved to FluentBookConfig.cs
            // Fluent_Book
            /*
            modelBuilder.Entity<Fluent_Book>().HasKey(b => b.BookId);
            modelBuilder.Entity<Fluent_Book>().Property(b => b.ISBN).IsRequired().HasMaxLength(15);
            modelBuilder.Entity<Fluent_Book>().Property(b => b.Title).IsRequired();
            modelBuilder.Entity<Fluent_Book>().Property(b => b.Price).IsRequired();
            
            // One-to-One Relationship Fluent_Book and Fluent_BookDetail
            modelBuilder.Entity<Fluent_Book>()
                .HasOne(b => b.Fluent_BookDetail)
                .WithOne(bd => bd.Fluent_Book)
                //.HasForeignKey<Fluent_Book>("BookDetailId");
                .HasForeignKey<Fluent_Book>(b => b.BookDetailId);    
            */

            // Note! Moved to FluentAuthorConfig.cs
            // Fluent_Author
            /*
            modelBuilder.Entity<Fluent_Author>().HasKey(b => b.AuthorId);
            modelBuilder.Entity<Fluent_Author>().Property(b => b.FirstName).IsRequired();
            modelBuilder.Entity<Fluent_Author>().Property(b => b.LastName).IsRequired();
            modelBuilder.Entity<Fluent_Author>().Ignore(b => b.FullName);           // Not Mapped
            */

            /*  Note! Moved to FluentPublisherConfig.cs   
            // Fluent_Publisher
            modelBuilder.Entity<Fluent_Publisher>().HasKey(b => b.PublisherId);
            modelBuilder.Entity<Fluent_Publisher>().Property(b => b.Name).IsRequired();
            modelBuilder.Entity<Fluent_Publisher>().Property(b => b.Location).IsRequired();
            */

            // Note! Moved to FluentBookConfig.cs
            // One-To-Many Relationship between Book and Publisher
            /*
            modelBuilder.Entity<Fluent_Book>()
                .HasOne(b => b.Fluent_Publisher)
                .WithMany(p => p.Fluent_Books)
                .HasForeignKey(b => b.PublisherId);
            */    

            // Note! Moved to FluentBookAuthorConfig.cs
            /*
            // Setup Many-to-Many relationship between Fluent_Book and Fluent_Author using Fluent_BookAuthor link table with Fluent API
            // 1.) Define composite primary key
            modelBuilder.Entity<Fluent_BookAuthor>().HasKey(ba => new {ba.AuthorId, ba.BookId});   
            // 2.) The 1st set of 2 one-to-many relationship composition of many-to-many relationship
            modelBuilder.Entity<Fluent_BookAuthor>()
                .HasOne(ba => ba.Fluent_Book)
                .WithMany(b => b.Fluent_BookAuthors)
                .HasForeignKey(ba => ba.BookId);    
            // 3.) The 2nd set of 2 one-to-many relationship composition of many-to-many relationship
            modelBuilder.Entity<Fluent_BookAuthor>()
                .HasOne(ba => ba.Fluent_Author)
                .WithMany(a => a.Fluent_BookAuthors)
                .HasForeignKey(ba => ba.AuthorId);    
            */

            // Note! Apply Configuration from each Config class
            modelBuilder.ApplyConfiguration(new FluentBookConfig());
            modelBuilder.ApplyConfiguration(new FluentBookDetailConfig());
            modelBuilder.ApplyConfiguration(new FluentPublisherConfig());
            modelBuilder.ApplyConfiguration(new FluentAuthorConfig());
            modelBuilder.ApplyConfiguration(new FluentBookAuthorConfig());


            // Setup a View; Note! View has no Primary Key and Object will not be tracked by EF Core
            modelBuilder.Entity<BookDetailsFromView>().HasNoKey().ToView("GetOnlyBookDetails");
        }
    }
}