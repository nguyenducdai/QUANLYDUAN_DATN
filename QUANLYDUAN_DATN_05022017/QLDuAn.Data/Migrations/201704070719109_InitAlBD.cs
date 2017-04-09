namespace QLDuAn.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitAlBD : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 250),
                        Description = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ApplicationRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Description = c.String(maxLength: 250),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ApplicationUserRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.ApplicationRoles", t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id);
            
            CreateTable(
                "dbo.ApplicationRoleGroups",
                c => new
                    {
                        GroupId = c.Int(nullable: false),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.GroupId, t.RoleId })
                .ForeignKey("dbo.ApplicationGroups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.ApplicationRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.GroupId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.ApplicationUserGroups",
                c => new
                    {
                        IdGroup = c.Int(nullable: false),
                        IdUser = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.IdGroup, t.IdUser })
                .ForeignKey("dbo.ApplicationGroups", t => t.IdGroup, cascadeDelete: true)
                .ForeignKey("dbo.ApplicationUsers", t => t.IdUser, cascadeDelete: true)
                .Index(t => t.IdGroup)
                .Index(t => t.IdUser);
            
            CreateTable(
                "dbo.ApplicationUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Adress = c.String(),
                        FullName = c.String(),
                        Bio = c.String(),
                        Sex = c.Boolean(),
                        Image = c.String(),
                        Function = c.String(),
                        Birthday = c.DateTime(nullable: false),
                        Startdate = c.DateTime(nullable: false),
                        Created_at = c.DateTime(nullable: false),
                        Updatted_at = c.DateTime(nullable: false),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ApplicationUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ApplicationUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.DuAn",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TenDuAn = c.String(nullable: false, maxLength: 200),
                        IdHopDong = c.Int(nullable: false),
                        MoTa = c.String(),
                        SoNgayThucHienThucTe = c.Int(nullable: false),
                        NamQuyetToan = c.DateTime(nullable: false),
                        LoaiCongTrinh = c.Int(nullable: false),
                        TyLeTheoDT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TongDiem = c.Decimal(precision: 18, scale: 2),
                        DonGiaDiemDiem = c.Decimal(precision: 18, scale: 2),
                        LuongThueNgoai = c.Int(),
                        TongChiQL = c.Decimal(precision: 18, scale: 2),
                        LuongTTQtt = c.Decimal(precision: 18, scale: 2),
                        LuongDPQdp = c.Decimal(precision: 18, scale: 2),
                        LuongGTQgt = c.Decimal(precision: 18, scale: 2),
                        LuongGTV21 = c.Decimal(precision: 18, scale: 2),
                        LuongGTV22 = c.Decimal(precision: 18, scale: 2),
                        TrangThai = c.Int(nullable: false),
                        Created_at = c.DateTime(),
                        Updated_at = c.DateTime(),
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
                        NoiDung = c.String(),
                        NgayBatDau = c.DateTime(nullable: false),
                        NgayKetThuc = c.DateTime(nullable: false),
                        NgayKy = c.DateTime(nullable: false),
                        IdKhachHang = c.Int(nullable: false),
                        Created_at = c.DateTime(),
                        Updated_at = c.DateTime(),
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
                "dbo.Errors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        Stacktrace = c.String(),
                        Created_at = c.DateTime(),
                        Updated_at = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.HangMuc",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IdDuAn = c.Int(nullable: false),
                        TenHangMuc = c.String(),
                        IdNguoiThucHienTheoLenhSX = c.String(maxLength: 128),
                        MoTaHangMuc = c.String(),
                        IdNhomCongViec = c.Int(nullable: false),
                        IdMucDoTruyenThong = c.Int(nullable: false),
                        NgayBatDau = c.DateTime(),
                        ThoiGianDuKien = c.Int(nullable: false),
                        NgayHoanThanh = c.DateTime(),
                        SoNguoiThucHien = c.Int(nullable: false),
                        DiemDanhGia = c.Int(nullable: false),
                        HesoKcn = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LoaiHangMuc = c.Boolean(nullable: false),
                        TrangThai = c.Boolean(nullable: false),
                        Created_at = c.DateTime(),
                        Updated_at = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ApplicationUsers", t => t.IdNguoiThucHienTheoLenhSX)
                .ForeignKey("dbo.DuAn", t => t.IdDuAn, cascadeDelete: true)
                .ForeignKey("dbo.HeSoLap", t => t.IdMucDoTruyenThong, cascadeDelete: true)
                .ForeignKey("dbo.HeSoTg", t => t.ThoiGianDuKien, cascadeDelete: true)
                .ForeignKey("dbo.NhomCongViec", t => t.IdNhomCongViec, cascadeDelete: true)
                .Index(t => t.IdDuAn)
                .Index(t => t.IdNguoiThucHienTheoLenhSX)
                .Index(t => t.IdNhomCongViec)
                .Index(t => t.IdMucDoTruyenThong)
                .Index(t => t.ThoiGianDuKien);
            
            CreateTable(
                "dbo.HeSoLap",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SoNam = c.String(),
                        Hesl = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HeSoTg",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ThoiGianDk = c.String(),
                        HeSoTgdk = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
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
                "dbo.ThamGia",
                c => new
                    {
                        IdHangMuc = c.Int(nullable: false),
                        IdNhanVien = c.String(nullable: false, maxLength: 128),
                        LoaiHangMuc = c.Int(nullable: false),
                        HeSoThamGia = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.IdHangMuc, t.IdNhanVien })
                .ForeignKey("dbo.ApplicationUsers", t => t.IdNhanVien, cascadeDelete: true)
                .ForeignKey("dbo.HangMuc", t => t.IdHangMuc, cascadeDelete: true)
                .Index(t => t.IdHangMuc)
                .Index(t => t.IdNhanVien);
            
            CreateTable(
                "dbo.HeSoNhanCong",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SoNguoiThucHien = c.Int(nullable: false),
                        HeSoNcKcn = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ThongBao",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TieuDe = c.String(),
                        NoiDung = c.String(),
                        NguoiTao = c.String(),
                        MoreFile = c.String(storeType: "xml"),
                        Created_at = c.DateTime(),
                        Updated_at = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationUserRoles", "IdentityRole_Id", "dbo.ApplicationRoles");
            DropForeignKey("dbo.ThamGia", "IdHangMuc", "dbo.HangMuc");
            DropForeignKey("dbo.ThamGia", "IdNhanVien", "dbo.ApplicationUsers");
            DropForeignKey("dbo.HangMuc", "IdNhomCongViec", "dbo.NhomCongViec");
            DropForeignKey("dbo.HangMuc", "ThoiGianDuKien", "dbo.HeSoTg");
            DropForeignKey("dbo.HangMuc", "IdMucDoTruyenThong", "dbo.HeSoLap");
            DropForeignKey("dbo.HangMuc", "IdDuAn", "dbo.DuAn");
            DropForeignKey("dbo.HangMuc", "IdNguoiThucHienTheoLenhSX", "dbo.ApplicationUsers");
            DropForeignKey("dbo.DuAn", "IdHopDong", "dbo.HopDong");
            DropForeignKey("dbo.HopDong", "IdKhachHang", "dbo.khachHang");
            DropForeignKey("dbo.ApplicationUserGroups", "IdUser", "dbo.ApplicationUsers");
            DropForeignKey("dbo.ApplicationUserRoles", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.ApplicationUserLogins", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.ApplicationUserClaims", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.ApplicationUserGroups", "IdGroup", "dbo.ApplicationGroups");
            DropForeignKey("dbo.ApplicationRoleGroups", "RoleId", "dbo.ApplicationRoles");
            DropForeignKey("dbo.ApplicationRoleGroups", "GroupId", "dbo.ApplicationGroups");
            DropIndex("dbo.ThamGia", new[] { "IdNhanVien" });
            DropIndex("dbo.ThamGia", new[] { "IdHangMuc" });
            DropIndex("dbo.HangMuc", new[] { "ThoiGianDuKien" });
            DropIndex("dbo.HangMuc", new[] { "IdMucDoTruyenThong" });
            DropIndex("dbo.HangMuc", new[] { "IdNhomCongViec" });
            DropIndex("dbo.HangMuc", new[] { "IdNguoiThucHienTheoLenhSX" });
            DropIndex("dbo.HangMuc", new[] { "IdDuAn" });
            DropIndex("dbo.HopDong", new[] { "IdKhachHang" });
            DropIndex("dbo.DuAn", new[] { "IdHopDong" });
            DropIndex("dbo.ApplicationUserLogins", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ApplicationUserClaims", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ApplicationUserGroups", new[] { "IdUser" });
            DropIndex("dbo.ApplicationUserGroups", new[] { "IdGroup" });
            DropIndex("dbo.ApplicationRoleGroups", new[] { "RoleId" });
            DropIndex("dbo.ApplicationRoleGroups", new[] { "GroupId" });
            DropIndex("dbo.ApplicationUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.ApplicationUserRoles", new[] { "ApplicationUser_Id" });
            DropTable("dbo.ThongBao");
            DropTable("dbo.HeSoNhanCong");
            DropTable("dbo.ThamGia");
            DropTable("dbo.NhomCongViec");
            DropTable("dbo.HeSoTg");
            DropTable("dbo.HeSoLap");
            DropTable("dbo.HangMuc");
            DropTable("dbo.Errors");
            DropTable("dbo.khachHang");
            DropTable("dbo.HopDong");
            DropTable("dbo.DuAn");
            DropTable("dbo.ApplicationUserLogins");
            DropTable("dbo.ApplicationUserClaims");
            DropTable("dbo.ApplicationUsers");
            DropTable("dbo.ApplicationUserGroups");
            DropTable("dbo.ApplicationRoleGroups");
            DropTable("dbo.ApplicationUserRoles");
            DropTable("dbo.ApplicationRoles");
            DropTable("dbo.ApplicationGroups");
        }
    }
}
