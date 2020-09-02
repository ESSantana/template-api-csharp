using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.Authorization;

namespace Sample.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationContoller : ControllerBase
    {
        private readonly ILogger<ExampleController> _logger;
        public AuthenticationContoller(ILogger<ExampleController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<User> Authenticate()
        {
            _logger.LogDebug("Authenticate");

            var token = TokenHandler.GenerateToken();
            var userAuthentication = new User { Token = token };

            _logger.LogDebug("Token value: " + token);

            return userAuthentication;
        }

        [HttpGet]
        [Route("validateToken")]
        [Authorize]
        public ActionResult<string> IsTokenValide() => "Valid Token!";
    }
}
