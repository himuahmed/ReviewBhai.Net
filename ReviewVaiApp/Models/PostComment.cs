using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReviewVaiApp.Models
{
	public class PostComment
	{
		public long Id { get; set; }
		public virtual ApplicationUser ApplicationUser { get; set; }
		public string ApplicationUserId { get; set; }
		public virtual List<CommentReaction> CommentReactions { get; set; }
		public Post Post { get; set; }
		public long PostId { get; set; }
		public string Text { get; set; }
	
		public DateTime TimeStamp { get; set; }
		public virtual ICollection<SubComment> SubComments { get; set; }
	}
}