namespace BankAccount;

internal class Montant
{
    private readonly int _valeur;

    public static readonly Montant Zéro = new (0);

    public Montant(int valeur)
    {
        _valeur = valeur;
    }

    public bool EstPositif() => _valeur >= 0;

    public static Montant operator +(Montant a, Montant b)
        => new (a._valeur + b._valeur);

    /// <inheritdoc />
    public override string ToString() => _valeur.ToString("C0");
}