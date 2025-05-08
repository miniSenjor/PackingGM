namespace PackingGM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteTableManufactoryGM : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ManufactoryGMs", "ManyfactoryId", "dbo.Manufactories");
            DropForeignKey("dbo.ManufactoryGMs", "GMId", "dbo.GMs");
            DropIndex("dbo.ManufactoryGMs", new[] { "ManyfactoryId" });
            DropIndex("dbo.ManufactoryGMs", new[] { "GMId" });
            AddColumn("dbo.GMs", "ManufactoryId", c => c.Int());
            AddColumn("dbo.GMs", "PR", c => c.String());
            CreateIndex("dbo.GMs", "ManufactoryId");
            AddForeignKey("dbo.GMs", "ManufactoryId", "dbo.Manufactories", "Id");
            DropTable("dbo.ManufactoryGMs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ManufactoryGMs",
                c => new
                    {
                        ManyfactoryId = c.Int(nullable: false),
                        GMId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ManyfactoryId, t.GMId });
            
            DropForeignKey("dbo.GMs", "ManufactoryId", "dbo.Manufactories");
            DropIndex("dbo.GMs", new[] { "ManufactoryId" });
            DropColumn("dbo.GMs", "PR");
            DropColumn("dbo.GMs", "ManufactoryId");
            CreateIndex("dbo.ManufactoryGMs", "GMId");
            CreateIndex("dbo.ManufactoryGMs", "ManyfactoryId");
            AddForeignKey("dbo.ManufactoryGMs", "GMId", "dbo.GMs", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ManufactoryGMs", "ManyfactoryId", "dbo.Manufactories", "Id", cascadeDelete: true);
        }
    }
}
