using System;
using System.Security.Cryptography;
using System.Text;

namespace hwapp
{

    //compute the hash of the data in C# .NET Core running in Ubuntu 17.10
    //same as echo -n foobar | sha256sum
    static class Program
    {
        static void Main(string[] args)
        {

            //GenerateSHA256String("abc")
            Console.WriteLine( GenerateSHA256String("abc"));

            //GenerateHMACString("abc", "thekey")
            Console.WriteLine( GenerateHMACString("abc", "thekey"));


        }


        public static string GenerateHMACString(string stringToBeHashed, string key)
        {
            byte[] dataToBeHashed = Encoding.UTF8.GetBytes(stringToBeHashed);
			byte[] keybytes = Encoding.UTF8.GetBytes(key);

            var hmac = new HMACSHA256(keybytes);

            byte[] hash = hmac.ComputeHash(dataToBeHashed);

            return GetStringFromHash(hash);


		}
		


        public static string GenerateSHA256String(string inputString)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha256.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }

        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }


    }


}
