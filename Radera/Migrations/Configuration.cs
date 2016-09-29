namespace Radera.Migrations
{
    using Radera.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Radera.Models.RaderaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
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
            #region Users created
            User Chrille = new User
            {
                Username = "Balin",
                FirstName = "Chrille",
                LastName = "Jforce",
                Address = "BestStreet 5",
                City = "Kungälv",
                Gender = "Man",
                Mail = "ChrilleJforce@Mega.com",
                Password = "123",
                PhoneNumber = 070222344,
                isAdmin = true
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
                PhoneNumber = 070225544,
                isAdmin = true
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
                PhoneNumber = 0734444344,
                isAdmin = true

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
                PhoneNumber = 076222344,
                isAdmin = true
            };
            User Random = new User
            {
                Username = "Random",
                FirstName = "Random",
                LastName = "Random",
                Address = "Random 5",
                City = "Random",
                Gender = "Kvinna",
                Mail = "Random@Mega.com",
                Password = "123",
                PhoneNumber = 076222344,
                isAdmin = false
            };
            #endregion
            #region Users added
            if (context.Users.Count() == 0)
            {
                context.Users.AddOrUpdate(Chrille, Alex, Tony, Linus, Random);
            }
            else
            {
                if (!context.Users.Any(u => u.Username == Chrille.Username))
                {
                    context.Users.AddOrUpdate(Chrille);
                }
                if (!context.Users.Any(u => u.Username == Alex.Username))
                {
                    context.Users.AddOrUpdate(Alex);
                }
                if (!context.Users.Any(u => u.Username == Tony.Username))
                {
                    context.Users.AddOrUpdate(Tony);
                }
                if (!context.Users.Any(u => u.Username == Linus.Username))
                {
                    context.Users.AddOrUpdate(Linus);
                }
                if (!context.Users.Any(u => u.Username == Random.Username))
                {
                    context.Users.AddOrUpdate(Random);
                }
            }
            #endregion
            context.SaveChanges();
            #region Auctions created
            Auction Bluebike = new Auction
            {
                Title = "Blue Bike in its best Gears",
                AuctionOwner = context.Users.Where(u => u.Username == "Balin").First(),
                Picture = "http://edcolor.com/images/Blue%20Bike.jpg",
                Description = "Finally i have to sell my old precious bike. It's very blue and i love it very much. Hopefully you will too! :)",
                StartPrice = 150000
            };
            Auction ComfySofa = new Auction
            {
                Title = "Comfy sofa with orund curves",
                AuctionOwner = context.Users.Where(u => u.Username == "Litos").First(),
                Picture = "http://www.broadwayfurniture.net/wp-content/uploads/2015/10/Ashley-Bradington-Sofa.gif?3247be",
                Description = "Finally i have to sell my old precious sofa. It's very comfy and i love it very much. Hopefully you will too! :)",
                StartPrice = 20000
            };
            Auction FreshNails = new Auction
            {
                Title = "Fresh nails for your Home",
                AuctionOwner = context.Users.Where(u => u.Username == "Toni").First(),
                Picture = "http://www.carpentrypages.com/images/nails.jpg",
                Description = "These are some really qualiti nails which are going to make you the finest of homes. Can also be used for carpentry. Buy now!! :)",
                StartPrice = 200
            };
            Auction AppleTV = new Auction
            {
                Title = "Apple Tv for you Entertainment",
                AuctionOwner = context.Users.Where(u => u.Username == "Cooly").First(),
                Picture = "http://www.cutiegadget.com/pict/apple_tv.jpg",
                Description = "This apparatus will give you a fresh fun time. It fun to watch and if you get hungry you can take a crunch at the exterior. Love it a lot! Buy NOW! :)",
                StartPrice = 3000
            };
            #endregion
            #region Auctions added
            if (context.Auctions.Count() == 0)
            {
                context.Auctions.AddOrUpdate(Bluebike, ComfySofa, FreshNails, AppleTV);
            }
            else
            {
                if (!context.Auctions.Any(a => a.Title == Bluebike.Title))
                {
                    context.Auctions.AddOrUpdate(Bluebike);
                }
                if (!context.Auctions.Any(a => a.Title == ComfySofa.Title))
                {
                    context.Auctions.AddOrUpdate(ComfySofa);
                }
                if (!context.Auctions.Any(a => a.Title == FreshNails.Title))
                {
                    context.Auctions.AddOrUpdate(FreshNails);
                }
                if (!context.Auctions.Any(a => a.Title == AppleTV.Title))
                {
                    context.Auctions.AddOrUpdate(AppleTV);
                }
            }
            #endregion
            context.SaveChanges();
            #region StartBids
            Bid Bike1 = new Bid
            {
                BidAmount = context.Auctions.Single(a => a.Title == Bluebike.Title).StartPrice * +111,
                Date = new DateTime(2016, 08, 03),
                BidOwner = context.Users.Single(u => u.Username == "Balin"),
                ThisAuction = context.Auctions.Single(a => a.Title == Bluebike.Title)
            };
            Bid Sofa1 = new Bid
            {
                BidAmount = context.Auctions.Single(a => a.Title == ComfySofa.Title).StartPrice * +111,
                Date = new DateTime(2016, 08, 03),
                BidOwner = context.Users.Single(u => u.Username == "Balin"),
                ThisAuction = context.Auctions.Single(a => a.Title == ComfySofa.Title)
            };
            Bid Nails1 = new Bid
            {
                BidAmount = context.Auctions.Single(a => a.Title == FreshNails.Title).StartPrice * +111,
                Date = new DateTime(2016, 08, 03),
                BidOwner = context.Users.Single(u => u.Username == "Balin"),
                ThisAuction = context.Auctions.Single(a => a.Title == FreshNails.Title)
            };
            Bid Apple1 = new Bid
            {
                BidAmount = context.Auctions.Single(a => a.Title == AppleTV.Title).StartPrice * +111,
                Date = new DateTime(2016, 08, 03),
                BidOwner = context.Users.Single(u => u.Username == "Balin"),
                ThisAuction = context.Auctions.Single(a => a.Title == AppleTV.Title)
            };
            #endregion
            #region Startbids added
            if (context.Bids.Count() == 0)
            {
                context.Bids.AddOrUpdate(Bike1, Sofa1, Nails1, Apple1);
            }
            #endregion
            context.SaveChanges();
            #region ExtraBid Bike
            Bid Bike2 = new Bid
            {
                BidAmount = context.Auctions.Single(a => a.Title == Bluebike.Title).Bids.OrderBy(x => x.BidAmount).Last().BidAmount + 112,
                Date = new DateTime(2016, 08, 04),
                BidOwner = context.Users.Single(u => u.Username == "Litos"),
                ThisAuction = context.Auctions.Single(a => a.Title == Bluebike.Title)
            };
            if (!context.Bids.Any(b => b.BidAmount == Bike2.BidAmount && b.BidOwner.Username == Bike2.BidOwner.Username && b.Date == Bike2.Date && b.ThisAuction.Title == Bike2.ThisAuction.Title))
            {
                context.Bids.AddOrUpdate(Bike2);
            }
            context.SaveChanges();
            Bid Bike3 = new Bid
            {
                BidAmount = context.Auctions.Single(a => a.Title == Bluebike.Title).Bids.OrderBy(x => x.BidAmount).Last().BidAmount + 112,
                Date = new DateTime(2016, 08, 05),
                BidOwner = context.Users.Single(u => u.Username == "Toni"),
                ThisAuction = context.Auctions.Single(a => a.Title == Bluebike.Title)
            };
            if (!context.Bids.Any(b => b.BidAmount == Bike3.BidAmount && b.BidOwner.Username == Bike3.BidOwner.Username && b.Date == Bike3.Date && b.ThisAuction.Title == Bike3.ThisAuction.Title))
            {
                context.Bids.AddOrUpdate(Bike3);
            }
            context.SaveChanges();
            Bid Bike4 = new Bid
            {
                BidAmount = context.Auctions.Single(a => a.Title == Bluebike.Title).Bids.OrderBy(x => x.BidAmount).Last().BidAmount + 112,
                Date = new DateTime(2016, 08, 06),
                BidOwner = context.Users.Single(u => u.Username == "Cooly"),
                ThisAuction = context.Auctions.Single(a => a.Title == Bluebike.Title)
            };
            if (!context.Bids.Any(b => b.BidAmount == Bike4.BidAmount && b.BidOwner.Username == Bike4.BidOwner.Username && b.Date == Bike4.Date && b.ThisAuction.Title == Bike4.ThisAuction.Title))
            {
                context.Bids.AddOrUpdate(Bike4);
            }
            context.SaveChanges();
            #endregion
            #region ExtraBid Sofa
            Bid Sofa2 = new Bid
            {
                BidAmount = context.Auctions.Single(a => a.Title == ComfySofa.Title).Bids.OrderBy(x => x.BidAmount).Last().BidAmount + 112,
                Date = new DateTime(2016, 08, 03),
                BidOwner = context.Users.Single(u => u.Username == "Cooly"),
                ThisAuction = context.Auctions.Single(a => a.Title == ComfySofa.Title)
            }; if (!context.Bids.Any(b => b.BidAmount == Sofa2.BidAmount && b.BidOwner.Username == Sofa2.BidOwner.Username && b.Date == Sofa2.Date && b.ThisAuction.Title == Sofa2.ThisAuction.Title))
            {
                context.Bids.AddOrUpdate(Sofa2);
            }
            context.SaveChanges();
            Bid Sofa3 = new Bid
            {
                BidAmount = context.Auctions.Single(a => a.Title == ComfySofa.Title).Bids.OrderBy(x => x.BidAmount).Last().BidAmount + 112,
                Date = new DateTime(2016, 08, 03),
                BidOwner = context.Users.Single(u => u.Username == "Litos"),
                ThisAuction = context.Auctions.Single(a => a.Title == ComfySofa.Title)
            };
            if (!context.Bids.Any(b => b.BidAmount == Sofa3.BidAmount && b.BidOwner.Username == Sofa3.BidOwner.Username && b.Date == Sofa3.Date && b.ThisAuction.Title == Sofa3.ThisAuction.Title))
            {
                context.Bids.AddOrUpdate(Sofa3);
            }
            context.SaveChanges();
            #endregion
            #region ExtraBid Tv
            Bid Apple2 = new Bid
            {
                BidAmount = context.Auctions.Single(a => a.Title == AppleTV.Title).Bids.OrderBy(x => x.BidAmount).Last().BidAmount + 112,
                Date = new DateTime(2016, 08, 03),
                BidOwner = context.Users.Single(u => u.Username == "Toni"),
                ThisAuction = context.Auctions.Single(a => a.Title == AppleTV.Title)
            };
            if (!context.Bids.Any(b => b.BidAmount == Apple2.BidAmount && b.BidOwner.Username == Apple2.BidOwner.Username && b.Date == Apple2.Date && b.ThisAuction.Title == Apple2.ThisAuction.Title))
            {
                context.Bids.AddOrUpdate(Apple2);
            }
            #endregion
        }
    }
}
