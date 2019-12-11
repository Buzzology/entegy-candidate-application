using CandidateAssessment;
using CandidateAssessment.Controllers.Api.WebMessages;
using CandidateAssessment.Controllers.Api.WebMessages.Cheque;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CandidateAssessmentTestProject.Tests.Cheque
{
    public class AmountInWordsTest : IntegrationTestBase
    {
        public AmountInWordsTest(CustomWebApplicationFactory<Startup> factory) : base(factory) { }

        [Fact]
        public async Task VerifyValidAmountsInWords()
        {
            await RunTest(async () => {

                Task[] wordTests = {

                    // Additional edge case tests
                    VerifyAmountResponseEquals(Client, 25, "TWENTY-FIVE DOLLARS AND ZERO CENTS"),                    
                    VerifyAmountResponseEquals(Client, 2234.56M, "TWO THOUSAND TWO HUNDRED THIRTY-FOUR DOLLARS AND FIFTY-SIX CENTS"),
                    VerifyAmountResponseEquals(Client, 2234.01M, "TWO THOUSAND TWO HUNDRED THIRTY-FOUR DOLLARS AND ONE CENT"),
                    VerifyAmountResponseEquals(Client, 9984.00M, "NINE THOUSAND NINE HUNDRED EIGHTY-FOUR DOLLARS AND ZERO CENTS"),
                    VerifyAmountResponseEquals(Client, 0.01M, "ONE CENT ONLY"),
                    VerifyAmountResponseEquals(Client, 0.02M, "TWO CENTS ONLY"),
                    VerifyAmountResponseEquals(Client, 1.10M, "ONE DOLLAR AND TEN CENTS"),
                    VerifyAmountResponseEquals(Client, 10984.00M, "TEN THOUSAND NINE HUNDRED EIGHTY-FOUR DOLLARS AND ZERO CENTS"),
                    VerifyAmountResponseEquals(Client, 99004.01M, "NINETY-NINE THOUSAND FOUR DOLLARS AND ONE CENT"),
                    VerifyAmountResponseEquals(Client, 14000.01M, "FOURTEEN THOUSAND DOLLARS AND ONE CENT"),
                    VerifyAmountResponseEquals(Client, 99004.01M, "NINETY-NINE THOUSAND FOUR DOLLARS AND ONE CENT"),
                    VerifyAmountResponseEquals(Client, 99044.01M, "NINETY-NINE THOUSAND FORTY-FOUR DOLLARS AND ONE CENT"),
                    VerifyAmountResponseEquals(Client, 100004.01M, "ONE HUNDRED THOUSAND FOUR DOLLARS AND ONE CENT"),
                    VerifyAmountResponseEquals(Client, 290004.40M, "TWO HUNDRED NINETY THOUSAND FOUR DOLLARS AND FORTY CENTS"),
                    VerifyAmountResponseEquals(Client, 291124.30M, "TWO HUNDRED NINETY-ONE THOUSAND ONE HUNDRED TWENTY-FOUR DOLLARS AND THIRTY CENTS"),
                    VerifyAmountResponseEquals(Client, 45290004.01M, "FORTY-FIVE MILLION TWO HUNDRED NINETY THOUSAND FOUR DOLLARS AND ONE CENT"),
                    VerifyAmountResponseEquals(Client, 945290004.01M, "NINE HUNDRED FORTY-FIVE MILLION TWO HUNDRED NINETY THOUSAND FOUR DOLLARS AND ONE CENT"),
                    VerifyAmountResponseEquals(Client, 945290004.99M, "NINE HUNDRED FORTY-FIVE MILLION TWO HUNDRED NINETY THOUSAND FOUR DOLLARS AND NINETY-NINE CENTS"),
                    VerifyAmountResponseEquals(Client, 100000000.00M, "ONE HUNDRED MILLION DOLLARS AND ZERO CENTS"),

                    // Entegy test cases
                    VerifyAmountResponseEquals(Client, 1234.56M, "ONE THOUSAND TWO HUNDRED THIRTY-FOUR DOLLARS AND FIFTY-SIX CENTS"),
                    VerifyAmountResponseEquals(Client, 0.22M, "TWENTY-TWO CENTS ONLY"),
                    VerifyAmountResponseEquals(Client, 102.03M, "ONE HUNDRED TWO DOLLARS AND THREE CENTS"),
                    VerifyAmountResponseEquals(Client, 1.021M, "ONE DOLLAR AND TWO CENTS"),
                    VerifyAmountResponseEquals(Client, 1.23987M, "ONE DOLLAR AND TWENTY-THREE CENTS"),
                };

                // Ensure that valid amounts return an expected result
                await Task.WhenAll(wordTests);
                
                return true;
            });            
        }


        /* Generic utility to verify that amount to words generics the correct sentence */
        internal static async Task<bool> VerifyAmountResponseEquals(HttpClient client, decimal amount, string expectedOutput)
        {
            var resp = await ConvertAmountToWords(client, amount);

            Assert.NotNull(resp);
            Assert.True(resp.Success);
            Assert.NotNull(resp.Data);
            Assert.NotNull(resp.Data.amountInWords);
            Assert.Equal(resp.Data.amountInWords.Value, expectedOutput);

            return true;
        }


        /* Generic get amount in words utility */
        internal static async Task<ApiMessageResponseBase> ConvertAmountToWords(HttpClient client, decimal amount)
        {
            // Invoke create action on controller
            var httpResponse = await client.PostAsync($"/cheques/amountInWords", new StringContentWrapper(GetDefaultAmountToWordsPayload(amount)));
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();

            if (!httpResponse.IsSuccessStatusCode) throw new HttpRequestException($"Http error occurred: ${httpResponse.ToString()}");

            // Check http response payload
            return JsonConvert.DeserializeObject<ApiMessageResponseBase>(stringResponse);
        }


        /* Get default create payload */
        internal static ChequeAmountToWordsWebRequest GetDefaultAmountToWordsPayload(decimal amount)
        {
            return new ChequeAmountToWordsWebRequest {
                Amount = amount,
            };
        }
    }
}
