namespace BankAccount.Desktop.ViewModel;

public record LigneCompteViewModel(string Date, string Crédit, string Débit, string SoldeAprèsOpération)
{
    public static LigneCompteViewModel FromOperationAndMontant(
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

        return new LigneCompteViewModel(date, crédit, débit, soldeAprèsOpération);
    }
}