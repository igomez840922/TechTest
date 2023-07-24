namespace Test.Server.AuxService
{
    public interface ISourceStorage
    {
        Task EliminarArchivo(string ruta, string nombreContenedor);
        Task<string> GuardarArchivo(byte[] contenido, string extension, string nombreContenedor);

        async Task<string> EditarArchivo(byte[] contenido, string extension,
            string nombreContenedor, string ruta)
        {
            if (ruta is not null)
            {
                await EliminarArchivo(ruta, nombreContenedor);
            }

            return await GuardarArchivo(contenido, extension, nombreContenedor);
        }
    }
}
