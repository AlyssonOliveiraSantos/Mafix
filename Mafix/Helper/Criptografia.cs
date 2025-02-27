

using System.Security.Cryptography;

namespace Mafix.Helper
{
    public static class Criptografia
    {
        public static string GerarHash(this string valor)
        {
            var hash = SHA256.Create();
            var bytes = System.Text.Encoding.UTF8.GetBytes(valor);
            var hashBytes = hash.ComputeHash(bytes);

            return System.Convert.ToBase64String(hashBytes);
        }
    }
}
