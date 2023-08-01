using JetBrains.Annotations;

namespace BankAccount.Api.Presenters;

[PublicAPI]
public record LigneComptePresenter(DateTime Date, int Crédit, int Débit, int SoldeAprèsOpération)
{
    public string NomClient => "Mme Michu";

    public static LigneComptePresenter FromOperationAndMontant(
        Montant montantAprèsOpération,
        Opération opération,
        out Montant montantAvantOpération)
    {
        var date = opération.Date;
        var crédit = opération.BalanceEnCrédit;
        var débit = opération.BalanceEnDébit;
        var soldeAprèsOpération = montantAprèsOpération.ToSignedInteger();

        montantAvantOpération = opération.Annuler(montantAprèsOpération);

        return new LigneComptePresenter(date, crédit, débit, soldeAprèsOpération);
    }
}