namespace BankAccount;

public record Opération(DateTime Date, Montant Balance)
{
    public bool EstCrédit() => Balance.EstPositif();

    public bool EstDébit() => !EstCrédit();

    public Montant Annuler(Montant montantAvantOpération) 
        => montantAvantOpération - Balance;

    public int BalanceEnCrédit => EstCrédit() ? Balance.ToSignedInteger() : 0;
    public int BalanceEnDébit => EstDébit() ? Balance.ToSignedInteger() : 0;
}