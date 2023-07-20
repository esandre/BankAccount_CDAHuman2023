using BankAccount;
using BankAccount.Console;

var account = Account.ApprovisionnéAuDépartAvec(60);

account.Déposer(40);
account.Retirer(10);
account.Déposer(30);
account.Retirer(150);

Console.WriteLine(new RelevéCompte(account));