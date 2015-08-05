namespace Hunter.DataAccess.Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeNameOfFieldsInCandidates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Candidate", "Resolution", c => c.Int(nullable: false));
            DropColumn("dbo.Candidate", "Resoultion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Candidate", "Resoultion", c => c.Int(nullable: false));
            DropColumn("dbo.Candidate", "Resolution");
        }
    }
}
