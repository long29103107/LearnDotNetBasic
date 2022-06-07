using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;

namespace TodoList.Helpers
{
    public class AuthenticationHelper
    {

        public async Task<Guid> GetUserId()
        {
            var httpContext = new HttpContextAccessor().HttpContext;
            var accessToken = await httpContext.GetTokenAsync("access_token");
            var handler = new JwtSecurityTokenHandler();
            var tokenDecode = handler.ReadJwtToken(accessToken);
            return Guid.Parse(tokenDecode.Claims.First(claim => claim.Type == "Id").Value);
        }
    }
}
