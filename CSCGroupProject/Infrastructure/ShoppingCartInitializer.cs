using CSCGroupProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CSCGroupProject.Infrastructure
{
    public class ShoppingCartInitializer : DropCreateDatabaseIfModelChanges<ShoppingCartDBContext>
    {
        protected override void Seed(ShoppingCartDBContext context)
        {
            context.Products.AddRange(new Product[] { 
            new Product{
                Name = "Shoe",
                Category="Fashion",
                Description="Some Random Shit",
                Price = 3000,            
            },
             new Product{
                Name = "Bag",
                Category="Fashion",
                Description="Some Random Shit",
                Price = 3000,            
            },
              new Product{
                Name = "Gown",
                Category="Fashion",
                Description="Some Random Shit",
                Price = 3000,            
            },
              new Product{
                Name = "Suit",
                Category="Fashion",
                Description="Some Random Shit",
                Price = 3000,            
            },
              new Product{
                Name = "Phone",
                Category="Technology",
                Description="Some Random Shit",
                Price = 3000,            
            },
              new Product{
                Name = "Laptop",
                Category="Technology",
                Description="Some Random Shit",
                Price = 3000,            
            },
              new Product{
                Name = "Tablet",
                Category="Technology",
                Description="Some Random Shit",
                Price = 3000,            
            },
              new Product{
                Name = "Television",
                Category="Home Appliance",
                Description="Some Random Shit",
                Price = 3000,            
            },
              new Product{
                Name = "Blender",
                Category="Home Appliance",
                Description="Some Random Shit",
                Price = 3000,            
            },
              new Product{
                Name = "Iron",
                Category="Home Appliance",
                Description="Some Random Shit",
                Price = 3000,            
            },
            });
            base.Seed(context);
        }
    }
}