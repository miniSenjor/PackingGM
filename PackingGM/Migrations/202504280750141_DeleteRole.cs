namespace PackingGM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteRole : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropIndex("dbo.Users", new[] { "RoleId" });
            AddColumn("dbo.Users", "IsAlowedViewing", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "IsAlowedWriting", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "IsAlowedAdmining", c => c.Boolean(nullable: false));
            DropColumn("dbo.Users", "RoleId");
            DropTable("dbo.Roles");
        }
        
        public override void Down()
        {
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
            
            AddColumn("dbo.Users", "RoleId", c => c.Int(nullable: false));
            DropColumn("dbo.Users", "IsAlowedAdmining");
            DropColumn("dbo.Users", "IsAlowedWriting");
            DropColumn("dbo.Users", "IsAlowedViewing");
            CreateIndex("dbo.Users", "RoleId");
            AddForeignKey("dbo.Users", "RoleId", "dbo.Roles", "Id", cascadeDelete: true);
        }
    }
}
