using CandidateAssessment.Models;
using System;
using System.ComponentModel.DataAnnotations;
using static CandidateAssessment.Utilities.Constants;

namespace CandidateAssessment.Services.Cheque.ServiceMessages
{
    public class ChequeAmountInWordsRequest : ServiceRequestBase
    {
        public ChequeAmountInWordsRequest(ApplicationDbContext db) : base(db) { }

        [Required]
        [Range(ChequeConfiguration.MinimumChequeAmount, ChequeConfiguration.MaximumChequeAmount)]
        public decimal Amount { get; set; }
    }
}