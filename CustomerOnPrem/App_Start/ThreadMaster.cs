using CustomerOnPrem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

[assembly: PreApplicationStartMethod(typeof(ProcessFactory), "Register")]
namespace CustomerOnPrem
{
    public static class ProcessFactory
    {
        private static TaskFactory factory;
        public static void Register()
        {
            
        }

        public static void AddJob(string customerId)
        {
            
        }

        public static void CheckStatus(Guid id)
        {

        }
    }
}