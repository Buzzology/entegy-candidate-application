using CandidateAssessment.Services.Cheque.ServiceMessages;
using CandidateAssessment.Utilities;
using System;
using System.Threading.Tasks;

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

            resp.AmountInWords = amountInWordsResponse.AmountInWords;
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
                var cents = Helpers.GetDecimalPortionAsInt(request.Amount);
                var centsSuffix = cents == 0 ? Constants.MoneyWords.Only : cents == 1 ? Constants.MoneyWords.Cent : Constants.MoneyWords.Cents;
                var dollars = (int)request.Amount;
                var dollarSuffix = dollars == 1 ? Constants.MoneyWords.Dollar : Constants.MoneyWords.Dollars;

                resp.AmountInWords = $"{Helpers.NumberToWords(dollars)} {dollarSuffix} {Helpers.NumberToWords(cents)} {centsSuffix}";
            }
            catch(Exception e)
            {
                resp.AddError("Unable to convert cheque amount to words.");
                Helpers.LogError(e, "Failed to parse cheque amount to words");
                return resp;
            }
            

            // TODO: ensure that when cents only is provided that the word only is appended as a suffix
            // TODO: ensure that pluralisations are used correctly e.g. one dollar, two dollars

            return resp;
        }

    }
}
