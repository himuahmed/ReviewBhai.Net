using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReviewVaiApp.Models
{
	public class Stars
	{
		public long Id { get; set; }
		
		public double Food { get; set; }
		public double Environment { get; set; }
		public double Service { get; set; }
		public double Cleanliness { get; set; }
	}

	
}