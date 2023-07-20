namespace BankAccount;

internal class Account
{
    private readonly ICollection<Opération> _opérations 
        = new List<Opération>();

    public Montant Balance => _opérations
        .Aggregate(Montant.Zéro, (balance, kv) => balance + kv.Balance);

    public IOrderedEnumerable<Opération> OpérationsEnOrdreChronologique
        => _opérations
            .OrderBy(opération => opération.Date);

    public string Relevé => new RelevéCompte(this).ToString();

    public void Déposer(ushort montant)
    {
        _opérations.Add(new Opération(DateTime.Now, new Montant(montant)));
    }

    public void Retirer(ushort montant)
    {
        _opérations.Add(new Opération(DateTime.Now, new Montant(-montant)));
    }
}