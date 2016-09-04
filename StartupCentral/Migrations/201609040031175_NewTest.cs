namespace StartupCentral.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewTest : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Aceleradora",
                c => new
                    {
                        AceleradoraId = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        BeneficioId = c.Int(nullable: false),
                        Observacoes = c.String(),
                        Endereco_EnderecoId = c.Int(),
                    })
                .PrimaryKey(t => t.AceleradoraId)
                .ForeignKey("dbo.Beneficio", t => t.BeneficioId)
                .ForeignKey("dbo.Endereco", t => t.Endereco_EnderecoId)
                .Index(t => t.BeneficioId)
                .Index(t => t.Endereco_EnderecoId);
            
            CreateTable(
                "dbo.Beneficio",
                c => new
                    {
                        BeneficioId = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.BeneficioId);
            
            CreateTable(
                "dbo.Contato",
                c => new
                    {
                        ContatoId = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Telefone = c.String(),
                        Email = c.String(nullable: false),
                        TipoDoContato = c.String(),
                    })
                .PrimaryKey(t => t.ContatoId);
            
            CreateTable(
                "dbo.Startupbs",
                c => new
                    {
                        StartupbsId = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        MicrosoftAccount = c.String(nullable: false),
                        BizSparkID = c.String(),
                        BeneficioId = c.Int(nullable: false),
                        AceleradoraId = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                        ConsumoMes = c.Double(nullable: false),
                        ConsumoAcumulado = c.Double(nullable: false),
                        ConsumoPago = c.Double(nullable: false),
                        Observacoes = c.String(),
                        Owner = c.String(),
                    })
                .PrimaryKey(t => t.StartupbsId)
                .ForeignKey("dbo.Aceleradora", t => t.AceleradoraId)
                .ForeignKey("dbo.Beneficio", t => t.BeneficioId)
                .ForeignKey("dbo.Status", t => t.StatusId)
                .Index(t => t.BeneficioId)
                .Index(t => t.AceleradoraId)
                .Index(t => t.StatusId);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        StatusId = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.StatusId);
            
            CreateTable(
                "dbo.Endereco",
                c => new
                    {
                        EnderecoId = c.Int(nullable: false, identity: true),
                        Rua = c.String(nullable: false),
                        Numero = c.String(),
                        Complemento = c.String(),
                        CEP = c.String(),
                        Estado = c.String(maxLength: 2),
                        Pais = c.String(nullable: false, maxLength: 2),
                    })
                .PrimaryKey(t => t.EnderecoId);
            
            CreateTable(
                "dbo.LogLogin",
                c => new
                    {
                        LogLoginId = c.Int(nullable: false, identity: true),
                        datetime = c.DateTime(nullable: false),
                        user_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.LogLoginId)
                .ForeignKey("dbo.User", t => t.user_UserId)
                .Index(t => t.user_UserId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        nome = c.String(nullable: false),
                        email = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.ContatoAceleradora",
                c => new
                    {
                        Contato_ContatoId = c.Int(nullable: false),
                        Aceleradora_AceleradoraId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Contato_ContatoId, t.Aceleradora_AceleradoraId })
                .ForeignKey("dbo.Contato", t => t.Contato_ContatoId)
                .ForeignKey("dbo.Aceleradora", t => t.Aceleradora_AceleradoraId)
                .Index(t => t.Contato_ContatoId)
                .Index(t => t.Aceleradora_AceleradoraId);
            
            CreateTable(
                "dbo.StartupbsContato",
                c => new
                    {
                        Startupbs_StartupbsId = c.Int(nullable: false),
                        Contato_ContatoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Startupbs_StartupbsId, t.Contato_ContatoId })
                .ForeignKey("dbo.Startupbs", t => t.Startupbs_StartupbsId)
                .ForeignKey("dbo.Contato", t => t.Contato_ContatoId)
                .Index(t => t.Startupbs_StartupbsId)
                .Index(t => t.Contato_ContatoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LogLogin", "user_UserId", "dbo.User");
            DropForeignKey("dbo.Aceleradora", "Endereco_EnderecoId", "dbo.Endereco");
            DropForeignKey("dbo.Startupbs", "StatusId", "dbo.Status");
            DropForeignKey("dbo.StartupbsContato", "Contato_ContatoId", "dbo.Contato");
            DropForeignKey("dbo.StartupbsContato", "Startupbs_StartupbsId", "dbo.Startupbs");
            DropForeignKey("dbo.Startupbs", "BeneficioId", "dbo.Beneficio");
            DropForeignKey("dbo.Startupbs", "AceleradoraId", "dbo.Aceleradora");
            DropForeignKey("dbo.ContatoAceleradora", "Aceleradora_AceleradoraId", "dbo.Aceleradora");
            DropForeignKey("dbo.ContatoAceleradora", "Contato_ContatoId", "dbo.Contato");
            DropForeignKey("dbo.Aceleradora", "BeneficioId", "dbo.Beneficio");
            DropIndex("dbo.StartupbsContato", new[] { "Contato_ContatoId" });
            DropIndex("dbo.StartupbsContato", new[] { "Startupbs_StartupbsId" });
            DropIndex("dbo.ContatoAceleradora", new[] { "Aceleradora_AceleradoraId" });
            DropIndex("dbo.ContatoAceleradora", new[] { "Contato_ContatoId" });
            DropIndex("dbo.LogLogin", new[] { "user_UserId" });
            DropIndex("dbo.Startupbs", new[] { "StatusId" });
            DropIndex("dbo.Startupbs", new[] { "AceleradoraId" });
            DropIndex("dbo.Startupbs", new[] { "BeneficioId" });
            DropIndex("dbo.Aceleradora", new[] { "Endereco_EnderecoId" });
            DropIndex("dbo.Aceleradora", new[] { "BeneficioId" });
            DropTable("dbo.StartupbsContato");
            DropTable("dbo.ContatoAceleradora");
            DropTable("dbo.User");
            DropTable("dbo.LogLogin");
            DropTable("dbo.Endereco");
            DropTable("dbo.Status");
            DropTable("dbo.Startupbs");
            DropTable("dbo.Contato");
            DropTable("dbo.Beneficio");
            DropTable("dbo.Aceleradora");
        }
    }
}
