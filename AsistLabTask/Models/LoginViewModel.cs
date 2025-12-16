using System.ComponentModel.DataAnnotations;

namespace AsistLabTask.Models
{
    public class LoginViewModel
    {
        [Required, EmailAddress(ErrorMessage = "Incorrect email-text")]
        public string Email { get; set; } = default!;

        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = default!;
    }
}
