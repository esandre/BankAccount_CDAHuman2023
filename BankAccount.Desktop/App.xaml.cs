using BankAccount.FakeDataProvider;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BankAccount.Desktop;

public partial class App
{
    public App()
    {
        var container = Host.CreateApplicationBuilder();
        container.Services.AddSingleton<IAccountProvider>(new FakeDataAccountProvider());
        container.Services.AddSingleton<MainWindow>();

        var app = container.Build();
        var mainWindow = app.Services.GetRequiredService<MainWindow>();

        mainWindow.Show();
    }
}