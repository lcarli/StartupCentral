namespace StartupCentral.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Startupbs", "Email", c => c.String());
            AlterColumn("dbo.Startupbs", "MicrosoftAccount", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Startupbs", "MicrosoftAccount", c => c.String(nullable: false));
            AlterColumn("dbo.Startupbs", "Email", c => c.String(nullable: false));
        }
    }
}
