namespace C4PRESENTATION_CONSOLE.DTOs
{
    public class ClienteDTO
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public Guid IdGuid { get; set; }
        public string? Telefone { get; set; }
        public string? Cidade { get; set; }
    }
}