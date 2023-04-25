using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManager.Models;

namespace StoreManager.Data.Mappings;

public class ProductMap : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder
            .ToTable("products");

        builder
            .HasKey(x => x.Id)
            .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

        builder
            .Property(x => x.Id)
            .HasColumnName("id");

        builder
            .Property(x => x.Name)
            .HasColumnName("name")
            .HasMaxLength(30);
    }
}
