namespace BankAccount;

public partial class Account
{
    private readonly IHorloge _horloge;

    private Account(IHorloge horloge)
    {
        _horloge = horloge;
    }

    private readonly ICollection<Opération> _opérations 
        = new List<Opération>();

    public Montant Balance => _opérations
        .Aggregate(Montant.Zéro, (balance, kv) => balance + kv.Balance);

    public IOrderedEnumerable<Opération> OpérationsEnOrdreAntéchronologique
        => _opérations
            .OrderByDescending(opération => opération.Date);

    public void Déposer(ushort montant)
    {
        _opérations.Add(new Opération(_horloge.Now, new Montant(montant)));
    }

    public void Retirer(ushort montant)
    {
        _opérations.Add(new Opération(_horloge.Now, new Montant(-montant)));
    }

    public static Account ADécouvertDe(ushort découvert, IHorloge horloge)
    {
        var account = new Account(horloge);
        account.Retirer(découvert);
        return account;
    }

    public static Account ApprovisionnéAuDépartAvec(ushort provision, IHorloge horloge)
    {
        var account = new Account(horloge);
        account.Déposer(provision);
        return account;
    }
}