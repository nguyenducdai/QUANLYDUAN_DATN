namespace QLDuAn.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addColumnIsdelete : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HangMuc", "isDelete", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HangMuc", "isDelete");
        }
    }
}
