using AnalyticCelTech.Application.Ports;
using AnalyticCelTech.Domain.Models;
using OpenAI;
using OpenAI.Chat;
using OpenAI.Models;
using System.Data;
using System.Reflection;
using System.Text.Json;

namespace AnalyticCelTech.Infrastructure.IA
{
    public class OpenAiAnalyzer : IAIAnalyzerPort
    {
        private readonly ChatClient _client;

        public OpenAiAnalyzer(IConfiguration configuration)
        {
            var apiKey = configuration["OpenAI:ApiKey"];
            var openAiClient = new OpenAIClient(apiKey);
            _client = openAiClient.GetChatClient("gpt-3.5-turbo");
        }
        

        public async Task<List<ClausulaAnalizada>> AnalizarTextoContratoAsync(string textoPlano)
        {
            var prompt = GenerarPrompt(textoPlano);
            var mensajes = new List<ChatMessage>
        {
            new SystemChatMessage("Sos un asistente legal especializado en contratos y sos implacable. Capaz de detectar cualquier clausula interesante."),
            new UserChatMessage(prompt)
        };

            var options = new ChatCompletionOptions
            {
                ResponseFormat = ChatResponseFormat.CreateJsonObjectFormat()
            };

            var completion = await _client.CompleteChatAsync(mensajes, options);
            var json = completion.Value.Content[0].Text;

            try
            {
                var clausulas = JsonSerializer.Deserialize<List<ClausulaAnalizada>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return clausulas ?? new List<ClausulaAnalizada>();
            }
            catch
            {
                return new List<ClausulaAnalizada>
            {
                new ClausulaAnalizada
                {
                    Entidad = "Desconocida",
                    Clausula = "Error al interpretar JSON.",
                    Beneficio = "Formato inválido",
                    Riesgo = "El contrato puede contener ambigüedades"
                }
            };
            }
        }

        private string GenerarPrompt(string texto) =>
        $@"Analizá el siguiente contrato. Extraé las cláusulas más relevantes y agrupalas por la entidad que se beneficia.
            Para cada cláusula devolvé:
            - entidad beneficiada
            - cláusula (texto)
            - beneficio que obtiene
            - riesgo para otras partes
            Formato JSON:
            [
              {{
                ""entidad"": ""Empresa A"",
                ""clausula"": ""La Empresa A puede cancelar el contrato con 5 días de aviso..."",
                ""beneficio"": ""Cancelación sin penalidad"",
                ""riesgo"": ""Inestabilidad para la otra parte""
              }}
            ]
            CONTRATO:
            ===
            {texto}
            ===";
    }
}

