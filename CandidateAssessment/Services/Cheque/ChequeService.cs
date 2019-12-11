using CandidateAssessment.Services.Cheque.ServiceMessages;
using CandidateAssessment.Utilities;
using System;
using System.Threading.Tasks;
using static CandidateAssessment.Utilities.Constants;

namespace CandidateAssessment.Services.Cheque
{
    public class ChequeService : ServiceBase, IChequeService
    {
        /* Creates a cheque */
        public async Task<ChequeCreateResponse> Create(ChequeCreateRequest request)
        {
            var resp = new ChequeCreateResponse(request);

            #region Validation

            if (!ValidateObject(request, resp)) return resp;

            var cheque = new Models.Cheque { Name = request.Name, Amount = Helpers.RoundDown(request.Amount, 2), Created = DateTime.UtcNow };
            
            if (!ValidateObject(cheque, resp)) return resp;

            #endregion

            // Retrieve amount in words
            var amountInWordsResponse = await AmountInWords(new ChequeAmountInWordsRequest(resp.Db) {
                Amount = cheque.Amount,
            });

            if(amountInWordsResponse.AtLeastError())
            {
                resp.AddMessages(amountInWordsResponse.Messages);
                return resp;
            }

            resp.Db.Cheques.Add(cheque);

            // TODO: ensure that when cents only is provided that the word only is appended as a suffix
            // TODO: ensure that pluralisations are used correctly e.g. one dollar, two dollars

            resp.AmountInWords = amountInWordsResponse.AmountInWords.ToUpper();
            resp.Cheque = cheque;
            resp.Success = true;
            
            return resp;
        }

        /* Retrieves a check amount in words */
        public async Task<ChequeAmountInWordsResponse> AmountInWords(ChequeAmountInWordsRequest request)
        {
            var resp = new ChequeAmountInWordsResponse(request);

            #region Validation

            if (!ValidateObject(request, resp)) return resp;

            #endregion

            try
            {
                // Construct cents amount
                string centsInWords;
                var cents = Helpers.GetDecimalPortionAsInt(request.Amount);
                if(cents == 0)
                {
                    centsInWords = $"{NumberWords.ZeroToNineteen[0]} {MoneyWords.Cents}";
                }
                else if(cents == 1)
                {
                    centsInWords = $"{Helpers.NumberToWords(cents)} {MoneyWords.Cent}";
                }
                else
                {
                    centsInWords = $"{Helpers.NumberToWords(cents)} {MoneyWords.Cents}";
                }

                // Construct dollar amount
                var dollars = (int)request.Amount;
                if (dollars == 0)
                {
                    resp.AmountInWords = $"{centsInWords} {MoneyWords.Only}";
                }
                else if (dollars == 1)
                {
                    resp.AmountInWords = $"{Helpers.NumberToWords(dollars)} {MoneyWords.Dollar} AND {centsInWords}";
                }
                else
                {
                    resp.AmountInWords = $"{Helpers.NumberToWords(dollars)} {MoneyWords.Dollars} AND {centsInWords}";
                }
            }
            catch(Exception e)
            {
                resp.AddError("Unable to convert cheque amount to words.");
                Helpers.LogError(e, "Failed to parse cheque amount to words");
                return resp;
            }

            resp.AmountInWords = resp.AmountInWords.Trim().ToUpper();
            resp.Success = true;
            return resp;
        }

    }
}
