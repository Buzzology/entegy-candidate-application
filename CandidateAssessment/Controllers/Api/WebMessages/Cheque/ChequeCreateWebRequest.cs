namespace CandidateAssessment.Controllers.Api.WebMessages.Cheque
{
    public class ChequeCreateWebRequest : ApiMessageRequestBase
    {
        public string Name { get; set; }

        public decimal Amount { get; set; }
    }
}
