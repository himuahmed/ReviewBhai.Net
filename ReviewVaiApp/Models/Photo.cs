using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReviewVaiApp.Models
{
	public class Photo
	{
		public long Id { get; set; }
		public Post Post { get; set; }
		public long PostId { get; set; }
		public string Caption { get; set; }
		public string Url { get; set; }
		public DateTime? TimeStamp { get; set; }
	}
}