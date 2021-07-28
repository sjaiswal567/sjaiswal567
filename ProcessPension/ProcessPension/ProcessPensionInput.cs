using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPension
{
    public class ProcessPensionInput
    {
        public String panNumber { get; set; }
        public int amount { get; set; }
       public  IDictionary<string, int> typeofbank { get;set;}

    }
}
