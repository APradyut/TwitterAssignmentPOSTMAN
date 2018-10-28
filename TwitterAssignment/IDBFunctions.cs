using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterAssignment.Entities;

namespace TwitterAssignment
{
	public interface IUserFunctions
	{
		Exception LastException { get; set; }
		int isVerifiedUser(string Username, string Password);
		int AddUser(Users user);
	}
}
