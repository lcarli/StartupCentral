namespace StartupCentral.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Change2 : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LogLogins", "user_ID", "dbo.User");
            DropIndex("dbo.LogLogins", new[] { "user_ID" });
            DropTable("dbo.LogLogins");
        }
    }
}
