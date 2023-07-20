namespace BankAccount.Console;

internal class LigneCompte
{
    public static string Print(Montant montantAprèsOpération,
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
}