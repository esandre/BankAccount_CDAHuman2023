using BankAccount;
using BankAccount.Console;
using BankAccount.FakeDataProvider;

IAccountProvider accountProvider = new FakeDataAccountProvider();
var account = accountProvider.Provide();
Console.WriteLine(new RelevéCompte(account));