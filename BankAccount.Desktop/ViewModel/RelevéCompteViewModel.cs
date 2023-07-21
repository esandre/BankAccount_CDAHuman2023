using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BankAccount.Desktop.ViewModel;

public class RelevéCompteViewModel : IEnumerable<LigneCompteViewModel>
{
    private readonly Account _compteARelever;

    public RelevéCompteViewModel(Account account)
    {
        _compteARelever = account;
    }

    /// <inheritdoc />
    public IEnumerator<LigneCompteViewModel> GetEnumerator()
    {
        var balanceDeFin = _compteARelever.Balance;

        var opérationsEnOrdreAntéchronologique = _compteARelever
            .OpérationsEnOrdreAntéchronologique
            .ToArray();

        var lignesOpération =
            opérationsEnOrdreAntéchronologique
                .Select(opération =>
                    LigneCompteViewModel.FromOperationAndMontant(balanceDeFin, opération, out balanceDeFin))
                .Reverse();

        return lignesOpération.GetEnumerator();
    }

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}