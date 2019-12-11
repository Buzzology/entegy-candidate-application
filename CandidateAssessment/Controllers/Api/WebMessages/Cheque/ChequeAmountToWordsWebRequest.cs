namespace CandidateAssessment.Controllers.Api.WebMessages.Cheque
{
    public class ChequeAmountToWordsWebRequest : ApiMessageRequestBase
    {
        public decimal Amount { get; set; }
    }
}