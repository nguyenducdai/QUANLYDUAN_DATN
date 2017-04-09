namespace QLDuAn.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteTbNvDa : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.NhanVienDuAn", "IdNhanVien", "dbo.ApplicationUsers");
            DropForeignKey("dbo.NhanVienDuAn", "IdDuAn", "dbo.DuAn");
            DropIndex("dbo.NhanVienDuAn", new[] { "IdDuAn" });
            DropIndex("dbo.NhanVienDuAn", new[] { "IdNhanVien" });
            DropTable("dbo.NhanVienDuAn");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.NhanVienDuAn",
                c => new
                    {
                        IdDuAn = c.Int(nullable: false),
                        IdNhanVien = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.IdDuAn, t.IdNhanVien });
            
            CreateIndex("dbo.NhanVienDuAn", "IdNhanVien");
            CreateIndex("dbo.NhanVienDuAn", "IdDuAn");
            AddForeignKey("dbo.NhanVienDuAn", "IdDuAn", "dbo.DuAn", "ID", cascadeDelete: true);
            AddForeignKey("dbo.NhanVienDuAn", "IdNhanVien", "dbo.ApplicationUsers", "Id", cascadeDelete: true);
        }
    }
}
