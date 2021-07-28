using AuthPen.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AuthPen.Controllers
{
    [ApiController]
    public class PensionerDisController : ControllerBase
    {
        [HttpPost]

        [Route("api/PensionerDis")]

        public async Task<PensionerOutputResponse> PensionerDis([FromBody] PensionerData hj)
        {
            using (var client = new HttpClient())
            {
                PensionerOutputResponse df = new PensionerOutputResponse();
                df.responses = "Not Responded";
                client.BaseAddress = new Uri("https://localhost:44305/");
                //HTTP POST
                StringContent content = new StringContent(JsonConvert.SerializeObject(hj), Encoding.UTF8, "application/json");
                var result = await client.PostAsync($"api/ProcessPension", content);
                if (result.IsSuccessStatusCode)
                {
                    var results = result.Content.ReadAsStringAsync().Result;
                    var pd = JsonConvert.DeserializeObject<PensionerOutputResponse>(results);
                    return pd;
                }
                else //web api sent error response 
                {
                    return df;
                }

            }

        }
    }
}
