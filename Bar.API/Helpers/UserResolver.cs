using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Bar.API.Helpers
{
    public class UserResolver
    {
        public static int GetUserId(ClaimsPrincipal principal)
        {
            var identity = principal.Identity as ClaimsIdentity;
            return int.Parse(identity.FindFirst("UserId").Value);
        }
    }
}
