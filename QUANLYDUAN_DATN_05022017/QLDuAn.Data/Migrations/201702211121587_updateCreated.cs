namespace QLDuAn.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateCreated : DbMigration
    {
        public override void Up()
        {
            AlterColumn("HangMuc" , "Created_at" , x=>x.DateTime(nullable:true)); 
            AlterColumn("HangMuc" , "Updated_at", x => x.DateTime(nullable: true)); 
        }
        
        public override void Down()
        {
            DropColumn("HangMuc", "Created_at");
            DropColumn("HangMuc", "Updated_at");
        }
    }
}
