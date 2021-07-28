using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pension_disbursement_Microservice.Helper
{
    public class PensionData
    {
        public HttpClient Initial()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44335/");
            return client;
        }
    }
}
