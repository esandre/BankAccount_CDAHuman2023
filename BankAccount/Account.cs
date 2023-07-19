namespace BankAccount;

internal class Account
{
    private const string Header = "Date            |    Crédit |     Débit |";

    private readonly ICollection<Opération> _opérations 
        = new List<Opération>();

    private Montant Balance => _opérations
        .Aggregate(Montant.Zéro, (balance, kv) => balance + kv.Balance);

    private static string PrintLine(Opération opération)
    {
        var balance = opération.Balance;
        var montantAvecPadding = balance.ToString().PadLeft(11);

        var celluleCrédit = opération.EstCrédit() ? montantAvecPadding : new string(' ', 11);
        var celluleDébit = opération.EstDébit() ? montantAvecPadding : new string(' ', 11);

        return $"{opération.Date:g}|{celluleCrédit}|{celluleDébit}|";
    }

    public string Relevé => Header +
                            Environment.NewLine +
                            string.Join(Environment.NewLine, _opérations
                                .OrderBy(opération => opération.Date)
                                .Select(PrintLine)) +
                            Environment.NewLine +
                            $"Balance : {Balance}";

    public void Déposer(ushort montant)
    {
        _opérations.Add(new Opération(DateTime.Now, new Montant(montant)));
    }

    public void Retirer(ushort montant)
    {
        _opérations.Add(new Opération(DateTime.Now, new Montant(-montant)));
    }
}