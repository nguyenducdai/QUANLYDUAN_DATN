namespace QLDuAn.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeDuAnv2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DuAn", "TongDiem", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.DuAn", "DonGiaDiemDiem", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DuAn", "DonGiaDiemDiem", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.DuAn", "TongDiem", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
