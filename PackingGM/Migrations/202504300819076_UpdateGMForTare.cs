namespace PackingGM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateGMForTare : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GMs", "PromisedProvisionPeriod", c => c.DateTime());
            AddColumn("dbo.GMs", "Comment", c => c.String());
            AddColumn("dbo.GMs", "ServiceNote", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GMs", "ServiceNote");
            DropColumn("dbo.GMs", "Comment");
            DropColumn("dbo.GMs", "PromisedProvisionPeriod");
        }
    }
}
