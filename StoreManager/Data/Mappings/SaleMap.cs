using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManager.Models;

namespace StoreManager.Data.Mappings;

public class SaleMap : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("sales");

        builder
            .HasKey(x => x.Id)
            .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

        builder
            .Property(x => x.Id)
            .HasColumnName("id");

        builder
            .Property(x => x.Date)
            .HasColumnName("date")
            .HasDefaultValue(DateTime.UtcNow);
    }
}
