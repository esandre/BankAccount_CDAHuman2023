namespace BankAccount;

public class HorlogeSystème : IHorloge
{
    /// <inheritdoc />
    public DateTime Now => DateTime.Now;
}