using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthPen.Models;

namespace AuthPen.Services
{
   public interface ITokenGeneratorService
    {
        string GenerateJSONWebToken(PensionUser user);
    }
}
