using AnalyticCelTech.Application.Ports;
using AnalyticCelTech.Domain.Models;

namespace AnalyticCelTech.Application.UseCases
{
    public class AnalizarContratoUseCase : IContratoAnalyzerUseCase
    {
        private readonly IFileConverter _fileConverter;
        private readonly IAIAnalyzerPort _aiAnalyzer;

        public AnalizarContratoUseCase(IFileConverter fileConverter, IAIAnalyzerPort aiAnalyzer)
        {
            _fileConverter = fileConverter;
            _aiAnalyzer = aiAnalyzer;
        }

        public async Task<List<ClausulaAnalizada>> AnalizarContratoAsync(Stream archivoStream, string nombreArchivo)
        {
            var textoPlano = await _fileConverter.ConvertirATextoPlanoAsync(archivoStream, nombreArchivo);
            return await _aiAnalyzer.AnalizarTextoContratoAsync(textoPlano);
        }
    }
}
