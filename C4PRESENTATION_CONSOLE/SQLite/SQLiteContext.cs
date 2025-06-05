using Microsoft.Data.Sqlite;
using System.Data;

namespace C4PRESENTATION_CONSOLE.SQLite
{
    public class SQLiteContext
    {
        private readonly string? _conexao;

        public SQLiteContext()
        {
            string projectDirectory = Directory.GetCurrentDirectory();
            string pathDb = Path.Combine(projectDirectory.Replace("\\bin\\Debug\\net8.0", ""), "AppData", "P2025_Mapster.db");
            _conexao = $"Data Source={pathDb}";
        }

        public IDbConnection CreateConnection() => new SqliteConnection(_conexao);
    }
}
