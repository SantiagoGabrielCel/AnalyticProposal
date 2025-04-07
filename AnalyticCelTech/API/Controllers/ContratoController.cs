using AnalyticCelTech.Application.Ports;
using AnalyticCelTech.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnalyticCelTech.API.Controllers
{
    [ApiController]
    [Route("api/contratos")]
    public class ContratoController : ControllerBase
    {
        private readonly IContratoAnalyzerUseCase _analyzer;

        public ContratoController(IContratoAnalyzerUseCase analyzer)
        {
            _analyzer = analyzer;
        }


        [HttpPost("analizar")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Analizar([FromForm] contratoDTO dto)
        {

            if (dto.Archivo == null || dto.Archivo.Length == 0)
                return BadRequest("El archivo es requerido.");

            try
            {
                using var stream = dto.Archivo.OpenReadStream();
                var resultado = await _analyzer.AnalizarContratoAsync(stream, dto.Archivo.FileName);
                return Ok(resultado);
            }
            catch (NotSupportedException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }
    }
}
