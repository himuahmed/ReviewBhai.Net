using ReviewVaiApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using System.Web.Helpers;
using System.Data.Entity.Validation;
using System.Data.Entity.Migrations;
using System.Web;
using System.IO;



namespace ReviewVaiApp.Controllers
{    [Authorize]
	public class ReviewController : ApiController
	{
		private ApplicationDbContext db = new ApplicationDbContext();
		private object httpRequest;

		//[Route("Get-Review-post")]
		[HttpGet]
		public IHttpActionResult GetReviewPosts()
		{
			var posts = db.Posts.Include(p => p.Stars).Include(r => r.RestaurantOrPlace.ApplicationUser).Include(p => p.Items).Include(p => p.Tags).ToList();

			//if (posts == null)
			//{
			//	return NotFound();
			//}
			foreach (Post post in posts)
			{
				var reactions = db.Reactions.Where(p => p.PostId == post.Id).ToList();
				var photos = db.Photos.Where(p => p.PostId == post.Id).ToList();
				var comments = db.PostComments.Where(p => p.PostId == post.Id).ToList();



			}


			return Ok(posts);
			//string jason = JsonConvert.SerializeObject(posts, Formatting.Indented);
			//return posts.AsQueryable();
		}
		//[Route("Review/Review-Post-By-Id/{id}")]
		//[Route("{id}/ReviewPost",Name="Review-Post")]
		public IHttpActionResult GetReviewPost(long id)
		{
			var post = db.Posts.Include(p => p.Stars).Include(r => r.RestaurantOrPlace.ApplicationUser).Include(p => p.Items).Include(p => p.Tags).First(p => p.Id == id);
			var reactions = db.Reactions.Where(p => p.PostId == id).ToList();
			var photos = db.Photos.Where(p => p.PostId == id).ToList();
			var comments = db.PostComments.Where(p => p.PostId == id).ToList();


			return Ok(post);

		}
		[HttpGet]
		//[Route("{id:alpha}")]
		public IHttpActionResult ReviewPostByUserId(string id)
		{
			var posts = db.Posts.Include(p => p.Stars).Include(r => r.RestaurantOrPlace.ApplicationUser).Include(p => p.Items).Include(p => p.Tags).Where(p => p.ApplicationUserId == id).ToList();
			foreach (Post post in posts)
			{
				var reactions = db.Reactions.Where(p => p.PostId == post.Id).ToList();
				var photos = db.Photos.Where(p => p.PostId == post.Id).ToList();
				var comments = db.PostComments.Where(p => p.PostId == post.Id).ToList();
			}


			return Ok(posts);

		}
        ////--------------------------------------------------------------------//

        //public IHttpActionResult PostImages(HttpPostedFileBase[] images)

        //{
        //    List<string> urls = new List<string>();
        //    try

        //    {
        //        foreach (HttpPostedFileBase image in images)

        //        {

        //            string imagename = System.IO.Path.GetFileName(image.FileName);

        //            image.SaveAs(HttpContext.Current.Server.MapPath("~/ImgUpload/" + imagename));

        //            string filepathtosave = "ImgUpload" + imagename;
        //            urls.Add(filepathtosave);

        //        }

        //        //ViewBag.Message = "Selected Files are Upload successfully.";

        //    }

        //    catch

        //    {



        //    }

        //    return Ok(urls);

        //}
        [HttpGet]
		public IHttpActionResult GetImages(long id)
		{
			var photos = db.Photos.Where(i => i.PostId == id).ToList();
			List<byte[]> images = new List<byte[]>();
			foreach(var photo in photos)
			{
				var url = photo.Url;
				  Byte[] b = System.IO.File.ReadAllBytes(url);
				images.Add(b);
			}
			return Ok(images);
		}

