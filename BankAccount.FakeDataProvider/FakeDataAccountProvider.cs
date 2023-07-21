namespace BankAccount.FakeDataProvider;

public class FakeDataAccountProvider : IAccountProvider
{
    /// <inheritdoc />
    public Account Provide()
    {
        var account = Account.ApprovisionnéAuDépartAvec(60);

        account.Déposer(40);
        account.Retirer(10);
        account.Déposer(30);
        account.Retirer(150);

        return account;
    }
}