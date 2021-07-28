using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthPen.Models
{
    public class PensionUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
