using System.ComponentModel.DataAnnotations;

namespace AsistLabTask.Models
{
    public class RegisterViewModel
    {
        [Required, EmailAddress(ErrorMessage = "Incorrect email-text")]
        public string Email { get; set; } = default!;

        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = default!;

        [Required, DataType(DataType.Password), Compare("Password", ErrorMessage = "Verification of passwords is invalid (not the same)")]
        public string ConfirmPassword { get; set; } = default!;
    }
}
