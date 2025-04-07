namespace AnalyticCelTech.Infrastructure.Files
{
    using UglyToad.PdfPig;
    using System.Text;
    using AnalyticCelTech.Application.Ports;

    public class PdfFileConverter : IFileConverter
    {
        public async Task<string> ConvertirATextoPlanoAsync(Stream archivo, string nombreArchivo)
        {
            if (!nombreArchivo.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                throw new NotSupportedException("Solo se soportan archivos PDF por ahora.");

            using var memoryStream = new MemoryStream();
            await archivo.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            var builder = new StringBuilder();

            using (var document = PdfDocument.Open(memoryStream))
            {
                foreach (var page in document.GetPages())
                {
                    builder.AppendLine(page.Text);
                }
            }

            return builder.ToString();
        }
    }
}
