namespace BankAccount;

internal record Opération(DateTime Date, Montant Balance)
{
    public bool EstCrédit() => Balance.EstPositif();

    public bool EstDébit() => !EstCrédit();

    public Montant Annuler(Montant montantAvantOpération) 
        => montantAvantOpération - Balance;
}