namespace BankAccount;

public partial class Account
{
    public class AccountMemento : IMemento<Account>
    {
        public readonly IHorloge Horloge;
        public readonly Opération[] Opérations;

        public AccountMemento(IEnumerable<Opération> opérations, IHorloge horloge)
        {
            Horloge = horloge;
            Opérations = opérations.ToArray();
        }

        public Account Restore() => new (this);
    }

    public IMemento<Account> Save() => new AccountMemento(_opérations, _horloge);

    private Account(AccountMemento memento)
    {
        _opérations = memento.Opérations.ToList();
        _horloge = memento.Horloge;
    }
}