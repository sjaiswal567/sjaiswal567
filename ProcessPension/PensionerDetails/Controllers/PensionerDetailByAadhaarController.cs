using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionDetail.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PensionerDetailByAadhaarController : ControllerBase
    {
        private readonly MyContext _con;
        public PensionerDetailByAadhaarController(MyContext con)
        {
            _con = con;
        }
        [HttpGet]
        public PensionDetail GetPensionerDetail()
        {
           PensionDetail pd;
           pd=_con.pensionDetails.Find(AadhaarNo);
           return pd;
        }
    }
}
