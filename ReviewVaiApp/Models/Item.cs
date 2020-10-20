using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReviewVaiApp.Models
{
	public class Item
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
		public DateTime? Timestamp { get; set; }
		public int FoodOrTravel { get; set; }

		[JsonIgnore]
		public virtual ICollection<Post> Posts { get; set; }


	}
}