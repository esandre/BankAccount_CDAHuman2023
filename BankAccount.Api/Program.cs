using BankAccount;
using BankAccount.SQLite;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<SQLiteProvider>();
builder.Services.AddSingleton<IAccountProvider>(context => context.GetRequiredService<SQLiteProvider>());
builder.Services.AddSingleton<IAccountPersister>(context => context.GetRequiredService<SQLiteProvider>());
builder.Services.AddSingleton(new DatabaseParameters(
    @"C:\Users\kryza\Documents\Sources\Formations\CDA Human 2023\BankAccount\db.sqlite")
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
