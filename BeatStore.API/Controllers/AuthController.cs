using BeatStore.API.DTO.Requests.Auth;
using BeatStore.API.DTO.Requests.Orders;
using BeatStore.API.DTO.Responses;
using BeatStore.API.Entities;
using BeatStore.API.UseCases.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeatStore.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region UseCases
        private readonly CreateTokenUseCase _createTokenUseCase;
        #endregion

        public AuthController(CreateTokenUseCase createTokenUseCase)
        {
            _createTokenUseCase = createTokenUseCase;
        }

        #region POST /auth/login
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            { // re-render the view when validation failed.
                return BadRequest(ModelState);
            }
            await _createTokenUseCase.Handle(request);
            return _createTokenUseCase.OutputPort.GetResult();
        }
        #endregion
        
        #region DELETE /auth/logout
        [HttpDelete("logout")]
        [Authorize(Roles = Helpers.Constants.UserRole.Admin)]
        public async Task<ActionResult> Logout()
        {
            return Ok();
        }
        #endregion
    }
}
