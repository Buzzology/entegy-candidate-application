using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateAssessment.Services.Cheque.ServiceMessages
{
    public class ChequeCreateRequest : ServiceRequestBase
    {
        public ChequeCreateRequest(ApplicationDbContext db) : base(db) { }
    }
}
