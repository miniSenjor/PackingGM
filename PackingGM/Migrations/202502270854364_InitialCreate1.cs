namespace PackingGM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Manufactories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Users", "ManufactoryId", c => c.Int());
            CreateIndex("dbo.Users", "ManufactoryId");
            AddForeignKey("dbo.Users", "ManufactoryId", "dbo.Manufactories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "ManufactoryId", "dbo.Manufactories");
            DropIndex("dbo.Users", new[] { "ManufactoryId" });
            DropColumn("dbo.Users", "ManufactoryId");
            DropTable("dbo.Manufactories");
        }
    }
}
