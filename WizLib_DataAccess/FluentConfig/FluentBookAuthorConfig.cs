using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WizLib_Model.Models;

namespace WizLib_DataAccess.FluentConfig
{
    public class FluentBookAuthorConfig : IEntityTypeConfiguration<Fluent_BookAuthor>
    {
        public void Configure(EntityTypeBuilder<Fluent_BookAuthor> builder)
        {
            // Setup Many-to-Many relationship between Fluent_Book and Fluent_Author using Fluent_BookAuthor link table with Fluent API
            // 1.) Define composite primary key
            builder.HasKey(ba => new {ba.AuthorId, ba.BookId});   
            // 2.) The 1st set of 2 one-to-many relationship composition of many-to-many relationship
            builder.HasOne(ba => ba.Fluent_Book)
                .WithMany(b => b.Fluent_BookAuthors)
                .HasForeignKey(ba => ba.BookId);    
            // 3.) The 2nd set of 2 one-to-many relationship composition of many-to-many relationship
            builder.HasOne(ba => ba.Fluent_Author)
                .WithMany(a => a.Fluent_BookAuthors)
                .HasForeignKey(ba => ba.AuthorId);    
        }
    }
}