using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

using BlazorApp.Shared.User;
using BlazorApp.Shared.Extensions;

namespace Api.Auth
{
    public class UserFunctions
    {
        [FunctionName("UserInfo")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            var userInfo = req.GetUserInfo();
            return new OkObjectResult(userInfo);
        }
    }
}