		[HttpPost]
		public IHttpActionResult PostImages()
		{
			//Dictionary<string, object> dict = new Dictionary<string, object>();
			var httpRequest = HttpContext.Current.Request;
			List<string> urls = new List<string>();
			var flag = 0;
			foreach (string file in httpRequest.Files)
			{
				HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

				var postedFile = httpRequest.Files[file];
				if (postedFile != null && postedFile.ContentLength > 0)
				{
					//int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB  

					IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
					var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
					var extension = ext.ToLower();
					if (!AllowedFileExtensions.Contains(extension))
					{

						//var message = string.Format("Please Upload image of type .jpg,.gif,.png.");

						//dict.Add("error", message);
						return BadRequest();
					}

					else
					{
						var filePath = HttpContext.Current.Server.MapPath("~/ImgUpload/" + DateTime.Now.ToString("yyyyMMdd") + postedFile.FileName );
                        string url = "ImgUpload/" + DateTime.Now.ToString("yyyyMMdd") + postedFile.FileName;
                        postedFile.SaveAs(filePath);
						urls.Add(url);

					}

				}
			}
			return Ok(urls);
		}
		[HttpDelete]
		public IHttpActionResult DeleteImage(long Id)
		{
			var photo = db.Photos.Where(p => p.Id == Id).SingleOrDefault();
			var url = photo.Url;
			var imageName = url.Substring(10);
			File.Delete(HttpContext.Current.Server.MapPath("~/ImgUpload/"+imageName));
			db.Photos.Remove(photo);
			db.SaveChanges();
			//FileInfo file = new FileInfo(url);
			//if (file.Exists)
			//{
			//	file.Delete();
			//}
			return Ok();

		}
		//--------------------------------------------------------------------//

		[HttpPost]
		public IHttpActionResult PostReviewPost(Post post)
		{
			if (!User.Identity.IsAuthenticated)
			{
				return BadRequest();
			}
			var userName = User.Identity.Name;
			string json = JsonConvert.SerializeObject(post, Formatting.Indented);
			var model = JsonConvert.DeserializeObject<Post>(json);
			model.TimePosted = DateTime.Now;
			
			var errors = ModelState.Values.SelectMany(v => v.Errors);
			
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}
			
