using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReviewVaiApp.Models
{
	public class Discussion
	{
		public long Id { get; set; }
		public string Question { get; set; }
		public DateTime? TimeStamp { get; set; }
		public ApplicationUser ApplicationUser { get; set; }
		public List<DiscussionComment> DiscussionComments { get; set; }
	}
}