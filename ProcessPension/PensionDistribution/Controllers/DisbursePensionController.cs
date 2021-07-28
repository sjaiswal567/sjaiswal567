using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionDistribution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisbursePensionController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetPensionDist(ProcessPensionInput ppi)
        {

            int val;
            if (BankType == "Public")
            {
                val = 500;
            }
            else
            {
                val = 550;
            }
            ppi.PensionAmt

            MICROSERVICE TO GET BANK DETIL OF PENSIONER bank detail(pensionDetails)
                calculate correct amout if correct otrnot wise code
            ProcessPensionResponse
        }
    }
}
