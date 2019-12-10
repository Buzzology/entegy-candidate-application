using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
