using System;
using Microsoft.AspNetCore.Mvc;
using TwitterAssignment.Models.RequestModels;
using TwitterAssignment.Models.ResponseModels;

namespace TwitterAssignment.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly Entities.DBContext _Db;
		public UsersController(Entities.DBContext Db)
		{
			_Db = Db;
		}

		// POST: api/Users/Register
		[HttpPost("Register")]
		public ActionResult<RegisterUserResponseModel> Register([FromBody] RegisterUserRequestModel Data)
		{
			try
			{
				if (ModelState.IsValid)
				{
					int ResultCode = Services.UserServices.RegisterUser(Data, _Db);
					if (ResultCode == 0)
					{
						return Ok(new RegisterUserResponseModel()
						{
							Code = ResultCode,
							Message = "Ok"
						});
					}
					else
					{
						return BadRequest(new RegisterUserResponseModel()
						{
							Code = ResultCode,
							Message = "Username already exists!"
						});
					}
				}
				else
				{
					return BadRequest(ModelState);
				}
			}
			catch (Exception e)
			{
				return StatusCode(500, Helpers.ExceptionHandler.Handle(e));
			}
		}

		// POST: api/Users/Login
		[HttpPost("Login")]
		public ActionResult<LoginResponseModel> Login([FromBody]LoginRequestModel Data)
		{
			try
			{
				if(ModelState.IsValid)
				{
					LoginResponseModel loginResponseModel = Services.UserServices.Login(Data,_Db);
					return loginResponseModel;
				}
				else
				{
					return BadRequest(ModelState);
				}
			}
			catch (Exception e)
			{
				return StatusCode(500, Helpers.ExceptionHandler.Handle(e));
			}
		}
	}
}
