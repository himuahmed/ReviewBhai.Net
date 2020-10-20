using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReviewVaiApp.Models
{
	public class SubComment
	{
		public long Id { get; set; }
		public PostComment PostComment { get; set; }
		public long PostCommentId { get; set; }
		public string Text { get; set; }
		public virtual ApplicationUser ApplicationUser { get; set; }
		public string ApplicationUserId { get; set; }
		public DateTime TimeStamp { get; set; }

		public virtual ICollection<ReplyReaction> ReplyReactions { get; set; }
	}
}