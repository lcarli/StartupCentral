namespace StartupCentral.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Aceleradoras", newName: "Aceleradora");
            RenameTable(name: "dbo.Contatoes", newName: "Contato");
            RenameTable(name: "dbo.Users", newName: "User");
            AddColumn("dbo.Contato", "TipoDoContato", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contato", "TipoDoContato");
            RenameTable(name: "dbo.User", newName: "Users");
            RenameTable(name: "dbo.Contato", newName: "Contatoes");
            RenameTable(name: "dbo.Aceleradora", newName: "Aceleradoras");
        }
    }
}
