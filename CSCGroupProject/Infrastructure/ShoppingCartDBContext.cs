using CSCGroupProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CSCGroupProject.Infrastructure
{
    public class ShoppingCartDBContext : DbContext
    {
        public virtual DbSet<Product> Products { get; set; }
        public ShoppingCartDBContext()
        {
            Database.SetInitializer<ShoppingCartDBContext>(new ShoppingCartInitializer());
        }
    }
}