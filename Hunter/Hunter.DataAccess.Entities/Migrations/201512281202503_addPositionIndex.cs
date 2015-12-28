namespace Hunter.DataAccess.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPositionIndex : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RoleMappings", "Position", c => c.String(maxLength: 30));
            CreateIndex("dbo.RoleMappings", "Position", unique: true, name: "PositionIndex");
        }
        
        public override void Down()
        {
            DropIndex("dbo.RoleMappings", "PositionIndex");
            AlterColumn("dbo.RoleMappings", "Position", c => c.String());
        }
    }
}
