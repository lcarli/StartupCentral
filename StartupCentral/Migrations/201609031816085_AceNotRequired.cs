namespace StartupCentral.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AceNotRequired : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Aceleradora", new[] { "Benefício_ID" });
            AlterColumn("dbo.Aceleradora", "Benefício_ID", c => c.Guid());
            CreateIndex("dbo.Aceleradora", "Benefício_ID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Aceleradora", new[] { "Benefício_ID" });
            AlterColumn("dbo.Aceleradora", "Benefício_ID", c => c.Guid(nullable: false));
            CreateIndex("dbo.Aceleradora", "Benefício_ID");
        }
    }
}
