using static BCrypt.Net.BCrypt;
using ExpenseTracker.Infrastructure.Crypter.Interface;

namespace ExpenseTracker.Infrastructure.Crypter
{
    public class Crypter : ICrypter
    {
        public string Hash(string plaintext)
            => HashPassword(plaintext);

        public bool Verify(string plaintext, string hash)
            => BCrypt.Net.BCrypt.Verify(plaintext, hash);
    }
}