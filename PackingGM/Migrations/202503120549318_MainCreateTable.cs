namespace PackingGM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MainCreateTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Aggregates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(),
                        DrawingNameAggregateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DrawingNameAggregates", t => t.DrawingNameAggregateId, cascadeDelete: true)
                .Index(t => t.DrawingNameAggregateId);
            
            CreateTable(
                "dbo.DrawingNameAggregates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AggregateTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AggregateTypes", t => t.AggregateTypeId, cascadeDelete: true)
                .Index(t => t.AggregateTypeId);
            
            CreateTable(
                "dbo.AggregateTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DrawingNameAggregateD3",
                c => new
                    {
                        DrawingNameAggregateId = c.Int(nullable: false),
                        D3Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DrawingNameAggregateId, t.D3Id })
                .ForeignKey("dbo.D3", t => t.D3Id, cascadeDelete: true)
                .ForeignKey("dbo.DrawingNameAggregates", t => t.DrawingNameAggregateId, cascadeDelete: true)
                .Index(t => t.D3Id)
                .Index(t => t.DrawingNameAggregateId);
            
            CreateTable(
                "dbo.D3",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GMs",
                c => new
                    {
                        D3Id = c.Int(nullable: false),
                        SPUId = c.Int(nullable: false),
                        NumberGM = c.String(),
                        PlannedDeadline = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.D3Id, t.SPUId })
                .ForeignKey("dbo.SPUs", t => t.SPUId, cascadeDelete: true)
                .ForeignKey("dbo.D3", t => t.D3Id, cascadeDelete: true)
                .Index(t => t.SPUId)
                .Index(t => t.D3Id);
            
            CreateTable(
                "dbo.ManufactoryGMs",
                c => new
                    {
                        ManyfactoryId = c.Int(nullable: false),
                        D3Id = c.Int(nullable: false),
                        SPUId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ManyfactoryId, t.D3Id, t.SPUId })
                .ForeignKey("dbo.Manufactories", t => t.ManyfactoryId, cascadeDelete: true)
                .ForeignKey("dbo.SPUs", t => t.SPUId, cascadeDelete: true)
                .ForeignKey("dbo.GMs", t => new { t.D3Id, t.SPUId }, cascadeDelete: true)
                .Index(t => t.ManyfactoryId)
                .Index(t => t.SPUId)
                .Index(t => new { t.D3Id, t.SPUId });
            
            CreateTable(
                "dbo.SPUs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SPUTares",
                c => new
                    {
                        SPUId = c.Int(nullable: false),
                        TareId = c.Int(nullable: false),
                        CountNeed = c.Int(nullable: false),
                        CountGet = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SPUId, t.TareId })
                .ForeignKey("dbo.Tares", t => t.TareId, cascadeDelete: true)
                .ForeignKey("dbo.SPUs", t => t.SPUId, cascadeDelete: true)
                .Index(t => t.TareId)
                .Index(t => t.SPUId);
            
            CreateTable(
                "dbo.Tares",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderAggregates",
                c => new
                    {
                        OrderId = c.Int(nullable: false),
                        AggregateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderId, t.AggregateId })
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Aggregates", t => t.AggregateId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.AggregateId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        ConteragentId = c.Int(nullable: false),
                        Contragent_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contragents", t => t.Contragent_Id)
                .Index(t => t.Contragent_Id);
            
            CreateTable(
                "dbo.Contragents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderAggregates", "AggregateId", "dbo.Aggregates");
            DropForeignKey("dbo.OrderAggregates", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "Contragent_Id", "dbo.Contragents");
            DropForeignKey("dbo.DrawingNameAggregateD3", "DrawingNameAggregateId", "dbo.DrawingNameAggregates");
            DropForeignKey("dbo.GMs", "D3Id", "dbo.D3");
            DropForeignKey("dbo.ManufactoryGMs", new[] { "D3Id", "SPUId" }, "dbo.GMs");
            DropForeignKey("dbo.ManufactoryGMs", "SPUId", "dbo.SPUs");
            DropForeignKey("dbo.SPUTares", "SPUId", "dbo.SPUs");
            DropForeignKey("dbo.SPUTares", "TareId", "dbo.Tares");
            DropForeignKey("dbo.GMs", "SPUId", "dbo.SPUs");
            DropForeignKey("dbo.ManufactoryGMs", "ManyfactoryId", "dbo.Manufactories");
            DropForeignKey("dbo.DrawingNameAggregateD3", "D3Id", "dbo.D3");
            DropForeignKey("dbo.DrawingNameAggregates", "AggregateTypeId", "dbo.AggregateTypes");
            DropForeignKey("dbo.Aggregates", "DrawingNameAggregateId", "dbo.DrawingNameAggregates");
            DropIndex("dbo.OrderAggregates", new[] { "AggregateId" });
            DropIndex("dbo.OrderAggregates", new[] { "OrderId" });
            DropIndex("dbo.Orders", new[] { "Contragent_Id" });
            DropIndex("dbo.DrawingNameAggregateD3", new[] { "DrawingNameAggregateId" });
            DropIndex("dbo.GMs", new[] { "D3Id" });
            DropIndex("dbo.ManufactoryGMs", new[] { "D3Id", "SPUId" });
            DropIndex("dbo.ManufactoryGMs", new[] { "SPUId" });
            DropIndex("dbo.SPUTares", new[] { "SPUId" });
            DropIndex("dbo.SPUTares", new[] { "TareId" });
            DropIndex("dbo.GMs", new[] { "SPUId" });
            DropIndex("dbo.ManufactoryGMs", new[] { "ManyfactoryId" });
            DropIndex("dbo.DrawingNameAggregateD3", new[] { "D3Id" });
            DropIndex("dbo.DrawingNameAggregates", new[] { "AggregateTypeId" });
            DropIndex("dbo.Aggregates", new[] { "DrawingNameAggregateId" });
            DropTable("dbo.Contragents");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderAggregates");
            DropTable("dbo.Tares");
            DropTable("dbo.SPUTares");
            DropTable("dbo.SPUs");
            DropTable("dbo.ManufactoryGMs");
            DropTable("dbo.GMs");
            DropTable("dbo.D3");
            DropTable("dbo.DrawingNameAggregateD3");
            DropTable("dbo.AggregateTypes");
            DropTable("dbo.DrawingNameAggregates");
            DropTable("dbo.Aggregates");
        }
    }
}
