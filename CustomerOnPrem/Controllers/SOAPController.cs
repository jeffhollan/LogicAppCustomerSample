using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using CustomerOnPrem.CustomerService;
using TRex.Metadata;

namespace CustomerOnPrem.Controllers
{
    public class SOAPController : ApiController
    {
        private CustomerServiceClient client = new CustomerServiceClient();

        [Metadata("SOAP - Add Customer")]
        [HttpPost, Route("api/soap/add")]
        public string AddCustomer(string value)
        {
            return value;
        }

        [Metadata("SOAP - Get Data Using Data Contract")]
        [HttpPost, Route("api/soap/DataContract")]
        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            return client.GetDataUsingDataContract(composite);
        }

    }
}
