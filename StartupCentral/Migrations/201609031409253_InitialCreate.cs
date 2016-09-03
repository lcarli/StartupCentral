namespace StartupCentral.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Aceleradora",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        nome = c.String(nullable: false),
                        Benefício_ID = c.Guid(nullable: false),
                        Endereço_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Benefício", t => t.Benefício_ID)
                .ForeignKey("dbo.Endereço", t => t.Endereço_ID)
                .Index(t => t.Benefício_ID)
                .Index(t => t.Endereço_ID);
            
            CreateTable(
                "dbo.Benefício",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Nome = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Contato",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Nome = c.String(nullable: false),
                        Telefone = c.String(),
                        Email = c.String(nullable: false),
                        TipoDoContato = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Startupbs",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Nome = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        MicrosoftAccount = c.String(nullable: false),
                        BizSparkID = c.String(),
                        ConsumoMes = c.Double(nullable: false),
                        ConsumoAcumulado = c.Double(nullable: false),
                        ConsumoPago = c.Double(nullable: false),
                        Observações = c.String(),
                        Owner = c.String(),
                        Aceleradora_ID = c.Guid(),
                        Benefício_ID = c.Guid(),
                        Status_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Aceleradora", t => t.Aceleradora_ID)
                .ForeignKey("dbo.Benefício", t => t.Benefício_ID)
                .ForeignKey("dbo.Status", t => t.Status_ID)
                .Index(t => t.Aceleradora_ID)
                .Index(t => t.Benefício_ID)
                .Index(t => t.Status_ID);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Nome = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Endereço",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Rua = c.String(nullable: false),
                        Numero = c.String(),
                        Complemento = c.String(),
                        CEP = c.String(),
                        Estado = c.String(),
                        País = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.LogLogins",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        datetime = c.DateTime(nullable: false),
                        user_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.User", t => t.user_ID)
                .Index(t => t.user_ID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        nome = c.String(nullable: false),
                        email = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ContatoAceleradoras",
                c => new
                    {
                        Contato_ID = c.Guid(nullable: false),
                        Aceleradora_ID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Contato_ID, t.Aceleradora_ID })
                .ForeignKey("dbo.Contato", t => t.Contato_ID)
                .ForeignKey("dbo.Aceleradora", t => t.Aceleradora_ID)
                .Index(t => t.Contato_ID)
                .Index(t => t.Aceleradora_ID);
            
            CreateTable(
                "dbo.StartupbsContatoes",
                c => new
                    {
                        Startupbs_ID = c.Guid(nullable: false),
                        Contato_ID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Startupbs_ID, t.Contato_ID })
                .ForeignKey("dbo.Startupbs", t => t.Startupbs_ID)
                .ForeignKey("dbo.Contato", t => t.Contato_ID)
                .Index(t => t.Startupbs_ID)
                .Index(t => t.Contato_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LogLogins", "user_ID", "dbo.User");
            DropForeignKey("dbo.Aceleradora", "Endereço_ID", "dbo.Endereço");
            DropForeignKey("dbo.Startupbs", "Status_ID", "dbo.Status");
            DropForeignKey("dbo.StartupbsContatoes", "Contato_ID", "dbo.Contato");
            DropForeignKey("dbo.StartupbsContatoes", "Startupbs_ID", "dbo.Startupbs");
            DropForeignKey("dbo.Startupbs", "Benefício_ID", "dbo.Benefício");
            DropForeignKey("dbo.Startupbs", "Aceleradora_ID", "dbo.Aceleradora");
            DropForeignKey("dbo.ContatoAceleradoras", "Aceleradora_ID", "dbo.Aceleradora");
            DropForeignKey("dbo.ContatoAceleradoras", "Contato_ID", "dbo.Contato");
            DropForeignKey("dbo.Aceleradora", "Benefício_ID", "dbo.Benefício");
            DropIndex("dbo.StartupbsContatoes", new[] { "Contato_ID" });
            DropIndex("dbo.StartupbsContatoes", new[] { "Startupbs_ID" });
            DropIndex("dbo.ContatoAceleradoras", new[] { "Aceleradora_ID" });
            DropIndex("dbo.ContatoAceleradoras", new[] { "Contato_ID" });
            DropIndex("dbo.LogLogins", new[] { "user_ID" });
            DropIndex("dbo.Startupbs", new[] { "Status_ID" });
            DropIndex("dbo.Startupbs", new[] { "Benefício_ID" });
            DropIndex("dbo.Startupbs", new[] { "Aceleradora_ID" });
            DropIndex("dbo.Aceleradora", new[] { "Endereço_ID" });
            DropIndex("dbo.Aceleradora", new[] { "Benefício_ID" });
            DropTable("dbo.StartupbsContatoes");
            DropTable("dbo.ContatoAceleradoras");
            DropTable("dbo.User");
            DropTable("dbo.LogLogins");
            DropTable("dbo.Endereço");
            DropTable("dbo.Status");
            DropTable("dbo.Startupbs");
            DropTable("dbo.Contato");
            DropTable("dbo.Benefício");
            DropTable("dbo.Aceleradora");
        }
    }
}
