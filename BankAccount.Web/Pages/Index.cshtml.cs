using BankAccount.Web.Composants;
using BankAccount.Web.Presenters;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankAccount.Web.Pages
{
    public class IndexModel : PageModel
    {
        public IEnumerable<LigneComptePresenter> LignesCompte { get; }
        public string BalanceFinale { get; }

        public IndexModel(IAccountProvider accountProvider)
        {
            var account = accountProvider.Provide();
            BalanceFinale = account.Balance.ToString();
            LignesCompte = account.OpérationsEnOrdreAntéchronologique.Select(LigneComptePresenter.FromOperation);
        }

        public void OnGet()
        {

        }
    }
}