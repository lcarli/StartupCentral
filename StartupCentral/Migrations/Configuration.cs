namespace StartupCentral.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
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
            var listBeneficios = new List<Beneficio>
            {
                new Beneficio { BeneficioId = 1, Nome = "BizSpark" },
                new Beneficio { BeneficioId = 2, Nome = "BizSpark PLUS" },
                new Beneficio { BeneficioId = 3, Nome = "BizSpark Sponsorship" }
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
                new Aceleradora {Nome = "Nenhuma", BeneficioId = listBeneficios.Where(n => n.BeneficioId == 2).SingleOrDefault().BeneficioId}
                );
        }
    }
}
