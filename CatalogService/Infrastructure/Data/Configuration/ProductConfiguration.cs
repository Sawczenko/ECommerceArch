using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(product => product.Id).IsRequired();
            builder.Property(product => product.ProductName).IsRequired().HasMaxLength(100);
            builder.Property(product => product.Description).IsRequired();
            builder.Property(product => product.Price).HasColumnType("decimal(18,2)");
            builder.Property(product => product.PictureUrl).IsRequired();
            builder.HasOne(brand => brand.ProductBrand).WithMany().HasForeignKey(product => product.ProductBrandID);
            builder.HasOne(type => type.ProductType).WithMany().HasForeignKey(product => product.ProductTypeID);
        }
    }
}
