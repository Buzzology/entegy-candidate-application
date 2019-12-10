using CandidateAssessment.Models;

namespace CandidateAssessment.Services.Cheque.ServiceMessages
{
    public class ChequeCreateRequest : ServiceRequestBase
    {
        public ChequeCreateRequest(ApplicationDbContext db) : base(db) { }

        public string Name { get; set; }

        public decimal Amount { get; set; }
    }
}
