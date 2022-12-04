using System.ComponentModel.DataAnnotations;

namespace BeatStore.API.DTO.Requests.Accounts
{
    public class RegisterUserRequest
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
