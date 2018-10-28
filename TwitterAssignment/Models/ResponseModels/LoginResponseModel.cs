using System;

namespace TwitterAssignment.Models.ResponseModels
{
	public class LoginResponseModel
	{
		public LoginResponseModel(string token, DateTime expiresOn,string message, int Code)
		{
			YourToken = token;
			ExpiresOn = expiresOn;
			Message = message;
			this.Code = Code;
		}
		internal int Code { get; set; }
		public string YourToken { get; protected set; }
		public DateTime ExpiresOn { get; protected set; }
		public string Message { get; protected set; }
	}
}