			try
			{
				var items = model.Items;
				List<Item> itemss = new List<Item>();
				if (items != null)
				{
					foreach (var item in items)
					{
						var aItem = item;
						var itemInDb = db.Items.Where(i => i.Id == item.Id).SingleOrDefault();
						if (itemInDb == null)
						{
							db.Items.Add(item);
						}
						itemss.Add(itemInDb);
					}
				}
				var tags = model.Tags;
				List<Tag> tagss = new List<Tag>();
				if(tags!=null)
				{
				foreach(var tag in tags)
				{
					var tagInDb=db.Tags.Where(i => i.Id == tag.Id).SingleOrDefault();
					if (tagInDb == null)
					{
						db.Tags.Add(tag);
					}
					tagss.Add(tagInDb);
				}
				}
				//var restaurantInDb ;
				if (model.RestaurantOrPlace != null)
				{
					var restaurantInDb = db.RestaurantOrPlaces.Where(i => i.Id == model.RestaurantOrPlace.Id).SingleOrDefault();

					if (restaurantInDb == null)
					{
						db.RestaurantOrPlaces.Add(model.RestaurantOrPlace);
					}
					model.RestaurantOrPlace = restaurantInDb;
				}
				else if(model.RestaurantOrPlaceId!=null && model.RestaurantOrPlace != null)
				{
					var restaurantInDb = db.RestaurantOrPlaces.Where(i => i.Id == model.RestaurantOrPlace.Id).SingleOrDefault();

					if (restaurantInDb == null)
					{
						db.RestaurantOrPlaces.Add(model.RestaurantOrPlace);
					}
					model.RestaurantOrPlace = restaurantInDb;
				}
				else if(model.RestaurantOrPlaceId!=null)
				{
					var restaurantInDb = db.RestaurantOrPlaces.Where(i => i.Id == model.RestaurantOrPlaceId).SingleOrDefault();

					if (restaurantInDb == null)
					{
						db.RestaurantOrPlaces.Add(model.RestaurantOrPlace);
					}
					model.RestaurantOrPlace = restaurantInDb;
				}

				//db.Items.AddOrUpdate(items.ToArray());
				model.Tags = tagss;
				model.Items = itemss;
				db.Posts.Add(model);
				//db.Items.AddOrUpdate(items.ToArray());
				db.SaveChanges();
			}
			catch (DbEntityValidationException e)
			{
				foreach (var eve in e.EntityValidationErrors)
				{
					Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
						eve.Entry.Entity.GetType().Name, eve.Entry.State);
					foreach (var ve in eve.ValidationErrors)
					{
						Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
							ve.PropertyName, ve.ErrorMessage);
					}
				}
				throw;
			}
			return Created(new Uri(Request.RequestUri + "/" + model.Id), model);
		}
		[HttpPut]
		public IHttpActionResult UpdatePost(long id, Post post)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}
			var postInDb = db.Posts.Include(p => p.Stars).Include(r => r.RestaurantOrPlace.ApplicationUser).Include(p => p.Items).Include(p => p.Tags).Include(p=>p.Photos).First(p => p.Id == id);
			if (postInDb == null)
			{
				return NotFound();
			}
			var reactions = db.Reactions.Where(p => p.PostId == id).ToList();
			var photos = db.Photos.Where(p => p.PostId == id).ToList();
			foreach (var item in photos)
			{
				db.Entry(item).State = EntityState.Deleted;
			}
			var comments = db.PostComments.Where(p => p.PostId == id).ToList();
			postInDb.FoodOrTravel = post.FoodOrTravel;
			postInDb.IsOfferOrPlanned = post.IsOfferOrPlanned;
			postInDb.IsRecommended = post.IsRecommended;
			var items = post.Items;
			List<Item> itemss = new List<Item>();
			if (items != null)
			{
				foreach (var item in items)
				{
					var aItem = item;
					var itemInDb = db.Items.Where(i => i.Id == item.Id).SingleOrDefault();
					if (itemInDb == null)
					{
						db.Items.Add(item);
					}
					itemss.Add(itemInDb);
				}
			}
			postInDb.Items = itemss;
			postInDb.Photos = post.Photos;
			postInDb.PostBody = post.PostBody;
			postInDb.PostTitle = post.PostTitle;
			if(post.RestaurantOrPlaceId!=null )
			{
				var restaurantInDb = db.RestaurantOrPlaces.Where(i => i.Id == post.RestaurantOrPlaceId).SingleOrDefault();



				if (restaurantInDb != null)
				{
					//db.RestaurantOrPlaces.Add(post.RestaurantOrPlace);

					postInDb.RestaurantOrPlaceId = post.RestaurantOrPlaceId;
					var restaurant = db.RestaurantOrPlaces.Where(r => r.Id == post.RestaurantOrPlaceId).SingleOrDefault();
					postInDb.RestaurantOrPlace = restaurant;
				}
			}
			else if(post.RestaurantOrPlaceId!=null && post.RestaurantOrPlace!=null)
			{

				var restaurantInDb = db.RestaurantOrPlaces.Where(i => i.Id == post.RestaurantOrPlaceId).SingleOrDefault();



				if (restaurantInDb != null)
				{
					//db.RestaurantOrPlaces.Add(post.RestaurantOrPlace);

					postInDb.RestaurantOrPlaceId = post.RestaurantOrPlaceId;
					var restaurant = db.RestaurantOrPlaces.Where(r => r.Id == post.RestaurantOrPlaceId).SingleOrDefault();
					postInDb.RestaurantOrPlace = restaurant;
				}
			}
			 else if (post.RestaurantOrPlace!=null)
			{
				var restaurantInDb = db.RestaurantOrPlaces.Where(i => i.Id == post.RestaurantOrPlace.Id).SingleOrDefault();

				if (restaurantInDb == null)
				{
					db.RestaurantOrPlaces.Add(post.RestaurantOrPlace);
				}
				postInDb.RestaurantOrPlace = post.RestaurantOrPlace;
				postInDb.RestaurantOrPlaceId = null;
			}
			
			postInDb.Stars = post.Stars;
			var tags = post.Tags;
			List<Tag> tagss = new List<Tag>();
			if (tags != null)
			{
				foreach (var tag in tags)
				{
					var tagInDb = db.Tags.Where(i => i.Id == tag.Id).SingleOrDefault();
					if (tagInDb == null)
					{
						db.Tags.Add(tag);
					}
					tagss.Add(tagInDb);
				}
			}
			postInDb.Tags = tagss;
			
			db.SaveChanges();


			return Ok();
		}
		[HttpDelete]
		public IHttpActionResult DeletePost(long id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}
			var postInDb = db.Posts.Include(p => p.Stars).Include(r => r.RestaurantOrPlace.ApplicationUser).Include(p => p.Items).Include(p => p.Tags).Include(p => p.Photos).First(p => p.Id == id);
			if (postInDb == null)
			{
				return NotFound();
			}
			var reactions = db.Reactions.Where(p => p.PostId == id).ToList();
			var photos = db.Photos.Where(p => p.PostId == id).ToList();
			foreach (var item in photos)
			{
				db.Entry(item).State = EntityState.Deleted;
			}
			db.Posts.Remove(postInDb);
			db.SaveChanges();
			return Ok();

		}
		[HttpGet]
		public IHttpActionResult GetComment(long id)
		{
			var commnet = db.PostComments.Where(c => c.PostId == id).ToList();
			return Ok(commnet);
		}
		public IHttpActionResult PostAComment(PostComment postComment)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}
			postComment.TimeStamp = DateTime.Now;
			db.PostComments.Add(postComment);
			db.SaveChanges();
			return Created(new Uri(Request.RequestUri + "/" + postComment.Id), postComment);
		}
		public IHttpActionResult DeleteAComment(long id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}
			var commentInDb = db.PostComments.Where(i => i.Id == id).FirstOrDefault();
			if(commentInDb==null)
			{
				return NotFound();

			}
			db.PostComments.Remove(commentInDb);
			db.SaveChanges();
			return Ok();
		}
		[HttpGet]
		public IHttpActionResult GetReactions()
		{
			var posts = db.Posts.ToList();
			List<Reaction> reactions = new List<Reaction>();
			foreach(var post in posts )
			{
				var reaction = db.Reactions.Where(p => p.PostId == post.Id).FirstOrDefault();
				reactions.Add(reaction);
			}
			//var reactions = db.Reactions.Where(p => p.PostId == post.Id).ToList();
			return Ok(reactions);
		}
		[HttpGet]
		public IHttpActionResult GetReaction(long id)
		{
			var post = db.Posts.FirstOrDefault(p => p.Id == id);
			var reactions = db.Reactions.Where(p => p.PostId == post.Id).ToList();
			return Ok(reactions);
		}
		[HttpPost]
		public IHttpActionResult CreateReaction(Reaction reaction)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}
			db.Reactions.Add(reaction);
			db.SaveChanges();
			return Created(new Uri(Request.RequestUri + "/" + reaction.Id), reaction);

		}
		[HttpPut]
		public IHttpActionResult UpdateReaction(long id,Reaction reaction)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}
			var reactionInDB = db.Reactions.SingleOrDefault(i => i.Id == id);
			if (reactionInDB == null)
			{
				return NotFound();
			}
			reactionInDB.IsHelpfull = reaction.IsHelpfull;
			reactionInDB.IsLiked = reaction.IsLiked;
			reactionInDB.PostId = reaction.PostId;
			reactionInDB.ApplicationUserId = reaction.ApplicationUserId;
			db.SaveChanges();
			return Ok();
		}
		[HttpDelete]
		public IHttpActionResult DeleteReaction(long id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}
			var reaction = db.Reactions.SingleOrDefault(i => i.Id == id);
			if (reaction == null)
			{
				return NotFound();
			}
			db.Reactions.Remove(reaction);
			db.SaveChanges();
			return Ok();
		}
		[HttpGet]
		public IHttpActionResult GetRepliesInAComment(long id)
		{
			var replay = db.SubComments.Include(u=>u.ApplicationUser).Where(i => i.PostCommentId == id).ToList();
			return Ok(replay);
		}
		[HttpPost]
		public IHttpActionResult PostAReplayAComment(SubComment subComment)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}
			subComment.TimeStamp = DateTime.Now;
			db.SubComments.Add(subComment);
			db.SaveChanges();
			return Created(new Uri(Request.RequestUri + "/" + subComment.Id), subComment);
			
		}
		public IHttpActionResult DeleteAReplyInAComment(long id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}
			var replyInDb = db.SubComments.Where(i => i.Id == id).FirstOrDefault();
			if(replyInDb==null)
			{
				return NotFound();
			}
			db.SubComments.Remove(replyInDb);
			db.SaveChanges();
			return Ok();

		}
		[HttpGet]
		public IHttpActionResult GetACommentReactions(long id)
		{
			var reaction = db.CommentReactions.Where(i => i.PostCommentId == id).ToList();
			if (reaction == null)
			{
				return NotFound();
			}
			return Ok(reaction);
		}
		[HttpGet]
		public IHttpActionResult GetACommentReaction(long id)
		{
			var reaction = db.CommentReactions.Where(i => i.Id == id).FirstOrDefault();
			if(reaction==null)
			{
				return NotFound();
			}
			return Ok(reaction);
		}
		[HttpPost]
		public IHttpActionResult PostACommentReaction(CommentReaction commentReaction)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}
			db.CommentReactions.Add(commentReaction);
			db.SaveChanges();
			return Created(new Uri(Request.RequestUri + "/" + commentReaction.Id), commentReaction);
		}
		[HttpPut]
		public IHttpActionResult UpdateACommentReaction(long id, CommentReaction commentReaction)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}
			var reactionInDb = db.CommentReactions.Where(i => i.Id == id).FirstOrDefault();
			if (reactionInDb == null)
			{
				return NotFound();
			}
			reactionInDb.IsHelpfull = commentReaction.IsHelpfull;
			reactionInDb.IsLiked = commentReaction.IsLiked;
			db.SaveChanges();
			return Ok();
		}
		[HttpDelete]
		public IHttpActionResult DeleteACommentReaction(long id)
		{

			if (!ModelState.IsValid)
			{
				return BadRequest();
			}
			var reactionInDb = db.CommentReactions.Where(i => i.Id == id).FirstOrDefault();
			db.CommentReactions.Remove(reactionInDb);
			db.SaveChanges();
			return Ok();
		}
		[HttpGet]
		public IHttpActionResult GetAReplyReactions(long id)
		{
			var reaction = db.ReplyReactions.Where(i => i.SubCommentId == id).ToList();
			if (reaction == null)
			{
				return NotFound();
			}

			return Ok(reaction);
		}
		[HttpGet]
		public IHttpActionResult GetAReplyReaction(long id)
		{
			var reaction = db.ReplyReactions.Where(i => i.Id == id).FirstOrDefault();
			if(reaction==null)
			{
				return NotFound();
			}
			return Ok(reaction);
		}
		[HttpPost]
		public IHttpActionResult PostAReplyReaction(ReplyReaction replyReaction)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}
			db.ReplyReactions.Add(replyReaction);
			db.SaveChanges();
			return Created(new Uri(Request.RequestUri + "/" + replyReaction.Id), replyReaction);
		}
		[HttpDelete]
		public IHttpActionResult DeleteAReplyReaction(long id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}
			var reactionInDb = db.ReplyReactions.Where(i => i.Id == id).FirstOrDefault();
			if(reactionInDb==null)
			{
				return NotFound();
			}
			db.ReplyReactions.Remove(reactionInDb);
			db.SaveChanges();
			return Ok();
		}
		[HttpPut]
		public IHttpActionResult UpdateAReplyReaction(long id,ReplyReaction replyReaction)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			var replayReactionInDb = db.ReplyReactions.Where(i => i.Id == id).FirstOrDefault();
			if (replayReactionInDb == null)
			{
				return NotFound();
			}
			replayReactionInDb.IsHelpfull = replyReaction.IsHelpfull;
			replayReactionInDb.IsLiked = replyReaction.IsLiked;
			db.SaveChanges();
			return Ok();
		}
	}
}
