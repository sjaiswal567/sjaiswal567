using DocumentFormat.OpenXml.Office2010.Excel;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pension_disbursement_Microservice.Helper;
using Pension_disbursement_Microservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pension_disbursement_Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PensionDisursmentController : ControllerBase
    {
        
        PensionData _api = new PensionData();
        [HttpPost]
        public async Task<int>  GetPentionerAsync(Models.PensionerData obj )
        {
            PensionInfo p = new PensionInfo();
            HttpClient client = _api.Initial();
            bool status = false;
            HttpResponseMessage res = await client.GetAsync($"api/PensionerDetails/{obj.panNumber}");

            if (res.IsSuccessStatusCode)
            {
                status = true;
                var results = res.Content.ReadAsStringAsync().Result;
                p = JsonConvert.DeserializeObject<PensionInfo>(results);

                //var type = obj.typeofbank.FirstOrDefault().Key;
                var type1 = p.typeofbank.ToUpper();
                //string type = obj.typeofbank.FindFirstKeyByValue(Value);
                string typeofpention = p.SelforFamilypension;
                int lastsalary = p.SalaryEarned;
                int penstion = 0; int allownaces = p.Allowances;
                if (type1 == "PUBLIC" && status == true)
                {
                    if (typeofpention == "SELF")
                    { penstion = lastsalary / 100 * 80 + allownaces + 500; }
                    else
                    { penstion = lastsalary / 100 * 50 + allownaces + 500; Console.WriteLine("pension=" + penstion); }

                }
                else if (type1 == "PRIVATE" && status == true)
                {
                    if (typeofpention.ToUpper() == "SELF")
                    { penstion = ((lastsalary / 100) * 80) + allownaces + 550; Console.WriteLine("pension=" + penstion); }
                    else
                    { penstion = lastsalary / 100 * 50 + allownaces + 550; Console.WriteLine("pension=" + penstion); }

                }
                if (penstion == obj.amount)
                {
                    return 10;
                }
                else
                {
                    return 21;
                }
            
            }
            else { return 21; }
        }
    }
}
