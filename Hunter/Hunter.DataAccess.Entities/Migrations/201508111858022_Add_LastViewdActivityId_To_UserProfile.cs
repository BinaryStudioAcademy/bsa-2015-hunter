namespace Hunter.DataAccess.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_LastViewdActivityId_To_UserProfile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfile", "LViewedActivity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserProfile", "LViewedActivity");
        }
    }
}
