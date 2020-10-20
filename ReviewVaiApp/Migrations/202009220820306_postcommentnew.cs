namespace ReviewVaiApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class postcommentnew : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.PostComment", name: "ApplicationUser_Id", newName: "ApplicationUserId");
            RenameIndex(table: "dbo.PostComment", name: "IX_ApplicationUser_Id", newName: "IX_ApplicationUserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.PostComment", name: "IX_ApplicationUserId", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.PostComment", name: "ApplicationUserId", newName: "ApplicationUser_Id");
        }
    }
}
