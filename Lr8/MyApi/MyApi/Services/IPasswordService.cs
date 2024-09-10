using System;
using System.Security.Cryptography;
using System.Text;

namespace MyApi.Services
{
    public interface IPasswordService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hash);
    }

    public class PasswordService : IPasswordService
    {
        public string HashPassword(string password)
        {
            using (var hasher = new Rfc2898DeriveBytes(password, 32, 10000, HashAlgorithmName.SHA256))
            {
                byte[] hash = hasher.GetBytes(32);
                byte[] salt = hasher.Salt;
                return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
            }
        }

        public bool VerifyPassword(string password, string hash)
        {
            var parts = hash.Split(':');
            if (parts.Length != 2) return false;

            var salt = Convert.FromBase64String(parts[0]);
            var hashBytes = Convert.FromBase64String(parts[1]);

            using (var hasher = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256))
            {
                var computedHash = hasher.GetBytes(32);
                return computedHash.SequenceEqual(hashBytes);
            }
        }
    }
}
