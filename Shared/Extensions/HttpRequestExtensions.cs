using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using BlazorApp.Shared.User;

namespace BlazorApp.Shared.Extensions
{
    public static class HttpRequestExtensions
    {
        private class ClientPrincipal
        {
            private ClientPrincipal(string identityProvider, string userId, string userDetails)
            {
                this.IdentityProvider = identityProvider;
                this.UserId = userId;
                this.UserDetails = userDetails;

            }
            public string IdentityProvider { get; set; }
            public string UserId { get; set; }
            public string UserDetails { get; set; }
            public IEnumerable<string> UserRoles { get; set; }
        }

        public static ClaimsPrincipal ParseClaims(this HttpRequest req)
        {
            var header = req.Headers["x-ms-client-principal"];
            var data = header[0];
            var decoded = System.Convert.FromBase64String(data);
            var json = System.Text.ASCIIEncoding.ASCII.GetString(decoded);
            var principal = JsonSerializer.Deserialize<ClientPrincipal>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            principal.UserRoles = principal.UserRoles.Except(new string[] { "anonymous" }, StringComparer.CurrentCultureIgnoreCase);

            if (!principal.UserRoles.Any())
            {
                return new ClaimsPrincipal();
            }

            var identity = new ClaimsIdentity(principal.IdentityProvider);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, principal.UserId));
            identity.AddClaim(new Claim(ClaimTypes.Name, principal.UserDetails));
            identity.AddClaims(principal.UserRoles.Select(r => new Claim(ClaimTypes.Role, r)));
            return new ClaimsPrincipal(identity);
        }

        public static UserInfo GetUserInfo(this HttpRequest req)
        {
            var claims = req.ParseClaims();

            var userInfo = new UserInfo 
            {
                Name = claims?.Claims?.FirstOrDefault(p => p.Type == ClaimTypes.Name)?.Value
            };

            return userInfo;
        }
    }
}