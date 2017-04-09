namespace QLDuAn.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeGiaTriHd : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.HopDong", "GiaTriHopDong", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.HopDong", "GiaTriHopDong", c => c.String());
        }
    }
}
