using System;

namespace TwitterAssignment.Models.ResponseModels
{
	public class LoginResponseModel
	{
		public LoginResponseModel(string token, DateTime expiresOn,string message)
		{
			YourToken = token;
			ExpiresOn = expiresOn;
			Message = message;
		}
		public string YourToken { get; protected set; }
		public DateTime ExpiresOn { get; protected set; }
		public string Message { get; protected set; }
	}
}
