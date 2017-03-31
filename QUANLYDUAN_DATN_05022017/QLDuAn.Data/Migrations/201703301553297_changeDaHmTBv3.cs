namespace QLDuAn.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeDaHmTBv3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.DuAnHangMuc", "DiemHangMuc");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DuAnHangMuc", "DiemHangMuc", c => c.Int(nullable: false));
        }
    }
}
