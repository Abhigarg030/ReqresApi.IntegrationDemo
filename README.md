# ReqresApiIntegration




A modular, testable, and robust .NET Core / .NET 5+ class library for integrating with an external user API (https://reqres.in).  
It demonstrates real-world practices such as clean architecture, resilient HTTP communication, configuration management, and testability.



---

## ⚙️ Requirements

- [.NET 5+ SDK](https://dotnet.microsoft.com/download)
- Optionally: Visual Studio / Rider / VS Code

---

Run the Console App
cd ReqresApi.ConsoleApp
dotnet run

🧪 Testing

cd ReqresApi.Tests
dotnet test


## 📌 Features

- ✅ Async API client using `HttpClientFactory`
- ✅ Clean separation of concerns
- ✅ Strongly typed models and configuration via Options pattern
- ✅ Resilient error handling (network, HTTP errors, deserialization)
- ✅ Configurable `x-api-key` and base URL
- ✅ Pagination handled internally
- ✅ Unit tested with `xUnit` and `Moq`
- ✅ Optional: Retry logic (via Polly) & In-memory caching

---
⚙️ Configuration
Update appsettings.json in ReqresApi.ConsoleApp:
{
  "ReqresApi": {
    "BaseUrl": "https://reqres.in/api/",
    "ApiKey": "reqres-free-v1"
  }
}
These settings are injected into HttpClient using the Options pattern.

🧠 Design Decisions
Design Area	Implementation
HTTP Calls	HttpClientFactory + DI
Resilience	Try/Catch + Custom Exceptions
Config Management	IOptions<T> Pattern
Modularity	Client/Service split with interfaces
Pagination	Handled internally in service layer
Testability	Uses Moq for isolation in unit tests
