using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CandidateAssessment.Utilities.Constants;

namespace CandidateAssessment.Models
{
    public class Cheque
    {
        [Key]
        public int ChequeId { get; set; }

        [Required]
        [Range(ChequeConfiguration.MinimumChequeAmount, ChequeConfiguration.MaximumChequeAmount)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime Created { get; set; }
    }
}
