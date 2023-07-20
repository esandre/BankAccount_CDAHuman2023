namespace BankAccount.Web.Presenters;

public record LigneComptePresenter(string Date, string Crédit, string Débit,string SoldeAprèsOpération)
{
    public static LigneComptePresenter FromOperation(Opération operation)
    {
        var date = operation.Date.ToString("f");
        var crédit = operation.EstCrédit() ? operation.Balance.ToString() : string.Empty;
        var débit = operation.EstDébit() ? operation.Balance.ToString() : string.Empty;
        var soldeAprèsOpération = string.Empty;

        return new LigneComptePresenter(date, crédit, débit, soldeAprèsOpération);
    }
}