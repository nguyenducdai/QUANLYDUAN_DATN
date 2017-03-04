namespace QLDuAn.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTableDuAn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DuAn", "SoNgayThucHienThucTe", c => c.Int(nullable: false));
            AddColumn("dbo.DuAn", "TongDiem", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.DuAn", "DonGiaDiemDiem", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.DuAn", "GiaTriDuAn");
            DropColumn("dbo.DuAn", "SoNgayThucHien");
            DropColumn("dbo.DuAn", "NgayKy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DuAn", "NgayKy", c => c.DateTime(nullable: false));
            AddColumn("dbo.DuAn", "SoNgayThucHien", c => c.Int(nullable: false));
            AddColumn("dbo.DuAn", "GiaTriDuAn", c => c.Int(nullable: false));
            DropColumn("dbo.DuAn", "DonGiaDiemDiem");
            DropColumn("dbo.DuAn", "TongDiem");
            DropColumn("dbo.DuAn", "SoNgayThucHienThucTe");
        }
    }
}
