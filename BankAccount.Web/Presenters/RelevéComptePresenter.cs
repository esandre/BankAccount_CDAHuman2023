using System.Collections;

namespace BankAccount.Web.Presenters;

public class RelevéComptePresenter : IEnumerable<LigneComptePresenter>
{
    private readonly Account _compteARelever;

    public RelevéComptePresenter(Account compteARelever)
    {
        _compteARelever = compteARelever;
    }

    /// <inheritdoc />
    public IEnumerator<LigneComptePresenter> GetEnumerator()
    {
        var balanceDeFin = _compteARelever.Balance;

        var opérationsEnOrdreAntéchronologique = _compteARelever
            .OpérationsEnOrdreAntéchronologique
            .ToArray();

        var lignesOpération =
            opérationsEnOrdreAntéchronologique
                .Select(opération => 
                    LigneComptePresenter.FromOperationAndMontant(balanceDeFin, opération, out balanceDeFin))
                .Reverse();

        return lignesOpération.GetEnumerator();
    }

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}