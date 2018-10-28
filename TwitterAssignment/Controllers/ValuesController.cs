using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TwitterAssignment.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ValuesController : ControllerBase
	{
		// GET api/values
		[HttpGet]
		[Authorize]
		public ActionResult<string> Get()		// This endpoint is just to test the token
		{
			var CurrentUser = HttpContext.User;
			if (CurrentUser.HasClaim(c => c.Type == ClaimTypes.NameIdentifier))
			{
				string Username = CurrentUser.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier).Value;
				return Ok(new { Message = "Hello again " + Username });
			}
			else return StatusCode(403, new { Message = "Invalid Token" });
		}
	}
}
