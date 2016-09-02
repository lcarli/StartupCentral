namespace StartupCentral.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<StartupCentral.Models.StartupDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(StartupCentral.Models.StartupDBContext context)
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

            context.Benefício.AddOrUpdate(p => p.Nome,
                new Models.Benefício { ID = Guid.NewGuid(), Nome = "BizSpark" },
                new Models.Benefício { ID = Guid.NewGuid(), Nome = "BizSpark PLUS" },
                new Models.Benefício { ID = Guid.NewGuid(), Nome = "BizSpark Sponsorship" }
                );

            context.Status.AddOrUpdate(p => p.Nome,
                new Models.Status { ID = Guid.NewGuid(), Nome = "Não Inscrito"},
                new Models.Status { ID = Guid.NewGuid(), Nome = "WIN" },
                new Models.Status { ID = Guid.NewGuid(), Nome = "BizSpark" },
                new Models.Status { ID = Guid.NewGuid(), Nome = "BizSpark PLUS" },
                new Models.Status { ID = Guid.NewGuid(), Nome = "Aguardando BizSpark" },
                new Models.Status { ID = Guid.NewGuid(), Nome = "Aguardando BizSpark PLUS" },
                new Models.Status { ID = Guid.NewGuid(), Nome = "Azure BS" },
                new Models.Status { ID = Guid.NewGuid(), Nome = "Azure BS+" }
                );
        }
    }
}
