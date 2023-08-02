namespace BankAccount.FakeDataProvider;

public class FakeDataAccountProvider : IAccountProvider
{
    private readonly IHorloge _horloge;

    public FakeDataAccountProvider(IHorloge horloge)
    {
        _horloge = horloge;
    }

    /// <inheritdoc />
    public Task<Account> ProvideAsync(CancellationToken token)
    {
        var account = Account.ApprovisionnéAuDépartAvec(60, _horloge);

        account.Déposer(40);
        account.Retirer(10);
        account.Déposer(30);
        account.Retirer(150);

        return Task.FromResult(account);
    }
}