namespace BankAccount;

internal class RelevéCompte
{
    private const string Header = "Date            |    Crédit |     Débit | Solde après opération |";

    private readonly Account _compteARelever;

    public RelevéCompte(Account compteARelever)
    {
        _compteARelever = compteARelever;
    }

    private static string PrintLine(Opération opération)
    {
        var balance = opération.Balance;
        var montantAvecPadding = balance.ToString().PadLeft(11);

        var celluleCrédit = opération.EstCrédit() ? montantAvecPadding : new string(' ', 11);
        var celluleDébit = opération.EstDébit() ? montantAvecPadding : new string(' ', 11);

        return $"{opération.Date:g}|{celluleCrédit}|{celluleDébit}|";
    }

    /// <inheritdoc />
    public override string ToString()
    {
        var opérationsEnOrdreChronologique = _compteARelever
            .OpérationsEnOrdreChronologique
            .ToArray();

        var lignesOpération = string.Join(
            Environment.NewLine,
            opérationsEnOrdreChronologique.Select(PrintLine)
        );

        return Header +
               Environment.NewLine +
               lignesOpération +
               Environment.NewLine +
               $"Balance : {_compteARelever.Balance}";
    }
}