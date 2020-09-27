using System.Collections.Generic;

namespace BlazorApp.Shared.User
{
    public class ClientPrincipal
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
}