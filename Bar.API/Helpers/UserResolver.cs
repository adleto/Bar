using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Bar.API.Helpers
{
    public class UserResolver
    {
        public static string GetUserId(ClaimsPrincipal principal)
        {
            var identity = principal.Identity as ClaimsIdentity;
            return identity.FindFirst("UserId").Value;
        }
    }
}
