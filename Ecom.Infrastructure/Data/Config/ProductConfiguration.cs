using Ecom.Core.Entites.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.name).IsRequired();
            builder.Property(p => p.description).IsRequired();
            builder.Property(p => p.NewPrice).HasColumnType("decimal(18,2)");

            builder.HasData(
                new Product
                {
                    Id = 1,
                    name = "Test Product",
                    description = "Test Product Description",
                    NewPrice = 99.99m,
                    categoryId = 1
                }
            );
        }
    }
}
