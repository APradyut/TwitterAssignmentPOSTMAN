using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using TwitterAssignment.Entities;

namespace TwitterAssignment.DBFunctions
{
	public class UserFunctions : IUserFunctions
	{
		private DBContext _Db;
		public UserFunctions(DBContext Db)
		{
			_Db = Db;
		}
		public Exception LastException;
		Exception IUserFunctions.LastException { get; set; }

		/// <summary>
		/// Adds the user to the Database
		/// </summary>
		/// <param name="user">The new user to be added</param>
		/// <returns>
		/// 0. Ok
		/// 1. Username Exists
		/// 2. Error connecting DB
		/// </returns>
		public int AddUser(Users user)
		{
			IQueryable<Users> Exists;
			try
			{
				try		//Trying to establish connection with the DB
				{
					_Db.Database.Migrate();
					Exists = _Db.Users.Where(a => a.Username == user.Username);     //Creating a query on users table for finding the username
				}
				catch (Exception e)		//returns 2 if the DB connection has issues
				{
					return 2;
				}
				Users UserExisting = Exists.First();	//Finding a user with the same username
				return 1;		//If found user with the same username, send 1
			}
			catch (Exception)	//If the username is unique
			{
				_Db.Users.Add(user);		//Adding the username to the table
				_Db.SaveChanges();			//Saving changes
				return 0;
			}
		}

		/// <summary>
		/// Finds if the user is correct
		/// </summary>
		/// <param name="Username"></param>
		/// <param name="Password"></param>
		/// <returns>
		/// 0. True
		/// 1. False
		/// 2. Error connecting DB
		/// </returns>
		public int isVerifiedUser(string Username, string Password)
		{
			IQueryable<Users> UserQuery;
			try			//Trying connection to the DB
			{
				_Db.Database.Migrate();
				UserQuery = _Db.Users.Where(a => a.Username == Username && a.Password == Password);     //Creating the Query for the verification
			}
			catch(Exception e) //Error connecting to the DB
			{
				return 2;	
			}
			try			//Finding the user
			{
				Users User = UserQuery.First();
				return 0;	//User found and thus returning zero indicating Verified User
			}
			catch (Exception e)
			{
				return 1;	//User not found and thus return 1 indicating inValid user
			}
		}
	}
}
