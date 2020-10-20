using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReviewVaiApp.Models
{
	public class ReplyReaction
	{
		public long Id { get; set; }
		[JsonIgnore]
		public SubComment SubComment { get; set; }
		public long SubCommentId { get; set; }
		public virtual ApplicationUser ApplicationUser { get; set; }
		public string ApplicationUserId { get; set; }
		public int IsLiked { get; set; }
		public int IsHelpfull { get; set; }
	}
}