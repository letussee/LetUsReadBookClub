using LibraryServices.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Xunit;
using LibraryServices.Models;
using LibraryServices.DataAccessLayer;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using LibraryServices;
using Microsoft.AspNetCore.TestHost;
using System.Threading.Tasks;

namespace LibraryServices_XUnitTest
{
    public class UTLibraryServices
    {
        public HttpClient client { get; private set; }
        public UTLibraryServices()
        {
            var config = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", false, true).Build();
                    


            var webHostBuilder = new WebHostBuilder()
                .UseConfiguration(config)
                .UseEnvironment("Development")
                .UseStartup(typeof(Startup));
            var server = new TestServer(webHostBuilder);
            client = server.CreateClient();



        }

        [Fact]
        public  async Task Test_Get()
        {
            
            var request = new HttpRequestMessage(new HttpMethod("GET"),"/api/book");
            
            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            Assert.True( response.IsSuccessStatusCode);
        }
    }
  
}
