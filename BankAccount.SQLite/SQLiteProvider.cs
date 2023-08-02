using Microsoft.Data.Sqlite;

namespace BankAccount.SQLite;

public class SQLiteProvider : IAccountProvider, IAccountPersister
{
    private readonly DatabaseParameters _parameters;
    private readonly IHorloge _horloge;

    public SQLiteProvider(DatabaseParameters parameters, IHorloge horloge)
    {
        _parameters = parameters;
        _horloge = horloge;
    }

    /// <inheritdoc />
    public async Task<Account> ProvideAsync(CancellationToken token)
    {
        var connString = new SqliteConnectionStringBuilder
            {
                DataSource = _parameters.Path,
                Mode = SqliteOpenMode.ReadWriteCreate
            }
            .ConnectionString;

        await using var connection = new SqliteConnection(connString);
        await connection.OpenAsync(token);

        var opérations = new List<Opération>();

        await using var selectionCommand = connection.CreateCommand();
        selectionCommand.CommandText = "CREATE TABLE IF NOT EXISTS operation(date INTEGER PRIMARY KEY, balance INTEGER NOT NULL);" +
                                       "SELECT date, balance FROM operation;";
        await using var reader = await selectionCommand.ExecuteReaderAsync(token);

        while (reader.Read())
        {
            var dateOffset = DateTimeOffset.FromUnixTimeSeconds(reader.GetInt64(0));
            var date = dateOffset.UtcDateTime;
            var balance = reader.GetInt32(1);

            opérations.Add(new Opération(date, new Montant(balance)));
        }

        return new Account.AccountMemento(opérations, _horloge).Restore();
    }

    /// <inheritdoc />
    public async Task PersistAsync(Account account, CancellationToken token)
    {
        var connString = new SqliteConnectionStringBuilder
            {
                DataSource = _parameters.Path,
                Mode = SqliteOpenMode.ReadWriteCreate
            }
            .ConnectionString;

        await using var connection = new SqliteConnection(connString);
        await connection.OpenAsync(token);

        var operations = account.OpérationsEnOrdreAntéchronologique
            .Select(op => $"({new DateTimeOffset(op.Date).ToUnixTimeSeconds()}," +
                          $"{op.Balance.ToSignedInteger()})");

        await using var creationCommand = connection.CreateCommand();

        creationCommand.CommandText =
            "CREATE TABLE IF NOT EXISTS operation(date INTEGER PRIMARY KEY, balance INTEGER NOT NULL);" +
            "INSERT OR IGNORE INTO operation(date, balance) VALUES " +
            string.Join(',', operations) + ";";
        await creationCommand.ExecuteScalarAsync(token);
    }
}