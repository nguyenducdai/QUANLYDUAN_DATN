namespace QLDuAn.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeColunmIdThamGiaTB : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ThamGia", "IdNhanVien", "dbo.ApplicationUsers");
            DropIndex("dbo.ThamGia", new[] { "IdNhanVien" });
            DropPrimaryKey("dbo.ThamGia");
            AlterColumn("dbo.ThamGia", "IdNhanVien", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.ThamGia", new[] { "IdDuAn", "IdHangMuc", "IdNhanVien" });
            CreateIndex("dbo.ThamGia", "IdNhanVien");
            AddForeignKey("dbo.ThamGia", "IdNhanVien", "dbo.ApplicationUsers", "Id", cascadeDelete: true);
            DropColumn("dbo.ThamGia", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ThamGia", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.ThamGia", "IdNhanVien", "dbo.ApplicationUsers");
            DropIndex("dbo.ThamGia", new[] { "IdNhanVien" });
            DropPrimaryKey("dbo.ThamGia");
            AlterColumn("dbo.ThamGia", "IdNhanVien", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.ThamGia", "Id");
            CreateIndex("dbo.ThamGia", "IdNhanVien");
            AddForeignKey("dbo.ThamGia", "IdNhanVien", "dbo.ApplicationUsers", "Id");
        }
    }
}
