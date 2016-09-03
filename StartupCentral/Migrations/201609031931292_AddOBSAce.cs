namespace StartupCentral.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOBSAce : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Aceleradora", "Observacoes", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Aceleradora", "Observacoes");
        }
    }
}
