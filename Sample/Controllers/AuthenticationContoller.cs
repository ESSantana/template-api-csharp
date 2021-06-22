using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.Authorization;

namespace Sample.API.Controllers
{
    /// <summary>
    /// Endpoints in charge of handling authentication request
    /// </summary>
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationContoller : ControllerBase
    {
        private readonly ILogger<ExampleController> _logger;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">Logger instance</param>
        public AuthenticationContoller(ILogger<ExampleController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Get token to access others endpoints
        /// </summary>
        /// <returns>Return object with auth info</returns>
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

        /// <summary>
        /// Check if your token is valid
        /// </summary>
        /// <returns>Returns string if is valid</returns>
        [HttpGet]
        [Route("validateToken")]
        [Authorize]
        public ActionResult<string> IsTokenValide() => "Valid Token!";
    }
}
