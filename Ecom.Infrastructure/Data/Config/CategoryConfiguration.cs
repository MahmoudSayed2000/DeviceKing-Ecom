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
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.name)
                .IsRequired()
                .HasMaxLength(30);
            builder.Property(c => c.Id).IsRequired();

            builder.HasData(
                new Category
                {
                    Id = 1,
                    name = "Test Category",
                    description = "Test Category Description"
                }
            );
        }
    }
}
