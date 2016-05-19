using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web;
using System.Web.Http;
using TRex.Metadata;

namespace CustomerOnPrem.Controllers
{
    public class PatternsController : ApiController
    {
        #region Polling
        private static IDictionary<Guid, Thread> threads = new Dictionary<Guid, Thread>();
        [Metadata("Long Process - Poll")]
        [HttpPost, Route("api/pattern/poll/process")]
        public HttpResponseMessage PollProcess(string customerId)
        {
            Thread t = new Thread(() => {
                
                //Do a long task

                Thread.Sleep(18000);
            });
            
            Guid guid = Guid.NewGuid();
            threads.Add(guid, t);
            t.Start();
            return GenerateAsyncResponse(HttpStatusCode.Accepted, guid.ToString(), "15");
        }

        [Metadata("Check Status - Poll")]
        [HttpGet, Route("api/pattern/poll/checkStatus")]
        public HttpResponseMessage CheckStatus(Guid jobId)
        {
            if(threads[jobId].IsAlive == false)
            {
                return Request.CreateResponse();
            }
            return GenerateAsyncResponse(HttpStatusCode.Accepted, jobId.ToString(), "15");
        }

        
        private HttpResponseMessage GenerateAsyncResponse(HttpStatusCode code, string jobId, string retryAfter)
        {
            HttpResponseMessage responseMessage = Request.CreateResponse(code); //Return a 200 to tell it to fire.
            responseMessage.Headers.Add("location", String.Format("{0}://{1}/api/pattern/poll/checkStatus?jobId={2}", Request.RequestUri.Scheme, Request.RequestUri.Host, HttpUtility.UrlEncode(jobId)));  //Where the engine will poll to check status
            responseMessage.Headers.Add("retry-after", retryAfter);   //How many seconds it should wait.  If multiple files are available you can return a 0 here and the engine will immediately come back and grab other triggers.
            return responseMessage;
        }
        #endregion

        #region Webhook
        [Metadata("Long Process - Webhook")]
        [HttpPost, Route("api/pattern/webhook")]
        public HttpResponseMessage WebhookProcess([FromBody] WebhookBody requestBody)
        {
            Thread t = new Thread(async () =>
            {

                //Do a long task

                Thread.Sleep(18000);
                using (var client = new HttpClient())
                {
                    await client.PostAsync(requestBody.callbackUrl, null);
                }
            });
            t.Start();
            return Request.CreateResponse(HttpStatusCode.Accepted);
        }

        public class WebhookBody
        {
            public string customerId { get; set; }
            public string callbackUrl { get; set; }
        }
        #endregion
    }
}
