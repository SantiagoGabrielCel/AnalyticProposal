using AnalyticCelTech.Domain.Models;

namespace AnalyticCelTech.Application.Ports
{
    public interface IAIAnalyzerPort
    {
        Task<List<ClausulaAnalizada>> AnalizarTextoContratoAsync(string textoPlano);
    }
}
