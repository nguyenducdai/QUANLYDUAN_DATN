namespace QLDuAn.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNhanVienDuAnTb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NhanVienDuAn",
                c => new
                    {
                        IdDuAn = c.Int(nullable: false),
                        IdNhanVien = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.IdDuAn, t.IdNhanVien })
                .ForeignKey("dbo.ApplicationUsers", t => t.IdNhanVien, cascadeDelete: true)
                .ForeignKey("dbo.DuAn", t => t.IdDuAn, cascadeDelete: true)
                .Index(t => t.IdDuAn)
                .Index(t => t.IdNhanVien);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NhanVienDuAn", "IdDuAn", "dbo.DuAn");
            DropForeignKey("dbo.NhanVienDuAn", "IdNhanVien", "dbo.ApplicationUsers");
            DropIndex("dbo.NhanVienDuAn", new[] { "IdNhanVien" });
            DropIndex("dbo.NhanVienDuAn", new[] { "IdDuAn" });
            DropTable("dbo.NhanVienDuAn");
        }
    }
}
