namespace Hunter.DataAccess.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dropUsertable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.User", "RoleId", "dbo.UserRole");
            //DropForeignKey("dbo.Vacancy", "User_Id", "dbo.User");
            DropIndex("dbo.Vacancy", new[] { "User_Id" });
            DropIndex("dbo.User", new[] { "RoleId" });
            DropColumn("dbo.Vacancy", "User_Id");
            DropTable("dbo.User");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false, maxLength: 200),
                        PasswordHash = c.String(nullable: false, maxLength: 300),
                        State = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);

            AddColumn("dbo.Vacancy", "User_Id", c => c.Int());
            CreateIndex("dbo.User", "RoleId");
            CreateIndex("dbo.Vacancy", "User_Id");
            //AddForeignKey("dbo.Vacancy", "User_Id", "dbo.User", "Id");
            AddForeignKey("dbo.User", "RoleId", "dbo.UserRole", "Id");
        }
    }
}
