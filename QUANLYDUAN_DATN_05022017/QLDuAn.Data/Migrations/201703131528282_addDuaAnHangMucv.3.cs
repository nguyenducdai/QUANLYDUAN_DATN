namespace QLDuAn.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDuaAnHangMucv3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DuAnHangMuc", "LoaiHangMuc", c => c.Int(nullable: false));
            AddColumn("dbo.DuAnHangMuc", "TrangThai", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DuAnHangMuc", "TrangThai");
            DropColumn("dbo.DuAnHangMuc", "LoaiHangMuc");
        }
    }
}
