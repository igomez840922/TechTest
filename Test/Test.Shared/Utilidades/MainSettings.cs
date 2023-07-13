namespace Test.Shared.Utilidades
{
    public class MainSettings : IMainSettings
    {
        public string KeyCripto { get; set; } = String.Empty;
        public string KeyToken { get; set; } = String.Empty;
        public string Domain { get; set; } = String.Empty;
        public string PathFotoProducto { get; set; } = String.Empty;
    }

    public interface IMainSettings
    {
        string KeyCripto { get; set; }
        string KeyToken { get; set; }
        string Domain { get; set; }
        string PathFotoProducto { get; set; }
    }
}
