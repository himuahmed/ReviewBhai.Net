using ReviewVaiApp.Models;
using ReviewVaiApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ReviewVaiApp.Controllers
{
    [Authorize]
    public class ValuesController : ApiController
    {
		private ApplicationDbContext db = new ApplicationDbContext();
		[HttpGet]
		public IHttpActionResult GetItemTagRestaurantOrPlace()
		{
			ItemTagRestaurantOrPlace itemTagRestaurantOrPlace = new ItemTagRestaurantOrPlace();
			var item = db.Items.ToList();
			var tag = db.Tags.ToList();
			var restaurantOrPlace = db.RestaurantOrPlaces.ToList();
			itemTagRestaurantOrPlace.Items = item;
			itemTagRestaurantOrPlace.Tags = tag;
			itemTagRestaurantOrPlace.RestaurantOrPlaces = restaurantOrPlace;
			return Ok(itemTagRestaurantOrPlace);
		}
		// GET api/values
		public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
