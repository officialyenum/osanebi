using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Osanebi.Utility.IUtility
{
    public interface ITokenHandler
    {
        string GenerateJwtToken(List<Claim> claims);
    }
}
