using BankAccount.Desktop.ViewModel;

namespace BankAccount.Desktop;

public partial class MainWindow
{
    public RelevéCompteViewModel LignesCompte { get; } = new();

    public MainWindow()
    {
        InitializeComponent();
    }
}