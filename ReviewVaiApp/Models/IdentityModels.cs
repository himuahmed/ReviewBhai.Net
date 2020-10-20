using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace ReviewVaiApp.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
		public string Photo { get; set; }
		public string Location { get; set; }
		public DateTime? CreatedAt { get; set; }
		[Required]
		public int Gender { get; set; }

		public ProfileTitle ProfileTitle { get; set; }
		public long? ProfileTitleId { get; set; }
		public Badge Badge { get; set; }
		public long? BadgeId { get; set; }

		public int Contact { get; set; }
		public string ProfileType { get; set; }
		
		public bool IsBanned { get; set; }
		public string Name { get; set; }
		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
		public DbSet<RestaurantOrPlace> RestaurantOrPlaces { get; set; }
		public DbSet<Post> Posts { get; set; }
		public DbSet<Stars> Stars { get; set; }
		public DbSet<Tag> Tags { get; set; }
		public DbSet<Reaction> Reactions { get; set; }
		public DbSet<Photo> Photos { get; set; }
		public DbSet<Badge> Badges { get; set; }
		public DbSet<ProfileTitle> ProfileTitles { get; set; }
		public DbSet<Item> Items { get; set; }
		public DbSet<PostComment> PostComments { get; set; }
		public DbSet<Discussion> Discussions { get; set; }
		public DbSet<DiscussionComment> DiscussionComments { get; set; }
		public DbSet<SubComment> SubComments { get; set; }
		public DbSet<CommentReaction> CommentReactions { get; set; }
		public DbSet<ReplyReaction> ReplyReactions { get; set; }
		
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
			modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
			modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
			modelBuilder.Entity<Post>()
				.HasMany(t => t.Items)
				.WithMany(t => t.Posts)
				.Map(m =>
				{
					m.ToTable("PostItems");
					m.MapLeftKey("PostId");
					m.MapRightKey("ItemId");
				});
			modelBuilder.Entity<Post>()
				.HasMany(t => t.Tags)
				.WithMany(t => t.Posts)
				.Map(m =>
				{
					m.ToTable("TagPosts");
					m.MapLeftKey("PostId");
					m.MapRightKey("TagId");
				});
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
			base.OnModelCreating(modelBuilder);
		}
		public ApplicationDbContext()
            : base("ProjectDbContext", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}