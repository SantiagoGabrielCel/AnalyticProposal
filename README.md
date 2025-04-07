# AnalyticProposal
Contract Clause Analyzer with ChatGPT (Hexagonal Architecture, .NET 8)


Features
🧠 Upload PDF contracts for automated clause analysis


🤖 Uses ChatGPT to detect:

Which entity benefits from each clause

The literal clause text

The benefit

The risk to other parties


📦 Clean, testable code using Hexagonal Architecture

✅ Multipart file upload via Swagger or API clients

🔐 Secure API key usage via config or secrets

📄 PDF-to-text conversion using PdfPig

🌐 Minimal API, built with ASP.NET Core Web API

🧱 Tech Stack
.NET 8

ASP.NET Core Web API

Hexagonal Architecture

OpenAI SDK (v2.1.0 official)

UglyToad.PdfPig (PDF to text)

Swashbuckle (Swagger)


API Endpoint
POST /api/contratos/analizar
Upload a contract PDF via multipart/form-data.

Request:

Key	Type	Description
archivo	File	PDF contract to analyze


🔐 Configuration
Add your OpenAI API Key:
Option 1: appsettings.Development.json

