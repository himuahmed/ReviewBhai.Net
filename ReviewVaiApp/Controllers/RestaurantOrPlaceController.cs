using ReviewVaiApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ReviewVaiApp.Controllers
{
    public class RestaurantOrPlaceController : ApiController
    {
		private ApplicationDbContext db = new ApplicationDbContext();

		public IHttpActionResult GetRestaurantOrPlaces()
		{

			var restaurantOrPlaces = db.RestaurantOrPlaces.ToList();

			//foreach(var restaurant in restaurantOrPlaces)
			//{
			//	var id=restaurant.ApplicationUser.Id
			//}

			if (restaurantOrPlaces==null)
			{
				return BadRequest();
			}
			return Ok(restaurantOrPlaces);
		}

		public IHttpActionResult PostRestaurantOrPlace(RestaurantOrPlace restaurantOrPlace)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}
	
			db.RestaurantOrPlaces.Add(restaurantOrPlace);
			db.SaveChanges();
			return Created(new Uri(Request.RequestUri + "/" + restaurantOrPlace.Id), restaurantOrPlace);
		}
		[HttpDelete]
		public IHttpActionResult DeleteRestaurantOrPlace(long id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			var restaurantOrPlace = db.RestaurantOrPlaces.Where(i => i.Id == id).FirstOrDefault();
			if(restaurantOrPlace==null)
			{
				return NotFound();
			}
			db.RestaurantOrPlaces.Remove(restaurantOrPlace);
			db.SaveChanges();
			return Ok();

		}
	}
}
