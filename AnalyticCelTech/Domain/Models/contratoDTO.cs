using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Http;

namespace AnalyticCelTech.Domain.Models
{
    public class contratoDTO
    {
        [SwaggerSchema("Archivo PDF del contrato a analizar")]
        [SwaggerParameter(Required = true)]
        public IFormFile Archivo { get; set; } = default!;
    }
}
