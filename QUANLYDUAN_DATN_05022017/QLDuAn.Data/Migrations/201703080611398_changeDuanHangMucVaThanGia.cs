namespace QLDuAn.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeDuanHangMucVaThanGia : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DuAnHangMuc", "HeSoCongViec", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.DuAnHangMuc", "SoNguoiThucHien", c => c.Int(nullable: false));
            AddColumn("dbo.DuAnHangMuc", "DiemDanhGia", c => c.Int(nullable: false));
            AddColumn("dbo.DuAnHangMuc", "DiemHangMuc", c => c.Int(nullable: false));
            AddColumn("dbo.ThamGia", "ThuNhap", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.ThamGia", "DiemThanhVien", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.DuAnHangMuc", "SoNgayThucHien");
            AddColumn("dbo.DuAnHangMuc", "SoNgayThucHien", c => c.Int(nullable: false));
            DropColumn("dbo.DuAnHangMuc", "IdNguoiThucHienThucTe");
            DropColumn("dbo.DuAnHangMuc", "DiemThanhVien");
            DropColumn("dbo.DuAnHangMuc", "ThuNhap");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DuAnHangMuc", "ThuNhap", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.DuAnHangMuc", "DiemThanhVien", c => c.Int(nullable: false));
            AddColumn("dbo.DuAnHangMuc", "IdNguoiThucHienThucTe", c => c.Int(nullable: false));
            AlterColumn("dbo.DuAnHangMuc", "SoNgayThucHien", c => c.DateTime(nullable: false));
            DropColumn("dbo.ThamGia", "DiemThanhVien");
            DropColumn("dbo.ThamGia", "ThuNhap");
            DropColumn("dbo.DuAnHangMuc", "DiemHangMuc");
            DropColumn("dbo.DuAnHangMuc", "DiemDanhGia");
            DropColumn("dbo.DuAnHangMuc", "SoNguoiThucHien");
            DropColumn("dbo.DuAnHangMuc", "HeSoCongViec");
        }
    }
}
