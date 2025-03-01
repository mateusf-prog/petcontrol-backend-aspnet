using System.Security.Cryptography;
using System.Text;

namespace PetControlSystem.Domain.Utils
{
    public class PasswordUtils
    {
        public static string EncryptPassword(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = SHA256.HashData(bytes);
            var builder = new StringBuilder();

            foreach (var item in hash)
                builder.Append(item.ToString("x2"));

            return builder.ToString();
        }

        private static string DecryptPassword(string password)
        {
            return Encoding.UTF8.GetString(SHA256.HashData(Encoding.UTF8.GetBytes(password)));
        }

        public static bool ValidatePassword(string passwordSavedOnDatabase, string passwordDto)
        {
            return DecryptPassword(passwordSavedOnDatabase) == passwordDto;
        }
    }
}
