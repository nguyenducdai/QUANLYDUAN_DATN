namespace QLDuAn.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addColumnDtv : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ThamGia", "DiemThanhVien", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ThamGia", "DiemThanhVien");
        }
    }
}
