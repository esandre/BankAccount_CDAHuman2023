using BankAccount.Web.Presenters;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankAccount.Web.Pages;

public class IndexModel : PageModel
{
    private readonly IAccountProvider _accountProvider;
    public RelevéComptePresenter LignesCompte => new (Account);
    public string BalanceFinale => Account.Balance.ToString();
    public Account Account { get; private set; }

    public IndexModel(IAccountProvider accountProvider)
    {
        _accountProvider = accountProvider;
        Account = Account.ApprovisionnéAuDépartAvec(0);
    }

    public async void OnGet()
    {
        Account = await _accountProvider.ProvideAsync(CancellationToken.None);
    }
}