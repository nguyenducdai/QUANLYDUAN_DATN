namespace QLDuAn.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeFginkeyTGTB : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ThamGia", "IdNhanVien", "dbo.ApplicationUsers");
            DropIndex("dbo.ThamGia", new[] { "IdNhanVien" });
            DropPrimaryKey("dbo.ThamGia");
            AlterColumn("dbo.ThamGia", "IdNhanVien", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.ThamGia", new[] { "IdDuAn", "IdHangMuc" });
            CreateIndex("dbo.ThamGia", "IdNhanVien");
            AddForeignKey("dbo.ThamGia", "IdNhanVien", "dbo.ApplicationUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ThamGia", "IdNhanVien", "dbo.ApplicationUsers");
            DropIndex("dbo.ThamGia", new[] { "IdNhanVien" });
            DropPrimaryKey("dbo.ThamGia");
            AlterColumn("dbo.ThamGia", "IdNhanVien", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.ThamGia", new[] { "IdDuAn", "IdHangMuc", "IdNhanVien" });
            CreateIndex("dbo.ThamGia", "IdNhanVien");
            AddForeignKey("dbo.ThamGia", "IdNhanVien", "dbo.ApplicationUsers", "Id", cascadeDelete: true);
        }
    }
}
