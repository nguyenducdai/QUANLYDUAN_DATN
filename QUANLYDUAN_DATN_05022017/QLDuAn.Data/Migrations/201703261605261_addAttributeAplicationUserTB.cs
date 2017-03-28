namespace QLDuAn.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAttributeAplicationUserTB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationUsers", "Image", c => c.String());
            AddColumn("dbo.ApplicationUsers", "Function", c => c.String());
            AddColumn("dbo.ApplicationUsers", "Birthday", c => c.DateTime(nullable: false));
            AddColumn("dbo.ApplicationUsers", "Startdate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationUsers", "Startdate");
            DropColumn("dbo.ApplicationUsers", "Birthday");
            DropColumn("dbo.ApplicationUsers", "Function");
            DropColumn("dbo.ApplicationUsers", "Image");
        }
    }
}
