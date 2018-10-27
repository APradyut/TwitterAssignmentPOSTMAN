using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterAssignment.Models.RequestModels
{
	public class RegisterUserRequestModel
	{
		[Required]
		public string Username { get; set; }
		[Required]
		[MinLength(6, ErrorMessage = "Password should be 6 characters long")]
		public string Password { get; set; }
		[Required]
		[EmailAddress(ErrorMessage = "Email Id is not valid")]
		public string Email { get; set; }
		[Required]
		[RegularExpression("^[6-9][0-9]{9}$", ErrorMessage = "Phone number is not valid")]
		public string Phone { get; set; }
		[Required]
		[RegularExpression("^[a-zA-Z ]+$", ErrorMessage ="Name should not contain any numbers or any other special characters")]
		public string Name { get; set; }
	}
}
