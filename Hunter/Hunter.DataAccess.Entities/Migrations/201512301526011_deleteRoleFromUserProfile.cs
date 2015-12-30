namespace Hunter.DataAccess.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteRoleFromUserProfile : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.UserProfile", new[] { "RoleId" });
            RenameColumn(table: "dbo.UserProfile", name: "RoleId", newName: "UserRole_Id");
            AlterColumn("dbo.UserProfile", "UserRole_Id", c => c.Int());
            CreateIndex("dbo.UserProfile", "UserRole_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.UserProfile", new[] { "UserRole_Id" });
            AlterColumn("dbo.UserProfile", "UserRole_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.UserProfile", name: "UserRole_Id", newName: "RoleId");
            CreateIndex("dbo.UserProfile", "RoleId");
        }
    }
}
