namespace QLDuAn.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addColumnDuAnTb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DuAn", "TongDiemTT", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.DuAn", "TongDiemGT", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.DuAn", "DonGiaDiemTT", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.DuAn", "DonGiaDiemGT", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DuAn", "DonGiaDiemGT");
            DropColumn("dbo.DuAn", "DonGiaDiemTT");
            DropColumn("dbo.DuAn", "TongDiemGT");
            DropColumn("dbo.DuAn", "TongDiemTT");
        }
    }
}
