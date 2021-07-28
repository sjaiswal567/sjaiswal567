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
using System.Text;
using System.Threading.Tasks;

namespace PensionMvcModel.Controllers
{
    public class LoginController : Controller
    {
        private IConfiguration _Configure { get; set; }
        string apiBaseUrl = "";

        public LoginController(IConfiguration configuration)
        {

            _Configure = configuration;

            apiBaseUrl = _Configure.GetValue<string>("WebAPIBaseUrl");
        }

        public class JWT
        {
            public string Token { get; set; }
        }
       
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(PensionUser pu)
        {
            HttpClient client = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(pu), Encoding.UTF8, "application/json");
            var token = string.Empty;
            string endpoint = "https://localhost:44301/api/Login";
            client.BaseAddress = new Uri(endpoint);
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            var result = await client.PostAsync(endpoint, content);
            
            if (result.IsSuccessStatusCode)
            {
                var stringJWT = result.Content.ReadAsStringAsync().Result;
                JWT jwt = JsonConvert.DeserializeObject<JWT>(stringJWT);
                HttpContext.Session.SetString("token", jwt.Token);

                ViewBag.Message = "User logged in successfully!";

                return RedirectToAction("PensionerInput", "Pensioner");
            }
            else
            {
                return View();
            }
          }
    }
}
