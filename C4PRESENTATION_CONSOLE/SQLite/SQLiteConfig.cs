using Microsoft.Data.Sqlite;
using System.Text;

namespace C4PRESENTATION_CONSOLE.SQLite
{
    public class SQLiteConfig
    {
        private readonly SQLiteContext _context;

        public SQLiteConfig(SQLiteContext context)
        {
            _context = context;
        }

        private void CriarArquivoBanco()
        {
            try
            {
                string projectDirectory = Directory.GetCurrentDirectory();
                string pathDb = Path.Combine(projectDirectory.Replace("\\bin\\Debug\\net8.0", ""), "AppData", "P2025_Mapster.db");
                if (!File.Exists(pathDb))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(pathDb) ?? string.Empty);
                    using (File.Create(pathDb)) { } // Fecha imediatamente
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro: {ex.Message}", ex);
            }
        }

        private void CriarTabela()
        {
            try
            {
                using (var db = _context.CreateConnection())
                {
                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                        StringBuilder query = new StringBuilder();
                        query.AppendLine("CREATE TABLE IF NOT EXISTS Cliente ( ");
                        query.AppendLine("Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, ");
                        query.AppendLine("Nome TEXT NOT NULL, ");
                        query.AppendLine("IdGuid TEXT NOT NULL UNIQUE, ");
                        query.AppendLine("Telefone TEXT NOT NULL, ");
                        query.AppendLine("Cidade TEXT NOT NULL ");
                        query.AppendLine("); ");
                        cmd.CommandText = query.ToString();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqliteException ex)
            {
                throw new Exception($"Erro Sqlite: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro: {ex.Message}", ex);
            }
        }

        public void Iniciar()
        {
            CriarArquivoBanco();
            CriarTabela();
        }
    }
}