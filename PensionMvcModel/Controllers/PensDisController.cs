using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PensionMvcModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PensionMvcModel.Controllers
{
    public class PensDisController : Controller
    {

        
        [HttpGet("api/PensionerDis")]
          public async Task<PensionerOutputResponse> PensionerDis(PensionerData pdsa)
        {
            PensionerOutputResponse pen = new PensionerOutputResponse() ;
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                StringContent content = new StringContent(JsonConvert.SerializeObject(pdsa), Encoding.UTF8, "application/json");
                var token = string.Empty;
                string endpoint = "https://localhost:44301/api/PensionerDis";
                client.BaseAddress = new Uri(endpoint);
                client.DefaultRequestHeaders.Clear();
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44301/api/PensionerDis");
                request.Content = content;

                var result = await client.SendAsync(request);
                if (result.IsSuccessStatusCode)
                {
                    var jsonString = result.Content.ReadAsStringAsync().Result;
                    pen = JsonConvert.DeserializeObject<PensionerOutputResponse>(jsonString);
                    return pen;
                }
                else {
                    return pen;
                }
            }
            else
            {
                return pen;
            }

        }

    }
}
