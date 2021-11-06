namespace ExpenseTracker.Core.Crypter
{
    public interface ICrypter
    {
        public string Hash(string plaintext);
        public bool Verify(string plaintext, string hash);
    }
}