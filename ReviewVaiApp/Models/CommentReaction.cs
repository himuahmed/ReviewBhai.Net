using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReviewVaiApp.Models
{
	public class CommentReaction
	{
		public long Id { get; set; }
		[JsonIgnore]
		public  PostComment PostComment { get; set; }
		public long PostCommentId { get; set; }
		public virtual ApplicationUser ApplicationUser { get; set; }
		public string ApplicationUserId { get; set; }
		public int IsLiked { get; set; }
		public int IsHelpfull { get; set; }
	}
}