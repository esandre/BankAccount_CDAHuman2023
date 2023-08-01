using BankAccount;
using BankAccount.Console;
using BankAccount.SQLite;

SQLiteProvider accountProvider = new SQLiteProvider();

{
    var accountAvantStockage = Account.ApprovisionnéAuDépartAvec(850);
    accountAvantStockage.Retirer(300);
    await accountProvider.PersistAsync(accountAvantStockage, CancellationToken.None);
}

{
    var accountAprèsStockage = await accountProvider.ProvideAsync(CancellationToken.None);
    Console.WriteLine(new RelevéCompte(accountAprèsStockage));
}
