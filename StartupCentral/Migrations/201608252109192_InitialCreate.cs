namespace StartupCentral.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Aceleradoras",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        nome = c.String(nullable: false),
                        Benefício_ID = c.Guid(nullable: false),
                        Endereço_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Benefício", t => t.Benefício_ID, cascadeDelete: true)
                .ForeignKey("dbo.Endereço", t => t.Endereço_ID)
                .Index(t => t.Benefício_ID)
                .Index(t => t.Endereço_ID);
            
            CreateTable(
                "dbo.Benefício",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        nome = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Contatoes",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        nome = c.String(nullable: false),
                        telefone = c.String(),
                        email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Startupbs",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        nome = c.String(nullable: false),
                        email = c.String(nullable: false),
                        msa = c.String(nullable: false),
                        BizSparkID = c.String(),
                        ConsumoMes = c.Double(nullable: false),
                        ConsumoAcumulado = c.Double(nullable: false),
                        ConsumoPago = c.Double(nullable: false),
                        Aceleradora_ID = c.Guid(),
                        benefício_ID = c.Guid(),
                        status_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Aceleradoras", t => t.Aceleradora_ID)
                .ForeignKey("dbo.Benefício", t => t.benefício_ID)
                .ForeignKey("dbo.Status", t => t.status_ID)
                .Index(t => t.Aceleradora_ID)
                .Index(t => t.benefício_ID)
                .Index(t => t.status_ID);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        nome = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Endereço",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        rua = c.String(nullable: false),
                        numero = c.String(),
                        complemento = c.String(),
                        cep = c.String(),
                        estado = c.String(),
                        país = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        nome = c.String(nullable: false),
                        alias = c.String(nullable: false),
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
                .ForeignKey("dbo.Contatoes", t => t.Contato_ID, cascadeDelete: true)
                .ForeignKey("dbo.Aceleradoras", t => t.Aceleradora_ID, cascadeDelete: true)
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
                .ForeignKey("dbo.Startupbs", t => t.Startupbs_ID, cascadeDelete: true)
                .ForeignKey("dbo.Contatoes", t => t.Contato_ID, cascadeDelete: true)
                .Index(t => t.Startupbs_ID)
                .Index(t => t.Contato_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Aceleradoras", "Endereço_ID", "dbo.Endereço");
            DropForeignKey("dbo.Startupbs", "status_ID", "dbo.Status");
            DropForeignKey("dbo.StartupbsContatoes", "Contato_ID", "dbo.Contatoes");
            DropForeignKey("dbo.StartupbsContatoes", "Startupbs_ID", "dbo.Startupbs");
            DropForeignKey("dbo.Startupbs", "benefício_ID", "dbo.Benefício");
            DropForeignKey("dbo.Startupbs", "Aceleradora_ID", "dbo.Aceleradoras");
            DropForeignKey("dbo.ContatoAceleradoras", "Aceleradora_ID", "dbo.Aceleradoras");
            DropForeignKey("dbo.ContatoAceleradoras", "Contato_ID", "dbo.Contatoes");
            DropForeignKey("dbo.Aceleradoras", "Benefício_ID", "dbo.Benefício");
            DropIndex("dbo.StartupbsContatoes", new[] { "Contato_ID" });
            DropIndex("dbo.StartupbsContatoes", new[] { "Startupbs_ID" });
            DropIndex("dbo.ContatoAceleradoras", new[] { "Aceleradora_ID" });
            DropIndex("dbo.ContatoAceleradoras", new[] { "Contato_ID" });
            DropIndex("dbo.Startupbs", new[] { "status_ID" });
            DropIndex("dbo.Startupbs", new[] { "benefício_ID" });
            DropIndex("dbo.Startupbs", new[] { "Aceleradora_ID" });
            DropIndex("dbo.Aceleradoras", new[] { "Endereço_ID" });
            DropIndex("dbo.Aceleradoras", new[] { "Benefício_ID" });
            DropTable("dbo.StartupbsContatoes");
            DropTable("dbo.ContatoAceleradoras");
            DropTable("dbo.Users");
            DropTable("dbo.Endereço");
            DropTable("dbo.Status");
            DropTable("dbo.Startupbs");
            DropTable("dbo.Contatoes");
            DropTable("dbo.Benefício");
            DropTable("dbo.Aceleradoras");
        }
    }
}
