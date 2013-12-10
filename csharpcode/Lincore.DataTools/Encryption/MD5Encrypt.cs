using System;
using System.Security.Cryptography; 
using System.Text; 

namespace Lincore.DataTools.EncryptionLib
{
	public class MD5Encrypt
	{
		private Byte[] EncStringBytes; 
		private Byte[] StringBytes; 
		private UTF8Encoding myEncoder; 
		private MD5CryptoServiceProvider MD5HashProvider; 
		private string _Salt; 
 
		public MD5Encrypt(string Salt)
		{
			_Salt = Salt; 
			myEncoder = new UTF8Encoding(); 
			MD5HashProvider =  new MD5CryptoServiceProvider(); 
		}

		public string Encrypt(string EncString)
		{

			// convert string to Bytes 
			StringBytes = myEncoder.GetBytes(EncString + _Salt); 
			// create the Hash 
			MD5HashProvider.Clear(); 
			MD5HashProvider = new MD5CryptoServiceProvider(); 
			EncStringBytes = MD5HashProvider.ComputeHash(StringBytes); 
			// return the Hash as a string 
			return BitConverter.ToString(EncStringBytes).Replace("-",""); 
		}

		public bool VerifyMD5Hash(string EncString, string MD5Hash)
		{
			return Encrypt(EncString)==MD5Hash; 
		}

	}
}
