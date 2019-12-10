using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CandidateAssessment.Utilities
{
    public class StartupHelper
    {
        /* Inject configuration on startup */
        public static void AddConfiguration(IServiceCollection services, IConfiguration configuration)
        {

        }


        /* Inject services on startup */
        public static void AddServices(IServiceCollection services)
        {
            services.AddSingleton(typeof(IChequeService), typeof(ChequeService));
        }
    }
}
