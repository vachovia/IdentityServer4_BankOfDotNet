using System.Security.Cryptography;
using System.Text;

namespace BankOfDotNet.API.Static
{
    public class Utility
    {
        public static string Sha256_Hash(string value = "secret")
        {
            StringBuilder Sb = new StringBuilder();

            using (var hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }
    }
}
