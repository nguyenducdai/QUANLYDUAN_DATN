namespace QLDuAn.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HeSoLap",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SoNam = c.String(),
                        Hesl = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HeSoNhanCong",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SoNguoiThucHien = c.Int(nullable: false),
                        HeSoNcKcn = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HeSoTg",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ThoiGianDk = c.String(),
                        HeSoTgdk = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.HeSoTg");
            DropTable("dbo.HeSoNhanCong");
            DropTable("dbo.HeSoLap");
        }
    }
}
