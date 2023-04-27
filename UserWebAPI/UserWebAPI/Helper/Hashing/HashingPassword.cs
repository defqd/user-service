using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

namespace UserWebAPI.Helper.Hashing
{
    /// <summary>
    /// Класс для хэширования пароля
    /// </summary>
    public class HashingPassword
    {
        /// <summary>
        /// Метод для хэширования пароля пользователя
        /// </summary>
        /// <param name="password">Пароль пользователя</param>
        /// <returns></returns>
        public static string Hashing(string password)
        {
            byte[] salt = Encoding.Unicode.GetBytes("bybyby");

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password!,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return hashed;
        }
    }
}