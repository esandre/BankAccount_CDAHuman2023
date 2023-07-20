namespace BankAccount.Console;

internal class RelevéCompte
{
    private const string Header = "Date            |    Crédit |     Débit | Solde après opération |";

    private readonly Account _compteARelever;

    public RelevéCompte(Account compteARelever)
    {
        _compteARelever = compteARelever;
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
                .Select(opération => LigneCompte.Print(balanceDeFin, opération, out balanceDeFin))
                .Reverse();

        return string.Join(
            Environment.NewLine,
            lignesOpération
                .Prepend(Header)
                .Append(ligneBalanceFinale)
        );
    }
}