namespace Hunter.DataAccess.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addRoleMappingTable : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.UserProfile", new[] { "UserRole_Id" });
            DropColumn("dbo.UserProfile", "RoleId");
            RenameColumn(table: "dbo.UserProfile", name: "UserRole_Id", newName: "RoleId");
            CreateTable(
                "dbo.RoleMappings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Position = c.String(),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserRole", t => t.RoleId)
                .Index(t => t.RoleId);
            
            AlterColumn("dbo.UserProfile", "RoleId", c => c.Int(nullable: false));
            CreateIndex("dbo.UserProfile", "RoleId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoleMappings", "RoleId", "dbo.UserRole");
            DropIndex("dbo.RoleMappings", new[] { "RoleId" });
            DropIndex("dbo.UserProfile", new[] { "RoleId" });
            AlterColumn("dbo.UserProfile", "RoleId", c => c.Int());
            DropTable("dbo.RoleMappings");
            RenameColumn(table: "dbo.UserProfile", name: "RoleId", newName: "UserRole_Id");
            AddColumn("dbo.UserProfile", "RoleId", c => c.Int(nullable: false));
            CreateIndex("dbo.UserProfile", "UserRole_Id");
        }
    }
}
