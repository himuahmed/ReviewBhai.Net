using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReviewVaiApp.Models.ViewModels
{
	public class ItemTagRestaurantOrPlace
	{
	public List<Item> Items { get; set; }
		public List<Tag> Tags { get; set; }
		public List<RestaurantOrPlace> RestaurantOrPlaces { get; set; }
	}
}