using CandidateAssessment;
using CandidateAssessment.Controllers.Api.WebMessages;
using CandidateAssessment.Controllers.Api.WebMessages.Cheque;
using CandidateAssessment.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CandidateAssessmentTestProject.Tests.ChequeService
{
    public class CreateTest : IntegrationTestBase
    {
        public CreateTest(CustomWebApplicationFactory<Startup> factory) : base(factory) { }

        [Fact]
        public async Task CreateChequeIsSuccessful()
        {
            await RunTest(async () => {

                var name = "john doe";
                var amount = 49.95M;
                var response = await CreateCheque(Client, name, amount);

                Assert.NotNull(response);
                Assert.True(response.Success);
                Assert.NotNull(response.Data);
                Assert.NotNull(response.Data.cheque);

                Assert.Equal(response.Data.cheque.name.Value, name);
                Assert.True(response.Data.cheque.amount.Value == (double) amount);
                Assert.Equal(response.Data.amountInWords.Value, "forty nine dollars ninety five cents".ToUpper());

                return true;
            });
        }


        /* Generic create cheque utility */
        internal static async Task<ApiMessageResponseBase> CreateCheque(HttpClient client, string name, decimal amount)
        {
            // Invoke create action on controller
            var httpResponse = await client.PostAsync($"/cheques/create", new StringContentWrapper(GetDefaultCreateChequePayload(name, amount)));
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();

            if (!httpResponse.IsSuccessStatusCode) throw new HttpRequestException($"Http error occurred: ${httpResponse.ToString()}");

            // Check http response payload
            return JsonConvert.DeserializeObject<ApiMessageResponseBase>(stringResponse);
        }


        /* Get default create payload */
        internal static ChequeCreateWebRequest GetDefaultCreateChequePayload(string name, decimal amount)
        {
            return new ChequeCreateWebRequest
            {
                Name = name,
                Amount = amount,
            };
        }
    }
}
