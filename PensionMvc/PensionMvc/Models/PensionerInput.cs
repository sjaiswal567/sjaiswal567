using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PensionMvc.Models
{
    public class PensionerInput
    {
        public string NamePensioner { get; set; }
        public DateTime DOB { get; set; }
        [Key]
       public String PAN { get; set; }
        public String AadhaarNo { get; set; }
        public string PensionSelected { get; set; }

    }
}
