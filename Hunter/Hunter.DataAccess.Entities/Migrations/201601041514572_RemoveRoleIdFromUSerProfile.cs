namespace Hunter.DataAccess.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRoleIdFromUSerProfile : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserProfile", "FK_dbo.UserProfile_dbo.UserRole_UserRole_Id");
            DropIndex("dbo.UserProfile", new[] { "RoleId" });
            DropColumn("dbo.UserProfile", "RoleId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserProfile", "RoleId", c => c.Int(nullable: false));
            CreateIndex("dbo.UserProfile", "RoleId");
            AddForeignKey("dbo.UserProfile", "RoleId", "dbo.UserRole", "Id");
        }
    }
}
