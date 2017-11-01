namespace IsucorpTest.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContactDescriptionMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contacts", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contacts", "Description");
        }
    }
}
