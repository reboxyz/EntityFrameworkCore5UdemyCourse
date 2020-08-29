using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WizLib_Model.Models;

namespace WizLib_DataAccess.FluentConfig
{
    public class FluentPublisherConfig : IEntityTypeConfiguration<Fluent_Publisher>
    {
        public void Configure(EntityTypeBuilder<Fluent_Publisher> builder)
        {
            builder.HasKey(b => b.PublisherId);
            builder.Property(b => b.Name).IsRequired();
            builder.Property(b => b.Location).IsRequired();
        }
    }
}