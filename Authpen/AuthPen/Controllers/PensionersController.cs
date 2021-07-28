using AuthPen.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AuthPen.Controllers
{
    [ApiController]
    public class PesnsionersController : ControllerBase
    {
        [HttpPost("api/PensionerInput")]
        public async Task<PensionDetails> PensionerInput([FromBody] PensionerInput pds)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44305/");
                HttpResponseMessage result = await client.GetAsync($"api/ProcessPension/{pds.PAN}");
                if (result.IsSuccessStatusCode)
                {
                    var results = result.Content.ReadAsStringAsync().Result;
                    var pd = JsonConvert.DeserializeObject<PensionDetails>(results);
                    return pd;
                }
                else //web api sent error response 
                {
                    return new PensionDetails();
                }

            }

        }





    }
}