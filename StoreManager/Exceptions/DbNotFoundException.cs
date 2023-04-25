namespace StoreManager.Exceptions;

public class DbNotFoundException : Exception
{
    public DbNotFoundException(string message) : base(message) { }
}
