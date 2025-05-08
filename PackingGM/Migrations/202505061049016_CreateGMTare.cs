namespace PackingGM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateGMTare : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GMs", "SPUTareId", "dbo.SPUTares");
            DropIndex("dbo.GMs", new[] { "SPUTareId" });
            CreateTable(
                "dbo.GMTares",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountGet = c.Int(nullable: false),
                        Demind = c.String(),
                        DemindDate = c.DateTime(),
                        PromisedProvisionPeriod = c.DateTime(),
                        Comment = c.String(),
                        ServiceNote = c.String(),
                        ReserveField = c.String(),
                        GMId = c.Int(),
                        SPUTareId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GMs", t => t.GMId)
                .ForeignKey("dbo.SPUTares", t => t.SPUTareId)
                .Index(t => t.GMId)
                .Index(t => t.SPUTareId);
            
            DropColumn("dbo.GMs", "SPUTareId");
            DropColumn("dbo.GMs", "CountGet");
            DropColumn("dbo.GMs", "Demind");
            DropColumn("dbo.GMs", "DemindDate");
            DropColumn("dbo.GMs", "PromisedProvisionPeriod");
            DropColumn("dbo.GMs", "Comment");
            DropColumn("dbo.GMs", "ServiceNote");
            DropColumn("dbo.GMs", "ReserveField");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GMs", "ReserveField", c => c.String());
            AddColumn("dbo.GMs", "ServiceNote", c => c.String());
            AddColumn("dbo.GMs", "Comment", c => c.String());
            AddColumn("dbo.GMs", "PromisedProvisionPeriod", c => c.DateTime());
            AddColumn("dbo.GMs", "DemindDate", c => c.DateTime());
            AddColumn("dbo.GMs", "Demind", c => c.String());
            AddColumn("dbo.GMs", "CountGet", c => c.Int(nullable: false));
            AddColumn("dbo.GMs", "SPUTareId", c => c.Int());
            DropForeignKey("dbo.GMTares", "SPUTareId", "dbo.SPUTares");
            DropForeignKey("dbo.GMTares", "GMId", "dbo.GMs");
            DropIndex("dbo.GMTares", new[] { "SPUTareId" });
            DropIndex("dbo.GMTares", new[] { "GMId" });
            DropTable("dbo.GMTares");
            CreateIndex("dbo.GMs", "SPUTareId");
            AddForeignKey("dbo.GMs", "SPUTareId", "dbo.SPUTares", "Id");
        }
    }
}
