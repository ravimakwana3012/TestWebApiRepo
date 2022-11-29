using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TestWebApi;
using TestWebApi_BAL.Services;
using TestWebApi_DAL.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using System.Net.Http;

namespace TestWebApi_MSTest
{
    [TestClass]
    public class BookControllerTests
    {
        private static WebApplicationFactory<Startup> _factory;

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            _factory = new WebApplicationFactory<Startup>();
        }

        [TestMethod]
        public async Task GetAllBookShouldReturnSuccessResponse()
        {
            _factory = _factory.WithWebHostBuilder(builder =>
            {
                builder.UseSetting("https_port", "5001").UseEnvironment("Development");
                builder.ConfigureServices(services =>
                {
                    services.AddScoped<IServiceBook<book>, MockBookServiceOkResponse>();
                });
            });
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/Book/GetAll");
            var json = await response.Content.ReadAsStringAsync();
            List<book> lstData = JsonConvert.DeserializeObject<List<book>>(json);
            if (lstData.Count > 0)
            {
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                Assert.AreEqual("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());
                Assert.AreEqual(2, lstData.Count);
            }
        }

        [TestMethod]
        public async Task GetAllBookShouldReturnNotFoundResponse()
        {
            _factory = _factory.WithWebHostBuilder(builder =>
            {
                builder.UseSetting("https_port", "5001").UseEnvironment("Development");
                builder.ConfigureServices(services =>
                {
                    services.AddScoped<IServiceBook<book>, MockBookServiceNotFoundResponse>();
                });
            });
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/Book/GetAll");
            var json = await response.Content.ReadAsStringAsync();
            if (json == "No data in response.")
            {
                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
                Assert.AreEqual("text/plain; charset=utf-8", response.Content.Headers.ContentType?.ToString());
            }
        }

        [TestMethod]
        public async Task GetBookByIdShouldReturnSuccessResponse()
        {
            _factory = _factory.WithWebHostBuilder(builder =>
            {
                builder.UseSetting("https_port", "5001").UseEnvironment("Development");
                builder.ConfigureServices(services =>
                {
                    services.AddScoped<IServiceBook<book>, MockBookServiceOkResponse>();
                });
            });
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/Book/Get/7FFC1A3E-D7D8-4E07-95DA-E78CBB3F4B51");
            var json = await response.Content.ReadAsStringAsync();
            book objBook = JsonConvert.DeserializeObject<book>(json);
            if (objBook != null)
            {
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                Assert.AreEqual("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());
                Assert.AreEqual("Book 1", objBook.name);
            }
        }

        [TestMethod]
        public async Task GetBookByIdShouldReturnNotFoundResponse()
        {
            _factory = _factory.WithWebHostBuilder(builder =>
            {
                builder.UseSetting("https_port", "5001").UseEnvironment("Development");
                builder.ConfigureServices(services =>
                {
                    services.AddScoped<IServiceBook<book>, MockBookServiceNotFoundResponse>();
                });
            });
            var client = _factory.CreateClient();
            var response = await client.GetAsync("api/Book/Get/7FFC1A3E-D7D8-4E07-95DA-E78CBB3F4B51");
            var json = await response.Content.ReadAsStringAsync();
            if (json == "No data with id - 7ffc1a3e-d7d8-4e07-95da-e78cbb3f4b51")
            {
                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
                Assert.AreEqual("text/plain; charset=utf-8", response.Content.Headers.ContentType?.ToString());
            }
        }

        [TestMethod]
        public async Task PostBookShouldReturnSuccessResponse()
        {
            _factory = _factory.WithWebHostBuilder(builder =>
            {
                builder.UseSetting("https_port", "5001").UseEnvironment("Development");
                builder.ConfigureServices(services =>
                {
                    services.AddScoped<IServiceBook<book>, MockBookServiceOkResponse>();
                });
            });
            var client = _factory.CreateClient();

            book data1 = new book();
            data1.id = Guid.Parse("7FFC1A3E-D7D8-4E07-95DA-E78CBB3F4B51");
            data1.name = "Book 1 Added";
            data1.authorName = "Ravi Makwana";

            var myContent = JsonConvert.SerializeObject(data1);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync("api/Book/Post", byteContent);
            var json = await response.Content.ReadAsStringAsync();
            book objBook = JsonConvert.DeserializeObject<book>(json);
            if (objBook != null)
            {
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                Assert.AreEqual("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());
                Assert.AreEqual("Book 1 Added", objBook.name);
            }
        }

        [TestMethod]
        public async Task PostBookShouldReturnNotFoundResponse()
        {
            _factory = _factory.WithWebHostBuilder(builder =>
            {
                builder.UseSetting("https_port", "5001").UseEnvironment("Development");
                builder.ConfigureServices(services =>
                {
                    services.AddScoped<IServiceBook<book>, MockBookServiceNotFoundResponse>();
                });
            });
            var client = _factory.CreateClient();

            book data1 = new book();
            data1.id = Guid.Parse("7FFC1A3E-D7D8-4E07-95DA-E78CBB3F4B51");
            data1.name = "Book 1 Added";
            data1.authorName = "Ravi Makwana";

            var myContent = JsonConvert.SerializeObject(data1);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync("api/Book/Post", byteContent);
            var json = await response.Content.ReadAsStringAsync();
            if (json == "Data is not inserted.")
            {
                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
                Assert.AreEqual("text/plain; charset=utf-8", response.Content.Headers.ContentType?.ToString());
            }
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            _factory.Dispose();
        }
    }

    public class MockBookServiceOkResponse : IServiceBook<book>
    {
        public Task<book> AddBook(book _object)
        {
            book data1 = new book();
            data1.id = Guid.Parse("7FFC1A3E-D7D8-4E07-95DA-E78CBB3F4B51");
            data1.name = "Book 1 Added";
            data1.authorName = "Ravi Makwana";
            return Task.FromResult(data1);
        }

        public IEnumerable<book> GetAllBook()
        {
            List<book> lstData = new List<book>();
            book data1 = new book();
            data1.id = Guid.Parse("7FFC1A3E-D7D8-4E07-95DA-E78CBB3F4B51");
            data1.name = "Book 1";
            data1.authorName = "Ravi Makwana";
            lstData.Add(data1);
            book data2 = new book();
            data2.id = Guid.Parse("89DA6031-4423-461A-2727-08DAD1604350");
            data2.name = "Book 2";
            data2.authorName = "Ekta Makwana";
            lstData.Add(data2);
            return lstData;
        }

        public book GetById(Guid Id)
        {
            book data1 = new book();
            data1.id = Guid.Parse("7FFC1A3E-D7D8-4E07-95DA-E78CBB3F4B51");
            data1.name = "Book 1";
            data1.authorName = "Ravi Makwana";
            return data1;
        }
    }

    public class MockBookServiceNotFoundResponse : IServiceBook<book>
    {
        public Task<book> AddBook(book _object)
        {
            book data1 = null;
            return Task.FromResult(data1);
        }

        public IEnumerable<book> GetAllBook()
        {
            List<book> lstData = new List<book>();
            return lstData;
        }

        public book GetById(Guid Id)
        {
            return null;
        }
    }
}