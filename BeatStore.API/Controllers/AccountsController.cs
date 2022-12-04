using BeatStore.API.DTO.Requests.Accounts;
using BeatStore.API.UseCases.Accounts;
using BeatStore.API.UseCases.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeatStore.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class AccountsController : ControllerBase
    {
        #region UseCases
        private readonly CreateUserUseCase _createUserUseCase;
        #endregion

        public AccountsController(CreateUserUseCase createUserUseCase)
        {
            _createUserUseCase = createUserUseCase;
        }

        #region POST /accounts/register
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult> Register([FromBody] RegisterUserRequest request)
        {
            if (!ModelState.IsValid)
            { // re-render the view when validation failed.
                return BadRequest(ModelState);
            }
            await _createUserUseCase.Handle(request);
            return _createUserUseCase.OutputPort.GetResult();
        }
        #endregion
    }
}
