namespace Hunter.DataAccess.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAuthUserIdColumnToUserProfile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfile", "AuthUserId", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserProfile", "AuthUserId");
        }
    }
}
