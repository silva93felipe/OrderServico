using Dapper;
using System.Data;
using Microsoft.Data.Sqlite;

public class DatabaseInitializer
{
    private readonly string _connectionString;
    public DatabaseInitializer(string connectionString)
    {
        _connectionString = connectionString;
        InitializeDatabase();
    }

    public void InitializeDatabase()
    {
        using (IDbConnection connection = new SqliteConnection(_connectionString))
        {
            string createDatabaseSql = "CREATE DATABASE IF NOT EXISTS ordemservice";

            string createTableSql = @"
              CREATE TABLE IF NOT EXISTS ticket (
                ""Id"" INTEGER PRIMARY KEY AUTOINCREMENT,
                ""DataAbertura"" DATETIME NOT NULL,
                ""DataFechamento"" DATETIME,
                ""EquipamentoId"" INTEGER NOT NULL,
                ""SetorId"" INTEGER NOT NULL,
                ""Observacao"" TEXT NOT NULL,
                ""Ativo"" BOOLEAN NOT NULL DEFAULT true,
                ""UpdateAt"" DATETIME NOT NULL,
                ""Status"" INTEGER NOT NULL
            )";
        
            connection.Open();
            connection.Execute(createTableSql);
            connection.Close(); 
        }
    }
}
