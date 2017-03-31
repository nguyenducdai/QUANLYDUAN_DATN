namespace QLDuAn.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeDaHmTBv2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.DuAnHangMuc", name: "IdHeSoThoiGian", newName: "ThoiGianDuKien");
            RenameIndex(table: "dbo.DuAnHangMuc", name: "IX_IdHeSoThoiGian", newName: "IX_ThoiGianDuKien");
            DropColumn("dbo.DuAnHangMuc", "NgayDuKienKetThuc");
            DropColumn("dbo.DuAnHangMuc", "HeSoLapLai");
            DropColumn("dbo.DuAnHangMuc", "HeSoNhanCong");
            DropColumn("dbo.DuAnHangMuc", "HeSoCongViec");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DuAnHangMuc", "HeSoCongViec", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.DuAnHangMuc", "HeSoNhanCong", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.DuAnHangMuc", "HeSoLapLai", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.DuAnHangMuc", "NgayDuKienKetThuc", c => c.DateTime());
            RenameIndex(table: "dbo.DuAnHangMuc", name: "IX_ThoiGianDuKien", newName: "IX_IdHeSoThoiGian");
            RenameColumn(table: "dbo.DuAnHangMuc", name: "ThoiGianDuKien", newName: "IdHeSoThoiGian");
        }
    }
}
