namespace FanFiction.ViewModels.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class LoginInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 5)]
        [RegularExpression(@"[A-Za-z]+")]
        public string Nickname { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}