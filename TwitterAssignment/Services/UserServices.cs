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

				if (Result == 0)
					return 0;
				else if (Result == 1)
				{
					//Handling the Exception
					ExceptionHandler.Handle(userFunctions.LastException);
					return 1;
				}
				else return Result;
			}
			catch (Exception e)
			{
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
		internal static LoginResponseModel Login(LoginRequestModel data, DBContext Db)
		{
			IUserFunctions userFunctions = new UserFunctions(Db);
			//Get the security key from somewhere safe
			string SecurityKey = "This is a very long security key, which should not be stored or initialized here";

			//Create the Symmetric key from the security key
			var SymmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey));

			//Creating signing credentials
			var SigningCredentials = new SigningCredentials(SymmetricKey, SecurityAlgorithms.HmacSha256Signature);

			//Claims added according to the user
			bool isValid = userFunctions.isVerifiedUser(data.Username, Crypto.GetHashStringsFromStrings(data.Password));
			if (isValid)
			{
				var Claims = new List<Claim>();
				Claims.Add(new Claim(ClaimTypes.NameIdentifier, data.Username));

				int ExpiryDays = 1;
				//Create token
				var Token = new JwtSecurityToken(
					issuer: "CentralController",
					audience: "APP",
					expires: DateTime.Now.AddDays(1),
					signingCredentials: SigningCredentials,
					claims : Claims
					);
				string token = new JwtSecurityTokenHandler().WriteToken(Token);
				token = Crypto.EncodeTo64(token);
				return new LoginResponseModel(token, DateTime.Now.AddDays(ExpiryDays), "Ok");
			}
			else
			{
				return new LoginResponseModel(null, default(DateTime), "Username or Password is invalid");
			}
		}
	}
}
