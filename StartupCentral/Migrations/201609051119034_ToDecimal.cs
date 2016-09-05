namespace StartupCentral.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ToDecimal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Startupbs", "ConsumoMes", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Startupbs", "ConsumoAcumulado", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Startupbs", "ConsumoPago", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Startupbs", "ConsumoPago", c => c.Double(nullable: false));
            AlterColumn("dbo.Startupbs", "ConsumoAcumulado", c => c.Double(nullable: false));
            AlterColumn("dbo.Startupbs", "ConsumoMes", c => c.Double(nullable: false));
        }
    }
}
