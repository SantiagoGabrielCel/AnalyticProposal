using AnalyticCelTech.Application.Ports;
using AnalyticCelTech.Application.UseCases;
using AnalyticCelTech.Infrastructure.Files;
using AnalyticCelTech.Infrastructure.IA;



var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IContratoAnalyzerUseCase, AnalizarContratoUseCase>();

builder.Services.AddScoped<IFileConverter, PdfFileConverter>();
builder.Services.AddScoped<IAIAnalyzerPort, OpenAiAnalyzer>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

