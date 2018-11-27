namespace FanFiction.ViewModels.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 5)]
        [RegularExpression(@"[A-Za-z]+", ErrorMessage = "Your nickname should contain only alphabet symbols")]
        public string Nickname { get; set; }

        [Required]
        [RegularExpression(@"[A-Za-z]+", ErrorMessage = "Your username should contain only alphabet symbols")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}