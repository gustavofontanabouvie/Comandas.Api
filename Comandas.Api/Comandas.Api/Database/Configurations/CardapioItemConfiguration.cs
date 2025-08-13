using Comandas.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Comandas.Api.Database.Configurations;

public class CardapioItemConfiguration : IEntityTypeConfiguration<CardapioItem>
{
    public void Configure(EntityTypeBuilder<CardapioItem> builder)
    {
        builder.Property(ci => ci.Preco)
            .HasColumnType("decimal(10,2)");
    }
}
