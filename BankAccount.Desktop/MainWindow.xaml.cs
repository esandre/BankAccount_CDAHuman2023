using BankAccount.Desktop.ViewModel;

namespace BankAccount.Desktop;

public partial class MainWindow
{
    public RelevéCompteViewModel LignesCompte { get; }

    public MainWindow(IAccountProvider accountProvider)
    {
        LignesCompte = new RelevéCompteViewModel(accountProvider.Provide());

        InitializeComponent();
    }
}