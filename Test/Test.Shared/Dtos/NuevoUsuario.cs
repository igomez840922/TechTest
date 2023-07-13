namespace Test.Shared.Dtos
{
    public class NuevoUsuario
    {
        public int IdTipoDocumento { get; set; }
        public string NroIdentificacion { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
