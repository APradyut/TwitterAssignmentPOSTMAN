using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterAssignment.Helpers
{
	public class Crypto
	{
		public static string GetHashStringsFromStrings(string Data)
		{
			using (System.Security.Cryptography.SHA256 sha = System.Security.Cryptography.SHA256.Create())
			{
				byte[] raw = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Data));

				System.Text.StringBuilder builder = new System.Text.StringBuilder();
				for (int i = 0; i < raw.Length; i++)
				{
					builder.Append(raw[i].ToString("x2"));
				}
				return builder.ToString();
			}
		}
		public static string EncodeTo64(string toEncode)
		{
			byte[] toEncodeAsBytes
				  = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
			string returnValue
				  = System.Convert.ToBase64String(toEncodeAsBytes);
			return returnValue;
		}
	}
}
