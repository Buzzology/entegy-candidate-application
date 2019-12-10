namespace CandidateAssessment.Services.Cheque.ServiceMessages
{
    public class ChequeAmountInWordsResponse : ServiceResponseBase
    {
        public ChequeAmountInWordsResponse(ServiceRequestBase request) : base(request) { }

        public decimal Amount { get; set; }

        public string AmountInWords { get; set; }
    }
}