using BankAccount;
using BankAccount.Console;
using BankAccount.SQLite;

var accountProvider = new SQLiteProvider(new DatabaseParameters(
    @"C:\Users\kryza\Documents\Sources\Formations\CDA Human 2023\BankAccount\db.sqlite"), new HorlogeSystème());

{
    var accountAvantStockage = Account.ApprovisionnéAuDépartAvec(850, new HorlogeSystème());
    accountAvantStockage.Retirer(300);
    await accountProvider.PersistAsync(accountAvantStockage, CancellationToken.None);
}

{
    var accountAprèsStockage = await accountProvider.ProvideAsync(CancellationToken.None);
    Console.WriteLine(new RelevéCompte(accountAprèsStockage));
}
