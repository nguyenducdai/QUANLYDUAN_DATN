namespace QLDuAn.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeHopDong : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HopDong", "NoiDung", c => c.String());
            AddColumn("dbo.HopDong", "NgayBatDau", c => c.DateTime(nullable: false));
            AddColumn("dbo.HopDong", "NgayKetThuc", c => c.DateTime(nullable: false));
            AddColumn("dbo.HopDong", "Created_at", c => c.DateTime());
            AddColumn("dbo.HopDong", "Updated_at", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.HopDong", "Updated_at");
            DropColumn("dbo.HopDong", "Created_at");
            DropColumn("dbo.HopDong", "NgayKetThuc");
            DropColumn("dbo.HopDong", "NgayBatDau");
            DropColumn("dbo.HopDong", "NoiDung");
        }
    }
}
