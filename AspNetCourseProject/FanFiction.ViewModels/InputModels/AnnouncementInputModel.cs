namespace FanFiction.ViewModels.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class AnnouncementInputModel
    {
        public string Author { get; set; }

        [Required]
        [StringLength(400, MinimumLength = 5)]
        public string Content { get; set; }
    }
}