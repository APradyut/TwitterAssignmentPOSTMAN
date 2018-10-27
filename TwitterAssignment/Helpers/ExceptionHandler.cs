using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterAssignment.Helpers
{
	public class ExceptionHandler
	{
		public static object Handle(Exception e)
		{
			//Handle log files here
			string Id = Guid.NewGuid().ToString();
			System.Diagnostics.Debug.WriteLine("Error "+Id+": "+e);		//Printing to debugger console
			return new
			{
				Status = "Failed",
				Message = "There is an Internal Server error. Contact Admin with error Id: " + Id
			};
		}
	}
}
