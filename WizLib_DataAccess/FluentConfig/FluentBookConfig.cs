using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WizLib_Model.Models;

namespace WizLib_DataAccess.FluentConfig
{
    public class FluentBookConfig : IEntityTypeConfiguration<Fluent_Book>
    {
        public void Configure(EntityTypeBuilder<Fluent_Book> builder)
        {
            // Define Name of Table if you want to change

            // Define Primary Key
            builder.HasKey(b => b.BookId);

            // Define Props
            builder.Property(b => b.ISBN).IsRequired().HasMaxLength(15);
            builder.Property(b => b.Title).IsRequired();
            builder.Property(b => b.Price).IsRequired();
            
            // Define Relationships

            // One-to-One Relationship Fluent_Book and Fluent_BookDetail
            builder.HasOne(b => b.Fluent_BookDetail)
                .WithOne(bd => bd.Fluent_Book)
                //.HasForeignKey<Fluent_Book>("BookDetailId");
                .HasForeignKey<Fluent_Book>(b => b.BookDetailId);    

            // One-To-Many Relationship between Book and Publisher
            builder.HasOne(b => b.Fluent_Publisher)
                .WithMany(p => p.Fluent_Books)
                .HasForeignKey(b => b.PublisherId);
                
        }
    }
}