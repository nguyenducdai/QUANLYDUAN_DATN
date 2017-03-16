namespace QLDuAn.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeDuAnv22 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ThamGia", "IdNhanVien", "dbo.NhanVien");
            DropIndex("dbo.ThamGia", new[] { "IdNhanVien" });
            DropPrimaryKey("dbo.ThamGia");
            AlterColumn("dbo.DuAnHangMuc", "IdNguoiThucHienTheoLenhSX", c => c.String());
            AlterColumn("dbo.ThamGia", "IdNhanVien", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.ThamGia", new[] { "IdDuAn", "IdHangMuc", "IdNhanVien" });
            CreateIndex("dbo.ThamGia", "IdNhanVien");
            AddForeignKey("dbo.ThamGia", "IdNhanVien", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ThamGia", "IdNhanVien", "dbo.AspNetUsers");
            DropIndex("dbo.ThamGia", new[] { "IdNhanVien" });
            DropPrimaryKey("dbo.ThamGia");
            AlterColumn("dbo.ThamGia", "IdNhanVien", c => c.Int(nullable: false));
            AlterColumn("dbo.DuAnHangMuc", "IdNguoiThucHienTheoLenhSX", c => c.Int());
            AddPrimaryKey("dbo.ThamGia", new[] { "IdDuAn", "IdHangMuc", "IdNhanVien" });
            CreateIndex("dbo.ThamGia", "IdNhanVien");
            AddForeignKey("dbo.ThamGia", "IdNhanVien", "dbo.NhanVien", "ID", cascadeDelete: true);
        }
    }
}
