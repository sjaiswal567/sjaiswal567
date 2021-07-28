using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProcessPension
{
    public class PensionDetail
    {
        public string Name { get; set; }
        public string Dateofbirth { get; set; }

        [Key]
        public string PAN { get; set; }

        public int SalaryEarned { get; set; }

        public int Allowances { get; set; }

        public string SelforFamilypension { get; set; }

        public string bank_name { get; set; }
        public string accountNumber { get; set; }

        public string typeofbank { get; set; }
    }
}
