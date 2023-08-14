using System.Net;

namespace DevsuTest.Tests.IntegrationTests
{
    public class ClientesControllerTests : IClassFixture<CustomWebApplicationFactory>, IDisposable
    {
        private CustomWebApplicationFactory _factory;
        private HttpClient _client;

        public ClientesControllerTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async void Get_Clientes()
        {
            var response = await _client.GetAsync("api/clientes");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        public void Dispose()
        {
            _client.Dispose();
            _factory.Dispose();
        }
    }
}
