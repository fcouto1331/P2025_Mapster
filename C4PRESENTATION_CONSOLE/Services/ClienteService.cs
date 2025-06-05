using C4PRESENTATION_CONSOLE.Entities;
using C4PRESENTATION_CONSOLE.Interfaces.IRepositories;
using C4PRESENTATION_CONSOLE.Interfaces.IServices;

namespace C4PRESENTATION_CONSOLE.Services
{
    public class ClienteService : IClienteService
    {
        public readonly IClienteRepository _repository;
        public ClienteService(IClienteRepository repository)
        {
            _repository = repository;
        }

        public void Alterar(Cliente cliente, int id) => _repository.Alterar(cliente, id);

        public void Criar(Cliente cliente) => _repository.Criar(cliente);

        public void Excluir(int id) => _repository.Excluir(id);

        public List<Cliente> Listar() => _repository.Listar();

        public Cliente PegarPorId(int id) => _repository.PegarPorId(id);
    }
}