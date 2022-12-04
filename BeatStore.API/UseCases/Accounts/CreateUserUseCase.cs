using BeatStore.API.DTO.Requests.Accounts;
using BeatStore.API.Entities;
using BeatStore.API.Interfaces.Repositories;
using System.Diagnostics;

namespace BeatStore.API.UseCases.Accounts
{
    public class CreateUserUseCase : ABaseUseCase
    {
        private readonly IUserRepository _userRepository;
        public CreateUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task Handle(RegisterUserRequest message)
        {
            var user = new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = message.Email,
                //UserName = message.Email
            };
            var response = await _userRepository.Create(user, message.Password);
            if (response.Success)
            {
                /*Console.WriteLine(response.ConfirmationLink);
                var sentMail = await MailClient.Send(message.Email, "Email confirmation", response.ConfirmationLink);
                if (!sentMail)
                    Trace.WriteLine("RegisterUserUseCase: Something went wrong while sending a mail");*/
            }
            OutputPort = response;
        }
    }
}
