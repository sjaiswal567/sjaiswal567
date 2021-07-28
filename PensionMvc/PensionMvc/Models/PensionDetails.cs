using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PensionMvc.Models
{
    public class PensionDetails
    {
        public string NamePensioner { get; set; }
        public DateTime DOB { get; set; }
       [Key]
        public string PAN { get; set; }
        public string PensionSelected { get; set; }
        public int PensionAmt { get; set; }

    }
}
