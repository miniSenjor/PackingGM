namespace PackingGM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.GMs", "PlannedDeadline", c => c.DateTime());
            AlterColumn("dbo.GMs", "WaybillDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.GMs", "WaybillDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.GMs", "PlannedDeadline", c => c.DateTime(nullable: false));
        }
    }
}
