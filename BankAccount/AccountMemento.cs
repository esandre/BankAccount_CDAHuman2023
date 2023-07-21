namespace BankAccount;

public partial class Account
{
    public class AccountMemento : IMemento<Account>
    {
        public readonly Opération[] Opérations;

        public AccountMemento(IEnumerable<Opération> opérations)
        {
            Opérations = opérations.ToArray();
        }

        public Account Restore() => new (this);
    }

    public IMemento<Account> Save() => new AccountMemento(_opérations);

    private Account(AccountMemento memento)
    {
        _opérations = memento.Opérations.ToList();
    }
}