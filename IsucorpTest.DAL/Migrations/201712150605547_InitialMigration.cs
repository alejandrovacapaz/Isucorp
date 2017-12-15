namespace IsucorpTest.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        PhoneNumber = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        ContactTypeId = c.Int(nullable: false),
                        Description = c.String(),
                        CreatedByName = c.String(),
                        CreatedById = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedByName = c.String(),
                        ModifiedById = c.String(),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContactTypes", t => t.ContactTypeId, cascadeDelete: true)
                .Index(t => t.ContactTypeId);
            
            CreateTable(
                "dbo.ContactTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedByName = c.String(),
                        CreatedById = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedByName = c.String(),
                        ModifiedById = c.String(),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contacts", "ContactTypeId", "dbo.ContactTypes");
            DropIndex("dbo.Contacts", new[] { "ContactTypeId" });
            DropTable("dbo.ContactTypes");
            DropTable("dbo.Contacts");
        }
    }
}
