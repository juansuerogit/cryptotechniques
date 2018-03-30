using System;
using System.Security.Cryptography;
using System.Text;
namespace crypto
{
    //compute the hash of the data in C# .NET Core running in Ubuntu 17.10
    //same as echo -n foobar | sha256sum
    static class Program
    {

        private static RSAParameters _publicKey;
        private static RSAParameters _privateKey;

        static void Main(string[] args)
        {
            Console.WriteLine("GenerateSHA256String from abc");
            Console.WriteLine(GenerateSHA256String("abc", false));
            Console.WriteLine("");
            Console.WriteLine("GenerateSHA256String from abc with base64encoding");
            Console.WriteLine(GenerateSHA256String("abc", true));
            Console.WriteLine("");
            Console.WriteLine("GenerateHMACString from abc w/ key 'thekey'");
            Console.WriteLine(GenerateHMACString("abc", "thekey", false));
            Console.WriteLine("");
            Console.WriteLine("GenerateHMACString from abc  w/ key 'thekey' with base64encoding");
            Console.WriteLine(GenerateHMACString("abc", "thekey", true));
            Console.WriteLine("");

            Console.WriteLine("New Private Key then hash the sign hash, output as string");
            AssignNewKey();

            //this byte[] of the hash will be used to sign and verify
            byte[] hashOfDataToSign = GenerateSHA256ByteArray("abc");
            
            //to be veryfied on the other side
            byte[] signedHashBytes = SignData(hashOfDataToSign);


            string signedHashString = GetStringFromHash(signedHashBytes);
            Console.WriteLine(signedHashString);


            var valid = VerifySignature(hashOfDataToSign, signedHashBytes);

            Console.WriteLine("valid: {0}", valid);

            Console.ReadLine();
        }

        public static void AssignNewKey()
        {
            using(var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                _publicKey = rsa.ExportParameters(false);
                _privateKey = rsa.ExportParameters(true);
                 
            }
        }

        public static byte[] SignData(byte[] hashOfDataToSign)
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(_privateKey);

                var rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);
                rsaFormatter.SetHashAlgorithm("SHA256");
                return rsaFormatter.CreateSignature(hashOfDataToSign);
            }
        }

        public static bool VerifySignature(byte[] hashOfDataToSign, byte[] signature)
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.ImportParameters(_publicKey);

                var rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
                rsaDeformatter.SetHashAlgorithm("SHA256");

                return rsaDeformatter.VerifySignature(hashOfDataToSign, signature);

            }
       
        }
        public static byte[] GenerateSHA256ByteArray(string inputString)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);

            byte[] hash = sha256.ComputeHash(bytes);
            return hash;

        }


        public static string GenerateSHA256String(string inputString, bool convertToBase64)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            if (!convertToBase64)
            {
                byte[] hash = sha256.ComputeHash(bytes);
                return GetStringFromHash(hash);
            }
            else
            {
                return Convert.ToBase64String(sha256.ComputeHash(bytes));
            }
        }
        public static string GenerateHMACString(string stringToBeHashed, string key, bool convertToBase64)
        {
            byte[] dataToBeHashed = Encoding.UTF8.GetBytes(stringToBeHashed);
            byte[] keybytes = Encoding.UTF8.GetBytes(key);
            var hmac = new HMACSHA256(keybytes);
            if (!convertToBase64)
            {
                byte[] hash = hmac.ComputeHash(dataToBeHashed);
                return GetStringFromHash(hash);
            }
            else
            {
                return Convert.ToBase64String(hmac.ComputeHash(dataToBeHashed));
            }
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
