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

        public static bool ValidatePassword(string passwordSavedOnDatabase, string passwordDto)
        {
            var enteredHash = EncryptPassword(passwordDto);
            return enteredHash == passwordSavedOnDatabase;
        }
    }
}
