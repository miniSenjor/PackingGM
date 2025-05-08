namespace PackingGM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteDrawingName : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Aggregates", "DrawingNameId", "dbo.DrawingNames");
            DropForeignKey("dbo.DrawingNames", "AggregateTypeId", "dbo.AggregateTypes");
            DropForeignKey("dbo.DrawingNameVersions", "DrawingNameId", "dbo.DrawingNames");
            DropForeignKey("dbo.DrawingNameD3", "D3Id", "dbo.D3");
            DropForeignKey("dbo.DrawingNameD3", "DrawingNameVersionId", "dbo.DrawingNameVersions");
            DropIndex("dbo.Aggregates", new[] { "DrawingNameId" });
            DropIndex("dbo.DrawingNames", new[] { "AggregateTypeId" });
            DropIndex("dbo.DrawingNameVersions", new[] { "DrawingNameId" });
            DropIndex("dbo.DrawingNameD3", new[] { "D3Id" });
            DropIndex("dbo.DrawingNameD3", new[] { "DrawingNameVersionId" });
            AddColumn("dbo.Aggregates", "AggregateTypeId", c => c.Int());
            AddColumn("dbo.D3", "AggregateTypeId", c => c.Int());
            CreateIndex("dbo.Aggregates", "AggregateTypeId");
            CreateIndex("dbo.D3", "AggregateTypeId");
            AddForeignKey("dbo.Aggregates", "AggregateTypeId", "dbo.AggregateTypes", "Id");
            AddForeignKey("dbo.D3", "AggregateTypeId", "dbo.AggregateTypes", "Id");
            DropColumn("dbo.Aggregates", "DrawingNameId");
            DropTable("dbo.DrawingNames");
            DropTable("dbo.DrawingNameVersions");
            DropTable("dbo.DrawingNameD3");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.DrawingNameD3",
                c => new
                    {
                        DrawingNameVersionId = c.Int(nullable: false),
                        D3Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DrawingNameVersionId, t.D3Id });
            
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
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Aggregates", "DrawingNameId", c => c.Int());
            DropForeignKey("dbo.D3", "AggregateTypeId", "dbo.AggregateTypes");
            DropForeignKey("dbo.Aggregates", "AggregateTypeId", "dbo.AggregateTypes");
            DropIndex("dbo.D3", new[] { "AggregateTypeId" });
            DropIndex("dbo.Aggregates", new[] { "AggregateTypeId" });
            DropColumn("dbo.D3", "AggregateTypeId");
            DropColumn("dbo.Aggregates", "AggregateTypeId");
            CreateIndex("dbo.DrawingNameD3", "DrawingNameVersionId");
            CreateIndex("dbo.DrawingNameD3", "D3Id");
            CreateIndex("dbo.DrawingNameVersions", "DrawingNameId");
            CreateIndex("dbo.DrawingNames", "AggregateTypeId");
            CreateIndex("dbo.Aggregates", "DrawingNameId");
            AddForeignKey("dbo.DrawingNameD3", "DrawingNameVersionId", "dbo.DrawingNameVersions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DrawingNameD3", "D3Id", "dbo.D3", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DrawingNameVersions", "DrawingNameId", "dbo.DrawingNames", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DrawingNames", "AggregateTypeId", "dbo.AggregateTypes", "Id");
            AddForeignKey("dbo.Aggregates", "DrawingNameId", "dbo.DrawingNames", "Id");
        }
    }
}
