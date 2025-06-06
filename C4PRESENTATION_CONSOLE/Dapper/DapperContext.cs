//using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using System.Data;

namespace C4PRESENTATION_CONSOLE.Dapper
{
    public class DapperContext
    {
        private readonly string? _conexao;
        public DapperContext() 
        {
            string projectDirectory = Directory.GetCurrentDirectory();
            string caminhoCompleto = Path.Combine(projectDirectory.Replace("\\bin\\Debug\\net8.0", ""), "AppData", "P2025_Mapster.db");
            _conexao = $"Data Source={caminhoCompleto}";
        }

        //public IDbConnection DapperConexao() => new SqlConnection(_conexao);
        public IDbConnection DapperConexao() => new SqliteConnection(_conexao);
    }
}
