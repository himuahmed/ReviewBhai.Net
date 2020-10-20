using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReviewVaiApp.Models
{
	public class DiscussionComment
	{
		public long Id { get; set; }
		public ApplicationUser ApplicationUser { get; set; }

		public Discussion Discussion { get; set; }
		public long DiscussionId { get; set; }
		public string Text { get; set; }
		public long ParentId { get; set; }
		public DateTime TimeStamp { get; set; }
	}
}