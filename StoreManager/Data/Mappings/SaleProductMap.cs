using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManager.Models;

namespace StoreManager.Data.Mappings;

public class SaleProductMap : IEntityTypeConfiguration<SaleProduct>
{
    public void Configure(EntityTypeBuilder<SaleProduct> builder)
    {
        builder
            .ToTable("sales_products");

        builder
            .HasKey(x => new { x.SaleId, x.ProductId });

        builder
            .Property(x => x.Quantity)
            .HasColumnName("quantity")
            .IsRequired();

        builder
            .Property(x => x.ProductId)
            .HasColumnName("product_id");

        builder
            .Property(x => x.SaleId)
            .HasColumnName("sale_id");
    }
}
