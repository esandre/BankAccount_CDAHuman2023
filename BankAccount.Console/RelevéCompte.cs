namespace BankAccount.Console;

internal class RelevéCompte
{
    private const string Header = "Date            |    Crédit |     Débit | Solde après opération |";

    private readonly Account _compteARelever;

    public RelevéCompte(Account compteARelever)
    {
        _compteARelever = compteARelever;
    }

    private static string PrintLine(
        Montant montantAprèsOpération, 
        Opération opération, 
        out Montant montantAvantOpération)
    {
        var balance = opération.Balance;
        var montantAvecPadding = balance.ToString().PadLeft(11);

        var celluleCrédit = opération.EstCrédit() ? montantAvecPadding : new string(' ', 11);
        var celluleDébit = opération.EstDébit() ? montantAvecPadding : new string(' ', 11);
        var soldeAprèsOpération = montantAprèsOpération.ToString().PadLeft(23);

        montantAvantOpération = opération.Annuler(montantAprèsOpération);

        return $"{opération.Date:g}|{celluleCrédit}|{celluleDébit}|{soldeAprèsOpération}|";
    }

    /// <inheritdoc />
    public override string ToString()
    {
        var balanceDeFin = _compteARelever.Balance;
        var ligneBalanceFinale = $"Balance : {balanceDeFin}";

        var opérationsEnOrdreAntéchronologique = _compteARelever
            .OpérationsEnOrdreAntéchronologique
            .ToArray();

        var lignesOpération =
            opérationsEnOrdreAntéchronologique
                .Select(opération => PrintLine(balanceDeFin, opération, out balanceDeFin))
                .Reverse();

        return string.Join(
            Environment.NewLine,
            lignesOpération
                .Prepend(Header)
                .Append(ligneBalanceFinale)
        );
    }
}