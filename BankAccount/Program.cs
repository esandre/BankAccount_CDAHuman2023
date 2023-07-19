using BankAccount;

var account = new Account();

account.Déposer(40);
account.Retirer(10);
account.Déposer(30);
account.Retirer(150);

Console.WriteLine(account.Relevé);
// Balance -90€