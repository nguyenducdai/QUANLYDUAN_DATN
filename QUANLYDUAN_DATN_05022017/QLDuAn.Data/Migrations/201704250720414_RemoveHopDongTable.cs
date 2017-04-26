namespace QLDuAn.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveHopDongTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.HopDong", "IdKhachHang", "dbo.khachHang");
            DropForeignKey("dbo.DuAn", "IdHopDong", "dbo.HopDong");
            DropIndex("dbo.DuAn", new[] { "IdHopDong" });
            DropIndex("dbo.HopDong", new[] { "IdKhachHang" });
            AddColumn("dbo.DuAn", "SoHopDong", c => c.String());
            AddColumn("dbo.DuAn", "GiaTriHopDong", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.DuAn", "NgayBatDau", c => c.DateTime(nullable: false));
            AddColumn("dbo.DuAn", "NgayKetThuc", c => c.DateTime(nullable: false));
            AddColumn("dbo.DuAn", "NgayKy", c => c.DateTime(nullable: false));
            AddColumn("dbo.DuAn", "NgayHoanThanh", c => c.DateTime());
            AddColumn("dbo.DuAn", "IdKhachHang", c => c.Int(nullable: false));
            AlterColumn("dbo.DuAn", "TenDuAn", c => c.String());
            CreateIndex("dbo.DuAn", "IdKhachHang");
            AddForeignKey("dbo.DuAn", "IdKhachHang", "dbo.khachHang", "ID", cascadeDelete: true);
            DropColumn("dbo.DuAn", "IdHopDong");
            DropColumn("dbo.DuAn", "SoNgayThucHienThucTe");
            DropColumn("dbo.DuAn", "TongDiem");
            DropColumn("dbo.DuAn", "DonGiaDiemDiem");
            DropTable("dbo.HopDong");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.HopDong",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SoHopDong = c.String(),
                        TenHopDong = c.String(),
                        GiaTriHopDong = c.Decimal(precision: 18, scale: 2),
                        SoNgayThucHien = c.String(),
                        NoiDung = c.String(),
                        NgayBatDau = c.DateTime(nullable: false),
                        NgayKetThuc = c.DateTime(nullable: false),
                        NgayKy = c.DateTime(nullable: false),
                        IdKhachHang = c.Int(nullable: false),
                        Created_at = c.DateTime(),
                        Updated_at = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.DuAn", "DonGiaDiemDiem", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.DuAn", "TongDiem", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.DuAn", "SoNgayThucHienThucTe", c => c.Int(nullable: false));
            AddColumn("dbo.DuAn", "IdHopDong", c => c.Int(nullable: false));
            DropForeignKey("dbo.DuAn", "IdKhachHang", "dbo.khachHang");
            DropIndex("dbo.DuAn", new[] { "IdKhachHang" });
            AlterColumn("dbo.DuAn", "TenDuAn", c => c.String(nullable: false, maxLength: 200));
            DropColumn("dbo.DuAn", "IdKhachHang");
            DropColumn("dbo.DuAn", "NgayHoanThanh");
            DropColumn("dbo.DuAn", "NgayKy");
            DropColumn("dbo.DuAn", "NgayKetThuc");
            DropColumn("dbo.DuAn", "NgayBatDau");
            DropColumn("dbo.DuAn", "GiaTriHopDong");
            DropColumn("dbo.DuAn", "SoHopDong");
            CreateIndex("dbo.HopDong", "IdKhachHang");
            CreateIndex("dbo.DuAn", "IdHopDong");
            AddForeignKey("dbo.DuAn", "IdHopDong", "dbo.HopDong", "ID", cascadeDelete: true);
            AddForeignKey("dbo.HopDong", "IdKhachHang", "dbo.khachHang", "ID", cascadeDelete: true);
        }
    }
}
