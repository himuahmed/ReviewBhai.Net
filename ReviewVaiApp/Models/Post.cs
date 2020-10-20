using Newtonsoft.Json;
using ReviewVaiApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReviewVaiApp.Models
{
	//[JsonObject(IsReference = false)]
	public class Post
	{
		
		public long Id { get; set; }
		public string PostTitle { get; set; }
		public bool IsOfferOrPlanned { get; set; }
		public bool IsRecommended { get; set; }
	
		public RestaurantOrPlace RestaurantOrPlace { get; set; }
		public long? RestaurantOrPlaceId { get; set; }


		public virtual ApplicationUser ApplicationUser { get; set; }
		public string ApplicationUserId { get; set; }
		//public List<Tag> Tags { get; set; }
		//public List<Item> Items { get; set; }
		public List<Reaction> Reactions { get; set; }
		public List<Photo> Photos { get; set; }
		public List<PostComment> PostComments { get; set; }
		public DateTime? TimePosted { get; set; }
		public string PostBody { get; set; }
		
		public int FoodOrTravel { get; set; }
		public Stars Stars { get; set; }
		

		public virtual ICollection<Item> Items { get; set; }
		public virtual ICollection<Tag> Tags { get; set; }
	}
}