namespace BankAccount.Web.Presenters;

public record LigneComptePresenter(string Date, string Crédit, string Débit,string SoldeAprèsOpération)
{
    public static LigneComptePresenter FromOperationAndMontant(
        Montant montantAprèsOpération,
        Opération opération,
        out Montant montantAvantOpération)
    {
        var balance = opération.Balance.ToString();

        var date = opération.Date.ToString("f");
        var crédit = opération.EstCrédit() ? balance : string.Empty;
        var débit = opération.EstDébit() ? balance : string.Empty;
        var soldeAprèsOpération = montantAprèsOpération.ToString();

        montantAvantOpération = opération.Annuler(montantAprèsOpération);

        return new LigneComptePresenter(date, crédit, débit, soldeAprèsOpération);
    }
}