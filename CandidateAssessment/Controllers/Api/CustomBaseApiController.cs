using CandidateAssessment.Controllers.Api.WebMessages;
using CandidateAssessment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CandidateAssessment.Controllers.Api
{
    [ApiController]
    [CustomInitialisationFilter]
    public class CustomBaseApiController : Controller
    {
        public readonly ApplicationDbContext Db;
        public ApiMessageResponseBase WebResponse { get; set; }

        public CustomBaseApiController(ApplicationDbContext db)
        {
            Db = db;
        }
    }

    public class CustomInitialisationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context == null || context.Controller == null) return;

            var controller = (CustomBaseApiController)context.Controller;

            controller.WebResponse = new ApiMessageResponseBase();

            // TODO: (cjo) inject user etc
        }
    }
}
