namespace PackingGM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Aggregates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(),
                        DrawingNameId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DrawingNames", t => t.DrawingNameId)
                .Index(t => t.DrawingNameId);
            
            CreateTable(
                "dbo.DrawingNames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AggregateTypeId = c.Int(),
                        NormalizedText = c.String(),
                        Note = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AggregateTypes", t => t.AggregateTypeId)
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
                "dbo.DrawingNameVersions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DrawingNameId = c.Int(nullable: false),
                        IdTCS = c.Int(nullable: false),
                        Name = c.String(),
                        State = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DrawingNames", t => t.DrawingNameId, cascadeDelete: true)
                .Index(t => t.DrawingNameId);
            
            CreateTable(
                "dbo.DrawingNameD3",
                c => new
                    {
                        DrawingNameVersionId = c.Int(nullable: false),
                        D3Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DrawingNameVersionId, t.D3Id })
                .ForeignKey("dbo.D3", t => t.D3Id, cascadeDelete: true)
                .ForeignKey("dbo.DrawingNameVersions", t => t.DrawingNameVersionId, cascadeDelete: true)
                .Index(t => t.D3Id)
                .Index(t => t.DrawingNameVersionId);
            
            CreateTable(
                "dbo.D3",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NormalizedText = c.String(),
                        Note = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.D3Version",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        D3Id = c.Int(nullable: false),
                        IdTCS = c.Int(nullable: false),
                        Name = c.String(),
                        State = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.D3", t => t.D3Id, cascadeDelete: true)
                .Index(t => t.D3Id);
            
            CreateTable(
                "dbo.GMNumbers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        D3VersionId = c.Int(nullable: false),
                        SPUId = c.Int(nullable: false),
                        NumberGM = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.D3Version", t => t.D3VersionId, cascadeDelete: true)
                .ForeignKey("dbo.SPUs", t => t.SPUId, cascadeDelete: true)
                .Index(t => t.D3VersionId)
                .Index(t => t.SPUId);
            
            CreateTable(
                "dbo.GMs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GMNumberId = c.Int(nullable: false),
                        SPUTareId = c.Int(nullable: false),
                        CountGet = c.Int(nullable: false),
                        PlannedDeadline = c.DateTime(nullable: false),
                        OrderAggregate_OrderId = c.Int(),
                        OrderAggregate_AggregateId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GMNumbers", t => t.GMNumberId, cascadeDelete: true)
                .ForeignKey("dbo.OrderAggregates", t => new { t.OrderAggregate_OrderId, t.OrderAggregate_AggregateId })
                .ForeignKey("dbo.SPUTares", t => t.SPUTareId, cascadeDelete: true)
                .Index(t => t.GMNumberId)
                .Index(t => new { t.OrderAggregate_OrderId, t.OrderAggregate_AggregateId })
                .Index(t => t.SPUTareId);
            
            CreateTable(
                "dbo.ManufactoryGMs",
                c => new
                    {
                        ManyfactoryId = c.Int(nullable: false),
                        GMId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ManyfactoryId, t.GMId })
                .ForeignKey("dbo.Manufactories", t => t.ManyfactoryId, cascadeDelete: true)
                .ForeignKey("dbo.GMs", t => t.GMId, cascadeDelete: true)
                .Index(t => t.ManyfactoryId)
                .Index(t => t.GMId);
            
            CreateTable(
                "dbo.Manufactories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Password = c.String(),
                        RoleId = c.Int(nullable: false),
                        ManufactoryId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Manufactories", t => t.ManufactoryId)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.ManufactoryId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsAlowedViewing = c.Boolean(nullable: false),
                        IsAlowedWriting = c.Boolean(nullable: false),
                        IsAlowedAdmining = c.Boolean(nullable: false),
                        Version = c.Int(nullable: false),
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
                        ContragentId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contragents", t => t.ContragentId)
                .Index(t => t.ContragentId);
            
            CreateTable(
                "dbo.Contragents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SPUTares",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SPUVersionId = c.Int(nullable: false),
                        TareId = c.Int(nullable: false),
                        CountNeed = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tares", t => t.TareId, cascadeDelete: true)
                .ForeignKey("dbo.SPUVersions", t => t.SPUVersionId, cascadeDelete: true)
                .Index(t => t.TareId)
                .Index(t => t.SPUVersionId);
            
            CreateTable(
                "dbo.Tares",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NormalizedText = c.String(),
                        Note = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SPUs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NormalizedText = c.String(),
                        Note = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SPUVersions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SPUId = c.Int(nullable: false),
                        IdTCS = c.Int(nullable: false),
                        Name = c.String(),
                        State = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SPUs", t => t.SPUId, cascadeDelete: true)
                .Index(t => t.SPUId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SPUTares", "SPUVersionId", "dbo.SPUVersions");
            DropForeignKey("dbo.SPUVersions", "SPUId", "dbo.SPUs");
            DropForeignKey("dbo.GMNumbers", "SPUId", "dbo.SPUs");
            DropForeignKey("dbo.OrderAggregates", "AggregateId", "dbo.Aggregates");
            DropForeignKey("dbo.DrawingNameD3", "DrawingNameVersionId", "dbo.DrawingNameVersions");
            DropForeignKey("dbo.DrawingNameD3", "D3Id", "dbo.D3");
            DropForeignKey("dbo.SPUTares", "TareId", "dbo.Tares");
            DropForeignKey("dbo.GMs", "SPUTareId", "dbo.SPUTares");
            DropForeignKey("dbo.OrderAggregates", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "ContragentId", "dbo.Contragents");
            DropForeignKey("dbo.GMs", new[] { "OrderAggregate_OrderId", "OrderAggregate_AggregateId" }, "dbo.OrderAggregates");
            DropForeignKey("dbo.ManufactoryGMs", "GMId", "dbo.GMs");
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Users", "ManufactoryId", "dbo.Manufactories");
            DropForeignKey("dbo.ManufactoryGMs", "ManyfactoryId", "dbo.Manufactories");
            DropForeignKey("dbo.GMs", "GMNumberId", "dbo.GMNumbers");
            DropForeignKey("dbo.GMNumbers", "D3VersionId", "dbo.D3Version");
            DropForeignKey("dbo.D3Version", "D3Id", "dbo.D3");
            DropForeignKey("dbo.DrawingNameVersions", "DrawingNameId", "dbo.DrawingNames");
            DropForeignKey("dbo.DrawingNames", "AggregateTypeId", "dbo.AggregateTypes");
            DropForeignKey("dbo.Aggregates", "DrawingNameId", "dbo.DrawingNames");
            DropIndex("dbo.SPUTares", new[] { "SPUVersionId" });
            DropIndex("dbo.SPUVersions", new[] { "SPUId" });
            DropIndex("dbo.GMNumbers", new[] { "SPUId" });
            DropIndex("dbo.OrderAggregates", new[] { "AggregateId" });
            DropIndex("dbo.DrawingNameD3", new[] { "DrawingNameVersionId" });
            DropIndex("dbo.DrawingNameD3", new[] { "D3Id" });
            DropIndex("dbo.SPUTares", new[] { "TareId" });
            DropIndex("dbo.GMs", new[] { "SPUTareId" });
            DropIndex("dbo.OrderAggregates", new[] { "OrderId" });
            DropIndex("dbo.Orders", new[] { "ContragentId" });
            DropIndex("dbo.GMs", new[] { "OrderAggregate_OrderId", "OrderAggregate_AggregateId" });
            DropIndex("dbo.ManufactoryGMs", new[] { "GMId" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.Users", new[] { "ManufactoryId" });
            DropIndex("dbo.ManufactoryGMs", new[] { "ManyfactoryId" });
            DropIndex("dbo.GMs", new[] { "GMNumberId" });
            DropIndex("dbo.GMNumbers", new[] { "D3VersionId" });
            DropIndex("dbo.D3Version", new[] { "D3Id" });
            DropIndex("dbo.DrawingNameVersions", new[] { "DrawingNameId" });
            DropIndex("dbo.DrawingNames", new[] { "AggregateTypeId" });
            DropIndex("dbo.Aggregates", new[] { "DrawingNameId" });
            DropTable("dbo.SPUVersions");
            DropTable("dbo.SPUs");
            DropTable("dbo.Tares");
            DropTable("dbo.SPUTares");
            DropTable("dbo.Contragents");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderAggregates");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.Manufactories");
            DropTable("dbo.ManufactoryGMs");
            DropTable("dbo.GMs");
            DropTable("dbo.GMNumbers");
            DropTable("dbo.D3Version");
            DropTable("dbo.D3");
            DropTable("dbo.DrawingNameD3");
            DropTable("dbo.DrawingNameVersions");
            DropTable("dbo.AggregateTypes");
            DropTable("dbo.DrawingNames");
            DropTable("dbo.Aggregates");
        }
    }
}
