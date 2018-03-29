using System;
using System.Security.Cryptography;
using System.Text;

namespace hwapp {

	//compute the hash of the data in C# .NET Core running in Ubuntu 17.10
	//same as echo -n foobar | sha256sum
	static class Program {
		static void Main (string[] args) {

			Console.WriteLine ("GenerateSHA256String from abc");
			Console.WriteLine (GenerateSHA256String ("abc", false));
			Console.WriteLine ("");

			Console.WriteLine ("GenerateSHA256String from abc with base64encoding");
			Console.WriteLine (GenerateSHA256String ("abc", true));
			Console.WriteLine ("");

			Console.WriteLine ("GenerateHMACString from abc w/ key 'thekey'");
			Console.WriteLine (GenerateHMACString ("abc", "thekey", false));
			Console.WriteLine ("");

			Console.WriteLine ("GenerateHMACString from abc  w/ key 'thekey' with base64encoding");
			Console.WriteLine (GenerateHMACString ("abc", "thekey", true));
			Console.WriteLine ("");

		}

		public static string GenerateSHA256String (string inputString, bool convertToBase64) {
			SHA256 sha256 = SHA256Managed.Create ();
			byte[] bytes = Encoding.UTF8.GetBytes (inputString);

			if (!convertToBase64) {
				byte[] hash = sha256.ComputeHash (bytes);
				return GetStringFromHash (hash);
			} else {
				return Convert.ToBase64String(sha256.ComputeHash (bytes));
			}

		}

		public static string GenerateHMACString (string stringToBeHashed, string key, bool convertToBase64) {
			byte[] dataToBeHashed = Encoding.UTF8.GetBytes (stringToBeHashed);
			byte[] keybytes = Encoding.UTF8.GetBytes (key);

			var hmac = new HMACSHA256 (keybytes);
			if (!convertToBase64) {
				byte[] hash = hmac.ComputeHash (dataToBeHashed);

				return GetStringFromHash (hash);
			} else {
				return Convert.ToBase64String (hmac.ComputeHash (dataToBeHashed));
			}
		}

		private static string GetStringFromHash (byte[] hash) {
			StringBuilder result = new StringBuilder ();
			for (int i = 0; i < hash.Length; i++) {
				result.Append (hash[i].ToString ("X2"));
			}
			return result.ToString ();
		}

	}

}