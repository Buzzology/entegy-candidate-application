using CandidateAssessment.Models;

namespace CandidateAssessment.Services
{
    public class ServiceRequestBase : ServiceMessageBase
    {
        public ServiceRequestBase(ApplicationDbContext db) : base(db)
        {
        }

        public bool IsInternalCall { get; set; }
    }
}
