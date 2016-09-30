namespace Radera.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Radera.Models.RaderaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Radera.Models.RaderaContext";
        }

        protected override void Seed(Radera.Models.RaderaContext context)
        {
            
            Category cat1 = new Category() { CategoryName = "Trips" };
            Category cat2 = new Category() { CategoryName = "Clothes" };
            Category cat3 = new Category() { CategoryName = "Cars" };
            Category cat4 = new Category() { CategoryName = "Electronics" };

            User admin = new User() { Username = "admin", Password = "123", isAdmin = true };
            

            var categories = from c in context.Category
                            select c;


            if (!context.Users.Any(u => u.isAdmin == true))
            {
                context.Users.Add(admin);
            }

            if (categories.Count() == 0)
            {
                context.Category.Add(cat1);
                context.Category.Add(cat2);
                context.Category.Add(cat3);
                context.Category.Add(cat4);

                context.SaveChanges();
            }
        }
    }
}
