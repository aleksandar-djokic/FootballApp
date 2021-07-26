namespace FootballApp.Domain.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FootballApp.Domain.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FootballApp.Domain.Models.ApplicationDbContext context)
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
           
            context.TeamRoles.AddOrUpdate(
                x => x.Id,
                new Models.TeamRole() { Id = 1, Name = "Owner", AdminPrivilege = true },
                new Models.TeamRole() { Id = 2, Name = "Admin", AdminPrivilege = true },
                new Models.TeamRole() { Id = 3, Name = "Member", AdminPrivilege = false }
                );
        }
    }
}
