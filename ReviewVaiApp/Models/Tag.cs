using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReviewVaiApp.Models
{
	public class Tag
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public DateTime? TimeStamp { get; set; }
		
		public int FOodOrTravel { get; set; }
		[JsonIgnore]
		public virtual ICollection<Post> Posts { get; set; }
	}
}