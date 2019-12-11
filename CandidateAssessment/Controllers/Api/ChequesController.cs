using CandidateAssessment.Controllers.Api.WebMessages.Cheque;
using CandidateAssessment.Models;
using CandidateAssessment.Services.Cheque;
using CandidateAssessment.Services.Cheque.ServiceMessages;
using CandidateAssessment.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateAssessment.Controllers.Api
{
    [Route("[controller]")]
    public class ChequesController : CustomBaseApiController
    {
        private readonly IChequeService _chequeService;

        public ChequesController(ApplicationDbContext db, IChequeService chequeService) : base(db)
        {
            _chequeService = chequeService;
        }


        [HttpPost]
        [Produces("application/json")]
        [Route("create")]
        public async Task<IActionResult> Create(ChequeCreateWebRequest request)
        {
            var serviceResp = await _chequeService.Create(
                new ChequeCreateRequest(Db) {
                    Name = request.Name,
                    Amount = request.Amount,
                });

            if (serviceResp.Success)
            {
                Db.SaveChanges();
            }

            WebResponse.Messages.AddRange(serviceResp.Messages);
            WebResponse.Data = new { serviceResp.Cheque, serviceResp.AmountInWords };
            WebResponse.Success = !WebResponse.Messages.Any(x => x.Type == Message.MessageType.Error);

            return Ok(WebResponse);
        }


        [HttpPost]
        [Produces("application/json")]
        [Route("amountinwords")]
        public async Task<IActionResult> AmountInWords(ChequeAmountToWordsWebRequest request)
        {
            var serviceResp = await _chequeService.AmountInWords(
                new ChequeAmountInWordsRequest(Db) {
                    Amount = request.Amount,
                });

            if (serviceResp.Success)
            {
                Db.SaveChanges();
            }

            WebResponse.Messages.AddRange(serviceResp.Messages);
            WebResponse.Data = new { serviceResp.AmountInWords };
            WebResponse.Success = !WebResponse.Messages.Any(x => x.Type == Message.MessageType.Error);

            return Ok(WebResponse);
        }
    }
}
