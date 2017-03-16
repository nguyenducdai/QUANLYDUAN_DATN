namespace QLDuAn.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDuanhangmuc : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.DuAnHangMuc");
            AlterColumn("dbo.DuAnHangMuc", "IdNguoiThucHienTheoLenhSX", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.DuAnHangMuc", new[] { "IdDuAn", "IdHangMuc", "IdNhomCongViec", "IdNguoiThucHienTheoLenhSX" });
            CreateIndex("dbo.DuAnHangMuc", "IdNguoiThucHienTheoLenhSX");
            AddForeignKey("dbo.DuAnHangMuc", "IdNguoiThucHienTheoLenhSX", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DuAnHangMuc", "IdNguoiThucHienTheoLenhSX", "dbo.AspNetUsers");
            DropIndex("dbo.DuAnHangMuc", new[] { "IdNguoiThucHienTheoLenhSX" });
            DropPrimaryKey("dbo.DuAnHangMuc");
            AlterColumn("dbo.DuAnHangMuc", "IdNguoiThucHienTheoLenhSX", c => c.String());
            AddPrimaryKey("dbo.DuAnHangMuc", new[] { "IdDuAn", "IdHangMuc", "IdNhomCongViec" });
        }
    }
}
