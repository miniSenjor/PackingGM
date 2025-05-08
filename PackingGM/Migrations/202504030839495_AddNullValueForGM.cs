namespace PackingGM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNullValueForGM : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GMs", "GMNumberId", "dbo.GMNumbers");
            DropForeignKey("dbo.GMs", "SPUTareId", "dbo.SPUTares");
            DropIndex("dbo.GMs", new[] { "GMNumberId" });
            DropIndex("dbo.GMs", new[] { "SPUTareId" });
            AlterColumn("dbo.GMs", "GMNumberId", c => c.Int());
            AlterColumn("dbo.GMs", "SPUTareId", c => c.Int());
            CreateIndex("dbo.GMs", "GMNumberId");
            CreateIndex("dbo.GMs", "SPUTareId");
            AddForeignKey("dbo.GMs", "GMNumberId", "dbo.GMNumbers", "Id");
            AddForeignKey("dbo.GMs", "SPUTareId", "dbo.SPUTares", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GMs", "SPUTareId", "dbo.SPUTares");
            DropForeignKey("dbo.GMs", "GMNumberId", "dbo.GMNumbers");
            DropIndex("dbo.GMs", new[] { "SPUTareId" });
            DropIndex("dbo.GMs", new[] { "GMNumberId" });
            AlterColumn("dbo.GMs", "SPUTareId", c => c.Int(nullable: false));
            AlterColumn("dbo.GMs", "GMNumberId", c => c.Int(nullable: false));
            CreateIndex("dbo.GMs", "SPUTareId");
            CreateIndex("dbo.GMs", "GMNumberId");
            AddForeignKey("dbo.GMs", "SPUTareId", "dbo.SPUTares", "Id", cascadeDelete: true);
            AddForeignKey("dbo.GMs", "GMNumberId", "dbo.GMNumbers", "Id", cascadeDelete: true);
        }
    }
}
