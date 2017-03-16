namespace QLDuAn.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeDuAnv21 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DuAnHangMuc", "MucDoTruyenThong", c => c.Int(nullable: false));
            AlterColumn("dbo.DuAnHangMuc", "NgayBatDau", c => c.DateTime());
            AlterColumn("dbo.DuAnHangMuc", "NgayDuKienKetThuc", c => c.DateTime());
            AlterColumn("dbo.DuAnHangMuc", "NgayHoanThanh", c => c.DateTime());
            DropColumn("dbo.DuAnHangMuc", "SoNgayThucHien");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DuAnHangMuc", "SoNgayThucHien", c => c.Int(nullable: false));
            AlterColumn("dbo.DuAnHangMuc", "NgayHoanThanh", c => c.DateTime(nullable: false));
            AlterColumn("dbo.DuAnHangMuc", "NgayDuKienKetThuc", c => c.DateTime(nullable: false));
            AlterColumn("dbo.DuAnHangMuc", "NgayBatDau", c => c.DateTime(nullable: false));
            DropColumn("dbo.DuAnHangMuc", "MucDoTruyenThong");
        }
    }
}
