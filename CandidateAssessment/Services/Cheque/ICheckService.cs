using CandidateAssessment.Services.Cheque.ServiceMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateAssessment.Services.Cheque
{
    public interface ICheckService
    {
        ChequeCreateResponse Create(ChequeCreateRequest request);
    }
}
