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
                    VerifyAmountResponseEquals(Client, 14, "FOURTEEN DOLLARS AND ZERO CENTS"),
                    VerifyAmountResponseEquals(Client, 19.14M, "NINETEEN DOLLARS AND FOURTEEN CENTS"),
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
                    VerifyAmountResponseEquals(Client, 49999999.5M, "FORTY-NINE MILLION NINE HUNDRED NINETY-NINE THOUSAND NINE HUNDRED NINETY-NINE DOLLARS AND FIFTY CENTS"),

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


        [Fact]
        public async Task VerifyInputOfZeroReturnsError()
        {
            await RunTest(async () => {

                var amount = 0M;
                var response = await ConvertAmountToWords(Client, amount);

                Assert.NotNull(response);
                Assert.False(response.Success);
                Assert.True(response.Messages.Count == 1);
                Assert.Equal("The field Amount must be between 0.01 and 999999999.", response.Messages[0].Text);

                return true;
            });
        }


        [Fact]
        public async Task VerifyInputOf1BillionReturnsError()
        {
            await RunTest(async () => {

                var amount = -9999999M;
                var response = await ConvertAmountToWords(Client, amount);

                Assert.NotNull(response);
                Assert.False(response.Success);
                Assert.True(response.Messages.Count == 1);
                Assert.Equal("The field Amount must be between 0.01 and 999999999.", response.Messages[0].Text);

                return true;
            });
        }


        [Fact]
        public async Task VerifyInputInvalidReturnsError()
        {
            await RunTest(async () => {

                var amount = "INVALID_AMOUNT";

                // Invoke create action on controller
                var httpResponse = await Client.GetAsync($"/cheques/amountInWords?amount={amount}");
                var stringResponse = await httpResponse.Content.ReadAsStringAsync();

                // Check http response payload
                var resp = JsonConvert.DeserializeObject<ApiMessageResponseBase>(stringResponse);

                Assert.NotNull(resp);
                Assert.False(resp.Success);
                Assert.True(resp.Messages.Count == 1);
                Assert.Equal($"Please ensure that amount is a valid decimal: ${amount}", resp.Messages[0].Text);

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
            var httpResponse = await client.GetAsync($"/cheques/amountInWords?amount={amount}");
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();

            // Check http response payload
            return JsonConvert.DeserializeObject<ApiMessageResponseBase>(stringResponse);
        }


        /* Get default create payload */
        internal static ChequeAmountToWordsWebRequest GetDefaultAmountToWordsPayload(decimal amount)
        {
            return new ChequeAmountToWordsWebRequest {
                Amount = amount.ToString(),
            };
        }
    }
}
