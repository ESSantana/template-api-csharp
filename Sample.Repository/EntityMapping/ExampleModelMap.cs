using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Core.Entities.Models;
using Sample.Repository.Context;

namespace Sample.Repository.Models.EntityMapping
{
    public class ExampleModelMap : IEntityTypeConfiguration<ExampleModel>
    {
        public readonly DbOptions _dbOptions;

        public ExampleModelMap(DbOptions dbOptions)
        {
            _dbOptions = dbOptions;
        }

        public void Configure(EntityTypeBuilder<ExampleModel> builder)
        {
            builder.ToTable("Example");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasColumnName("ID");

            builder.Property(x => x.Name)
                .HasColumnName("NAME")
                .HasMaxLength(50);

            builder.Property(x => x.Description)
                .HasColumnName("DESCRIPTION")
                .HasMaxLength(255);

        }
    }
}
