namespace PackingGM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOtherFieldForGM : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GMs", "Waybill", c => c.Int());
            AddColumn("dbo.GMs", "WaybillDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.GMs", "WhyDelay", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GMs", "WhyDelay");
            DropColumn("dbo.GMs", "WaybillDate");
            DropColumn("dbo.GMs", "Waybill");
        }
    }
}
