namespace BankAccount;

public interface IAccountProvider
{
    Task<Account> ProvideAsync(CancellationToken token);
}