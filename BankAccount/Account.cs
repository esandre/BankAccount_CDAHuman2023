namespace BankAccount;

public partial class Account
{
    private readonly ICollection<Opération> _opérations 
        = new List<Opération>();

    public Montant Balance => _opérations
        .Aggregate(Montant.Zéro, (balance, kv) => balance + kv.Balance);

    public IOrderedEnumerable<Opération> OpérationsEnOrdreAntéchronologique
        => _opérations
            .OrderByDescending(opération => opération.Date);

    public void Déposer(ushort montant)
    {
        _opérations.Add(new Opération(DateTime.Now, new Montant(montant)));
    }

    public void Retirer(ushort montant)
    {
        _opérations.Add(new Opération(DateTime.Now, new Montant(-montant)));
    }

    public static Account ADécouvertDe(ushort découvert)
    {
        var account = new Account();
        account.Retirer(découvert);
        return account;
    }

    public static Account ApprovisionnéAuDépartAvec(ushort provision)
    {
        var account = new Account();
        account.Déposer(provision);
        return account;
    }

    private Account(){}
}