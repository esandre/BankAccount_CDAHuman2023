namespace BankAccount;

public interface IAccountPersister
{
    void Persist(Account account);
}