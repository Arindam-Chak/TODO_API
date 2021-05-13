using System.Threading.Tasks;
using AutoMapper;
using ToDoAPI.Controllers.Resources;
using ToDoAPI.Core.Security.Tokens;
using ToDoAPI.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace ToDoAPI.Controllers
{
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IMapper mapper, IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
        }

        [Route("/api/login")]
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] UserCredentialsResource userCredentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _authenticationService.CreateAccessTokenAsync(userCredentials.Email, userCredentials.Password);
            if(!response.Success)
            {
                return BadRequest(response.Message);
            }

            var accessTokenResource = _mapper.Map<AccessToken, AccessTokenResource>(response.Token);
            return Ok(accessTokenResource);
        }

        [Route("/api/token/refresh")]
        [HttpPost]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenResource refreshTokenResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _authenticationService.RefreshTokenAsync(refreshTokenResource.Token, refreshTokenResource.UserEmail);
            if(!response.Success)
            {
                return BadRequest(response.Message);
            }
           
            var tokenResource = _mapper.Map<AccessToken, AccessTokenResource>(response.Token);
            return Ok(tokenResource);
        }
        

    }
}