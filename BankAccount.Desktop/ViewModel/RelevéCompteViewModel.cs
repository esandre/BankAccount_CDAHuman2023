using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BankAccount.Desktop.ViewModel;

public class RelevéCompteViewModel : IEnumerable<LigneCompteViewModel>
{
    /// <inheritdoc />
    public IEnumerator<LigneCompteViewModel> GetEnumerator()
        => new []
        {
            new LigneCompteViewModel(DateTime.Now.ToString(), " 3.00 €", "", "3 00€")

        }.Cast<LigneCompteViewModel>().GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}