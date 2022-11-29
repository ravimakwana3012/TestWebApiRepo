using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TestWebApi;
using Xunit;

namespace TestWebApi_xUnit
{
    public class BookTests
    {
        [Fact]
        public async Task TestGetBooksAsync()
        {
            // Arrange
            var hostBuilder = new HostBuilder()
                .ConfigureWebHost(webHost =>
                {
            // Add TestServer
                    webHost.UseTestServer();
                    webHost.UseStartup<TestWebApi.Startup>();
                });

            // Create and start up the host
            var host = await hostBuilder.StartAsync();

            // Create an HttpClient which is setup for the test host
            var client = host.GetTestClient();

            // Act
            var response = await client.GetAsync("/api/Book/GetAll");

            // Assert
            var responseString = await response.Content.ReadAsStringAsync();
            //responseString.Should().Be("This is a test");
        }
    }
}
