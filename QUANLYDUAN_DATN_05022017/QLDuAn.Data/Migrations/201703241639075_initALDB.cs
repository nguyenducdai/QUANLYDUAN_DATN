namespace QLDuAn.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initALDB : DbMigration
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
                "dbo.IdentityRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Description = c.String(maxLength: 250),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.IdentityRoles", t => t.IdentityRole_Id)
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
                .ForeignKey("dbo.IdentityRoles", t => t.RoleId, cascadeDelete: true)
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
                "dbo.IdentityUserClaims",
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
                "dbo.IdentityUserLogins",
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
                "dbo.DuAnHangMuc",
                c => new
                    {
                        IdDuAn = c.Int(nullable: false),
                        IdHangMuc = c.Int(nullable: false),
                        IdNguoiThucHienTheoLenhSX = c.String(maxLength: 128),
                        IdNhomCongViec = c.Int(nullable: false),
                        MucDoTruyenThong = c.Int(nullable: false),
                        NgayBatDau = c.DateTime(),
                        NgayDuKienKetThuc = c.DateTime(),
                        NgayHoanThanh = c.DateTime(),
                        HeSoLapLai = c.Decimal(nullable: false, precision: 18, scale: 2),
                        HeSoNhanCong = c.Decimal(nullable: false, precision: 18, scale: 2),
                        HeSoCongViec = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SoNguoiThucHien = c.Int(nullable: false),
                        HeSoThoiGian = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiemDanhGia = c.Int(nullable: false),
                        DiemHangMuc = c.Int(nullable: false),
                        LoaiHangMuc = c.Int(nullable: false),
                        TrangThai = c.Boolean(nullable: false),
                        Created_at = c.DateTime(),
                        Updated_at = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.IdDuAn, t.IdHangMuc })
                .ForeignKey("dbo.ApplicationUsers", t => t.IdNguoiThucHienTheoLenhSX)
                .ForeignKey("dbo.DuAn", t => t.IdDuAn, cascadeDelete: true)
                .ForeignKey("dbo.HangMuc", t => t.IdHangMuc, cascadeDelete: true)
                .ForeignKey("dbo.NhomCongViec", t => t.IdNhomCongViec, cascadeDelete: true)
                .Index(t => t.IdDuAn)
                .Index(t => t.IdHangMuc)
                .Index(t => t.IdNguoiThucHienTheoLenhSX)
                .Index(t => t.IdNhomCongViec);
            
            CreateTable(
                "dbo.HangMuc",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TenHangMuc = c.String(nullable: false),
                        MoTaHangMuc = c.String(),
                        LoaiHangMuc = c.Boolean(nullable: false),
                        TrangThai = c.Boolean(nullable: false),
                        Created_at = c.DateTime(),
                        Updated_at = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ThamGia",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdDuAn = c.Int(nullable: false),
                        IdHangMuc = c.Int(nullable: false),
                        IdNhanVien = c.String(maxLength: 128),
                        LoaiHangMuc = c.Int(nullable: false),
                        HeSoThamGia = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ThuNhap = c.Decimal(precision: 18, scale: 2),
                        DiemThanhVien = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.IdNhanVien)
                .ForeignKey("dbo.DuAn", t => t.IdDuAn, cascadeDelete: true)
                .ForeignKey("dbo.HangMuc", t => t.IdHangMuc, cascadeDelete: true)
                .Index(t => t.IdDuAn)
                .Index(t => t.IdHangMuc)
                .Index(t => t.IdNhanVien);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRoles", "IdentityRole_Id", "dbo.IdentityRoles");
            DropForeignKey("dbo.DuAnHangMuc", "IdNhomCongViec", "dbo.NhomCongViec");
            DropForeignKey("dbo.ThamGia", "IdHangMuc", "dbo.HangMuc");
            DropForeignKey("dbo.ThamGia", "IdDuAn", "dbo.DuAn");
            DropForeignKey("dbo.ThamGia", "IdNhanVien", "dbo.ApplicationUsers");
            DropForeignKey("dbo.DuAnHangMuc", "IdHangMuc", "dbo.HangMuc");
            DropForeignKey("dbo.DuAnHangMuc", "IdDuAn", "dbo.DuAn");
            DropForeignKey("dbo.DuAnHangMuc", "IdNguoiThucHienTheoLenhSX", "dbo.ApplicationUsers");
            DropForeignKey("dbo.DuAn", "IdHopDong", "dbo.HopDong");
            DropForeignKey("dbo.HopDong", "IdKhachHang", "dbo.khachHang");
            DropForeignKey("dbo.ApplicationUserGroups", "IdUser", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserRoles", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserLogins", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserClaims", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.ApplicationUserGroups", "IdGroup", "dbo.ApplicationGroups");
            DropForeignKey("dbo.ApplicationRoleGroups", "RoleId", "dbo.IdentityRoles");
            DropForeignKey("dbo.ApplicationRoleGroups", "GroupId", "dbo.ApplicationGroups");
            DropIndex("dbo.ThamGia", new[] { "IdNhanVien" });
            DropIndex("dbo.ThamGia", new[] { "IdHangMuc" });
            DropIndex("dbo.ThamGia", new[] { "IdDuAn" });
            DropIndex("dbo.DuAnHangMuc", new[] { "IdNhomCongViec" });
            DropIndex("dbo.DuAnHangMuc", new[] { "IdNguoiThucHienTheoLenhSX" });
            DropIndex("dbo.DuAnHangMuc", new[] { "IdHangMuc" });
            DropIndex("dbo.DuAnHangMuc", new[] { "IdDuAn" });
            DropIndex("dbo.HopDong", new[] { "IdKhachHang" });
            DropIndex("dbo.DuAn", new[] { "IdHopDong" });
            DropIndex("dbo.IdentityUserLogins", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaims", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ApplicationUserGroups", new[] { "IdUser" });
            DropIndex("dbo.ApplicationUserGroups", new[] { "IdGroup" });
            DropIndex("dbo.ApplicationRoleGroups", new[] { "RoleId" });
            DropIndex("dbo.ApplicationRoleGroups", new[] { "GroupId" });
            DropIndex("dbo.IdentityUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "ApplicationUser_Id" });
            DropTable("dbo.Errors");
            DropTable("dbo.NhomCongViec");
            DropTable("dbo.ThamGia");
            DropTable("dbo.HangMuc");
            DropTable("dbo.DuAnHangMuc");
            DropTable("dbo.khachHang");
            DropTable("dbo.HopDong");
            DropTable("dbo.DuAn");
            DropTable("dbo.IdentityUserLogins");
            DropTable("dbo.IdentityUserClaims");
            DropTable("dbo.ApplicationUsers");
            DropTable("dbo.ApplicationUserGroups");
            DropTable("dbo.ApplicationRoleGroups");
            DropTable("dbo.IdentityUserRoles");
            DropTable("dbo.IdentityRoles");
            DropTable("dbo.ApplicationGroups");
        }
    }
}
