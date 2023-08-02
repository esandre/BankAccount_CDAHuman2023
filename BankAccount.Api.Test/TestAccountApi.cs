using System.Net.Http.Json;
using System.Text.Json;
using BankAccount.Api.Presenters;
using Microsoft.AspNetCore.Mvc.Testing;

namespace BankAccount.Api.Test
{
    public class TestAccountApi
    {
        private readonly HttpClient _client;

        public TestAccountApi()
        {
            var applicationFactory = new WebApplicationFactory<Program>();
            _client = applicationFactory.CreateClient();
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

            if (!résultatConsultationInitiale.IsSuccessStatusCode)
                throw new HttpRequestException(
                    await résultatConsultationInitiale.Content.ReadAsStringAsync(),
                    null,
                    résultatConsultationInitiale.StatusCode);

            var relevéCompteInitial = JsonSerializer.Deserialize<LigneCompteContract[]>(content)!;
            return relevéCompteInitial.LastOrDefault()?.soldeAprèsOpération ?? 0;

        }

        private record PostDepotContract(ushort montant);
        
        private record LigneCompteContract(DateTime date, int crédit, int débit, int soldeAprèsOpération);

        [Fact]
        public async Task TestRetraitAvecApiAsync()
        {
            // ETANT DONNE un compte en banque ayant un solde donné
            var soldeAvantOpération = await GetSoldeAprèsDernièreOpérationAsync();
            
            // QUAND on retire 1€
            await _client.PostAsync(
                "/Account/retrait?montant=1", null);

            // ALORS le solde après opération diminue d'1€
            var soldeAttendu = soldeAvantOpération - 1;
            var soldeObtenu = await GetSoldeAprèsDernièreOpérationAsync();
            Assert.Equal(soldeAttendu, soldeObtenu);
        }

        [Fact]
        public async Task TestRetraitsMultiplesAvecApiAsync()
        {
            // ETANT DONNE un compte en banque ayant un solde donné
            var soldeAvantOpération = await GetSoldeAprèsDernièreOpérationAsync();

            // QUAND on retire 1€, 100 fois en même temps
            var tasks = Enumerable.Range(1, 100)
                .Select(_ => _client.PostAsync(
                    "/Account/retrait?montant=1", null));

            await Task.WhenAll(tasks);

            // ALORS le solde après opération diminue d'100€
            var soldeAttendu = soldeAvantOpération - 100;
            var soldeObtenu = await GetSoldeAprèsDernièreOpérationAsync();
            Assert.Equal(soldeAttendu, soldeObtenu);
        }
    }
}