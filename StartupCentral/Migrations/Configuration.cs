namespace StartupCentral.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using StartupCentral.Models;

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

            var listBeneficios = new List<Benefício>
            {
                new Benefício { BeneficioId = 1, Nome = "BizSpark" },
                new Benefício { BeneficioId = 2, Nome = "BizSpark PLUS" },
                new Benefício { BeneficioId = 3, Nome = "BizSpark Sponsorship" }
            };
            listBeneficios.ForEach(s => context.Benefício.AddOrUpdate(p => p.Nome, s));
            context.SaveChanges();

            context.Status.AddOrUpdate(p => p.Nome,
                new Status { StatusId = 1, Nome = "Não Inscrito" },
                new Status { StatusId = 2, Nome = "WIN" },
                new Status { StatusId = 3, Nome = "BizSpark" },
                new Status { StatusId = 4, Nome = "BizSpark PLUS" },
                new Status { StatusId = 5, Nome = "Aguardando BizSpark" },
                new Status { StatusId = 6, Nome = "Aguardando BizSpark PLUS" },
                new Status { StatusId = 7, Nome = "Azure BS" },
                new Status { StatusId = 8, Nome = "Azure BS+" }
                );

            context.Aceleradora.AddOrUpdate(p => p.Nome,
                new Aceleradora { AceleradoraId = 1, Nome = "Nenhuma", Beneficio = listBeneficios.Where(n => n.Nome == "BizSpark PLUS").SingleOrDefault() }
                );
        }
    }
}
