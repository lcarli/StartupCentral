namespace StartupCentral.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "email", c => c.String());
            DropColumn("dbo.User", "alias");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "alias", c => c.String(nullable: false));
            DropColumn("dbo.User", "email");
        }
    }
}
