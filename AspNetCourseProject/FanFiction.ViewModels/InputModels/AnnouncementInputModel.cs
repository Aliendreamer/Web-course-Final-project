namespace FanFiction.ViewModels.InputModels
{
    using Utilities;
    using System.ComponentModel.DataAnnotations;

    public class AnnouncementInputModel
    {
        public string Author { get; set; }

        [Required]
        [StringLength(ViewModelsConstants.AnnouncementMaxLength, MinimumLength = ViewModelsConstants.AnnouncementMinLength)]
        public string Content { get; set; }
    }
}