namespace QLDuAn.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changHangMucTB : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.HangMuc", "IdMucDoTruyenThong", "dbo.HeSoLap");
            DropForeignKey("dbo.HangMuc", "ThoiGianDuKien", "dbo.HeSoTg");
            DropForeignKey("dbo.HangMuc", "IdNhomCongViec", "dbo.NhomCongViec");
            DropIndex("dbo.HangMuc", new[] { "IdNhomCongViec" });
            DropIndex("dbo.HangMuc", new[] { "IdMucDoTruyenThong" });
            DropIndex("dbo.HangMuc", new[] { "ThoiGianDuKien" });
            AlterColumn("dbo.HangMuc", "IdNhomCongViec", c => c.Int());
            AlterColumn("dbo.HangMuc", "IdMucDoTruyenThong", c => c.Int());
            AlterColumn("dbo.HangMuc", "ThoiGianDuKien", c => c.Int());
            AlterColumn("dbo.HangMuc", "SoNguoiThucHien", c => c.Int());
            AlterColumn("dbo.HangMuc", "DiemDanhGia", c => c.Int());
            AlterColumn("dbo.HangMuc", "HesoKcn", c => c.Decimal(precision: 18, scale: 2));
            CreateIndex("dbo.HangMuc", "IdNhomCongViec");
            CreateIndex("dbo.HangMuc", "IdMucDoTruyenThong");
            CreateIndex("dbo.HangMuc", "ThoiGianDuKien");
            AddForeignKey("dbo.HangMuc", "IdMucDoTruyenThong", "dbo.HeSoLap", "Id");
            AddForeignKey("dbo.HangMuc", "ThoiGianDuKien", "dbo.HeSoTg", "Id");
            AddForeignKey("dbo.HangMuc", "IdNhomCongViec", "dbo.NhomCongViec", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HangMuc", "IdNhomCongViec", "dbo.NhomCongViec");
            DropForeignKey("dbo.HangMuc", "ThoiGianDuKien", "dbo.HeSoTg");
            DropForeignKey("dbo.HangMuc", "IdMucDoTruyenThong", "dbo.HeSoLap");
            DropIndex("dbo.HangMuc", new[] { "ThoiGianDuKien" });
            DropIndex("dbo.HangMuc", new[] { "IdMucDoTruyenThong" });
            DropIndex("dbo.HangMuc", new[] { "IdNhomCongViec" });
            AlterColumn("dbo.HangMuc", "HesoKcn", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.HangMuc", "DiemDanhGia", c => c.Int(nullable: false));
            AlterColumn("dbo.HangMuc", "SoNguoiThucHien", c => c.Int(nullable: false));
            AlterColumn("dbo.HangMuc", "ThoiGianDuKien", c => c.Int(nullable: false));
            AlterColumn("dbo.HangMuc", "IdMucDoTruyenThong", c => c.Int(nullable: false));
            AlterColumn("dbo.HangMuc", "IdNhomCongViec", c => c.Int(nullable: false));
            CreateIndex("dbo.HangMuc", "ThoiGianDuKien");
            CreateIndex("dbo.HangMuc", "IdMucDoTruyenThong");
            CreateIndex("dbo.HangMuc", "IdNhomCongViec");
            AddForeignKey("dbo.HangMuc", "IdNhomCongViec", "dbo.NhomCongViec", "ID", cascadeDelete: true);
            AddForeignKey("dbo.HangMuc", "ThoiGianDuKien", "dbo.HeSoTg", "Id", cascadeDelete: true);
            AddForeignKey("dbo.HangMuc", "IdMucDoTruyenThong", "dbo.HeSoLap", "Id", cascadeDelete: true);
        }
    }
}
