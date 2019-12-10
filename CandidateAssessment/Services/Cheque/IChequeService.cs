using CandidateAssessment.Services.Cheque.ServiceMessages;
using System.Threading.Tasks;

namespace CandidateAssessment.Services.Cheque
{
    public interface IChequeService
    {
        Task<ChequeCreateResponse> Create(ChequeCreateRequest request);
    }
}
