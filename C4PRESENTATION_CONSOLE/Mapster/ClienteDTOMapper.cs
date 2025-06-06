using C4PRESENTATION_CONSOLE.DTOs;
using C4PRESENTATION_CONSOLE.Entities;
using Mapster;

namespace C4PRESENTATION_CONSOLE.Mapster
{
    public partial class Mapper
    {
        public static ClienteDTO ToClienteDTO(Cliente cliente)
        {
            return cliente.Adapt<ClienteDTO>();
        }

        public static List<ClienteDTO> ToClienteDTO(List<Cliente> cliente)
        {
            return cliente.Adapt<List<ClienteDTO>>();
        }
    }
}
