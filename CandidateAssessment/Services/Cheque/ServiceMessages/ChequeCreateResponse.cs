using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateAssessment.Services.Cheque.ServiceMessages
{
    public class ChequeCreateResponse : ServiceResponseBase
    {
        public ChequeCreateResponse(ServiceRequestBase request) : base(request) { }

        public List<Models.Cheque> Cheque { get; set; }
    }
}
