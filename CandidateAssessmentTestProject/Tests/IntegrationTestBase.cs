using CandidateAssessment;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CandidateAssessmentTestProject.Tests
{
    public class IntegrationTestBase : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        protected readonly HttpClient Client;
        private readonly ServiceProvider ServiceProvider;
        public string TestId;

        public IntegrationTestBase(CustomWebApplicationFactory<Startup> factory)
        {
            Client = factory.WithWebHostBuilder(builder => builder.ConfigureTestServices(
                services => services.AddMvc(
                    options =>
                    {
                        //options.Filters.Add(new AllowAnonymousFilter());
                        //options.Filters.Insert(0, new FakeUserFilter());
                    }))).CreateClient();

            // Expose service provider so that we can query
            //ServiceProvider = factory.Sp;

            // Set a test id so that specific runs can be referenced
            TestId = $"{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}_{DateTime.UtcNow.Ticks}_{Guid.NewGuid().ToString()}";
        }

        /* Runs a test method */
        protected async Task<bool> RunTest(Func<Task<bool>> testToExecute)
        {
            // TODO: generate db context
            // TODO: generate any seed data etc

            return await testToExecute();
        }
    }
}
