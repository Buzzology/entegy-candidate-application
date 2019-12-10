using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateAssessment.Services
{
    public class ServiceResponseBase : ServiceMessageBase
    {
        public ServiceResponseBase(ServiceRequestBase request) : base(request.Db)
        {
            CurrentUserId = request.CurrentUserId;
        }
    }
}
