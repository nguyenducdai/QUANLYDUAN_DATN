namespace QLDuAn.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addColumnThuNhapTGTb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ThamGia", "ThuNhap", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ThamGia", "ThuNhap");
        }
    }
}
