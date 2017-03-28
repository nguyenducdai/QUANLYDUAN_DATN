namespace QLDuAn.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renameIdentityTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.IdentityRoles", newName: "ApplicationRoles");
            RenameTable(name: "dbo.IdentityUserRoles", newName: "ApplicationUserRoles");
            RenameTable(name: "dbo.IdentityUserClaims", newName: "ApplicationUserClaims");
            RenameTable(name: "dbo.IdentityUserLogins", newName: "ApplicationUserLogins");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ApplicationUserLogins", newName: "IdentityUserLogins");
            RenameTable(name: "dbo.ApplicationUserClaims", newName: "IdentityUserClaims");
            RenameTable(name: "dbo.ApplicationUserRoles", newName: "IdentityUserRoles");
            RenameTable(name: "dbo.ApplicationRoles", newName: "IdentityRoles");
        }
    }
}
