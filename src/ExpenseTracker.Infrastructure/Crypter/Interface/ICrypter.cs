namespace ExpenseTracker.Infrastructure.Crypter.Interface
{
    public interface ICrypter
    {
        public string Hash(string plaintext);
        public bool Verify(string plaintext, string hash);
    }
}