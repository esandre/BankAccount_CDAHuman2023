using BankAccount.Web.Presenters;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankAccount.Web.Pages;

public class IndexModel : PageModel
{
    public RelevéComptePresenter LignesCompte { get; }
    public string BalanceFinale { get; }

    public IndexModel(IAccountProvider accountProvider)
    {
        var account = accountProvider.Provide();
        BalanceFinale = account.Balance.ToString();
        LignesCompte = new RelevéComptePresenter(account);
    }

    public void OnGet()
    {

    }
}