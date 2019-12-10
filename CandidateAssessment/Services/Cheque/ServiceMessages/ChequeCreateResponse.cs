namespace CandidateAssessment.Services.Cheque.ServiceMessages
{
    public class ChequeCreateResponse : ServiceResponseBase
    {
        public ChequeCreateResponse(ServiceRequestBase request) : base(request) { }

        public Models.Cheque Cheque { get; set; }

        public string AmountInWords { get; set; }
    }
}
