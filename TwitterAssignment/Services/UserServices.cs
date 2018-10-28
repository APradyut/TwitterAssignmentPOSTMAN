using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TwitterAssignment.DBFunctions;
using TwitterAssignment.Entities;
using TwitterAssignment.Helpers;
using TwitterAssignment.Models.RequestModels;
using TwitterAssignment.Models.ResponseModels;

namespace TwitterAssignment.Services
{
	public class UserServices
	{
		/// <summary>
		/// This is a helper method to the User controllers Register Endpoint
		/// </summary>
		/// <param name="data">The Request Model coming in</param>
		/// <param name="Db">The Database context from the Dependency Injector</param>
		/// <returns>
		/// 0. Ok
		/// 1. Error
		/// </returns>
		internal static int RegisterUser(RegisterUserRequestModel data, DBContext Db)
		{
			try
			{
				IUserFunctions userFunctions = new UserFunctions(Db);

				//Hashing password before storing
				string PasswordHashed = Helpers.Crypto.GetHashStringsFromStrings(data.Password);

				///Creating user entity from input data
				Users NewUser = new Users()
				{
					Id = new Random().Next(0, 9999999),
					CreatedOn = DateTime.Now,
					Email = data.Email,
					Name = data.Name,
					Password = PasswordHashed,
					Phone = data.Phone,
					Username = data.Username,
					LastModifiedOn = DateTime.Now
				};

				//Adding user to database
				int Result = userFunctions.AddUser(NewUser);

				if (Result == 2)
				{
					//Handling the Exception
					ExceptionHandler.Handle(userFunctions.LastException);
				}
				return Result;
			}
			catch (Exception e)
			{
				ExceptionHandler.Handle(e);
				throw;
			}
		}

		/// <summary>
		/// This method is a helper to the Login endpoint.
		/// </summary>
		/// <param name="data">The input in the request</param>
		/// <param name="Db">DBcontext passed from the DI</param>
		/// <returns>
		/// Returns the login response model which needs to be sent as the response
		/// </returns>
		internal static LoginResponseModel Login(LoginRequestModel data, DBContext Db, IConfiguration Configuration)
		{
			IUserFunctions userFunctions = new UserFunctions(Db);
			//Getting security key
			//Please keep the security key somewhere safe like a registry
			
			string SecurityKey = Configuration["JWT:SecurityKey"];

			//Create the Symmetric key from the security key
			var SymmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey));

			//Creating signing credentials
			var SigningCredentials = new SigningCredentials(SymmetricKey, SecurityAlgorithms.HmacSha256Signature);

			//Claims added according to the user
			int ReturnResult = userFunctions.isVerifiedUser(data.Username, Crypto.GetHashStringsFromStrings(data.Password));
			if (ReturnResult == 0)
			{
				var Claims = new List<Claim>();
				Claims.Add(new Claim(ClaimTypes.NameIdentifier, data.Username));

				int ExpiryDays = Convert.ToInt32(Configuration["JWT:Validity"]);
				//Create token
				var Token = new JwtSecurityToken(
					issuer: Configuration["JWT:ValidIssuer"],
					audience: Configuration["JWT:ValidAudience"],
					expires: DateTime.Now.AddDays(ExpiryDays),
					signingCredentials: SigningCredentials,
					claims : Claims
					);
				string token = new JwtSecurityTokenHandler().WriteToken(Token);
				return new LoginResponseModel(token, DateTime.Now.AddDays(ExpiryDays), "Ok", 0);
			}
			else if(ReturnResult == 1)
			{
				return new LoginResponseModel(null, default(DateTime), "Username or Password is invalid", 1);
			}
			else
			{
				return new LoginResponseModel(null, default(DateTime), "Error connecting to DB. Contact administrator", 2);
			}
		}
	}
}
