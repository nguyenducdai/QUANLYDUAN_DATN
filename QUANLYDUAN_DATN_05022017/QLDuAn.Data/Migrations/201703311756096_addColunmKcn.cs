namespace QLDuAn.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addColunmKcn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DuAnHangMuc", "HesoKcn", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DuAnHangMuc", "HesoKcn");
        }
    }
}
