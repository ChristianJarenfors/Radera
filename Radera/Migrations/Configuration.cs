namespace Radera.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections.Generic;
    using Radera.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Radera.Models.RaderaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Radera.Models.RaderaContext";
        }

        protected override void Seed(Radera.Models.RaderaContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            User Chrille = new User
            {
                Username="Balin",
                FirstName = "Chrille",
                LastName = "Jforce",
                Address = "BestStreet 5",
                City = "Kungälv",
                Gender = "Man",
                Mail = "ChrilleJforce@Mega.com",
                Password = "123",
                PhoneNumber=070222344
            };
            User Alex = new User
            {
                Username = "Litos",
                FirstName = "Alexander",
                LastName = "Lastname",
                Address = "CoolStreet 5",
                City = "Göteborg",
                Gender = "Man",
                Mail = "LitosMan@Mega.com",
                Password = "123",
                PhoneNumber = 070225544
            };
            User Tony = new User
            {
                Username = "Toni",
                FirstName = "Antonio",
                LastName = "Lastname",
                Address = "SwagStreet 5",
                City = "Gothenburg",
                Gender = "Man",
                Mail = "Toniforce@Mega.com",
                Password = "123",
                PhoneNumber = 0734444344
            };
            User Linus = new User
            {
                Username = "Cooly",
                FirstName = "Linus",
                LastName = "Eriksson",
                Address = "LongStreet 5",
                City = "Vännersborg",
                Gender = "Man",
                Mail = "LinusEriksson@Mega.com",
                Password = "123",
                PhoneNumber = 076222344
            };
            List<User> UserList = new List<User>();
            if (!context.Users.Contains(Chrille))
            {
                context.Users.AddOrUpdate(Chrille);
            }
            if (!context.Users.Contains(Alex))
            {
                context.Users.AddOrUpdate(Alex);
            }
            if (!context.Users.Contains(Tony))
            {
                context.Users.AddOrUpdate(Tony);
            }
            if (!context.Users.Contains(Linus))
            {
                context.Users.AddOrUpdate(Linus);
            }
            
        }
    }
}
