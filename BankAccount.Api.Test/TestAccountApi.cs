using System.Net.Http.Json;
using System.Text.Json;
using BankAccount.Api.Presenters;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace BankAccount.Api.Test
{
    public class TestAccountApi
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public TestAccountApi()
        {
            // Arrange
            _server = new TestServer(
                new WebHostBuilder()
                .UseStartup("BankAccount.Api")
            );

            _client = _server.CreateClient();
        }

        [Fact]
        public void TestRetraitSansApi()
        {
            // ETANT DONNE un compte en banque
            var compte = Account.ApprovisionnéAuDépartAvec(0);

            // QUAND on retire 1€
            compte.Retirer(1);

            // ALORS le solde après opération diminue d'1€
            Assert.Equal(-1, compte.Balance.ToSignedInteger());
        }

        private async Task<int> GetSoldeAprèsDernièreOpérationAsync()
        {
            var résultatConsultationInitiale = await _client.GetAsync("/Account");
            var content = await résultatConsultationInitiale.Content.ReadAsStreamAsync();
            var relevéCompteInitial = JsonSerializer.Deserialize<RelevéComptePresenter>(content);
            return relevéCompteInitial!.Last().SoldeAprèsOpération;
        }

        private record PostDepotContract(ushort Montant);

        [Fact]
        public async Task TestRetraitAvecApiAsync()
        {
            // ETANT DONNE un compte en banque ayant un solde donné
            var soldeAvantOpération = await GetSoldeAprèsDernièreOpérationAsync();
            
            // QUAND on retire 1€
            await _client.PostAsync(
                "/Account/retrait", 
                JsonContent.Create(new PostDepotContract(1)));

            // ALORS le solde après opération diminue d'1€
            var soldeAttendu = soldeAvantOpération - 1;
            var soldeObtenu = await GetSoldeAprèsDernièreOpérationAsync();
            Assert.Equal(soldeAttendu, soldeObtenu);
        }
    }
}