using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
namespace ProcessPension.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessPensionController : ControllerBase
    {
        [HttpPost]
        public async Task<PensionerOutputResponse> PostProcessPension(PenstionDis pp)
        {
            ProcessPensionInput ppi = new ProcessPensionInput();
            ppi.panNumber = pp.panNumber;
            ppi.amount = pp.amount;
            IDictionary<string, int> BankList = new Dictionary<string, int>();
            BankList.Add("Public", 500);
            BankList.Add("Private", 550);
            ppi.typeofbank = BankList;
            IDictionary<int, string> ProcessCode = new Dictionary<int, string>();
            ProcessCode.Add(10, "Pension disbursement Success");
            ProcessCode.Add(21, "Pension amount calculated is wrong, Please redo the calculation");
           
                PensionerOutputResponse pres = new PensionerOutputResponse();
                using (var client = new HttpClient())
                {
                client.BaseAddress = new Uri("https://localhost:44354/");
                var rfesponse = await client.PostAsJsonAsync("api/PensionDisursment", ppi);
                // Verification  
                if (rfesponse.IsSuccessStatusCode)
                {
                    var results = rfesponse.Content.ReadAsStringAsync().Result;
                    var p = JsonConvert.DeserializeObject<int>(results);
                    if (p == 10)
                    {
                        pres.responses = "Pension disbursement Success";
                        return pres;
                    }
                    else 
                    {
                        pres.responses="Pension amount calculated is wrong, Please redo the calculation";
                        return pres;
                    }
                    // Reading Response.  
                }
                pres.responses = "Pension amount calculated is wrong, Please redo the calculation";
                return pres;
            }

        }
        [HttpGet("{id}")]
        public async Task<PensionDetails> GetProcessPension(string id)
        {
            PensionDetail pd;
            PensionDetails ps = new PensionDetails();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44335/api/");
                //HTTP GET
                var responseTask = await client.GetAsync($"PensionerDetails/{id}");

                 var result = responseTask;
                if (result.IsSuccessStatusCode)
                {
                    var results = result.Content.ReadAsStringAsync().Result;
                    pd =JsonConvert.DeserializeObject<PensionDetail>(results);
                    ps.NamePensioner = pd.Name;
                    ps.PAN = pd.PAN;
                    ps.DOB = Convert.ToDateTime(pd.Dateofbirth);
                    ps.PensionSelected = pd.SelforFamilypension.ToUpper();
                    //pd.SelforFamilypension;
                    if (pd.SelforFamilypension.ToUpper() == "SELF")
                    {
                        if (pd.typeofbank.ToUpper() == "PRIVATE")
                        {
                            ps.PensionAmt = (pd.SalaryEarned * 4) / 5 + pd.Allowances+550;
                        }
                        else
                            {
                            ps.PensionAmt = (pd.SalaryEarned * 4) / 5 + pd.Allowances+500;
                        }
                    }
                    else
                    {

                        if (pd.typeofbank.ToUpper() == "PRIVATE")
                        {
                            ps.PensionAmt = (pd.SalaryEarned * 1) / 2 + pd.Allowances+550;
                        }
                        else 
                        {
                            ps.PensionAmt = (pd.SalaryEarned * 1) / 2 + pd.Allowances+500;
                        }
                    }
                    return ps;
                }
                else //web api sent error response 
                {
                    
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    return ps;
                }
            }

           
            
        }
    }
}
