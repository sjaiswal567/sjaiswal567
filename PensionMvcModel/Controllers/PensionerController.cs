using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PensionMvcModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PensionMvcModel.Controllers
{
    public class PensionerController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;
        private IConfiguration _Configure { get; set; }
        string apiBaseUrl = "";
        public PensionerController( IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;

            _Configure = configuration;

            apiBaseUrl = _Configure.GetValue<string>("WebAPIBaseUrl");
        }

        [HttpGet]
        public IActionResult PensionerInput()
        {
            PensionerInput f = new PensionerInput();
            return View(f);
        }
         [HttpPost]
        public async Task<IActionResult> PensionerInput(PensionerInput input)
        {
            if (ModelState.IsValid)
            {
                PensionDetails pen;

                HttpClient client = new HttpClient();
                StringContent content = new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");
                var token = string.Empty;
                string endpoint = "https://localhost:44301/api/PensionerInput";
                client.BaseAddress = new Uri(endpoint);
                client.DefaultRequestHeaders.Clear();
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44301/api/PensionerInput");
                var result = await client.PostAsync(endpoint, content);
                
                    if (result.IsSuccessStatusCode)
                    {
                        var jsonString = result.Content.ReadAsStringAsync().Result;
                        pen = JsonConvert.DeserializeObject<PensionDetails>(jsonString);

                        return RedirectToAction("Index", pen);
                    }
                    return Ok(result);
            }
            else
            {
                return Ok("fhsd");
            }

        }

        public IActionResult Index(PensionDetails pi)
        {
           return View(pi);
        }


    }
}
