using Luqma.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Infrastructure.Configurations
{
    public class CartConfigurations : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("carts", c =>
            {
               c.HasCheckConstraint("CK_OrderItem_Quantity_Positive", "[Quantity] > 0");
            });
            builder.HasKey(cart => cart.Id);

            builder.HasOne(cart => cart.Customer)
                .WithMany(customer => customer.Carts)
                .HasForeignKey(cart => cart.CustomerId);

            builder.HasOne(cart => cart.MenuItem)
                .WithMany(menuitem => menuitem.Carts)
                .HasForeignKey(cart => cart.ItemId);
        }
    }
}
