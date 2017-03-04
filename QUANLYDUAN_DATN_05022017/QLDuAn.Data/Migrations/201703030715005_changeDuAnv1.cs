namespace QLDuAn.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeDuAnv1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DuAn", "TrangThai", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DuAn", "TrangThai", c => c.Boolean(nullable: false));
        }
    }
}
