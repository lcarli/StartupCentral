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

        protected override void Seed(StartupDBContext context)
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
                new Status { StatusId = 8, Nome = "Azure BS+" },
                new Status { StatusId = 9, Nome = "Deletada" },
                new Status { StatusId = 10, Nome = "Graduada" },
                new Status { StatusId = 11, Nome = "Suspensa" },
                new Status { StatusId = 12, Nome = "Declinada" }
                );

            context.Aceleradora.AddOrUpdate(p => p.Nome,
                new Aceleradora { Nome = "Nenhuma", BeneficioId = listBeneficios.Where(n => n.BeneficioId == 2).SingleOrDefault().BeneficioId }
                );

            context.Roles.AddOrUpdate(p => p.Nome,
                new Roles { RoleId = 1, Nome = "Audience Marketing Manager", Descricao = "Responsável pela gestão da métrica, com olhar Business." },
                new Roles { RoleId = 2, Nome = "Technical Evangelist Lead", Descricao = "Responsável pela gestão da métrica, com olhar Técnico. Além de ser o técnico responsável por algumas Startups e da gestão de relacionamentos." },
                new Roles { RoleId = 3, Nome = "Technical Evangelist", Descricao = "Responsável pelo suporte técnico a algumas Startups." },
                new Roles { RoleId = 4, Nome = "Intern", Descricao = "Responsável pelo suporte ao time de gestão." },
                new Roles { RoleId = 5, Nome = "DX Manager", Descricao = "Responsável pela gestão da métrica, com olhar Business. Além disso, monitora o suporte técnico." },
                new Roles { RoleId = 6, Nome = "DX Director", Descricao = "Responsável Geral." },
                new Roles { RoleId = 7, Nome = "Audience Evangelism Manager", Descricao = "Responsável pelo evangelismo educacional" }
                );

            context.User.AddOrUpdate(p => p.nome,
                new User { UserId = 1, nome = "Lucas Humenhuk", email = "luhumenh@microsoft.com", RoleId = 2 },
                new User { UserId = 1, nome = "Elis Queiroz", email = "elisq@microsoft.com", RoleId = 1 },
                new User { UserId = 1, nome = "Alessandro Jannuzzi", email = "aljannuz@microsoft.com", RoleId = 5 },
                new User { UserId = 1, nome = "Richard Chaves", email = "rchaves@microsoft.com", RoleId = 6 },
                new User { UserId = 1, nome = "Marcelo Miranda", email = "t-mamira@microsoft.com", RoleId = 4 },
                new User { UserId = 1, nome = "Rodrigo Dias", email = "rodias@microsoft.com", RoleId = 7 }
                );
        }
    }
}
