namespace QLDuAn.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeDaHmTB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DuAnHangMuc", "IdMucDoTruyenThong", c => c.Int(nullable: false));
            AddColumn("dbo.DuAnHangMuc", "IdHeSoThoiGian", c => c.Int(nullable: false));
            CreateIndex("dbo.DuAnHangMuc", "IdMucDoTruyenThong");
            CreateIndex("dbo.DuAnHangMuc", "IdHeSoThoiGian");
            AddForeignKey("dbo.DuAnHangMuc", "IdMucDoTruyenThong", "dbo.HeSoLap", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DuAnHangMuc", "IdHeSoThoiGian", "dbo.HeSoTg", "Id", cascadeDelete: true);
            DropColumn("dbo.DuAnHangMuc", "MucDoTruyenThong");
            DropColumn("dbo.DuAnHangMuc", "HeSoThoiGian");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DuAnHangMuc", "HeSoThoiGian", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.DuAnHangMuc", "MucDoTruyenThong", c => c.Int(nullable: false));
            DropForeignKey("dbo.DuAnHangMuc", "IdHeSoThoiGian", "dbo.HeSoTg");
            DropForeignKey("dbo.DuAnHangMuc", "IdMucDoTruyenThong", "dbo.HeSoLap");
            DropIndex("dbo.DuAnHangMuc", new[] { "IdHeSoThoiGian" });
            DropIndex("dbo.DuAnHangMuc", new[] { "IdMucDoTruyenThong" });
            DropColumn("dbo.DuAnHangMuc", "IdHeSoThoiGian");
            DropColumn("dbo.DuAnHangMuc", "IdMucDoTruyenThong");
        }
    }
}
