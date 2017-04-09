namespace QLDuAn.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeLoaiHM : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.HangMuc", "LoaiHangMuc", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.HangMuc", "LoaiHangMuc", c => c.Boolean(nullable: false));
        }
    }
}
