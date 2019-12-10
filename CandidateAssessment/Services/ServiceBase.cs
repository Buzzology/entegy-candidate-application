using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateAssessment.Services
{
    public class ServiceBase
    {
        protected static bool ValidateObject(dynamic model, ServiceResponseBase response)
        {
            var validationResults = new HashSet<ValidationResult>();
            if (Validator.TryValidateObject(model, new ValidationContext(model, null, null), validationResults, true))
            {
                return true;
            }
            else
            {
                // Add each validation error
                response.Success = false;
                foreach (ValidationResult error in validationResults)
                {
                    response.AddError(error.ErrorMessage.ToString());
                }

                return false;
            }
        }
    }
}
