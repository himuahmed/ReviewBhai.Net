using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReviewVaiApp.Models.ViewModels
{
	public class UpdateUserViewModel
	{
		public string Name { get; set; }
		public string Location { get; set; }
		public string Email { get; set; }
		public int Contact { get; set; }
	}
}