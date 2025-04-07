using AnalyticCelTech.Domain.Models;

namespace AnalyticCelTech.Application.Ports
{
    public interface IContratoAnalyzerUseCase
    {
        Task<List<ClausulaAnalizada>> AnalizarContratoAsync(Stream archivoStream, string nombreArchivo);
    }
}
