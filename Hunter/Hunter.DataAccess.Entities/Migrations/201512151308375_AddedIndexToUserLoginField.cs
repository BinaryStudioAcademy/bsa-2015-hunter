namespace Hunter.DataAccess.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIndexToUserLoginField : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.UserProfile", "UserLogin", unique: true, name: "UserLoginIndex");
        }
        
        public override void Down()
        {
            DropIndex("dbo.UserProfile", "UserLoginIndex");
        }
    }
}
