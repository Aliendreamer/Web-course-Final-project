namespace FanFiction.ViewModels.InputModels
{
	using System;
	using Utilities;
	using System.ComponentModel.DataAnnotations;

	public class ChapterInputModel
	{
		public string Author { get; set; }

		[Required]
		public int StoryId { get; set; }

		[StringLength(ViewModelsConstants.TitleMaxLength, MinimumLength = ViewModelsConstants.TitleMinLength)]
		public string Title { get; set; }

		public DateTime CreatedOn { get; set; }

		[Required]
		[StringLength(ViewModelsConstants.ChapterLength, MinimumLength = ViewModelsConstants.ChapterMinLength, ErrorMessage = ViewModelsConstants.ChapterInputContentError)]
		public string Content { get; set; }
	}
}