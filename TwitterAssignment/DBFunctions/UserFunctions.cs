using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterAssignment.Entities;

namespace TwitterAssignment.DBFunctions
{
	public class UserFunctions:IUserFunctions
	{
		private DBContext _Db;
		public UserFunctions(DBContext Db)
		{
			_Db = Db;
		}
		public Exception LastException;
		Exception IUserFunctions.LastException { get; set;}

		/// <summary>
		/// Adds the user to the Database
		/// </summary>
		/// <param name="user">The new user to be added</param>
		/// <returns>
		/// 0. Ok
		/// 1. Error occured
		/// </returns>
		public int AddUser(Users user)
		{
			try
			{
				_Db.Users.Add(user);
				_Db.SaveChanges();
				return 0;
			}
			catch (Exception e)
			{
				LastException = e;
				return 1;
			}
		}

		public bool isVerifiedUser(string Username, string Password)
		{
			try
			{
				try
				{
					var User = _Db.Users.Where(a => a.Username == Username && a.Password == Password).First();
					return true;
				}
				catch (Exception)
				{
					return false;
				}

			}
			catch (Exception e)
			{
				return false;
			}
		}
	}
}
