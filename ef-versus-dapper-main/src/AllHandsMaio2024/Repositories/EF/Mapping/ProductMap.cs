using AllHandsMaio2024.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AllHandsMaio2024.Repositories.EF.Mapping;

public class ProductMap : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnType("VARCHAR(100)");

        builder.Property(x => x.Price)
            .IsRequired()
            .HasColumnType("DECIMAL(17,2)");

        builder.HasOne(x => x.Brand)
            .WithMany();
    }
}
