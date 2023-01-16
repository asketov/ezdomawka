using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Common.Generics
{
    public static class ClaimGeneric
    {
        public static T? GetClaimValueOrDefault<T>(this IEnumerable<Claim> claims, string claim)
        {
            var cl = claims.FirstOrDefault(x => x.Type == claim);
            return cl?.Value != null ? cl.Value.ConvertFromString<T>() : default;

        }
    }
}
