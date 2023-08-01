using Microsoft.Data.Sqlite;

namespace BankAccount.SQLite;

public class SQLiteProvider : IAccountProvider, IAccountPersister
{
    private const string Path = @"C:\Users\kryza\Documents\Sources\Formations\CDA Human 2023\BankAccount\db.sqlite";

    /// <inheritdoc />
    public async Task<Account> ProvideAsync(CancellationToken token)
    {
        var connString = new SqliteConnectionStringBuilder
            {
                DataSource = Path,
                Mode = SqliteOpenMode.ReadOnly
            }
            .ConnectionString;

        using var connection = new SqliteConnection(connString);
        connection.Open();

        var opérations = new List<Opération>();

        using var selectionCommand = connection.CreateCommand();
        selectionCommand.CommandText = "SELECT date, balance FROM operation;";
        using var reader = selectionCommand.ExecuteReader();

        while (reader.Read())
        {
            var dateOffset = DateTimeOffset.FromUnixTimeSeconds(reader.GetInt64(0));
            var date = dateOffset.UtcDateTime;
            var balance = reader.GetInt32(1);

            opérations.Add(new Opération(date, new Montant(balance)));
        }

        return new Account.AccountMemento(opérations).Restore();
    }

    /// <inheritdoc />
    public void Persist(Account account)
    {
        var connString = new SqliteConnectionStringBuilder
            {
                DataSource = Path,
                Mode = SqliteOpenMode.ReadWriteCreate
            }
            .ConnectionString;

        using var connection = new SqliteConnection(connString);
        connection.Open();

        var operations = account.OpérationsEnOrdreAntéchronologique
            .Select(op => $"({new DateTimeOffset(op.Date).ToUnixTimeSeconds()},{op.Balance.ToSignedInteger()})");

        using var creationCommand = connection.CreateCommand();

        creationCommand.CommandText =
            "CREATE TABLE IF NOT EXISTS operation(date INTEGER PRIMARY KEY, balance INTEGER NOT NULL);" +
            "INSERT INTO operation(date, balance) VALUES " +
            string.Join(',', operations) + ";";
        creationCommand.ExecuteScalar();
    }
}