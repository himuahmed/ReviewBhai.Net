namespace ReviewVaiApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db_version_01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Badge",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Point = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CommentReaction",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PostCommentId = c.Long(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                        IsLiked = c.Int(nullable: false),
                        IsHelpfull = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.PostComment", t => t.PostCommentId, cascadeDelete: true)
                .Index(t => t.PostCommentId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Photo = c.String(),
                        Location = c.String(),
                        CreatedAt = c.DateTime(),
                        Gender = c.Int(nullable: false),
                        ProfileTitleId = c.Long(),
                        BadgeId = c.Long(),
                        Contact = c.Int(nullable: false),
                        ProfileType = c.String(),
                        IsBanned = c.Boolean(nullable: false),
                        Name = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Badge", t => t.BadgeId)
                .ForeignKey("dbo.ProfileTitle", t => t.ProfileTitleId)
                .Index(t => t.ProfileTitleId)
                .Index(t => t.BadgeId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ProfileTitle",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(),
                        Point = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.PostComment",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PostId = c.Long(nullable: false),
                        Text = c.String(),
                        TimeStamp = c.DateTime(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Post", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Post",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PostTitle = c.String(),
                        IsOfferOrPlanned = c.Boolean(nullable: false),
                        IsRecommended = c.Boolean(nullable: false),
                        RestaurantOrPalceId = c.Long(),
                        ApplicationUserId = c.String(maxLength: 128),
                        TimePosted = c.DateTime(),
                        PostBody = c.String(),
                        FoodOrTravel = c.Int(nullable: false),
                        Stars_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.RestaurantOrPlace", t => t.RestaurantOrPalceId)
                .ForeignKey("dbo.Stars", t => t.Stars_Id)
                .Index(t => t.RestaurantOrPalceId)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.Stars_Id);
            
            CreateTable(
                "dbo.Item",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.String(),
                        Timestamp = c.DateTime(),
                        FoodOrTravel = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Photo",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PostId = c.Long(nullable: false),
                        Caption = c.String(),
                        Url = c.String(),
                        TimeStamp = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Post", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId);
            
            CreateTable(
                "dbo.Reaction",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PostId = c.Long(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                        IsLiked = c.Int(nullable: false),
                        IsHelpfull = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.Post", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.RestaurantOrPlace",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TotalRecommendation = c.Int(nullable: false),
                        TotalReviews = c.Int(nullable: false),
                        RestOrPlace = c.Int(nullable: false),
                        Location = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Stars",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Food = c.Double(nullable: false),
                        Environment = c.Double(nullable: false),
                        Service = c.Double(nullable: false),
                        Cleanliness = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tag",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        TimeStamp = c.DateTime(),
                        FOodOrTravel = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DiscussionComment",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DiscussionId = c.Long(nullable: false),
                        Text = c.String(),
                        ParentId = c.Long(nullable: false),
                        TimeStamp = c.DateTime(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Discussion", t => t.DiscussionId, cascadeDelete: true)
                .Index(t => t.DiscussionId)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Discussion",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Question = c.String(),
                        TimeStamp = c.DateTime(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ReplyReaction",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SubCommentId = c.Long(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                        IsLiked = c.Int(nullable: false),
                        IsHelpfull = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.SubComment", t => t.SubCommentId, cascadeDelete: true)
                .Index(t => t.SubCommentId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.SubComment",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PostCommentId = c.Long(nullable: false),
                        Text = c.String(),
                        ApplicationUserId = c.String(maxLength: 128),
                        TimeStamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.PostComment", t => t.PostCommentId, cascadeDelete: true)
                .Index(t => t.PostCommentId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.PostItems",
                c => new
                    {
                        PostId = c.Long(nullable: false),
                        ItemId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.PostId, t.ItemId })
                .ForeignKey("dbo.Post", t => t.PostId, cascadeDelete: true)
                .ForeignKey("dbo.Item", t => t.ItemId, cascadeDelete: true)
                .Index(t => t.PostId)
                .Index(t => t.ItemId);
            
            CreateTable(
                "dbo.TagPosts",
                c => new
                    {
                        PostId = c.Long(nullable: false),
                        TagId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.PostId, t.TagId })
                .ForeignKey("dbo.Post", t => t.PostId, cascadeDelete: true)
                .ForeignKey("dbo.Tag", t => t.TagId, cascadeDelete: true)
                .Index(t => t.PostId)
                .Index(t => t.TagId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ReplyReaction", "SubCommentId", "dbo.SubComment");
            DropForeignKey("dbo.SubComment", "PostCommentId", "dbo.PostComment");
            DropForeignKey("dbo.SubComment", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ReplyReaction", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.DiscussionComment", "DiscussionId", "dbo.Discussion");
            DropForeignKey("dbo.Discussion", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.DiscussionComment", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TagPosts", "TagId", "dbo.Tag");
            DropForeignKey("dbo.TagPosts", "PostId", "dbo.Post");
            DropForeignKey("dbo.Post", "Stars_Id", "dbo.Stars");
            DropForeignKey("dbo.Post", "RestaurantOrPalceId", "dbo.RestaurantOrPlace");
            DropForeignKey("dbo.RestaurantOrPlace", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reaction", "PostId", "dbo.Post");
            DropForeignKey("dbo.Reaction", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PostComment", "PostId", "dbo.Post");
            DropForeignKey("dbo.Photo", "PostId", "dbo.Post");
            DropForeignKey("dbo.PostItems", "ItemId", "dbo.Item");
            DropForeignKey("dbo.PostItems", "PostId", "dbo.Post");
            DropForeignKey("dbo.Post", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CommentReaction", "PostCommentId", "dbo.PostComment");
            DropForeignKey("dbo.PostComment", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.CommentReaction", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "ProfileTitleId", "dbo.ProfileTitle");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "BadgeId", "dbo.Badge");
            DropIndex("dbo.TagPosts", new[] { "TagId" });
            DropIndex("dbo.TagPosts", new[] { "PostId" });
            DropIndex("dbo.PostItems", new[] { "ItemId" });
            DropIndex("dbo.PostItems", new[] { "PostId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.SubComment", new[] { "ApplicationUserId" });
            DropIndex("dbo.SubComment", new[] { "PostCommentId" });
            DropIndex("dbo.ReplyReaction", new[] { "ApplicationUserId" });
            DropIndex("dbo.ReplyReaction", new[] { "SubCommentId" });
            DropIndex("dbo.Discussion", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.DiscussionComment", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.DiscussionComment", new[] { "DiscussionId" });
            DropIndex("dbo.RestaurantOrPlace", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Reaction", new[] { "ApplicationUserId" });
            DropIndex("dbo.Reaction", new[] { "PostId" });
            DropIndex("dbo.Photo", new[] { "PostId" });
            DropIndex("dbo.Post", new[] { "Stars_Id" });
            DropIndex("dbo.Post", new[] { "ApplicationUserId" });
            DropIndex("dbo.Post", new[] { "RestaurantOrPalceId" });
            DropIndex("dbo.PostComment", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.PostComment", new[] { "PostId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "BadgeId" });
            DropIndex("dbo.AspNetUsers", new[] { "ProfileTitleId" });
            DropIndex("dbo.CommentReaction", new[] { "ApplicationUserId" });
            DropIndex("dbo.CommentReaction", new[] { "PostCommentId" });
            DropTable("dbo.TagPosts");
            DropTable("dbo.PostItems");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.SubComment");
            DropTable("dbo.ReplyReaction");
            DropTable("dbo.Discussion");
            DropTable("dbo.DiscussionComment");
            DropTable("dbo.Tag");
            DropTable("dbo.Stars");
            DropTable("dbo.RestaurantOrPlace");
            DropTable("dbo.Reaction");
            DropTable("dbo.Photo");
            DropTable("dbo.Item");
            DropTable("dbo.Post");
            DropTable("dbo.PostComment");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.ProfileTitle");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.CommentReaction");
            DropTable("dbo.Badge");
        }
    }
}
