using System.Net;
using Xunit;

namespace ProcureRiskAnalyzer.Tests
{
    public class SuppliersApiTests
    {
        private readonly HttpClient _client;

        public SuppliersApiTests()
        {
            // Підключаємось до вже запущеного веб-сервера
            _client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5019") // порт твого застосунку
            };
        }

        [Fact]
        public async Task GetSuppliersV1_ReturnsOk()
        {
            var response = await _client.GetAsync("/api/v1/suppliersapi");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetSuppliersV2_ReturnsOk()
        {
            var response = await _client.GetAsync("/api/v2/suppliers");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
