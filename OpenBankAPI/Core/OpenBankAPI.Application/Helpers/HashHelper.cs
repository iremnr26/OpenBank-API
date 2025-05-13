using System.Security.Cryptography;
using System.Text;

namespace OpenBankAPI.Application.Helpers
{
    public static class HashHelper
    {
        public static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2")); // SHA-256 hash'ini hex formatına çeviriyoruz
                }
                return builder.ToString();
            }
        }
    }
}
