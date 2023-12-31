using BankAccount.SQLite;

namespace BankAccount.Api;

public partial class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        ConfigureServices(builder.Services);

        var app = builder.Build();
        ConfigureApp(app);

        app.Run();
    }

    private static void ConfigureApp(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddSingleton<SQLiteProvider>();
        services.AddSingleton<IHorloge, HorlogeSystème>();
        services.AddSingleton<IAccountProvider>(context => context.GetRequiredService<SQLiteProvider>());
        services.AddSingleton<IAccountPersister>(context => context.GetRequiredService<SQLiteProvider>());
        services.AddSingleton(context =>
        {
            var configuration = context.GetRequiredService<IConfiguration>();
            return new DatabaseParameters(configuration["DatabasePath"]!);
        });
    }
}