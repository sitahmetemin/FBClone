using FBClone.DataAccess.Concrete;

namespace FBClone.DataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FBClone.DataAccess.Concrete.FBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FBClone.DataAccess.Concrete.FBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Users.AddOrUpdate(x => x.Id, 
            new User()
            {
                Id = 1,
                FirstName = "Ahmet Emin",
                LastName = "ÞÝT",
                DisplayName = "sitahmetemin",
                Email = "sitahmetemin@hotmail.com",
                Password = "123",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                isDelete = false,
                
            });

            context.Shareds.AddOrUpdate(x => x.Id, 
                new Shared()
                {
                    Content = "Lorem ipsum sit dolor amert ekedf kgewfsla ",
                    UserId = 1,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    isDelete = false
                },
                new Shared()
                {
                    Content = "Lorem ipsum sit dolor amert ekedf kgewfsla Lorem ipsum sit dolor amert ekedf kgewfsla Lorem ipsum sit dolor amert ekedf kgewfsla Lorem ipsum sit dolor amert ekedf kgewfsla Lorem ipsum sit dolor amert ekedf kgewfsla ",
                    UserId = 1,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    isDelete = false
                },
                new Shared()
                {
                    Content = "Lorem ipsum sit dolor amert ekedf kgewfsla Lorem ipsum sit dolor amert ekedf kgewfsla ",
                    UserId = 1,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    isDelete = false
                },
                new Shared()
                {
                    Content = "Lorem ipsum sit dolor amert ekedf kgewfsla ",
                    UserId = 1,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    isDelete = false
                },
                new Shared()
                {
                    Content = "Lorem ipsum sit ",
                    UserId = 1,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    isDelete = false
                }
                );
        }
    }
}
