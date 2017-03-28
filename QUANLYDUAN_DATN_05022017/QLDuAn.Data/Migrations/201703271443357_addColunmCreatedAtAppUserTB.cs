namespace QLDuAn.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addColunmCreatedAtAppUserTB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationUsers", "Created_at", c => c.DateTime(nullable: false));
            AddColumn("dbo.ApplicationUsers", "Updatted_at", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationUsers", "Updatted_at");
            DropColumn("dbo.ApplicationUsers", "Created_at");
        }
    }
}
