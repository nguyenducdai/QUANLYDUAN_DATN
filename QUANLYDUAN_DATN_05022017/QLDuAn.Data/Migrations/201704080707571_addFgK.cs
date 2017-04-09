namespace QLDuAn.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFgK : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ThamGia");
            AddColumn("dbo.ThamGia", "IdDuAn", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.ThamGia", new[] { "IdHangMuc", "IdNhanVien", "IdDuAn" });
            CreateIndex("dbo.ThamGia", "IdDuAn");
            AddForeignKey("dbo.ThamGia", "IdDuAn", "dbo.DuAn", "ID", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ThamGia", "IdDuAn", "dbo.DuAn");
            DropIndex("dbo.ThamGia", new[] { "IdDuAn" });
            DropPrimaryKey("dbo.ThamGia");
            DropColumn("dbo.ThamGia", "IdDuAn");
            AddPrimaryKey("dbo.ThamGia", new[] { "IdHangMuc", "IdNhanVien" });
        }
    }
}
