namespace PackingGM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeNumberOrderToStr : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "Number", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Number", c => c.Int(nullable: false));
        }
    }
}
