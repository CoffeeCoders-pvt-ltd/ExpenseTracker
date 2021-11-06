using static BCrypt.Net.BCrypt;

namespace ExpenseTracker.Core.Crypter
{
    public class Crypter : ICrypter
    {
        public string Hash(string plaintext)
            => HashPassword(plaintext);

        public bool Verify(string plaintext, string hash)
            => BCrypt.Net.BCrypt.Verify(plaintext, hash);
    }
}