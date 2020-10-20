namespace ReviewVaiApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RestaurantOrPlaceId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Post", name: "RestaurantOrPalceId", newName: "RestaurantOrPlaceId");
            RenameIndex(table: "dbo.Post", name: "IX_RestaurantOrPalceId", newName: "IX_RestaurantOrPlaceId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Post", name: "IX_RestaurantOrPlaceId", newName: "IX_RestaurantOrPalceId");
            RenameColumn(table: "dbo.Post", name: "RestaurantOrPlaceId", newName: "RestaurantOrPalceId");
        }
    }
}
