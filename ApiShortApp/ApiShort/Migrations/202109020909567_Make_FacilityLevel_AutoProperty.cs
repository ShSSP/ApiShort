namespace ApiShort.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Make_FacilityLevel_AutoProperty : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Facilities", "FacilityLevel");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Facilities", "FacilityLevel", c => c.Int(nullable: false));
        }
    }
}
