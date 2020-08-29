using Microsoft.EntityFrameworkCore;
using WizLib_Model.Models;

namespace WizLib_DataAccess.FluentConfig
{
    public class FluentAuthorConfig : IEntityTypeConfiguration<Fluent_Author>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Fluent_Author> builder)
        {
            builder.HasKey(b => b.AuthorId);
            builder.Property(b => b.FirstName).IsRequired();
            builder.Property(b => b.LastName).IsRequired();
            builder.Ignore(b => b.FullName);           // Not Mapped
        }
    }
}