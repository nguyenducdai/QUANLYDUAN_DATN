namespace QLDuAn.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intialAD : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DuAn",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TenDuAn = c.String(nullable: false, maxLength: 200),
                        IdHopDong = c.Int(nullable: false),
                        MoTa = c.String(),
                        GiaTriDuAn = c.Int(nullable: false),
                        SoNgayThucHien = c.Int(nullable: false),
                        NgayKy = c.DateTime(nullable: false),
                        NamQuyetToan = c.DateTime(nullable: false),
                        LoaiCongTrinh = c.Int(nullable: false),
                        TyLeTheoDT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LuongThueNgoai = c.Int(),
                        TongChiQL = c.Decimal(precision: 18, scale: 2),
                        LuongTTQtt = c.Decimal(precision: 18, scale: 2),
                        LuongDPQdp = c.Decimal(precision: 18, scale: 2),
                        LuongGTQgt = c.Decimal(precision: 18, scale: 2),
                        LuongGTV21 = c.Decimal(precision: 18, scale: 2),
                        LuongGTV22 = c.Decimal(precision: 18, scale: 2),
                        TrangThai = c.Boolean(nullable: false),
                        Created_at = c.DateTime(nullable: false),
                        Updated_at = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.HopDong", t => t.IdHopDong, cascadeDelete: true)
                .Index(t => t.IdHopDong);
            
            CreateTable(
                "dbo.HopDong",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SoHopDong = c.String(),
                        TenHopDong = c.String(),
                        GiaTriHopDong = c.String(),
                        SoNgayThucHien = c.String(),
                        NgayKy = c.DateTime(nullable: false),
                        IdKhachHang = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.khachHang", t => t.IdKhachHang, cascadeDelete: true)
                .Index(t => t.IdKhachHang);
            
            CreateTable(
                "dbo.khachHang",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TenKhach = c.String(nullable: false, maxLength: 200),
                        SoDienThoai = c.Int(nullable: false),
                        SoFax = c.String(),
                        Email = c.String(nullable: false),
                        DiaChi = c.String(nullable: false, maxLength: 256),
                        GioiTinh = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.DuAnHangMuc",
                c => new
                    {
                        IdDuAn = c.Int(nullable: false),
                        IdHangMuc = c.Int(nullable: false),
                        IdNhomCongViec = c.Int(nullable: false),
                        NgayBatDau = c.DateTime(nullable: false),
                        NgayDuKienKetThuc = c.DateTime(nullable: false),
                        NgayHoanThanh = c.DateTime(nullable: false),
                        IdNguoiThucHienTheoLenhSX = c.Int(),
                        IdNguoiThucHienThucTe = c.Int(nullable: false),
                        DiemThanhVien = c.Int(nullable: false),
                        ThuNhap = c.Decimal(nullable: false, precision: 18, scale: 2),
                        HeSoLapLai = c.Decimal(nullable: false, precision: 18, scale: 2),
                        HeSoNhanCong = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SoNgayThucHien = c.DateTime(nullable: false),
                        HeSoThoiGian = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TrangThai = c.Boolean(nullable: false),
                        Created_at = c.DateTime(nullable: false),
                        Updated_at = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdDuAn, t.IdHangMuc, t.IdNhomCongViec })
                .ForeignKey("dbo.DuAn", t => t.IdDuAn, cascadeDelete: true)
                .ForeignKey("dbo.HangMuc", t => t.IdHangMuc, cascadeDelete: true)
                .ForeignKey("dbo.NhomCongViec", t => t.IdNhomCongViec, cascadeDelete: true)
                .Index(t => t.IdDuAn)
                .Index(t => t.IdHangMuc)
                .Index(t => t.IdNhomCongViec);
            
            CreateTable(
                "dbo.HangMuc",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TenHangMuc = c.String(nullable: false),
                        MoTaHangMuc = c.String(),
                        LoaiHangMuc = c.Int(nullable: false),
                        TrangThai = c.Boolean(),
                        Created_at = c.DateTime(nullable: false),
                        Updated_at = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.NhomCongViec",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NhomCV = c.String(nullable: false),
                        HeSoCV = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GhiChu = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.NhanVien",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TenNhanVien = c.String(maxLength: 200),
                        SoDienThoai = c.Int(nullable: false),
                        ChungMinhThu = c.Int(nullable: false),
                        DiaChi = c.String(),
                        GioiTinh = c.Boolean(nullable: false),
                        Email = c.String(maxLength: 100),
                        Created_at = c.DateTime(nullable: false),
                        Updated_at = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ThamGia",
                c => new
                    {
                        IdDuAn = c.Int(nullable: false),
                        IdHangMuc = c.Int(nullable: false),
                        IdNhanVien = c.Int(nullable: false),
                        HeSoThamGia = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.IdDuAn, t.IdHangMuc, t.IdNhanVien })
                .ForeignKey("dbo.DuAn", t => t.IdDuAn, cascadeDelete: true)
                .ForeignKey("dbo.HangMuc", t => t.IdHangMuc, cascadeDelete: true)
                .ForeignKey("dbo.NhanVien", t => t.IdNhanVien, cascadeDelete: true)
                .Index(t => t.IdDuAn)
                .Index(t => t.IdHangMuc)
                .Index(t => t.IdNhanVien);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ThamGia", "IdNhanVien", "dbo.NhanVien");
            DropForeignKey("dbo.ThamGia", "IdHangMuc", "dbo.HangMuc");
            DropForeignKey("dbo.ThamGia", "IdDuAn", "dbo.DuAn");
            DropForeignKey("dbo.DuAnHangMuc", "IdNhomCongViec", "dbo.NhomCongViec");
            DropForeignKey("dbo.DuAnHangMuc", "IdHangMuc", "dbo.HangMuc");
            DropForeignKey("dbo.DuAnHangMuc", "IdDuAn", "dbo.DuAn");
            DropForeignKey("dbo.DuAn", "IdHopDong", "dbo.HopDong");
            DropForeignKey("dbo.HopDong", "IdKhachHang", "dbo.khachHang");
            DropIndex("dbo.ThamGia", new[] { "IdNhanVien" });
            DropIndex("dbo.ThamGia", new[] { "IdHangMuc" });
            DropIndex("dbo.ThamGia", new[] { "IdDuAn" });
            DropIndex("dbo.DuAnHangMuc", new[] { "IdNhomCongViec" });
            DropIndex("dbo.DuAnHangMuc", new[] { "IdHangMuc" });
            DropIndex("dbo.DuAnHangMuc", new[] { "IdDuAn" });
            DropIndex("dbo.HopDong", new[] { "IdKhachHang" });
            DropIndex("dbo.DuAn", new[] { "IdHopDong" });
            DropTable("dbo.ThamGia");
            DropTable("dbo.NhanVien");
            DropTable("dbo.NhomCongViec");
            DropTable("dbo.HangMuc");
            DropTable("dbo.DuAnHangMuc");
            DropTable("dbo.khachHang");
            DropTable("dbo.HopDong");
            DropTable("dbo.DuAn");
        }
    }
}
