using System.Security.Cryptography;
using System.Text;

namespace BusinessSystem.CRM.Logics.Contexts.Authorization
{
    public class HashContext
    {
        public string MD5(string input)
        {
            StringBuilder outputHash = new StringBuilder();
            MD5CryptoServiceProvider md5Crypto = new MD5CryptoServiceProvider();
            byte[] bytes = md5Crypto.ComputeHash(new UTF8Encoding().GetBytes(input));
            for (int i = 0; i < bytes.Length; i++)
            {
                outputHash.Append(bytes[i].ToString("x2"));
            }
            return outputHash.ToString();
        }
    }
}
