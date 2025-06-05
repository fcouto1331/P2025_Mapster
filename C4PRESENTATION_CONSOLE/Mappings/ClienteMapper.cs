using C4PRESENTATION_CONSOLE.DTOs;
using C4PRESENTATION_CONSOLE.Entities;

namespace C4PRESENTATION_CONSOLE.Mappings
{
    public partial class Mapper
    {
        public static Cliente ToCliente(ClienteDTO clienteDTO)
        {
            var cliente = new Cliente(clienteDTO.Id, String.IsNullOrEmpty(clienteDTO.Nome) ? "" : clienteDTO.Nome, clienteDTO.IdGuid, clienteDTO.Telefone ?? "", clienteDTO.Cidade ?? "");
            return cliente;
        }

        public static List<Cliente> ToListCliente(List<ClienteDTO> lstClienteDTO)
        {
            return [.. lstClienteDTO.Select(ToCliente)];
        }
    }
}