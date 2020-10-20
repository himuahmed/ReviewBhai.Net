using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ReviewVaiApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace ReviewVaiApp.Controllers
{
	[Authorize]
    public class TagController : ApiController
    {
		private ApplicationUserManager _userManager;
		private ApplicationDbContext db = new ApplicationDbContext();
		public TagController()
		{
		}

		public TagController(ApplicationUserManager userManager,
			ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
		{
			UserManager = userManager;
			AccessTokenFormat = accessTokenFormat;
		}
		public ApplicationUserManager UserManager
		{
			get
			{
				return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
			}
			private set
			{
				_userManager = value;
			}
		}

		public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; }

		public async System.Threading.Tasks.Task<IHttpActionResult> GetTagsAsync()
		{
			//string Name = HttpContext.Current.User.Identity.Name;
			IdentityUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
			//string id = User.Identity.FindFirst(ClaimTypes.NameIdentifier).Value;
			var tags = db.Tags.ToList();
			if (tags == null)
			{
				return BadRequest();
			}
			return Ok(tags);
		}
		[HttpPost]
		public IHttpActionResult PostTag(Tag tag)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}
			tag.TimeStamp = DateTime.Now;
			db.Tags.Add(tag);
			db.SaveChanges();
			return Created(new Uri(Request.RequestUri + "/" + tag.Id), tag);
		}
		[HttpDelete]
		public IHttpActionResult DeleteTag(long id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}
			var tag = db.Tags.Where(i => i.Id == id).FirstOrDefault();
			if(tag==null)
			{
				return NotFound();
			}
			db.Tags.Remove(tag);
			db.SaveChanges();
			return Ok();
		}
	}
}
