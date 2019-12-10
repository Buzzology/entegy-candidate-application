using CandidateAssessment.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace CandidateAssessmentTestProject
{
    public static class Utilities
    {
        public static void InitializeDbForTests(ApplicationDbContext db)
        {
            // TODO: implement any seed data
        }
    }

    public class StringContentWrapper : StringContent
    {
        public StringContentWrapper(object objToConvert) : base(JsonConvert.SerializeObject(objToConvert), Encoding.UTF8, "application/json")
        {
        }
    }
}
