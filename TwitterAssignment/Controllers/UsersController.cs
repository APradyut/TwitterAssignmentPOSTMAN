using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TwitterAssignment.Models.RequestModels;
using TwitterAssignment.Models.ResponseModels;

namespace TwitterAssignment.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly Entities.DBContext _Db;
		private readonly IConfiguration _configuration;
		public UsersController(Entities.DBContext Db, IConfiguration configuration)	//Constructor used by the DI to inject DB context
		{
			_Db = Db;
			_configuration = configuration;
		}

		// POST: api/Users/Register
		[HttpPost("Register")]
		public ActionResult<RegisterUserResponseModel> Register([FromBody] RegisterUserRequestModel Data)
		{
			try
			{
				if (ModelState.IsValid)					//Validating the input
				{
					///Service to register a user
					int ResultCode = Services.UserServices.RegisterUser(Data, _Db);	
					if (ResultCode == 0)				//Registration successful
					{
						return Ok(new RegisterUserResponseModel()
						{
							Code = ResultCode,
							Message = "Ok"
						});
					}
					else if(ResultCode == 1)			//Username already present in the DB
					{
						return BadRequest(new RegisterUserResponseModel()
						{
							Code = ResultCode,
							Message = "Username already exists!"
						});
					}
					else								//Error connecting DB
					{
						return StatusCode(500, new { Message = "(Error: 101)There is a error in the Server. Please contact the Admin." });
					}
				}
				else
				{
					return BadRequest(ModelState);		//When the input is invalid
				}
			}
			catch (Exception e)		
			{
				///No error unhandled should be given to the outside world
				///This catch block receives all unhandled exceptions that might escape all the try catch blocks
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
					//Service that takes care for authenticating user and creating tokens
					LoginResponseModel loginResponseModel = Services.UserServices.Login(Data,_Db,_configuration);		
					switch (loginResponseModel.Code)
					{
						//Successful login
						case 0: return Ok(loginResponseModel);
						//Failed login
						case 1: return StatusCode(401, loginResponseModel); 
						//Error connecting DB
						case 2: return StatusCode(500, new { Message = "(Error: 102)There is a error in the Server. Please contact the Admin." });	
					}
					return Ok(loginResponseModel);
				}
				else
				{
					return BadRequest(ModelState);
				}
			}
			catch (Exception e)
			{
				///No error unhandled should be given to the outside world
				///This catch block receives all unhandled exceptions that might escape all the try catch blocks
				return StatusCode(500, Helpers.ExceptionHandler.Handle(e));
			}
		}
	}
}
