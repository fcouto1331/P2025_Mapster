using C4PRESENTATION_CONSOLE.DTOs;
using C4PRESENTATION_CONSOLE.Entities;
using Mapster;

namespace C4PRESENTATION_CONSOLE.Mapster
{
    public partial class Mapper
    {
        public static Cliente ToCliente(ClienteDTO clienteDTO)
        {
            return clienteDTO.Adapt<Cliente>();
        }

        public static List<Cliente> ToCliente(List<ClienteDTO> clienteDTO)
        {
            return clienteDTO.Adapt<List<Cliente>>();
        }
    }
}
