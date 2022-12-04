using BeatStore.API.DTO;
using BeatStore.API.DTO.Requests.Auth;
using BeatStore.API.DTO.Responses;
using BeatStore.API.Interfaces.Services;
using BeatStore.API.Interfaces.Repositories;
using Minio;
using System.Net;

namespace BeatStore.API.UseCases.Auth
{
    public class CreateTokenUseCase : ABaseUseCase
    {

        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtFactory;

        public CreateTokenUseCase(IUserRepository userRepository, IJwtService jwtFactory)
        {
            _userRepository = userRepository;
            _jwtFactory = jwtFactory;
        }

        public async Task Handle(LoginRequest message)
        {
            if (!string.IsNullOrEmpty(message.Email) && !string.IsNullOrEmpty(message.Password))
            {
                // confirm we have a user with the given name
                var user = await _userRepository.GetByUsername(message.Email);
                if (user != null)
                {
                    // validate password
                    if (await _userRepository.CheckPassword(user, message.Password))
                    {
                        // generate token
                        var roles = await _userRepository.GetUserRoles(user);
                        var token = await _jwtFactory.GenerateEncodedToken(user.Id.ToString(), user.UserName, roles);
                        OutputPort = new ValueResponse<Token>(token);
                        return;
                    }
                }
            }
            OutputPort = new StandardResponse("Invalid username or password.", HttpStatusCode.Unauthorized);
        }
    }
}
