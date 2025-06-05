using C4PRESENTATION_CONSOLE.Dapper;
using C4PRESENTATION_CONSOLE.Entities;
using C4PRESENTATION_CONSOLE.Interfaces.IRepositories;
using Dapper;
using System;
using System.Data;
using System.Text;

namespace C4PRESENTATION_CONSOLE.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly DapperContext _context;
        public ClienteRepository(DapperContext context)
        {
            _context = context;
        }

        public void Alterar(Cliente cliente, int id)
        {
            using (var dapper = _context.DapperConexao())
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("update Cliente set Nome = @Nome, Telefone = @Telefone, Cidade = @Cidade where Id = @Id");
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Nome", String.IsNullOrEmpty(cliente.Nome) ? DBNull.Value : cliente.Nome, dbType: DbType.String);
                parameters.Add("Telefone", String.IsNullOrEmpty(cliente.Telefone) ? DBNull.Value : cliente.Telefone, dbType: DbType.String);
                parameters.Add("Cidade", String.IsNullOrEmpty(cliente.Cidade) ? DBNull.Value : cliente.Cidade, dbType: DbType.String);
                parameters.Add("Id", id == 0 ? DBNull.Value : id, dbType: DbType.Int32);
                dapper.Execute(query.ToString(), parameters, commandType: CommandType.Text);

            }
        }

        public void Criar(Cliente cliente)
        {
            using (var dapper = _context.DapperConexao())
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("insert into Cliente (Nome, IdGuid, Telefone, Cidade) values (@Nome, @IdGuid, @Telefone, @Cidade)");
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Nome", String.IsNullOrEmpty(cliente.Nome) ? DBNull.Value : cliente.Nome, dbType: DbType.String);
                parameters.Add("IdGuid", cliente.IdGuid == Guid.Empty ? DBNull.Value : cliente.IdGuid, dbType: DbType.Guid);
                parameters.Add("Telefone", String.IsNullOrEmpty(cliente.Telefone) ? DBNull.Value : cliente.Telefone, dbType: DbType.String);
                parameters.Add("Cidade", String.IsNullOrEmpty(cliente.Cidade) ? DBNull.Value : cliente.Cidade, dbType: DbType.String);
                dapper.Execute(query.ToString(), parameters, commandType: CommandType.Text);
            }
        }

        public void Excluir(int id)
        {
            using (var dapper = _context.DapperConexao())
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("delete from Cliente where Id = @Id");
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Id", id == 0 ? DBNull.Value : id, dbType: DbType.Int32);
                dapper.Execute(query.ToString(), parameters, commandType: CommandType.Text);
            }
        }

        public List<Cliente> Listar()
        {
            using (var dapper = _context.DapperConexao())
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("select Id, Nome, IdGuid, Telefone, Cidade from Cliente");

                // método sem GUID
                //return [.. dapper.Query<Cliente>(query.ToString(), commandType: CommandType.Text)];

                // método sem private set 
                //var result = dapper.Query(query.ToString(), commandType: CommandType.Text)
                //    .Select(row => new Cliente
                //    {
                //        Id = (int)row.Id,
                //        Nome = (string)row.Nome,
                //        IdGuid = Guid.TryParse(row.IdGuid, out Guid guid) ? guid : Guid.Empty
                //    })
                //    .ToList();

                var result = dapper.Query(query.ToString(), commandType: CommandType.Text)
                    .Select(row => new Cliente(
                        (int)row.Id,
                        (string)row.Nome,
                        Guid.TryParse(row.IdGuid, out Guid guid) ? guid : Guid.Empty,
                        (string)row.Telefone,
                        (string)row.Cidade
                    ))
                    .ToList();
                return result;
            }
        }

        public Cliente PegarPorId(int id)
        {
            using (var dapper = _context.DapperConexao())
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("select Id, Nome, IdGuid, Telefone, Cidade from Cliente where Id = @Id");
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Id", id == 0 ? DBNull.Value : id, dbType: DbType.Int32);

                // método sem GUID
                //return dapper.QueryFirst<Cliente>(query.ToString(), parameters, commandType: CommandType.Text);

                // método sem private set
                //var row = dapper.QueryFirst(query.ToString(), parameters, commandType: CommandType.Text);
                //var result = new Cliente
                //{
                //    Id = (int)row.Id,
                //    Nome = (string)row.Nome,
                //    IdGuid = Guid.TryParse(row.IdGuid, out Guid guid) ? guid : Guid.Empty
                //};

                var row = dapper.QueryFirst(query.ToString(), parameters, commandType: CommandType.Text);
                var result = new Cliente(
                    (int)row.Id,
                    (string)row.Nome,
                    Guid.TryParse(row.IdGuid, out Guid guid) ? guid : Guid.Empty,
                    (string)row.Telefone,
                    (string)row.Cidade
                );
                return result;
            }
        }
    }
}