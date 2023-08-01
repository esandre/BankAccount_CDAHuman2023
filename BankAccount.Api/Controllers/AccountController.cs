using BankAccount.Api.Presenters;
using Microsoft.AspNetCore.Mvc;

namespace BankAccount.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountProvider _accountProvider;
        private readonly IAccountPersister _accountPersister;

        public AccountController(
            IAccountProvider accountProvider, 
            IAccountPersister accountPersister)
        {
            _accountProvider = accountProvider;
            _accountPersister = accountPersister;
        }
        
        [HttpGet]
        public async Task<RelevéComptePresenter> Get(CancellationToken token)
        {
            var account = await _accountProvider.ProvideAsync(token);
            return new RelevéComptePresenter(account);
        }

        [HttpPost("depot")]
        public async Task PostDepot(ushort montant, CancellationToken token)
        {
            var account = await _accountProvider.ProvideAsync(token);
            account.Déposer(montant);
            await _accountPersister.PersistAsync(account, token);
        }

        [HttpPost("retrait")]
        public async Task PostRetrait()
        {

        }
    }
}