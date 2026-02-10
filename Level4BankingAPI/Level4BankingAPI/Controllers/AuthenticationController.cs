using Level4BankingAPI.Models.DTOs.Responses;
using Level4BankingAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Level4BankingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationService _service;

        public AuthenticationController(AuthenticationService service)
        {
            _service = service;
        }
        
        /// <summary>
        /// Creates a JWT allowing access to the Accounts controller
        /// </summary>
        /// <returns>A JWT allowing access to Accounts controller </returns>
        [HttpGet]
        public ActionResult<TokenResponse> GetAuthenticationToken()
            => Ok(_service.CreateToken());
    }
}
