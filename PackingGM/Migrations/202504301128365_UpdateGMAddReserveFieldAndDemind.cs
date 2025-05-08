namespace PackingGM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateGMAddReserveFieldAndDemind : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GMs", "Demind", c => c.String());
            AddColumn("dbo.GMs", "DemindDate", c => c.DateTime());
            AddColumn("dbo.GMs", "ReserveField", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GMs", "ReserveField");
            DropColumn("dbo.GMs", "DemindDate");
            DropColumn("dbo.GMs", "Demind");
        }
    }
}
