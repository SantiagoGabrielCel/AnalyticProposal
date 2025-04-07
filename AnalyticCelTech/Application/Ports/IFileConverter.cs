namespace AnalyticCelTech.Application.Ports
{
    public interface IFileConverter
    {
        Task<string> ConvertirATextoPlanoAsync(Stream archivo, string nombreArchivo);
    }
}
