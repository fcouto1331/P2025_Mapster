using C4PRESENTATION_CONSOLE.Entities;

namespace C4PRESENTATION_CONSOLE.Interfaces.IServices
{
    public interface IClienteService
    {
        void Alterar(Cliente cliente, int id);
        void Criar(Cliente cliente);
        void Excluir(int id);
        List<Cliente> Listar();
        Cliente PegarPorId(int id);
    }
}
