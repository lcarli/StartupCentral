namespace StartupCentral.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewData : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Startupbs", new[] { "benefício_ID" });
            DropIndex("dbo.Startupbs", new[] { "status_ID" });
            AddColumn("dbo.Startupbs", "MicrosoftAccount", c => c.String(nullable: false));
            CreateIndex("dbo.Startupbs", "Benefício_ID");
            CreateIndex("dbo.Startupbs", "Status_ID");
            DropColumn("dbo.Startupbs", "msa");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Startupbs", "msa", c => c.String(nullable: false));
            DropIndex("dbo.Startupbs", new[] { "Status_ID" });
            DropIndex("dbo.Startupbs", new[] { "Benefício_ID" });
            DropColumn("dbo.Startupbs", "MicrosoftAccount");
            CreateIndex("dbo.Startupbs", "status_ID");
            CreateIndex("dbo.Startupbs", "benefício_ID");
        }
    }
}
