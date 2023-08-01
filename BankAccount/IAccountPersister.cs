namespace BankAccount;

public interface IAccountPersister
{
    Task PersistAsync(Account account, CancellationToken token);
}