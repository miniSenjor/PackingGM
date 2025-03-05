namespace PackingGM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVersion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Roles", "Version", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            Sql("DROP TRIGGER IF EXISTS update_role_version ON \"dbo\".\"Roles\";");
            Sql("DROP FUNCTION IF EXISTS update_version();");
            DropColumn("dbo.Roles", "Version");
        }
    }
}
