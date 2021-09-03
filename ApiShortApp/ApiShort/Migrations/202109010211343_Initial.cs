using System.Data.Entity.Migrations;

namespace ApiShort.Migrations
{
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Facilities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                        FacilityLevel = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cipher = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ChildParentFacilities",
                c => new
                    {
                        ParentId = c.Int(nullable: false),
                        ChildId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ParentId, t.ChildId })
                .ForeignKey("dbo.Facilities", t => t.ParentId)
                .ForeignKey("dbo.Facilities", t => t.ChildId)
                .Index(t => t.ParentId)
                .Index(t => t.ChildId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Facilities", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.ChildParentFacilities", "ChildId", "dbo.Facilities");
            DropForeignKey("dbo.ChildParentFacilities", "ParentId", "dbo.Facilities");
            DropIndex("dbo.ChildParentFacilities", new[] { "ChildId" });
            DropIndex("dbo.ChildParentFacilities", new[] { "ParentId" });
            DropIndex("dbo.Facilities", new[] { "ProjectId" });
            DropTable("dbo.ChildParentFacilities");
            DropTable("dbo.Projects");
            DropTable("dbo.Facilities");
        }
    }
}
