using System.Threading;
using System.Windows;
using BankAccount.Desktop.ViewModel;

namespace BankAccount.Desktop;

public partial class MainWindow
{
    public RelevéCompteViewModel LignesCompte { get; }

    public static readonly DependencyProperty AccountProperty = DependencyProperty.Register(
        nameof(Account), typeof(Account), typeof(MainWindow), new PropertyMetadata(default(Account)));

    public Account Account
    {
        get => (Account)GetValue(AccountProperty);
        set => SetValue(AccountProperty, value);
    }

    public MainWindow(IAccountProvider accountProvider)
    {
        var account = await accountProvider.ProvideAsync(CancellationToken.None);
        LignesCompte = new RelevéCompteViewModel(account);

        InitializeComponent();
    }
}