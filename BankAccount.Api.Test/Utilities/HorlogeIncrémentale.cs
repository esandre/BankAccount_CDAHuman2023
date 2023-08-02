namespace BankAccount.Api.Test.Utilities;

internal class HorlogeIncrémentale : IHorloge
{
    private readonly object _lock = new ();
    private DateTime _previous = DateTime.Now;

    /// <inheritdoc />
    public DateTime Now { 
        get
        {
            lock (_lock)
            {
                _previous = _previous.AddSeconds(Random.Shared.Next(1600));
                return _previous;
            }
        }
    }
}